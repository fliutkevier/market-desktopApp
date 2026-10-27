using VerticeMobile.Data;
using VerticeMobile.Models;
using VerticeMobile.Services;

namespace VerticeMobile
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiClient _api;
        private readonly LocalDb _db;
        private readonly ConnectionState _state;
        private readonly SyncService _sync;
        private string _currentScannedCode = "";

        public MainPage(ApiClient api, LocalDb db, ConnectionState state, SyncService sync)
        {
            InitializeComponent();
            _api = api;
            _db = db;
            _state = state;
            _sync = sync;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _state.Changed += OnConnectionChanged;
            UpdateWriteButtons();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _state.Changed -= OnConnectionChanged;
        }

        private void OnConnectionChanged(object? sender, EventArgs e) => UpdateWriteButtons();

        // Crear y modificar solo con conexión: sin la PC no hay dónde guardarlo.
        private void UpdateWriteButtons()
        {
            bool online = _state.IsOnline;
            btnEditMode.IsEnabled = online;
            btnCreate.IsEnabled = online;
            btnSaveEdit.IsEnabled = online;
        }

        // ¿Vale la pena intentar contra la PC? Si ya sabemos que no hay conexión, directo a la copia local.
        private bool ShouldTryServer => _state.Status is ConnectionStatus.Online or ConnectionStatus.Unknown;

        private async void OnToggleScannerClicked(object sender, EventArgs e)
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();

                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlertAsync("Permiso Denegado", "No se puede escanear sin acceso a la cámara.", "OK");
                    return;
                }
            }

            // ABRIMOS LA PANTALLA EXCLUSIVA DE LA CÁMARA
            // Usamos PushAsync para que tenga la flechita de volver atrás.
            await Navigation.PushAsync(new ScannerPage((codigoEscaneado) =>
            {
                txtBarcode.Text = codigoEscaneado;
                OnSearchClicked(this, EventArgs.Empty);
            }));
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            string code = txtBarcode.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(code)) return;

            if (!_api.IsConfigured)
            {
                await DisplayAlertAsync("Aviso", "Primero configure la IP y el PIN en la pestaña Ajustes ⚙️.", "OK");
                return;
            }

            // En la base hay códigos cargados con espacios adelante, y la PC los busca exactos: si
            // la copia local lo conoce, se usa el código tal cual está guardado.
            var known = await _db.GetProductAsync(code);
            code = known?.Code ?? code;
            _currentScannedCode = code; // Lo guardamos en memoria

            btnSearch.IsEnabled = false;
            btnSearch.Text = "Buscando...";
            lblError.IsVisible = false;
            borderResult.IsVisible = false;
            borderCreate.IsVisible = false; // Ocultamos el form de creación por si estaba abierto
            borderEdit.IsVisible = false;

            try
            {
                if (ShouldTryServer && await SearchOnServerAsync(code))
                    return;

                await SearchInCacheAsync(code);
            }
            finally
            {
                btnSearch.IsEnabled = true;
                btnSearch.Text = "Por Código";
            }
        }

        /// <summary>Busca en la PC. Devuelve false si no hubo conexión (para buscar en la copia local).</summary>
        private async Task<bool> SearchOnServerAsync(string code)
        {
            try
            {
                var product = await _api.GetProductAsync(code);

                if (product != null)
                {
                    ShowProduct(product, fromCache: false);
                    await _db.SaveProductAsync(product); // la copia local queda al día con lo que se vio
                }
                else
                {
                    //formulario de creación
                    txtNewName.Text = "";
                    txtNewCost.Text = "";
                    txtNewPercentage.Text = "";
                    txtNewPrice.Text = "";
                    txtNewStock.Text = "";

                    borderCreate.IsVisible = true; //formulario de alta
                }
                return true;
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    _state.SetStatus(ConnectionStatus.Unauthorized, ex.Message);

                lblError.Text = ex.Message;
                lblError.IsVisible = true;
                return true;
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                _state.SetStatus(ConnectionStatus.Offline);
                return false;
            }
        }

        private async Task SearchInCacheAsync(string code)
        {
            var cached = await _db.GetProductAsync(code);
            if (cached == null)
            {
                lblError.Text = "No está en los datos guardados. Para darlo de alta hay que estar conectado a la PC del local.";
                lblError.IsVisible = true;
                return;
            }

            ShowProduct(new ProductResponse
            {
                Code = cached.Code,
                Name = cached.Name,
                SalePrice = cached.SalePrice,
                CostPrice = cached.CostPrice,
                Stock = cached.Stock,
                LastStockUpdate = cached.LastStockUpdate
            }, fromCache: true);
        }

        private void ShowProduct(ProductResponse product, bool fromCache)
        {
            lblName.Text = product.Name;
            lblCostPrice.Text = $"Costo: {product.CostPrice:C0}";
            lblSalePrice.Text = $"Venta: {product.SalePrice:C0}";
            lblStock.Text = $"Stock: {product.Stock}";

            lblCachedNotice.IsVisible = fromCache;
            lblCachedNotice.Text = _state.LastSync is DateTime d
                ? $"Sin conexión: datos guardados del {d:dd/MM HH:mm}"
                : "Sin conexión: datos guardados";

            // Pre-llenar campos de edición (por si el usuario decide modificarlo)
            txtEditName.Text = product.Name;
            txtEditCost.Text = product.CostPrice.ToString("0");
            txtEditPrice.Text = product.SalePrice.ToString("0");
            txtEditStock.Text = product.Stock.ToString();
            txtEditPercentage.Text = ""; // Lo dejamos vacío para que decida si quiere aplicar un % nuevo

            borderResult.IsVisible = true; // info
        }

        private async void OnSearchByNameClicked(object sender, EventArgs e)
        {
            // Usamos PushAsync para que vuelva a tener la barra superior con el botón Atrás
            await Navigation.PushAsync(new ProductListPage(_db, _sync, (codigoElegido) =>
            {
                txtBarcode.Text = codigoElegido;
                OnSearchClicked(this, EventArgs.Empty);
            }));
        }

        private void OnCreateCalculationChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(txtNewCost.Text, out decimal cost) &&
                decimal.TryParse(txtNewPercentage.Text, out decimal percentage))
            {
                decimal salePrice = cost + (cost * (percentage / 100m));
                txtNewPrice.Text = salePrice.ToString("0");
            }
        }

        private void OnEditCalculationChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(txtEditCost.Text, out decimal cost) &&
                decimal.TryParse(txtEditPercentage.Text, out decimal percentage))
            {
                decimal salePrice = cost + (cost * (percentage / 100m));
                txtEditPrice.Text = salePrice.ToString("0");
            }
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            // 1. Validaciones tontas de UI
            if (string.IsNullOrWhiteSpace(txtNewName.Text))
            {
                await DisplayAlertAsync("Error", "El nombre es obligatorio.", "OK");
                return;
            }

            decimal.TryParse(txtNewCost.Text, out decimal cost);
            decimal.TryParse(txtNewPrice.Text, out decimal price);
            int.TryParse(txtNewStock.Text, out int stock);

            // 2. Armar el DTO para el backend (CreateProductDto de la API)
            var newProduct = new CreateProductMobileDto
            {
                Code = _currentScannedCode, // Usamos el código que falló en la búsqueda
                Name = txtNewName.Text.Trim(),
                CostPrice = cost,
                SalePrice = price,
                InitialStock = stock
            };

            btnCreate.IsEnabled = false;
            btnCreate.Text = "Guardando...";

            try
            {
                await _api.CreateProductAsync(_currentScannedCode, newProduct);

                var saved = new ProductResponse
                {
                    Code = _currentScannedCode,
                    Name = newProduct.Name,
                    CostPrice = cost,
                    SalePrice = price,
                    Stock = stock,
                    LastStockUpdate = DateTime.Now
                };
                await _db.SaveProductAsync(saved);

                // Mostramos el resultado como si lo hubiéramos buscado
                borderCreate.IsVisible = false;
                ShowProduct(saved, fromCache: false);
            }
            catch (ApiException ex)
            {
                await DisplayAlertAsync("Error al guardar", ex.Message, "OK");
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                _state.SetStatus(ConnectionStatus.Offline);
                await DisplayAlertAsync("Sin conexión", "No se pudo conectar con la PC. El producto no se guardó.", "OK");
            }
            finally
            {
                btnCreate.Text = "Guardar Producto";
                UpdateWriteButtons();
            }
        }

        private void OnEditModeClicked(object sender, EventArgs e)
        {
            borderResult.IsVisible = false;
            borderEdit.IsVisible = true;
        }

        private void OnCancelEditClicked(object sender, EventArgs e)
        {
            borderEdit.IsVisible = false;
            borderResult.IsVisible = true;
        }

        private async void OnSaveEditClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEditName.Text)) return;

            decimal.TryParse(txtEditCost.Text, out decimal cost);
            decimal.TryParse(txtEditPrice.Text, out decimal price);
            int.TryParse(txtEditStock.Text, out int stock);

            // DTO de actualización (UpdateProductDto de la API)
            var editProduct = new EditProductMobileDto
            {
                Code = _currentScannedCode,
                Name = txtEditName.Text.Trim(),
                CostPrice = cost,
                SalePrice = price,
                Stock = stock,
                IsActive = true
            };

            btnSaveEdit.IsEnabled = false;

            try
            {
                await _api.UpdateProductAsync(_currentScannedCode, editProduct);

                var saved = new ProductResponse
                {
                    Code = _currentScannedCode,
                    Name = editProduct.Name,
                    CostPrice = cost,
                    SalePrice = price,
                    Stock = stock,
                    LastStockUpdate = DateTime.Now
                };
                await _db.SaveProductAsync(saved);

                borderEdit.IsVisible = false;
                ShowProduct(saved, fromCache: false); // Volvemos a mostrar la tarjeta con los datos nuevos
            }
            catch (ApiException ex)
            {
                await DisplayAlertAsync("Error", ex.Message, "OK");
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                _state.SetStatus(ConnectionStatus.Offline);
                await DisplayAlertAsync("Sin conexión", "No se pudo conectar con la PC. Los cambios no se guardaron.", "OK");
            }
            finally
            {
                UpdateWriteButtons();
            }
        }
    }

    public class CreateProductMobileDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public int InitialStock { get; set; }
    }

    public class EditProductMobileDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
    }
}
