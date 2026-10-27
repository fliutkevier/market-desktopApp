using VerticeMobile.Data;

namespace VerticeMobile;

/// <summary>Productos vendidos en las ventas que cumplen los filtros de la pantalla de Ventas.</summary>
public partial class ProductRankingPage : ContentPage
{
    private readonly LocalDb _db;
    private readonly SalesFilter _filter;
    private List<ProductSalesRow> _rows = new();

    private static readonly string[] Orders = { "Más facturado", "Más unidades", "Más ganancia" };

    public ProductRankingPage(LocalDb db, SalesFilter filter, string description)
    {
        InitializeComponent();
        _db = db;
        _filter = filter;
        lblDescription.Text = $"Período: {description}. Importes sin recargos ni descuentos del medio de pago.";

        foreach (var o in Orders) pkOrder.Items.Add(o);
        pkOrder.SelectedIndex = 0;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _rows = await _db.GetProductRankingAsync(_filter);
        Apply();
    }

    private void OnOrderChanged(object? sender, EventArgs e) => Apply();

    private void OnSearchTextChanged(object? sender, TextChangedEventArgs e) => Apply();

    private void Apply()
    {
        IEnumerable<ProductSalesRow> ordered = pkOrder.SelectedIndex switch
        {
            1 => _rows.OrderByDescending(r => r.Quantity),
            2 => _rows.OrderByDescending(r => r.Profit),
            _ => _rows.OrderByDescending(r => r.SubTotal)
        };

        // La posición es la del ranking completo, aunque después se filtre por texto.
        var ranked = ordered.Select((r, i) => new ProductRankingRow
        {
            Position = $"{i + 1}",
            Name = r.ProductName,
            Detail = $"{r.Quantity} unid. · {r.ProductCode}",
            SubTotal = r.SubTotal,
            ProfitText = r.IsVarios ? "sin costo" : $"Gan. {r.Profit:C0}"
        });

        string keyword = searchBar.Text?.Trim().ToLowerInvariant() ?? "";
        if (keyword.Length > 0)
            ranked = ranked.Where(r => r.Name.ToLowerInvariant().Contains(keyword) || r.Detail.Contains(keyword));

        cvProducts.ItemsSource = ranked.ToList();
    }
}

public class ProductRankingRow
{
    public string Position { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public string ProfitText { get; set; } = string.Empty;
}
