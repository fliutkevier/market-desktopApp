using System.Net.Http.Json;

namespace VerticeMobile;

public partial class ProductListPage : ContentPage
{
    private readonly HttpClient _httpClient;

    private List<ProductMobileDto> _allProducts = new();
    private readonly Action<string> _onProductSelected;

    public ProductListPage(Action<string> onProductSelected)
    {
        InitializeComponent();
        _onProductSelected = onProductSelected;

        _httpClient = new HttpClient();
        // ELIMINAMOS EL BaseAddress FIJO DE ACÁ
        _httpClient.Timeout = TimeSpan.FromSeconds(15);
    }

    protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadProductsAsync();
        }

    private async Task LoadProductsAsync()
        {

        // LEEMOS LA IP GUARDADA
        string ip = Microsoft.Maui.Storage.Preferences.Default.Get("ApiIpAddress", "");
        if (string.IsNullOrEmpty(ip))
        {
            await DisplayAlertAsync("Aviso", "No hay IP configurada. Vaya a Ajustes en la pantalla principal.", "OK");
            return;
        }
        try
            {
                loadingIndicator.IsRunning = true;
                loadingIndicator.IsVisible = true;
                cvProducts.IsVisible = false;

            // ARMAMOS LA RUTA
            string url = $"http://{ip}:5000/products";
            var response = await _httpClient.GetFromJsonAsync<List<ProductMobileDto>>(url);

            if (response != null)
                {
                    _allProducts = response;
                    
                    // CRÍTICO: La respuesta del servidor puede volver en un hilo secundario.
                    // Modificar la vista desde acá rompe Android nativamente.
                    // Forzamos la actualización en el Hilo Principal (MainThread).
                    MainThread.BeginInvokeOnMainThread(() => 
                    {
                        cvProducts.ItemsSource = _allProducts;
                    });
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () => 
                {
                    await DisplayAlertAsync("Error", $"No se pudo cargar la lista: {ex.Message}", "OK");
                });
            }
            finally
            {
                MainThread.BeginInvokeOnMainThread(() => 
                {
                    loadingIndicator.IsRunning = false;
                    loadingIndicator.IsVisible = false;
                    cvProducts.IsVisible = true;
                });
            }
        }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = e.NewTextValue?.ToLowerInvariant() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                cvProducts.ItemsSource = _allProducts;
            }
            else
            {
                // Filtramos en memoria (muy rápido)
                var filtered = _allProducts.Where(p => 
                    p.Name.ToLowerInvariant().Contains(keyword) || 
                    p.Code.Contains(keyword)
                ).ToList();

                cvProducts.ItemsSource = filtered;
            }
        }

    private void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ProductMobileDto selectedProduct)
        {
            cvProducts.SelectedItem = null;

            _onProductSelected?.Invoke(selectedProduct.Code);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Como volvimos a usar NavigationPage, volvemos a usar PopAsync estándar
                await Navigation.PopAsync();
            });
        }
    }
}