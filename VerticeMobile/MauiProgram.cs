using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using VerticeMobile.Data;
using VerticeMobile.Services;
using ZXing.Net.Maui.Controls;

namespace VerticeMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCore()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Conexión con la PC y copia local
            builder.Services.AddSingleton<ConnectionSettings>();
            builder.Services.AddSingleton<ConnectionState>();
            builder.Services.AddSingleton<ApiClient>();
            builder.Services.AddSingleton<LocalDb>();
            builder.Services.AddSingleton<SyncService>();

            // Pantallas (las pestañas de AppShell se crean desde acá)
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<SalesPage>();
            builder.Services.AddTransient<SettingsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
