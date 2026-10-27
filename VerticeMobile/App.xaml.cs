using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using VerticeMobile.Services;

namespace VerticeMobile
{
    public partial class App : Application
    {
        private readonly IServiceProvider _services;
        private readonly SyncService _sync;

        public App(IServiceProvider services, SyncService sync)
        {
            InitializeComponent();

            // CLAVAR LA CULTURA A ARGENTINA
            // Esto obliga a toda la app a usar el punto '.' para los miles y la coma ',' para decimales.
            var culture = new CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            UserAppTheme = AppTheme.Light;

            _services = services;
            _sync = sync;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Un AppShell nuevo por ventana: si Android recrea la Activity, no se reutiliza uno que ya tiene dueño.
            var window = new Window(_services.GetRequiredService<AppShell>());

            // Al abrir la app y cada vez que se vuelve a ella se trae lo nuevo de la PC.
            // Sin conexión no pasa nada: se sigue con lo último guardado.
            window.Created += async (_, _) =>
            {
                await _sync.LoadLastSyncAsync();
                _sync.SyncInBackground();
            };
            window.Resumed += (_, _) => _sync.SyncInBackground();

            return window;
        }
    }
}
