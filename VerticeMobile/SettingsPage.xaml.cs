namespace VerticeMobile;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        // Cargamos la IP guardada al abrir la pantalla
        txtIpAddress.Text = Preferences.Default.Get("ApiIpAddress", "");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        string ip = txtIpAddress.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(ip))
        {
            await DisplayAlertAsync("Error", "La IP no puede estar vacía.", "OK");
            return;
        }

        // MAUI guarda este dato permanentemente en el celular
        Preferences.Default.Set("ApiIpAddress", ip);

        await DisplayAlertAsync("Éxito", "Dirección IP guardada correctamente.", "OK");

        // Volvemos a la pantalla principal
        await Navigation.PopAsync();
    }
}