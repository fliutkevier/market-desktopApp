using SQLite;
using VerticeMobile.Models;

namespace VerticeMobile.Data;

/// <summary>
/// Copia local de los datos de la PC (SQLite). Todo lo que se muestra sale de acá, así funciona
/// igual con o sin conexión; SyncService es el único que la actualiza desde el servidor.
/// </summary>
public class LocalDb
{
    public const string KeyLastSync = "LastSync";
    public const string KeyLastServerTime = "LastServerTime";
    public const string KeySalesFingerprint = "SalesFingerprint";
    private const string KeyCacheVersion = "CacheVersion";

    /// <summary>
    /// Subirlo cuando se agregan datos calculados a las ventas guardadas: las que ya estaban no
    /// los tienen, así que se borran y la próxima sincronización las baja de nuevo.
    /// </summary>
    private const int CacheVersion = 2;

    /// <summary>Producto comodín de precio abierto (fiambrería, etc.): no tiene costo cargado.</summary>
    public const string CodigoVarios = "VARIOS";

    private readonly SQLiteAsyncConnection _db;
    private readonly SemaphoreSlim _initLock = new(1, 1);
    private bool _initialized;

    public LocalDb() : this(Path.Combine(FileSystem.AppDataDirectory, "vertice-cache.db3"))
    {
    }

    public LocalDb(string path)
    {
        _db = new SQLiteAsyncConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
    }

    private async Task<SQLiteAsyncConnection> GetDbAsync()
    {
        if (_initialized) return _db;

        await _initLock.WaitAsync();
        try
        {
            if (!_initialized)
            {
                await _db.CreateTablesAsync(CreateFlags.None,
                    typeof(CachedProduct), typeof(CachedPaymentMethod), typeof(CachedSession),
                    typeof(CachedSale), typeof(CachedSaleItem), typeof(SyncInfo));

                var version = await _db.FindAsync<SyncInfo>(KeyCacheVersion);
                if (version?.Value != CacheVersion.ToString())
                {
                    await _db.RunInTransactionAsync(conn =>
                    {
                        conn.DeleteAll<CachedSaleItem>();
                        conn.DeleteAll<CachedSale>();
                        conn.Delete<SyncInfo>(KeySalesFingerprint);
                        conn.InsertOrReplace(new SyncInfo { Key = KeyCacheVersion, Value = CacheVersion.ToString() });
                    });
                }

                _initialized = true;
            }
        }
        finally
        {
            _initLock.Release();
        }
        return _db;
    }

    #region SyncInfo

    public async Task<string?> GetInfoAsync(string key)
    {
        var db = await GetDbAsync();
        return (await db.FindAsync<SyncInfo>(key))?.Value;
    }

    public async Task SetInfoAsync(string key, string? value)
    {
        var db = await GetDbAsync();
        await db.InsertOrReplaceAsync(new SyncInfo { Key = key, Value = value });
    }

    public async Task<DateTime?> GetLastSyncAsync()
    {
        string? value = await GetInfoAsync(KeyLastSync);
        return DateTime.TryParse(value, null, System.Globalization.DateTimeStyles.RoundtripKind, out var date) ? date : null;
    }

    #endregion

    #region Catálogo

    /// <summary>Reemplaza productos, medios de pago y cajas por lo que vino del servidor.</summary>
    public async Task ReplaceCatalogAsync(CatalogResponse catalog)
    {
        var db = await GetDbAsync();
        await db.RunInTransactionAsync(conn =>
        {
            conn.DeleteAll<CachedProduct>();
            conn.InsertAll(catalog.Products.Select(ToCached), runInTransaction: false);

            conn.DeleteAll<CachedPaymentMethod>();
            conn.InsertAll(catalog.PaymentMethods.Select(m => new CachedPaymentMethod
            {
                Id = m.Id,
                Name = m.Name,
                Recharge = m.Recharge,
                Discount = m.Discount,
                IsActive = m.IsActive
            }), runInTransaction: false);

            conn.DeleteAll<CachedSession>();
            conn.InsertAll(catalog.Sessions.Select(s => new CachedSession
            {
                Id = s.Id,
                Username = s.Username,
                OpenedAt = s.OpenedAt,
                ClosedAt = s.ClosedAt,
                Total = s.Total
            }), runInTransaction: false);
        });
    }

    public async Task<List<CachedProduct>> GetProductsAsync()
    {
        var db = await GetDbAsync();
        return await db.Table<CachedProduct>().OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<CachedProduct?> GetProductAsync(string code)
    {
        var db = await GetDbAsync();
        string key = code.Trim();
        return await db.Table<CachedProduct>().FirstOrDefaultAsync(p => p.CodeKey == key);
    }

    /// <summary>Después de crear o editar un producto desde el celular, para no esperar a la próxima sincronización.</summary>
    public async Task SaveProductAsync(ProductResponse product)
    {
        var db = await GetDbAsync();
        await db.InsertOrReplaceAsync(ToCached(product));
    }

    private static CachedProduct ToCached(ProductResponse p) => new()
    {
        Code = p.Code,
        CodeKey = p.Code.Trim(),
        Name = p.Name,
        SalePrice = p.SalePrice,
        CostPrice = p.CostPrice,
        Stock = p.Stock,
        LastStockUpdate = p.LastStockUpdate
    };

    public async Task<List<CachedPaymentMethod>> GetPaymentMethodsAsync()
    {
        var db = await GetDbAsync();
        return await db.Table<CachedPaymentMethod>().OrderBy(m => m.Name).ToListAsync();
    }

    public async Task<List<CachedSession>> GetSessionsAsync()
    {
        var db = await GetDbAsync();
        return await db.Table<CachedSession>().OrderByDescending(s => s.Id).ToListAsync();
    }

    #endregion

    #region Ventas

    public async Task<int> GetMaxSaleIdAsync()
    {
        var db = await GetDbAsync();
        return await db.ExecuteScalarAsync<int>("SELECT IFNULL(MAX(Id), 0) FROM Sales");
    }

    public async Task<int> CountSalesAsync()
    {
        var db = await GetDbAsync();
        return await db.Table<CachedSale>().CountAsync();
    }

    /// <summary>Ventas que cumplen el filtro, de la más nueva a la más vieja.</summary>
    public async Task<List<CachedSale>> GetSalesAsync(SalesFilter filter)
    {
        var db = await GetDbAsync();
        var (where, args) = filter.ToSql("s");
        return await db.QueryAsync<CachedSale>($"SELECT s.* FROM Sales s WHERE {where} ORDER BY s.Date DESC", args);
    }

    public async Task<CachedSale?> GetSaleAsync(int saleId)
    {
        var db = await GetDbAsync();
        return await db.FindAsync<CachedSale>(saleId);
    }

    public async Task<List<CachedSaleItem>> GetSaleItemsAsync(int saleId)
    {
        var db = await GetDbAsync();
        return await db.Table<CachedSaleItem>().Where(i => i.SaleId == saleId).ToListAsync();
    }

    /// <summary>
    /// Productos vendidos en las ventas del filtro, agrupados, del que más facturó al que menos.
    /// Importes antes del recargo/descuento del medio de pago (así figuran en cada ítem).
    /// </summary>
    public async Task<List<ProductSalesRow>> GetProductRankingAsync(SalesFilter filter)
    {
        var db = await GetDbAsync();
        var (where, args) = filter.ToSql("s");
        return await db.QueryAsync<ProductSalesRow>(
            "SELECT TRIM(i.ProductCode) AS ProductCode, MAX(i.ProductName) AS ProductName, " +
            "SUM(i.Quantity) AS Quantity, SUM(i.SubTotal) AS SubTotal, SUM(i.CostPrice * i.Quantity) AS Cost " +
            $"FROM SaleItems i JOIN Sales s ON s.Id = i.SaleId WHERE {where} " +
            "GROUP BY TRIM(i.ProductCode) ORDER BY SUM(i.SubTotal) DESC", args);
    }

    public async Task InsertSalesAsync(IEnumerable<SaleResponse> sales)
    {
        var db = await GetDbAsync();
        await db.RunInTransactionAsync(conn =>
        {
            foreach (var s in sales)
            {
                conn.InsertOrReplace(new CachedSale
                {
                    Id = s.Id,
                    Date = s.Date,
                    Total = s.Total,
                    SessionId = s.SessionId,
                    PaymentMethodId = s.PaymentMethodId,
                    FiscalStatus = s.FiscalStatus,
                    InvoiceType = s.InvoiceType,
                    InvoiceNumber = s.InvoiceNumber,
                    ItemsCost = s.Items.Sum(i => i.CostPrice * i.Quantity),
                    ItemCount = s.Items.Count,
                    ItemsSubTotal = s.Items.Sum(i => i.SubTotal),
                    VariosSubTotal = s.Items.Where(i => i.ProductCode.Trim() == CodigoVarios).Sum(i => i.SubTotal)
                });

                // Por si la venta ya estaba (una página que se cortó a la mitad): no duplicar ítems.
                conn.Execute("DELETE FROM SaleItems WHERE SaleId = ?", s.Id);
                conn.InsertAll(s.Items.Select(i => new CachedSaleItem
                {
                    SaleId = s.Id,
                    ProductCode = i.ProductCode,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    CostPrice = i.CostPrice,
                    SubTotal = i.SubTotal
                }), runInTransaction: false);
            }
        });
    }

    /// <summary>
    /// Ventas cuyo estado fiscal todavía puede cambiar: las pendientes en ARCA y las que
    /// llegaron sin factura hace muy poco (la factura se crea unos segundos después de la venta).
    /// </summary>
    public async Task<List<int>> GetSaleIdsToRecheckFiscalAsync(DateTime recentSince)
    {
        var db = await GetDbAsync();
        var sales = await db.QueryAsync<CachedSale>(
            "SELECT Id FROM Sales WHERE FiscalStatus = 0 OR (FiscalStatus IS NULL AND Date >= ?)",
            recentSince);
        return sales.Select(s => s.Id).ToList();
    }

    public async Task UpdateFiscalStatusAsync(IEnumerable<FiscalStatusResponse> statuses)
    {
        var db = await GetDbAsync();
        await db.RunInTransactionAsync(conn =>
        {
            foreach (var f in statuses)
            {
                conn.Execute("UPDATE Sales SET FiscalStatus = ?, InvoiceType = ?, InvoiceNumber = ? WHERE Id = ?",
                    f.Status, f.InvoiceType, f.InvoiceNumber, f.SaleId);
            }
        });
    }

    public async Task ClearSalesAsync()
    {
        var db = await GetDbAsync();
        await db.RunInTransactionAsync(conn =>
        {
            conn.DeleteAll<CachedSaleItem>();
            conn.DeleteAll<CachedSale>();
        });
    }

    #endregion

    /// <summary>Borra toda la copia local (botón de Ajustes). La próxima sincronización baja todo de nuevo.</summary>
    public async Task ClearAllAsync()
    {
        var db = await GetDbAsync();
        await db.RunInTransactionAsync(conn =>
        {
            conn.DeleteAll<CachedSaleItem>();
            conn.DeleteAll<CachedSale>();
            conn.DeleteAll<CachedProduct>();
            conn.DeleteAll<CachedPaymentMethod>();
            conn.DeleteAll<CachedSession>();
            conn.DeleteAll<SyncInfo>();
            conn.InsertOrReplace(new SyncInfo { Key = KeyCacheVersion, Value = CacheVersion.ToString() });
        });
    }
}
