namespace VerticeMobile.Services;

/// <summary>IP de la PC y PIN del servidor. La IP va en Preferences; el PIN en SecureStorage (cifrado).</summary>
public class ConnectionSettings
{
    private const string IpKey = "ApiIpAddress"; // mismo nombre que usaba la versión anterior
    private const string PinKey = "ApiPin";

    private string? _pin;

    public string Ip => Preferences.Default.Get(IpKey, "");

    public async Task<string> GetPinAsync()
    {
        if (_pin != null) return _pin;

        try
        {
            _pin = await SecureStorage.Default.GetAsync(PinKey) ?? "";
        }
        catch
        {
            // En algunos Android se pierde la clave de cifrado (ej. al restaurar un backup):
            // se pide el PIN de nuevo en vez de romper la app.
            SecureStorage.Default.Remove(PinKey);
            _pin = "";
        }
        return _pin;
    }

    public async Task SaveAsync(string ip, string pin)
    {
        Preferences.Default.Set(IpKey, ip.Trim());
        await SecureStorage.Default.SetAsync(PinKey, pin.Trim());
        _pin = pin.Trim();
    }
}
