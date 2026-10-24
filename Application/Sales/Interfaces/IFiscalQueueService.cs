using Application.Sales.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sales.Interfaces
{
    /// <summary>
    /// Procesa la cola de facturas fiscales que quedaron "Pending" (ARCA caído, timeout, etc.).
    /// Lo usan tanto el worker automático (cada 20 min) como el botón manual de la pantalla
    /// "Facturación Pendiente".
    /// </summary>
    public interface IFiscalQueueService
    {
        Task<FiscalQueueResultDto> ProcessPendingAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<PendingFiscalDocumentDto>> GetPendingAsync();
    }
}
