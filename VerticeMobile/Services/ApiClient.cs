using System.Net;
using System.Net.Http.Json;
using VerticeMobile.Models;

namespace VerticeMobile.Services;

/// <summary>Error del servidor con un mensaje para mostrar tal cual.</summary>
public class ApiException : Exception
{
    public HttpStatusCode? StatusCode { get; }

    public ApiException(string message, HttpStatusCode? statusCode = null) : base(message)
    {
        StatusCode = statusCode;
    }
}

/// <summary>
/// Único punto de contacto con el servidor de la PC (http://IP:5000). Arma la URL con la IP de
/// Ajustes y manda el PIN en cada pedido. Los errores de red salen como HttpRequestException o
/// TaskCanceledException (timeout); los del servidor como ApiException.
/// </summary>
public class ApiClient
{
    private const int Port = 5000;
    private const string PinHeader = "X-Vertice-Pin";

    // Un solo HttpClient para toda la app; el timeout se maneja por pedido.
    private readonly HttpClient _http = new() { Timeout = Timeout.InfiniteTimeSpan };
    private readonly ConnectionSettings _settings;

    public ApiClient(ConnectionSettings settings)
    {
        _settings = settings;
    }

    public bool IsConfigured => !string.IsNullOrWhiteSpace(_settings.Ip);

    /// <summary>¿La PC responde? No usa PIN. Timeout corto: si no contesta rápido, estamos fuera del local.</summary>
    public async Task<bool> IsReachableAsync()
    {
        if (!IsConfigured) return false;
        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            using var response = await _http.GetAsync(BuildUrl("health"), cts.Token);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public Task<CatalogResponse> GetCatalogAsync() =>
        SendAsync<CatalogResponse>(HttpMethod.Get, "catalog", timeoutSeconds: 30);

    public Task<SalesPageResponse> GetSalesAsync(int afterId, int take) =>
        SendAsync<SalesPageResponse>(HttpMethod.Get, $"sales?afterId={afterId}&take={take}", timeoutSeconds: 60);

    public Task<List<FiscalStatusResponse>> GetFiscalStatusAsync(IReadOnlyCollection<int> saleIds) =>
        SendAsync<List<FiscalStatusResponse>>(HttpMethod.Post, "sales/fiscal-status", saleIds, timeoutSeconds: 30);

    /// <summary>Producto por código. null si no existe (404).</summary>
    public async Task<ProductResponse?> GetProductAsync(string code)
    {
        try
        {
            return await SendAsync<ProductResponse>(HttpMethod.Get, ProductPath(code), timeoutSeconds: 10);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public Task CreateProductAsync(string code, object dto) =>
        SendAsync<object>(HttpMethod.Post, ProductPath(code), dto, timeoutSeconds: 15, readBody: false);

    public Task UpdateProductAsync(string code, object dto) =>
        SendAsync<object>(HttpMethod.Put, ProductPath(code), dto, timeoutSeconds: 15, readBody: false);

    private static string ProductPath(string code) => $"products/{Uri.EscapeDataString(code)}";

    private string BuildUrl(string path) => $"http://{_settings.Ip}:{Port}/{path}";

    private async Task<T> SendAsync<T>(HttpMethod method, string path, object? body = null, int timeoutSeconds = 15, bool readBody = true)
    {
        if (!IsConfigured) throw new ApiException("Configure la IP de la PC en Ajustes.");

        using var request = new HttpRequestMessage(method, BuildUrl(path));
        request.Headers.Add(PinHeader, await _settings.GetPinAsync());
        if (body != null) request.Content = JsonContent.Create(body);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
        using var response = await _http.SendAsync(request, cts.Token);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync(cts.Token);
            if (string.IsNullOrWhiteSpace(message)) message = $"Error del servidor ({(int)response.StatusCode}).";
            throw new ApiException(message, response.StatusCode);
        }

        if (!readBody) return default!;

        return await response.Content.ReadFromJsonAsync<T>(cts.Token)
            ?? throw new ApiException("El servidor devolvió una respuesta vacía.");
    }
}
