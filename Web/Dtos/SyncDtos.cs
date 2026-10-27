namespace Web.Dtos
{
    // DTOs de sincronización con el celular. Son solo de lectura: el celular guarda una copia
    // de todo esto para poder consultarlo sin conexión.

    public class CatalogDto
    {
        public DateTime ServerTime { get; set; }

        /// <summary>
        /// Identifica la "historia" de ventas de la base: Id y fecha de la primera venta. Si
        /// cambia (por ejemplo, se borraron las ventas y la numeración volvió a 1), el celular
        /// descarta su copia y baja todo de nuevo.
        /// </summary>
        public string? SalesFingerprint { get; set; }

        public List<ProductSyncDto> Products { get; set; } = new();
        public List<PaymentMethodSyncDto> PaymentMethods { get; set; } = new();
        public List<SessionSyncDto> Sessions { get; set; } = new();
    }

    public class ProductSyncDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public int Stock { get; set; }
        public DateTime LastStockUpdate { get; set; }
    }

    public class PaymentMethodSyncDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Recharge { get; set; }
        public decimal Discount { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>Una caja (Session en la base).</summary>
    public class SessionSyncDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public decimal Total { get; set; }
    }

    public class SalesPageDto
    {
        public List<SaleSyncDto> Sales { get; set; } = new();
        public bool HasMore { get; set; }
    }

    public class SaleSyncDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public int SessionId { get; set; }
        public int PaymentMethodId { get; set; }

        /// <summary>null = no fiscal, 0 = pendiente en ARCA, 1 = facturada.</summary>
        public int? FiscalStatus { get; set; }
        public int? InvoiceType { get; set; }
        public long? InvoiceNumber { get; set; }

        public List<SaleItemSyncDto> Items { get; set; } = new();
    }

    public class SaleItemSyncDto
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class FiscalStatusDto
    {
        public int SaleId { get; set; }
        public int Status { get; set; }
        public int InvoiceType { get; set; }
        public long? InvoiceNumber { get; set; }
    }
}
