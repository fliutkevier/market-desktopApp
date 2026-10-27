using SQLite;

namespace VerticeMobile.Data;

// Tablas de la copia local (SQLite en el celular). Es una copia de lo último que se bajó de
// la PC: se consulta sin conexión y se reemplaza o completa en cada sincronización.

[Table("Products")]
public class CachedProduct
{
    [PrimaryKey]
    public string Code { get; set; } = string.Empty;

    /// <summary>Código sin espacios: en la base hay códigos cargados con espacios adelante.</summary>
    [Indexed]
    public string CodeKey { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public decimal SalePrice { get; set; }
    public decimal CostPrice { get; set; }
    public int Stock { get; set; }
    public DateTime LastStockUpdate { get; set; }
}

[Table("PaymentMethods")]
public class CachedPaymentMethod
{
    [PrimaryKey]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Recharge { get; set; }
    public decimal Discount { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Una caja (Session en la base de la PC).</summary>
[Table("Sessions")]
public class CachedSession
{
    [PrimaryKey]
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime OpenedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public decimal Total { get; set; }
}

[Table("Sales")]
public class CachedSale
{
    [PrimaryKey]
    public int Id { get; set; }

    [Indexed]
    public DateTime Date { get; set; }

    public decimal Total { get; set; }

    [Indexed]
    public int SessionId { get; set; }

    [Indexed]
    public int PaymentMethodId { get; set; }

    /// <summary>null = no fiscal, 0 = pendiente en ARCA, 1 = facturada.</summary>
    public int? FiscalStatus { get; set; }
    public int? InvoiceType { get; set; }
    public long? InvoiceNumber { get; set; }

    /// <summary>Σ(costo × cantidad) de los ítems, calculado al guardar.</summary>
    public decimal ItemsCost { get; set; }
    public int ItemCount { get; set; }

    /// <summary>Σ subtotales de los ítems, antes del recargo/descuento del medio de pago.</summary>
    public decimal ItemsSubTotal { get; set; }

    /// <summary>Σ subtotales de "Varios / Fiambrería": precio abierto y sin costo cargado.</summary>
    public decimal VariosSubTotal { get; set; }

    /// <summary>Parte del total que corresponde a Varios, con el recargo/descuento del medio de pago aplicado.</summary>
    [Ignore]
    public decimal VariosTotal => ItemsSubTotal > 0 ? VariosSubTotal * Total / ItemsSubTotal : 0;

    /// <summary>
    /// Ganancia de la venta. Varios no entra: su costo está en 0 y daría 100% de ganancia.
    /// Ganancia = (Total − Varios) − Σ(costo × cantidad).
    /// </summary>
    [Ignore]
    public decimal Profit => Total - VariosTotal - ItemsCost;
}

[Table("SaleItems")]
public class CachedSaleItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int SaleId { get; set; }

    [Indexed]
    public string ProductCode { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SubTotal { get; set; }
}

/// <summary>Datos sueltos de la sincronización (fecha de la última, huella de las ventas, etc.).</summary>
[Table("SyncInfo")]
public class SyncInfo
{
    [PrimaryKey]
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
}
