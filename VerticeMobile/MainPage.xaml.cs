using System.Net.Http.Json;
using ZXing.Net.Maui;

namespace VerticeMobile
{
    public partial class MainPage : ContentPage
    {
        // 10.0.2.2 es la forma en que el emulador de Android ve a tu PC (localhost).
        // Si usás tu celular físico con WiFi, tenés que poner la IP de tu PC (ej: 192.168.0.x)
        //private const string ApiBaseUrl = "http://192.168.100.96:5000/products/";
        private readonly HttpClient _httpClient;
        private string _currentScannedCode = "";

        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            //_httpClient.BaseAddress = new Uri(ApiBaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(10);

            // Ya no configuramos el BarcodeReader acá
        }

        // BOTÓN PARA ABRIR LA PANTALLA DE AJUSTES
        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        // FUNCIÓN QUE ARMA LA RUTA COMPLETA LEYENDO LA IP EN TIEMPO REAL
        private string GetApiUrl(string path = "")
        {
            string ip = Microsoft.Maui.Storage.Preferences.Default.Get("ApiIpAddress", "");
            if (string.IsNullOrEmpty(ip)) return "";

            string baseUrl = $"http://{ip}:5000/products";
            return string.IsNullOrEmpty(path) ? baseUrl : $"{baseUrl}/{path}";
        }

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
            string code = txtBarcode.Text?.Trim();
            if (string.IsNullOrEmpty(code)) return;

            // VALIDAMOS QUE HAYA IP CONFIGURADA ANTES DE BUSCAR
            string url = GetApiUrl(code);
            if (string.IsNullOrEmpty(url))
            {
                await DisplayAlertAsync("Aviso", "Primero configure la IP del servidor local en el botón de Ajustes ⚙️.", "OK");
                return;
            }

            _currentScannedCode = code; // Lo guardamos en memoria

            btnSearch.IsEnabled = false;
            btnSearch.Text = "Buscando...";
            lblError.IsVisible = false;
            borderResult.IsVisible = false;
            borderCreate.IsVisible = false; // Ocultamos el form de creación por si estaba abierto

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadFromJsonAsync<ProductMobileDto>();
                    if (product != null)
                    {
                        lblName.Text = product.Name;
                        lblCostPrice.Text = $"Costo: {product.CostPrice:C0}";
                        lblSalePrice.Text = $"Venta: {product.SalePrice:C0}";
                        lblStock.Text = $"Stock: {product.Stock}";

                        // Pre-llenar campos de edición (por si el usuario decide modificarlo)
                        txtEditName.Text = product.Name;
                        txtEditCost.Text = product.CostPrice.ToString("0");
                        txtEditPrice.Text = product.SalePrice.ToString("0");
                        txtEditStock.Text = product.Stock.ToString();
                        txtEditPercentage.Text = ""; // Lo dejamos vacío para que decida si quiere aplicar un % nuevo

                        borderResult.IsVisible = true; // info
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //formulario de creación
                    txtNewName.Text = "";
                    txtNewCost.Text = "";
                    txtNewPercentage.Text = "";
                    txtNewPrice.Text = "";
                    txtNewStock.Text = "";

                    borderCreate.IsVisible = true; //formulario de alta
                }
                else
                {
                    lblError.Text = $"Error de servidor: {response.StatusCode}";
                    lblError.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error de red: {ex.Message}";
                lblError.IsVisible = true;
            }
            finally
            {
                btnSearch.IsEnabled = true;
                btnSearch.Text = "Buscar Producto";
            }
        }

        private async void OnSearchByNameClicked(object sender, EventArgs e)
        {
            // Usamos PushAsync para que vuelva a tener la barra superior con el botón Atrás
            await Navigation.PushAsync(new ProductListPage((codigoElegido) =>
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

            string url = GetApiUrl(_currentScannedCode);
            if (string.IsNullOrEmpty(url)) return;

            decimal.TryParse(txtNewCost.Text, out decimal cost);
            decimal.TryParse(txtNewPrice.Text, out decimal price);
            int.TryParse(txtNewStock.Text, out int stock);

            // 2. Armar el DTO para el backend
            // ATENCIÓN: Revisá que estas propiedades coincidan con el CreateProductDto de tu API.
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
                // USAMOS LA URL DINÁMICA ACÁ
                var response = await _httpClient.PostAsJsonAsync(url, newProduct);

                if (response.IsSuccessStatusCode)
                {
                    // 1. Ocultamos el formulario de alta
                    borderCreate.IsVisible = false;

                    // 2. Cargamos los datos en la tarjeta de resultado visual
                    lblName.Text = newProduct.Name;
                    lblCostPrice.Text = $"Costo: {newProduct.CostPrice:C0}";
                    lblSalePrice.Text = $"Venta: {newProduct.SalePrice:C0}";
                    lblStock.Text = $"Stock actual: {newProduct.InitialStock} unid.";

                    // 3. Mostramos el resultado como si lo hubiéramos buscado
                    borderResult.IsVisible = true;
                }
                else
                {
                    string errorObj = await response.Content.ReadAsStringAsync();
                    await DisplayAlertAsync("Error al guardar", errorObj, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error de Red", ex.Message, "OK");
            }
            finally
            {
                btnCreate.IsEnabled = true;
                btnCreate.Text = "Guardar Producto";
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

            string url = GetApiUrl(_currentScannedCode);
            if (string.IsNullOrEmpty(url)) return;

            decimal.TryParse(txtEditCost.Text, out decimal cost);
            decimal.TryParse(txtEditPrice.Text, out decimal price);
            int.TryParse(txtEditStock.Text, out int stock);

            // DTO de actualización. 
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
                // USAMOS LA URL DINÁMICA ACÁ
                var response = await _httpClient.PutAsJsonAsync(url, editProduct);

                if (response.IsSuccessStatusCode)
                {
                    // Actualizamos la tarjeta de resultado visual con los datos nuevos
                    lblName.Text = editProduct.Name;
                    lblCostPrice.Text = $"Costo: {editProduct.CostPrice:C0}";
                    lblSalePrice.Text = $"Venta: {editProduct.SalePrice:C0}";
                    lblStock.Text = $"Stock: {editProduct.Stock}";

                    borderEdit.IsVisible = false;
                    borderResult.IsVisible = true; // Volvemos a mostrar el GET actualizado
                }
                else
                {
                    string errorObj = await response.Content.ReadAsStringAsync();
                    await DisplayAlertAsync("Error", errorObj, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error de Red", ex.Message, "OK");
            }
            finally
            {
                btnSaveEdit.IsEnabled = true;
            }
        }
    }

    // DTO local para la app móvil
    public class ProductMobileDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public int Stock { get; set; }
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
