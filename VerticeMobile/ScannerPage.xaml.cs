using ZXing.Net.Maui;

namespace VerticeMobile;

public partial class ScannerPage : ContentPage
{
    private readonly Action<string> _onScanCompleted;
    private bool _isProcessing = false; // Evita que lea el mismo código 20 veces por segundo

    public ScannerPage(Action<string> onScanCompleted)
    {
        InitializeComponent();
        _onScanCompleted = onScanCompleted;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _isProcessing = false;

        // 1. Configuramos los formatos
        barcodeReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.Ean13 | BarcodeFormat.Ean8 | BarcodeFormat.Code128 | BarcodeFormat.UpcA,
            AutoRotate = true, // Ayuda a leer si el celular está un poco inclinado
            Multiple = false
        };

        // 2. Encendemos el lector AHORA, no antes.
        barcodeReader.IsDetecting = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Apagamos para ahorrar batería
        barcodeReader.IsDetecting = false;
    }

    private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (_isProcessing) return;

        var result = e.Results?.FirstOrDefault();

        if (result != null)
        {
            _isProcessing = true;
            barcodeReader.IsDetecting = false;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Mostramos el cuadro de diálogo nativo de Android/iOS
                bool isAccepted = await DisplayAlertAsync(
                    "Código Detectado",
                    $"N° escaneado:\n{result.Value}",
                    "Aceptar",
                    "Cancelar");

                if (isAccepted)
                {
                    // Si le da a Aceptar, devolvemos el código y cerramos la pantalla de la cámara
                    _onScanCompleted?.Invoke(result.Value);
                    await Navigation.PopAsync();
                }
                else
                {
                    // Si le da a Cancelar, liberamos el bloqueo y volvemos a encender la cámara
                    _isProcessing = false;
                    barcodeReader.IsDetecting = true;
                }
            });
        }
    }
}