using Microsoft.Extensions.DependencyInjection;
using VerticeMobile.Services;

namespace VerticeMobile.Controls;

/// <summary>
/// Cartel de arriba de cada pantalla: si hay conexión con la PC y de cuándo son los datos.
/// Tocarlo vuelve a sincronizar.
/// </summary>
public class StatusBanner : ContentView
{
    private readonly ConnectionState _state;
    private readonly SyncService _sync;
    private readonly Label _label;
    private readonly ActivityIndicator _spinner;
    private readonly Border _border;

    public StatusBanner()
    {
        var services = IPlatformApplication.Current!.Services;
        _state = services.GetRequiredService<ConnectionState>();
        _sync = services.GetRequiredService<SyncService>();

        _label = new Label { FontSize = 13, TextColor = Colors.White, VerticalOptions = LayoutOptions.Center, LineBreakMode = LineBreakMode.TailTruncation };
        _spinner = new ActivityIndicator { Color = Colors.White, WidthRequest = 16, HeightRequest = 16, IsRunning = false, IsVisible = false };

        _border = new Border
        {
            StrokeThickness = 0,
            Padding = new Thickness(12, 6),
            Content = new HorizontalStackLayout { Spacing = 8, Children = { _spinner, _label } }
        };

        var tap = new TapGestureRecognizer();
        tap.Tapped += (_, _) => { if (!_state.IsSyncing) _sync.SyncInBackground(); };
        _border.GestureRecognizers.Add(tap);

        Content = _border;
        Refresh();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        // Suscribirse solo mientras el control está en pantalla, para no perder memoria.
        _state.Changed -= OnStateChanged;
        if (Handler != null)
        {
            _state.Changed += OnStateChanged;
            Refresh();
        }
    }

    private void OnStateChanged(object? sender, EventArgs e) => Refresh();

    private void Refresh()
    {
        string since = _state.LastSync is DateTime d ? $"datos del {d:dd/MM HH:mm}" : "sin datos guardados";

        _spinner.IsVisible = _spinner.IsRunning = _state.IsSyncing;

        if (_state.IsSyncing)
        {
            _border.BackgroundColor = Color.FromArgb("#1976D2");
            _label.Text = _state.SyncProgress ?? "Actualizando...";
            return;
        }

        (_border.BackgroundColor, _label.Text) = _state.Status switch
        {
            ConnectionStatus.Online => (Color.FromArgb("#2E7D32"), $"Conectado · {since}"),
            ConnectionStatus.Offline => (Color.FromArgb("#E65100"), $"Sin conexión · {since}"
                + (string.IsNullOrEmpty(_state.Message) ? "" : $" · {_state.Message}")),
            ConnectionStatus.Unauthorized => (Color.FromArgb("#C62828"), $"PIN incorrecto · {since}"),
            ConnectionStatus.NotConfigured => (Color.FromArgb("#607D8B"), "Configure la IP y el PIN en Ajustes"),
            _ => (Color.FromArgb("#607D8B"), $"Conectando... · {since}")
        };
    }
}
