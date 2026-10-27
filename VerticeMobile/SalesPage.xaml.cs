using VerticeMobile.Data;
using VerticeMobile.Services;

namespace VerticeMobile;

public partial class SalesPage : ContentPage
{
    private readonly LocalDb _db;
    private readonly SyncService _sync;
    private readonly ConnectionState _state;

    public SalesPage(LocalDb db, SyncService sync, ConnectionState state)
    {
        InitializeComponent();
        _db = db;
        _sync = sync;
        _state = state;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _state.Changed += OnStateChanged;
        await LoadAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _state.Changed -= OnStateChanged;
    }

    // Cuando termina una sincronización, se recalcula con lo nuevo.
    private async void OnStateChanged(object? sender, EventArgs e)
    {
        if (!_state.IsSyncing) await LoadAsync();
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await _sync.SyncAsync();
        await LoadAsync();
        refreshView.IsRefreshing = false;
    }

    private async Task LoadAsync()
    {
        var today = DateTime.Today;
        var (count, total, cost) = await _db.GetSalesSummaryAsync(today, today.AddDays(1).AddTicks(-1));

        lblTodayCount.Text = $"{count} ventas";
        lblTodayTotal.Text = $"Total: {total:C2}";
        lblTodayProfit.Text = $"Ganancia: {total - cost:C2}";

        lblStored.Text = $"{await _db.CountSalesAsync()} ventas guardadas en el celular";
    }
}
