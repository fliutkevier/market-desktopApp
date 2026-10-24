using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IFiscalService
    {
        /// <summary>
        /// Genera una factura electrónica ante ARCA (ex AFIP), obteniendo el CAE.
        /// </summary>
        /// <param name="sale">La venta con todos sus datos (montos, fecha, items).</param>
        /// <param name="cancellationToken">Permite abortar el pedido HTTP real si ARCA tarda demasiado.</param>
        /// <returns>Un objeto FiscalDocument con el CAE y otros datos de respuesta.</returns>
        Task<FiscalDocument> GenerateInvoiceAsync(Sale sale, CancellationToken cancellationToken = default);
    }
}
