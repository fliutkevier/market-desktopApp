using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace VerticeMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // CLAVAR LA CULTURA A ARGENTINA
            // Esto obliga a toda la app a usar el punto '.' para los miles y la coma ',' para decimales.
            var culture = new CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            UserAppTheme = AppTheme.Light;
            MainPage = new NavigationPage(new MainPage());
        }
    }
}