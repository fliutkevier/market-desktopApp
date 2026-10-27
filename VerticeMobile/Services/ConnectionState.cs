namespace VerticeMobile.Services;

public enum ConnectionStatus
{
    /// <summary>Todavía no se intentó conectar.</summary>
    Unknown,
    Online,
    Offline,
    /// <summary>La PC responde pero el PIN no es el correcto.</summary>
    Unauthorized,
    /// <summary>Falta la IP o el PIN en Ajustes.</summary>
    NotConfigured
}

/// <summary>
/// Estado de la conexión con la PC, compartido por toda la app: de acá sale el cartel de
/// "sin conexión" y si se permite crear o editar (solo con Online).
/// </summary>
public class ConnectionState
{
    public ConnectionStatus Status { get; private set; } = ConnectionStatus.Unknown;
    public string? Message { get; private set; }
    public DateTime? LastSync { get; private set; }
    public bool IsSyncing { get; private set; }
    public string? SyncProgress { get; private set; }

    public bool IsOnline => Status == ConnectionStatus.Online;

    /// <summary>Se dispara en el hilo principal.</summary>
    public event EventHandler? Changed;

    public void SetStatus(ConnectionStatus status, string? message = null)
    {
        Status = status;
        Message = message;
        Notify();
    }

    public void SetLastSync(DateTime? lastSync)
    {
        LastSync = lastSync;
        Notify();
    }

    public void SetSyncing(bool syncing, string? progress = null)
    {
        IsSyncing = syncing;
        SyncProgress = syncing ? progress : null;
        Notify();
    }

    private void Notify() => MainThread.BeginInvokeOnMainThread(() => Changed?.Invoke(this, EventArgs.Empty));
}
