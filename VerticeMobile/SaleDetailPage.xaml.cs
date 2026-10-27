using VerticeMobile.Data;

namespace VerticeMobile;

public partial class SaleDetailPage : ContentPage
{
    private readonly LocalDb _db;
    private readonly int _saleId;

    public SaleDetailPage(LocalDb db, int saleId)
    {
        InitializeComponent();
        _db = db;
        _saleId = saleId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var sale = await _db.GetSaleAsync(_saleId);
        if (sale == null)
        {
            await DisplayAlertAsync("Aviso", "La venta ya no está en los datos guardados.", "OK");
            await Navigation.PopAsync();
            return;
        }

        var payment = (await _db.GetPaymentMethodsAsync()).FirstOrDefault(m => m.Id == sale.PaymentMethodId);
        var session = (await _db.GetSessionsAsync()).FirstOrDefault(s => s.Id == sale.SessionId);
        var (fiscalText, fiscalColor) = SalesPage.FiscalLabel(sale.FiscalStatus, sale.InvoiceType, sale.InvoiceNumber);

        lblTitle.Text = $"Venta #{sale.Id}";
        lblDate.Text = sale.Date.ToString("dddd dd/MM/yyyy HH:mm");
        lblPayment.Text = $"Medio de pago: {payment?.Name ?? "Desconocido"}";
        lblSession.Text = session == null ? $"Caja #{sale.SessionId}" : $"Caja #{session.Id} · {session.Username}";
        lblFiscal.Text = fiscalText;
        lblFiscal.TextColor = fiscalColor;

        // Recargo o descuento del medio de pago: diferencia entre el total y la suma de los ítems.
        decimal adjustment = sale.Total - sale.ItemsSubTotal;
        lblSubTotal.Text = sale.ItemsSubTotal.ToString("C2");
        lblAdjustmentTitle.Text = adjustment >= 0 ? "Recargo medio de pago" : "Descuento medio de pago";
        lblAdjustment.Text = adjustment.ToString("C2");
        lblTotal.Text = sale.Total.ToString("C2");
        lblProfit.Text = sale.VariosTotal > 0
            ? $"{sale.Profit:C2} (sin Varios)"
            : sale.Profit.ToString("C2");

        var items = await _db.GetSaleItemsAsync(_saleId);
        cvItems.ItemsSource = items.Select(i => new SaleItemRow
        {
            Name = i.ProductName,
            Detail = $"{i.Quantity} x {i.UnitPrice:C2} · costo {i.CostPrice:C2}",
            SubTotal = i.SubTotal,
            ProfitText = i.ProductCode.Trim() == LocalDb.CodigoVarios
                ? "sin costo"
                : $"Gan. {i.SubTotal - i.CostPrice * i.Quantity:C2}"
        }).ToList();
    }
}

/// <summary>Un producto dentro del detalle de una venta.</summary>
public class SaleItemRow
{
    public string Name { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public string ProfitText { get; set; } = string.Empty;
}
