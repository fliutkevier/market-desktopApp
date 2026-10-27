using VerticeMobile.Data;
using VerticeMobile.Services;

namespace VerticeMobile;

public partial class SettingsPage : ContentPage
{
    private readonly ConnectionSettings _settings;
    private readonly ApiClient _api;
    private readonly LocalDb _db;
    private readonly SyncService _sync;
    private readonly ConnectionState _state;

    public SettingsPage(ConnectionSettings settings, ApiClient api, LocalDb db, SyncService sync, ConnectionState state)
    {
        InitializeComponent();
        _settings = settings;
        _api = api;
        _db = db;
        _sync = sync;
        _state = state;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Cargamos lo guardado al abrir la pantalla
        txtIpAddress.Text = _settings.Ip;
        txtPin.Text = await _settings.GetPinAsync();

        _state.Changed += OnStateChanged;
        await RefreshCacheInfoAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _state.Changed -= OnStateChanged;
    }

    private async void OnStateChanged(object? sender, EventArgs e)
    {
        btnSyncNow.IsEnabled = !_state.IsSyncing;
        if (!_state.IsSyncing) await RefreshCacheInfoAsync();
    }

    private async Task RefreshCacheInfoAsync()
    {
        int products = (await _db.GetProductsAsync()).Count;
        int sales = await _db.CountSalesAsync();
        string last = _state.LastSync is DateTime d ? d.ToString("dd/MM/yyyy HH:mm") : "nunca";

        lblCacheInfo.Text = $"Última actualización: {last}\n{products} productos · {sales} ventas";
    }

    private async Task<bool> SaveInputsAsync()
    {
        string ip = txtIpAddress.Text?.Trim() ?? "";
        string pin = txtPin.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(pin))
        {
            await DisplayAlertAsync("Error", "La IP y el PIN son obligatorios.", "OK");
            return false;
        }

        // MAUI guarda estos datos permanentemente en el celular (el PIN, cifrado)
        await _settings.SaveAsync(ip, pin);
        return true;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!await SaveInputsAsync()) return;

        await DisplayAlertAsync("Éxito", "Configuración guardada.", "OK");
        _sync.SyncInBackground();
    }

    // Guarda y prueba: primero si la PC responde, después si el PIN es correcto.
    private async void OnTestClicked(object sender, EventArgs e)
    {
        if (!await SaveInputsAsync()) return;

        btnTest.IsEnabled = false;
        btnTest.Text = "Probando...";
        try
        {
            if (!await _api.IsReachableAsync())
            {
                await DisplayAlertAsync("Sin conexión",
                    "La PC no responde. Revise que el celular esté en el wifi del local, que Vertice esté abierto y que la IP sea correcta.", "OK");
                return;
            }

            bool ok = await _sync.SyncAsync();
            if (ok)
                await DisplayAlertAsync("Conectado", "Conexión correcta. Los datos quedaron actualizados.", "OK");
            else
                await DisplayAlertAsync("Error", _state.Message ?? "No se pudo actualizar.", "OK");
        }
        finally
        {
            btnTest.IsEnabled = true;
            btnTest.Text = "Probar Conexión";
        }
    }

    private void OnSyncNowClicked(object sender, EventArgs e) => _sync.SyncInBackground();

    private async void OnClearCacheClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlertAsync("Borrar datos guardados",
            "Se borran los productos y ventas guardados en el celular (en la PC no se toca nada). La próxima actualización los vuelve a bajar.",
            "Borrar", "Cancelar");
        if (!confirm) return;

        await _db.ClearAllAsync();
        _state.SetLastSync(null);
        await RefreshCacheInfoAsync();
    }
}
