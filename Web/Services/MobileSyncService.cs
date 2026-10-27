using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Web.Dtos;

namespace Web.Services
{
    /// <summary>
    /// Lecturas para la sincronización del celular. Va directo al DbContext con proyecciones
    /// AsNoTracking: es solo lectura y no tiene lógica de negocio que compartir con escritorio.
    /// </summary>
    public class MobileSyncService
    {
        // Mismo producto comodín que oculta el controller de productos.
        private const string CODIGO_VARIOS = "VARIOS";
        public const int MaxPageSize = 2000;

        private readonly AppDbContext _db;

        public MobileSyncService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CatalogDto> GetCatalogAsync()
        {
            var firstSale = await _db.Sales.AsNoTracking()
                .OrderBy(s => s.Id)
                .Select(s => new { s.Id, s.DateAndTime })
                .FirstOrDefaultAsync();

            return new CatalogDto
            {
                // Sin zona horaria, igual que las fechas de las ventas: el celular las compara entre sí.
                ServerTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                SalesFingerprint = firstSale == null ? null : $"{firstSale.Id}|{firstSale.DateAndTime:O}",

                Products = await _db.Products.AsNoTracking()
                    .Where(p => p.IsActive && p.Code != CODIGO_VARIOS)
                    .Select(p => new ProductSyncDto
                    {
                        Code = p.Code,
                        Name = p.Name,
                        SalePrice = p.SalePrice,
                        CostPrice = p.CostPrice,
                        Stock = p.Stock,
                        LastStockUpdate = p.LastStockUpdate
                    })
                    .ToListAsync(),

                // También los inactivos: las ventas viejas los siguen referenciando.
                PaymentMethods = await _db.PaymentMethods.AsNoTracking()
                    .Select(m => new PaymentMethodSyncDto
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Recharge = m.Recharge,
                        Discount = m.Discount,
                        IsActive = m.IsActive
                    })
                    .ToListAsync(),

                // Son pocas por día: van completas en cada sincronización, así se reflejan las
                // que se cerraron o cambiaron su total desde la última vez.
                Sessions = await _db.Sessions.AsNoTracking()
                    .Select(s => new SessionSyncDto
                    {
                        Id = s.Id,
                        Username = s.Username,
                        OpenedAt = s.OpenedAt,
                        ClosedAt = s.ClosedAt,
                        Total = s.Total
                    })
                    .ToListAsync()
            };
        }

        /// <summary>
        /// Ventas con Id mayor a <paramref name="afterId"/>, en orden. Las ventas no se modifican
        /// después de creadas (salvo el estado fiscal), así que el celular solo pide las nuevas.
        /// </summary>
        public async Task<SalesPageDto> GetSalesAsync(int afterId, int take)
        {
            take = Math.Clamp(take, 1, MaxPageSize);

            // Se pide uno de más para saber si quedan páginas sin otra consulta.
            var sales = await _db.Sales.AsNoTracking()
                .Where(s => s.Id > afterId)
                .OrderBy(s => s.Id)
                .Take(take + 1)
                .Select(s => new SaleSyncDto
                {
                    Id = s.Id,
                    Date = s.DateAndTime,
                    Total = s.Total,
                    SessionId = s.SessionId,
                    PaymentMethodId = s.PaymentMethodId,
                    FiscalStatus = _db.FiscalDocuments.Where(f => f.SaleId == s.Id).Select(f => (int?)f.Status).FirstOrDefault(),
                    InvoiceType = _db.FiscalDocuments.Where(f => f.SaleId == s.Id).Select(f => (int?)f.InvoiceType).FirstOrDefault(),
                    InvoiceNumber = _db.FiscalDocuments.Where(f => f.SaleId == s.Id).Select(f => f.InvoiceNumber).FirstOrDefault(),
                    Items = s.SaleDetails.Select(d => new SaleItemSyncDto
                    {
                        ProductCode = d.ProductCode,
                        ProductName = d.ProductName,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        CostPrice = d.CostPrice,
                        SubTotal = d.SubTotal
                    }).ToList()
                })
                .AsSplitQuery()
                .ToListAsync();

            bool hasMore = sales.Count > take;
            if (hasMore) sales.RemoveAt(sales.Count - 1);

            return new SalesPageDto { Sales = sales, HasMore = hasMore };
        }

        /// <summary>Estado fiscal actual de las ventas pedidas (las que el celular tiene pendientes).</summary>
        public async Task<List<FiscalStatusDto>> GetFiscalStatusAsync(IReadOnlyCollection<int> saleIds)
        {
            if (saleIds.Count == 0) return new List<FiscalStatusDto>();

            return await _db.FiscalDocuments.AsNoTracking()
                .Where(f => saleIds.Contains(f.SaleId))
                .Select(f => new FiscalStatusDto
                {
                    SaleId = f.SaleId,
                    Status = (int)f.Status,
                    InvoiceType = f.InvoiceType,
                    InvoiceNumber = f.InvoiceNumber
                })
                .ToListAsync();
        }
    }
}
