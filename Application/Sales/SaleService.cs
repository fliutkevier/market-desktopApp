using Application.AuditLogs.Interfaces;
using Application.Sales.Dtos;
using Application.Sales.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserSessionService _userSessionService;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFiscalService _fiscalService;
        private readonly IRepository<FiscalDocument> _fiscalDocRepository; // Repositorio para guardar el CAE
        private readonly IAuditLogService _auditLogService;

        public SaleService(
            ISaleRepository saleRepository,
            ISaleDetailRepository saleDetailRepository,
            IProductRepository productRepository,
            ISessionRepository sessionRepository,
            IUserSessionService userSessionService,
            IPaymentMethodRepository paymentMethodRepository,
            IUnitOfWork unitOfWork,
            IFiscalService fiscalService,
            IRepository<FiscalDocument> fiscalDocRepository,
            IAuditLogService auditLogService)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _productRepository = productRepository;
            _sessionRepository = sessionRepository;
            _userSessionService = userSessionService;
            _paymentMethodRepository = paymentMethodRepository;
            _unitOfWork = unitOfWork;
            _fiscalService = fiscalService;
            _fiscalDocRepository = fiscalDocRepository;
            _auditLogService = auditLogService;
        }

        public async Task<SaleResultDto> CreateSaleAsync(CreateSaleDto dto)
        {
            DtoValidator.Validate(dto);

            string currentUsername = _userSessionService.Username ?? throw new InvalidOperationException("No hay usuario.");
            var currentSession = await _sessionRepository.GetOpenSessionByUserAsync(currentUsername);
            if (currentSession == null) throw new InvalidOperationException("Caja cerrada.");

            //Método de Pago para calcular recargos/descuentos
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(dto.PaymentMethodId);
            if (paymentMethod == null) throw new InvalidOperationException("Método de pago no válido.");

            var newSale = new Sale
            {
                DateAndTime = DateTime.Now,
                SessionId = currentSession.Id,
                PaymentMethodId = dto.PaymentMethodId,
                SaleDetails = new List<SaleDetail>(),
                Total = 0
            };

            decimal subTotalProductos = 0;

            foreach (var itemDto in dto.Items)
            {
                var product = await _productRepository.GetByCodeAsync(itemDto.ProductCode);

                if (product == null) throw new InvalidOperationException($"El producto '{itemDto.ProductCode}' no existe.");
                if (!product.IsActive) throw new InvalidOperationException($"El producto '{product.Name}' está inactivo.");

                decimal finalPrice = product.SalePrice;

                // Lógica de Precio Abierto (VARIOS)
                if (product.IsOpenPrice && itemDto.UnitPrice.HasValue)
                {
                    finalPrice = itemDto.UnitPrice.Value;
                }

                // Descuento si NO es precio abierto
                if (!product.IsOpenPrice)
                {
                    int nuevoStock = product.Stock - itemDto.Quantity;

                    // Si el nuevo stock es negativo, queda 0 (pero vende igual)
                    if (nuevoStock < 0) nuevoStock = 0;

                    product.Stock = nuevoStock;
                    product.LastStockUpdate = DateTime.Now;
                    _productRepository.Update(product);
                }

                var detail = new SaleDetail
                {
                    ProductCode = product.Code,
                    ProductName = product.Name,
                    Quantity = itemDto.Quantity,
                    UnitPrice = finalPrice,
                    SubTotal = finalPrice * itemDto.Quantity
                };

                subTotalProductos += detail.SubTotal;
                newSale.SaleDetails.Add(detail);
            }

            //Total = Subtotal * (1 + (Recargo% - Descuento%) / 100)
            decimal factor = 1 + ((paymentMethod.Recharge - paymentMethod.Discount) / 100m);
            newSale.Total = subTotalProductos * factor;

            //Actualizar sesión
            currentSession.Total += newSale.Total;
            _sessionRepository.Update(currentSession);

            await _saleRepository.AddAsync(newSale);
            await _unitOfWork.SaveChangesAsync();
            await _auditLogService.LogAsync("VENTA Creada", $"Número: {newSale.Id}");
            await _unitOfWork.SaveChangesAsync();

            FiscalDocument? fiscalDoc = null;

            //LÓGICA FISCAL: nunca bloquea la venta. Se intenta facturar al momento con un timeout
            //corto; si ARCA está caído o tarda, la venta queda igual y la factura pasa a la cola
            //(FiscalDocumentStatus.Pending) para reintentarse después, sola o a demanda.
            if (dto.IsFiscal)
            {
                fiscalDoc = new FiscalDocument
                {
                    SaleId = newSale.Id,
                    Status = FiscalDocumentStatus.Pending
                };
                await _fiscalDocRepository.AddAsync(fiscalDoc);
                await _unitOfWork.SaveChangesAsync();

                try
                {
                    using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(8));
                    var resultado = await _fiscalService.GenerateInvoiceAsync(newSale, timeoutCts.Token);

                    fiscalDoc.Status = FiscalDocumentStatus.Approved;
                    fiscalDoc.IssuerCuit = resultado.IssuerCuit;
                    fiscalDoc.PointOfSale = resultado.PointOfSale;
                    fiscalDoc.InvoiceType = resultado.InvoiceType;
                    fiscalDoc.InvoiceNumber = resultado.InvoiceNumber;
                    fiscalDoc.CAE = resultado.CAE;
                    fiscalDoc.CAEExpirationDate = resultado.CAEExpirationDate;
                    fiscalDoc.CustomerDocType = resultado.CustomerDocType;
                    fiscalDoc.CustomerDocNumber = resultado.CustomerDocNumber;
                    fiscalDoc.AttemptCount++;
                    fiscalDoc.LastAttemptAt = DateTime.Now;
                    fiscalDoc.LastError = null;

                    _fiscalDocRepository.Update(fiscalDoc);
                    await _auditLogService.LogAsync("FISCAL VENTA Creada", $"Número: {newSale.Id}");
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // ARCA caído, timeout, o rechazo: la venta ya está guardada, la factura
                    // queda Pending en la cola para reintentarse después.
                    fiscalDoc.AttemptCount++;
                    fiscalDoc.LastAttemptAt = DateTime.Now;
                    fiscalDoc.LastError = ex.Message;

                    _fiscalDocRepository.Update(fiscalDoc);
                    await _auditLogService.LogAsync("FISCAL VENTA Pendiente", $"Número: {newSale.Id} - {ex.Message}");
                    await _unitOfWork.SaveChangesAsync();

                    fiscalDoc = null; // Indica a la UI que todavía no hay comprobante fiscal para imprimir
                }
            }

            return new SaleResultDto
            {
                SaleId = newSale.Id,
                FiscalData = fiscalDoc
            };
        }

        public async Task<FiscalDocument?> GetFiscalDocumentBySaleIdAsync(int saleId)
        {
            var docs = await _fiscalDocRepository.GetAsync(x => x.SaleId == saleId);
            return docs.FirstOrDefault();
        }

        public async Task<IEnumerable<SaleGridDto>> GetHistoryAsync(DateOnly? fromDate = null, DateOnly? toDate = null)
        {
            var sales = await _saleRepository.GetSalesWithDetailsAsync(fromDate, toDate);

            var saleIds = sales.Select(s => s.Id).ToList();
            var fiscalDocs = await _fiscalDocRepository.GetAsync(x => saleIds.Contains(x.SaleId));
            var fiscalBySale = fiscalDocs.ToDictionary(x => x.SaleId, x => x.Status);

            return sales.Select(s => new SaleGridDto
            {
                Id = s.Id,
                Date = s.DateAndTime,
                Total = s.Total,
                PaymentMethod = s.PaymentMethod?.Name ?? "Desconocido",
                ItemCount = s.SaleDetails.Count,
                User = s.Session?.Username ?? "Desconocido",

                FiscalStatus = fiscalBySale.TryGetValue(s.Id, out var status) ? status : null
            });
        }

        public async Task<IEnumerable<SaleDetailViewDto>> GetSaleDetailsAsync(int saleId)
        {
            var details = await _saleDetailRepository.GetAsync(d => d.SaleId == saleId);

            return details.Select(d => new SaleDetailViewDto
            {
                ProductCode = d.ProductCode,
                ProductName = d.ProductName,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                SubTotal = d.SubTotal
            });
        }

        public async Task<IEnumerable<SaleGridDto>> GetSalesBySessionAsync(int sessionId)
        {
            //ventas de la sesión
            var sales = await _saleRepository.GetSalesBySessionIdAsync(sessionId);

            //vacío
            if (!sales.Any()) return new List<SaleGridDto>();

            //IDs de ventas para consultar si tienen factura
            var saleIds = sales.Select(s => s.Id).ToList();

            //cuáles de esos IDs existen y en qué estado están
            var fiscalDocs = await _fiscalDocRepository.GetAsync(x => saleIds.Contains(x.SaleId));

            //búsqueda rápida
            var fiscalBySale = fiscalDocs.ToDictionary(x => x.SaleId, x => x.Status);

            return sales.Select(s => new SaleGridDto
            {
                Id = s.Id,
                Date = s.DateAndTime,
                Total = s.Total,
                PaymentMethod = s.PaymentMethod?.Name ?? "Desconocido",
                ItemCount = s.SaleDetails.Sum(d => d.Quantity),
                User = s.Session?.Username ?? "Desconocido",

                FiscalStatus = fiscalBySale.TryGetValue(s.Id, out var status) ? status : null
            });
        }
        /*
        private IEnumerable<SaleGridDto> MapToGridDto(IEnumerable<Sale> sales)
        {
            return sales.Select(s => new SaleGridDto
            {
                Id = s.Id,
                Date = s.DateAndTime,
                Total = s.Total,
                PaymentMethod = s.PaymentMethod?.Name ?? "Desconocido",

                ItemCount = s.SaleDetails.Sum(d => d.Quantity),

                User = s.Session?.Username ?? "Desconocido"
            });
        }*/

        public async Task<SaleGridDto?> GetSaleHeaderAsync(int saleId)
        {
            var sale = await _saleRepository.GetSaleWithRelationsByIdAsync(saleId);

            if (sale == null) return null;

            return new SaleGridDto
            {
                Id = sale.Id,
                Date = sale.DateAndTime,
                Total = sale.Total,
                PaymentMethod = sale.PaymentMethod?.Name ?? "Desconocido",
                ItemCount = sale.SaleDetails?.Sum(d => d.Quantity) ?? 0,
                User = sale.Session?.Username ?? "Desconocido"
            };
        }
    }
}
