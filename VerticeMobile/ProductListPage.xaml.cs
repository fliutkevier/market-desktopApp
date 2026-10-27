using VerticeMobile.Data;
using VerticeMobile.Services;

namespace VerticeMobile;

public partial class ProductListPage : ContentPage
{
    private readonly LocalDb _db;
    private readonly SyncService _sync;

    private List<CachedProduct> _allProducts = new();
    private readonly Action<string> _onProductSelected;

    public ProductListPage(LocalDb db, SyncService sync, Action<string> onProductSelected)
    {
        InitializeComponent();
        _db = db;
        _sync = sync;
        _onProductSelected = onProductSelected;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadProductsAsync();
    }

    // La lista sale de la copia local: abre al instante y funciona sin conexión.
    private async Task LoadProductsAsync()
    {
        _allProducts = await _db.GetProductsAsync();
        ApplyFilter(searchBar.Text);
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await _sync.SyncAsync();
        await LoadProductsAsync();
        refreshView.IsRefreshing = false;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e) => ApplyFilter(e.NewTextValue);

    private void ApplyFilter(string? text)
    {
        string keyword = text?.Trim().ToLowerInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(keyword))
        {
            cvProducts.ItemsSource = _allProducts;
            return;
        }

        // Filtramos en memoria (muy rápido)
        cvProducts.ItemsSource = _allProducts.Where(p =>
            p.Name.ToLowerInvariant().Contains(keyword) ||
            p.CodeKey.Contains(keyword)
        ).ToList();
    }

    private void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is CachedProduct selectedProduct)
        {
            cvProducts.SelectedItem = null;

            _onProductSelected?.Invoke(selectedProduct.CodeKey);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
            });
        }
    }
}
