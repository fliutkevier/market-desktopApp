namespace VerticeMobile.Data;

public enum FiscalFilter
{
    All,
    /// <summary>Factura aprobada por ARCA.</summary>
    Invoiced,
    /// <summary>Venta fiscal que todavía espera a ARCA.</summary>
    Pending,
    /// <summary>Venta sin factura.</summary>
    NotInvoiced
}

/// <summary>Filtros de la pantalla de Ventas. Se traducen a SQL sobre la copia local.</summary>
public class SalesFilter
{
    public DateTime From { get; set; } = DateTime.Today;
    public DateTime To { get; set; } = DateTime.Today.AddDays(1).AddTicks(-1);
    public FiscalFilter Fiscal { get; set; } = FiscalFilter.All;
    public int? SessionId { get; set; }
    public int? PaymentMethodId { get; set; }

    /// <summary>Condición WHERE (sin la palabra WHERE) sobre la tabla Sales con el alias dado.</summary>
    public (string Where, object[] Args) ToSql(string alias)
    {
        var conditions = new List<string> { $"{alias}.Date >= ?", $"{alias}.Date <= ?" };
        var args = new List<object> { From, To };

        switch (Fiscal)
        {
            case FiscalFilter.Invoiced: conditions.Add($"{alias}.FiscalStatus = 1"); break;
            case FiscalFilter.Pending: conditions.Add($"{alias}.FiscalStatus = 0"); break;
            case FiscalFilter.NotInvoiced: conditions.Add($"{alias}.FiscalStatus IS NULL"); break;
        }

        if (SessionId is int sessionId)
        {
            conditions.Add($"{alias}.SessionId = ?");
            args.Add(sessionId);
        }

        if (PaymentMethodId is int paymentMethodId)
        {
            conditions.Add($"{alias}.PaymentMethodId = ?");
            args.Add(paymentMethodId);
        }

        return (string.Join(" AND ", conditions), args.ToArray());
    }
}

/// <summary>Un producto en el ranking de vendidos.</summary>
public class ProductSalesRow
{
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Cost { get; set; }

    public bool IsVarios => ProductCode == LocalDb.CodigoVarios;
    public decimal Profit => IsVarios ? 0 : SubTotal - Cost;
}

/// <summary>Totales de un conjunto de ventas.</summary>
public class SalesSummary
{
    public int Count { get; private init; }
    public decimal Total { get; private init; }
    public decimal VariosTotal { get; private init; }
    public decimal Cost { get; private init; }
    public decimal Profit { get; private init; }

    public decimal InvoicedTotal { get; private init; }
    public decimal PendingTotal { get; private init; }
    public decimal NotInvoicedTotal { get; private init; }

    /// <summary>Lo vendido sin Varios: sobre esto se calcula el margen.</summary>
    public decimal ProductsTotal => Total - VariosTotal;
    public decimal AverageTicket => Count > 0 ? Total / Count : 0;
    /// <summary>Margen sobre lo vendido (sin Varios), en %.</summary>
    public decimal MarginPercent => ProductsTotal > 0 ? Profit / ProductsTotal * 100 : 0;

    public static SalesSummary From(IReadOnlyCollection<CachedSale> sales) => new()
    {
        Count = sales.Count,
        Total = sales.Sum(s => s.Total),
        VariosTotal = sales.Sum(s => s.VariosTotal),
        Cost = sales.Sum(s => s.ItemsCost),
        Profit = sales.Sum(s => s.Profit),
        InvoicedTotal = sales.Where(s => s.FiscalStatus == 1).Sum(s => s.Total),
        PendingTotal = sales.Where(s => s.FiscalStatus == 0).Sum(s => s.Total),
        NotInvoicedTotal = sales.Where(s => s.FiscalStatus == null).Sum(s => s.Total)
    };
}
