using VerticeMobile.Data;
using VerticeMobile.Services;

namespace VerticeMobile;

public partial class SalesPage : ContentPage
{
    private readonly LocalDb _db;
    private readonly SyncService _sync;
    private readonly ConnectionState _state;

    private static readonly string[] Periods = { "Hoy", "Ayer", "Últimos 7 días", "Este mes", "Mes anterior", "Todo", "Personalizado" };
    private const int PeriodCustom = 6;

    private static readonly (string Label, FiscalFilter Value)[] FiscalOptions =
    {
        ("Todas", FiscalFilter.All),
        ("Facturadas", FiscalFilter.Invoiced),
        ("Pendientes ARCA", FiscalFilter.Pending),
        ("Sin facturar", FiscalFilter.NotInvoiced)
    };

    // Listas paralelas a los Items de cada Picker (índice 0 = "todos").
    private List<CachedPaymentMethod> _paymentMethods = new();
    private List<CachedSession> _sessionsInPeriod = new();
    private Dictionary<int, CachedPaymentMethod> _paymentById = new();
    private Dictionary<int, CachedSession> _sessionById = new();

    private SalesFilter _filter = new();

    // Mientras se rellenan los Pickers por código no se recarga en cada SelectedIndexChanged.
    private bool _updatingControls;
    private bool _initialized;

    public SalesPage(LocalDb db, SyncService sync, ConnectionState state)
    {
        InitializeComponent();
        _db = db;
        _sync = sync;
        _state = state;

        _updatingControls = true;
        foreach (var p in Periods) pkPeriod.Items.Add(p);
        foreach (var f in FiscalOptions) pkFiscal.Items.Add(f.Label);
        pkPeriod.SelectedIndex = 0;
        pkFiscal.SelectedIndex = 0;
        dpFrom.Date = DateTime.Today;
        dpTo.Date = DateTime.Today;
        _updatingControls = false;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _state.Changed += OnStateChanged;

        await LoadLookupsAsync();
        _initialized = true;
        await ReloadAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _state.Changed -= OnStateChanged;
    }

    // Cuando termina una sincronización se recalcula con lo nuevo.
    private async void OnStateChanged(object? sender, EventArgs e)
    {
        if (_state.IsSyncing || !_initialized) return;
        await LoadLookupsAsync();
        await ReloadAsync();
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await _sync.SyncAsync();
        await LoadLookupsAsync();
        await ReloadAsync();
        refreshView.IsRefreshing = false;
    }

    private async void OnFilterChanged(object? sender, EventArgs e)
    {
        if (_updatingControls || !_initialized) return;
        gridCustomDates.IsVisible = pkPeriod.SelectedIndex == PeriodCustom;
        await ReloadAsync();
    }

    private async void OnDateChanged(object? sender, DateChangedEventArgs e)
    {
        if (_updatingControls || !_initialized || pkPeriod.SelectedIndex != PeriodCustom) return;
        await ReloadAsync();
    }

    /// <summary>Medios de pago y cajas para los Pickers y para mostrar nombres en el listado.</summary>
    private async Task LoadLookupsAsync()
    {
        _paymentMethods = await _db.GetPaymentMethodsAsync();
        _paymentById = _paymentMethods.ToDictionary(m => m.Id);
        _sessionById = (await _db.GetSessionsAsync()).ToDictionary(s => s.Id);

        int? selectedPayment = _filter.PaymentMethodId;

        _updatingControls = true;
        pkPayment.Items.Clear();
        pkPayment.Items.Add("Todos los medios de pago");
        foreach (var m in _paymentMethods) pkPayment.Items.Add(m.IsActive ? m.Name : $"{m.Name} (inactivo)");
        pkPayment.SelectedIndex = selectedPayment is int id ? _paymentMethods.FindIndex(m => m.Id == id) + 1 : 0;
        _updatingControls = false;
    }

    private (DateTime From, DateTime To) GetPeriod()
    {
        var today = DateTime.Today;
        DateTime EndOf(DateTime day) => day.Date.AddDays(1).AddTicks(-1);

        // El cast cubre DatePicker.Date nullable (MAUI 10) y no nullable.
        DateTime fromDay = ((DateTime?)dpFrom.Date ?? today).Date;
        DateTime toDay = ((DateTime?)dpTo.Date ?? today).Date;

        return pkPeriod.SelectedIndex switch
        {
            1 => (today.AddDays(-1), EndOf(today.AddDays(-1))),
            2 => (today.AddDays(-6), EndOf(today)),
            3 => (new DateTime(today.Year, today.Month, 1), EndOf(today)),
            4 => (new DateTime(today.Year, today.Month, 1).AddMonths(-1), new DateTime(today.Year, today.Month, 1).AddTicks(-1)),
            5 => (DateTime.MinValue, DateTime.MaxValue),
            PeriodCustom => fromDay <= toDay
                ? (fromDay, EndOf(toDay))
                : (toDay, EndOf(fromDay)),
            _ => (today, EndOf(today))
        };
    }

    /// <summary>Las cajas que se ofrecen en el filtro son las que estuvieron abiertas en el período.</summary>
    private void RefreshSessionPicker(DateTime from, DateTime to)
    {
        int? selected = _filter.SessionId;

        _sessionsInPeriod = _sessionById.Values
            .Where(s => s.OpenedAt <= to && (s.ClosedAt ?? DateTime.MaxValue) >= from)
            .OrderByDescending(s => s.Id)
            .ToList();

        _updatingControls = true;
        pkSession.Items.Clear();
        pkSession.Items.Add("Todas las cajas");
        foreach (var s in _sessionsInPeriod) pkSession.Items.Add(SessionLabel(s));

        int index = selected is int id ? _sessionsInPeriod.FindIndex(s => s.Id == id) + 1 : 0;
        pkSession.SelectedIndex = index; // si la caja elegida no está en el período, vuelve a "Todas"
        _updatingControls = false;
    }

    private static string SessionLabel(CachedSession s) =>
        $"Caja #{s.Id} · {s.Username} · {s.OpenedAt:dd/MM HH:mm}" + (s.ClosedAt == null ? " (abierta)" : "");

    private async Task ReloadAsync()
    {
        var (from, to) = GetPeriod();
        RefreshSessionPicker(from, to);

        _filter = new SalesFilter
        {
            From = from,
            To = to,
            Fiscal = FiscalOptions[Math.Max(pkFiscal.SelectedIndex, 0)].Value,
            PaymentMethodId = pkPayment.SelectedIndex > 0 ? _paymentMethods[pkPayment.SelectedIndex - 1].Id : null,
            SessionId = pkSession.SelectedIndex > 0 ? _sessionsInPeriod[pkSession.SelectedIndex - 1].Id : null
        };

        var sales = await _db.GetSalesAsync(_filter);
        ShowSummary(SalesSummary.From(sales), sales);

        lblListTitle.Text = sales.Count == 1 ? "1 venta" : $"{sales.Count} ventas";
        cvSales.ItemsSource = sales.Select(ToRow).ToList();
    }

    private void ShowSummary(SalesSummary summary, List<CachedSale> sales)
    {
        lblTotal.Text = summary.Total.ToString("C2");
        lblCount.Text = summary.Count == 0
            ? "Sin ventas"
            : $"{summary.Count} ventas · ticket promedio {summary.AverageTicket:C2}";

        lblProfit.Text = $"{summary.Profit:C2} ({summary.MarginPercent:0.#}%)";
        lblCost.Text = summary.Cost.ToString("C2");
        lblVarios.Text = summary.VariosTotal.ToString("C2");

        lblInvoiced.Text = summary.InvoicedTotal.ToString("C2");
        lblPending.Text = summary.PendingTotal.ToString("C2");
        lblNotInvoiced.Text = summary.NotInvoicedTotal.ToString("C2");

        BindableLayout.SetItemsSource(stackByPayment, sales
            .GroupBy(s => s.PaymentMethodId)
            .Select(g => new PaymentTotalRow
            {
                Label = $"{PaymentName(g.Key)} ({g.Count()})",
                Total = g.Sum(s => s.Total)
            })
            .OrderByDescending(r => r.Total)
            .ToList());
    }

    private string PaymentName(int id) => _paymentById.TryGetValue(id, out var m) ? m.Name : "Desconocido";

    private SaleRow ToRow(CachedSale s)
    {
        string user = _sessionById.TryGetValue(s.SessionId, out var session) ? session.Username : "?";
        var (fiscalText, fiscalColor) = FiscalLabel(s.FiscalStatus, s.InvoiceType, s.InvoiceNumber);

        return new SaleRow
        {
            SaleId = s.Id,
            Title = $"#{s.Id} · {s.Date:dd/MM/yy HH:mm}",
            Subtitle = $"{PaymentName(s.PaymentMethodId)} · Caja #{s.SessionId} {user}",
            FiscalText = fiscalText,
            FiscalColor = fiscalColor,
            Total = s.Total,
            Profit = s.Profit,
            ItemsText = s.ItemCount == 1 ? "1 ítem" : $"{s.ItemCount} ítems"
        };
    }

    public static (string Text, Color Color) FiscalLabel(int? status, int? invoiceType, long? invoiceNumber) => status switch
    {
        1 => ($"Facturada {InvoiceLetter(invoiceType)} N° {invoiceNumber}", Color.FromArgb("#2E7D32")),
        0 => ("Pendiente ARCA", Color.FromArgb("#E65100")),
        _ => ("Sin facturar", Colors.Gray)
    };

    private static string InvoiceLetter(int? type) => type switch
    {
        1 => "A",
        6 => "B",
        11 => "C",
        _ => ""
    };

    private async void OnSaleSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not SaleRow row) return;
        cvSales.SelectedItem = null;
        await Navigation.PushAsync(new SaleDetailPage(_db, row.SaleId));
    }

    private async void OnProductRankingClicked(object sender, EventArgs e)
    {
        string description = $"{pkPeriod.SelectedItem}" + (pkPeriod.SelectedIndex == PeriodCustom
            ? $" ({_filter.From:dd/MM/yy} al {_filter.To:dd/MM/yy})"
            : "");
        await Navigation.PushAsync(new ProductRankingPage(_db, _filter, description));
    }
}

/// <summary>Una venta en el listado.</summary>
public class SaleRow
{
    public int SaleId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string FiscalText { get; set; } = string.Empty;
    public Color FiscalColor { get; set; } = Colors.Gray;
    public decimal Total { get; set; }
    public decimal Profit { get; set; }
    public string ItemsText { get; set; } = string.Empty;
}

/// <summary>Total de un medio de pago en el resumen.</summary>
public class PaymentTotalRow
{
    public string Label { get; set; } = string.Empty;
    public decimal Total { get; set; }
}
