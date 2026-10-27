using System.Globalization;
using System.Net;
using VerticeMobile.Data;

namespace VerticeMobile.Services;

/// <summary>
/// Trae los datos de la PC a la copia local. Corre al abrir la app, al volver a ella y cuando
/// el usuario lo pide. Si la PC no responde, no toca nada: la app sigue con lo último guardado.
/// </summary>
public class SyncService
{
    private const int SalesPageSize = 1000;

    private readonly ApiClient _api;
    private readonly LocalDb _db;
    private readonly ConnectionState _state;
    private readonly SemaphoreSlim _gate = new(1, 1);

    public SyncService(ApiClient api, LocalDb db, ConnectionState state)
    {
        _api = api;
        _db = db;
        _state = state;
    }

    /// <summary>Carga la fecha de la última sincronización (para el cartel), sin conectarse.</summary>
    public async Task LoadLastSyncAsync() => _state.SetLastSync(await _db.GetLastSyncAsync());

    /// <summary>Lanza una sincronización sin esperarla (para eventos de la app).</summary>
    public void SyncInBackground() => _ = SyncAsync();

    /// <summary>
    /// Sincroniza todo. Si ya hay una en curso, espera a que termine y no arranca otra.
    /// Devuelve true si quedó todo actualizado.
    /// </summary>
    public async Task<bool> SyncAsync()
    {
        if (!await _gate.WaitAsync(0))
        {
            await _gate.WaitAsync();
            _gate.Release();
            return _state.IsOnline;
        }

        try
        {
            if (!_api.IsConfigured)
            {
                _state.SetStatus(ConnectionStatus.NotConfigured, "Configure la IP y el PIN en Ajustes.");
                return false;
            }

            _state.SetSyncing(true, "Conectando...");

            if (!await _api.IsReachableAsync())
            {
                _state.SetStatus(ConnectionStatus.Offline);
                return false;
            }

            await SyncAllAsync();

            var now = DateTime.Now;
            await _db.SetInfoAsync(LocalDb.KeyLastSync, now.ToString("O"));
            _state.SetLastSync(now);
            _state.SetStatus(ConnectionStatus.Online);
            return true;
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            _state.SetStatus(ConnectionStatus.Unauthorized, ex.Message);
            return false;
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            // Se cortó a mitad de camino: lo que se alcanzó a guardar queda, el resto en la próxima.
            _state.SetStatus(ConnectionStatus.Offline);
            return false;
        }
        catch (Exception ex)
        {
            _state.SetStatus(ConnectionStatus.Offline, $"Error al actualizar: {ex.Message}");
            return false;
        }
        finally
        {
            _state.SetSyncing(false);
            _gate.Release();
        }
    }

    private async Task SyncAllAsync()
    {
        // 1. Catálogo completo: productos, medios de pago y cajas.
        _state.SetSyncing(true, "Actualizando productos...");
        var catalog = await _api.GetCatalogAsync();
        await _db.ReplaceCatalogAsync(catalog);

        // 2. Si la historia de ventas de la PC no es la misma que tenemos (se borraron las ventas
        //    y la numeración volvió a empezar), la copia local ya no sirve: se baja todo de nuevo.
        string? localFingerprint = await _db.GetInfoAsync(LocalDb.KeySalesFingerprint);
        if (localFingerprint != catalog.SalesFingerprint)
        {
            await _db.ClearSalesAsync();
            await _db.SetInfoAsync(LocalDb.KeySalesFingerprint, catalog.SalesFingerprint);
        }

        // 3. Ventas nuevas, de a páginas. Se guarda cada página apenas llega: si se corta la
        //    conexión, la próxima vez sigue desde la última que quedó.
        int downloaded = 0;
        bool hasMore = true;
        while (hasMore)
        {
            int afterId = await _db.GetMaxSaleIdAsync();
            var page = await _api.GetSalesAsync(afterId, SalesPageSize);
            await _db.InsertSalesAsync(page.Sales);

            downloaded += page.Sales.Count;
            hasMore = page.HasMore && page.Sales.Count > 0;
            if (hasMore) _state.SetSyncing(true, $"Descargando ventas... ({downloaded})");
        }

        // 4. Estado fiscal de las que todavía pueden cambiar (pendientes en ARCA, o recién hechas,
        //    porque la factura se crea unos segundos después de la venta).
        DateTime previousServerTime = DateTime.TryParse(await _db.GetInfoAsync(LocalDb.KeyLastServerTime), CultureInfo.InvariantCulture, DateTimeStyles.None, out var t)
            ? t
            : catalog.ServerTime;
        var toRecheck = await _db.GetSaleIdsToRecheckFiscalAsync(previousServerTime.AddMinutes(-5));
        foreach (var chunk in toRecheck.Chunk(SalesPageSize))
        {
            var statuses = await _api.GetFiscalStatusAsync(chunk);
            await _db.UpdateFiscalStatusAsync(statuses);
        }

        await _db.SetInfoAsync(LocalDb.KeyLastServerTime, catalog.ServerTime.ToString("s", CultureInfo.InvariantCulture));
    }
}
