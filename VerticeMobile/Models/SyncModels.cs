namespace VerticeMobile.Models;

// Lo que devuelve el servidor de la PC (Web\Dtos\SyncDtos.cs). Mismos nombres de propiedades.

public class CatalogResponse
{
    public DateTime ServerTime { get; set; }
    public string? SalesFingerprint { get; set; }
    public List<ProductResponse> Products { get; set; } = new();
    public List<PaymentMethodResponse> PaymentMethods { get; set; } = new();
    public List<SessionResponse> Sessions { get; set; } = new();
}

public class ProductResponse
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal SalePrice { get; set; }
    public decimal CostPrice { get; set; }
    public int Stock { get; set; }
    public DateTime LastStockUpdate { get; set; }
}

public class PaymentMethodResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Recharge { get; set; }
    public decimal Discount { get; set; }
    public bool IsActive { get; set; }
}

public class SessionResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime OpenedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public decimal Total { get; set; }
}

public class SalesPageResponse
{
    public List<SaleResponse> Sales { get; set; } = new();
    public bool HasMore { get; set; }
}

public class SaleResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public int SessionId { get; set; }
    public int PaymentMethodId { get; set; }
    public int? FiscalStatus { get; set; }
    public int? InvoiceType { get; set; }
    public long? InvoiceNumber { get; set; }
    public List<SaleItemResponse> Items { get; set; } = new();
}

public class SaleItemResponse
{
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SubTotal { get; set; }
}

public class FiscalStatusResponse
{
    public int SaleId { get; set; }
    public int Status { get; set; }
    public int InvoiceType { get; set; }
    public long? InvoiceNumber { get; set; }
}
