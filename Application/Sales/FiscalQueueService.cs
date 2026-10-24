using Application.AuditLogs.Interfaces;
using Application.Sales.Dtos;
using Application.Sales.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sales
{
    public class FiscalQueueService : IFiscalQueueService
    {
        private readonly IRepository<FiscalDocument> _fiscalDocRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IFiscalService _fiscalService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditLogService _auditLogService;

        public FiscalQueueService(
            IRepository<FiscalDocument> fiscalDocRepository,
            ISaleRepository saleRepository,
            IFiscalService fiscalService,
            IUnitOfWork unitOfWork,
            IAuditLogService auditLogService)
        {
            _fiscalDocRepository = fiscalDocRepository;
            _saleRepository = saleRepository;
            _fiscalService = fiscalService;
            _unitOfWork = unitOfWork;
            _auditLogService = auditLogService;
        }

        public async Task<FiscalQueueResultDto> ProcessPendingAsync(CancellationToken cancellationToken = default)
        {
            var result = new FiscalQueueResultDto();

            // Orden ascendente: nunca hay que pedirle un CAE a ARCA fuera de orden, la numeración
            // de comprobantes tiene que quedar correlativa a como se generaron las ventas.
            var pendientes = (await _fiscalDocRepository.GetAsync(d => d.Status == FiscalDocumentStatus.Pending))
                .OrderBy(d => d.Id)
                .ToList();

            foreach (var doc in pendientes)
            {
                cancellationToken.ThrowIfCancellationRequested();
                result.Processed++;

                var sale = await _saleRepository.GetByIdAsync(doc.SaleId);
                if (sale == null)
                {
                    doc.LastError = "La venta asociada ya no existe.";
                    doc.LastAttemptAt = DateTime.Now;
                    doc.AttemptCount++;
                    _fiscalDocRepository.Update(doc);
                    result.StillPending++;
                    await _unitOfWork.SaveChangesAsync();
                    continue;
                }

                try
                {
                    var resultado = await _fiscalService.GenerateInvoiceAsync(sale, cancellationToken);

                    doc.Status = FiscalDocumentStatus.Approved;
                    doc.IssuerCuit = resultado.IssuerCuit;
                    doc.PointOfSale = resultado.PointOfSale;
                    doc.InvoiceType = resultado.InvoiceType;
                    doc.InvoiceNumber = resultado.InvoiceNumber;
                    doc.CAE = resultado.CAE;
                    doc.CAEExpirationDate = resultado.CAEExpirationDate;
                    doc.CustomerDocType = resultado.CustomerDocType;
                    doc.CustomerDocNumber = resultado.CustomerDocNumber;
                    doc.LastError = null;
                    result.Approved++;

                    await _auditLogService.LogAsync("FISCAL VENTA Creada (cola)", $"Número: {doc.SaleId}");
                }
                catch (Exception ex)
                {
                    doc.LastError = ex.Message;
                    result.StillPending++;
                }
                finally
                {
                    doc.AttemptCount++;
                    doc.LastAttemptAt = DateTime.Now;
                    _fiscalDocRepository.Update(doc);
                }

                // Guardamos después de cada comprobante: si el proceso se corta a mitad de la
                // cola, lo que ya se facturó no se vuelve a pedir en el próximo intento.
                await _unitOfWork.SaveChangesAsync();
            }

            return result;
        }

        public async Task<IEnumerable<PendingFiscalDocumentDto>> GetPendingAsync()
        {
            var pendientes = (await _fiscalDocRepository.GetAsync(d => d.Status == FiscalDocumentStatus.Pending))
                .OrderBy(d => d.Id)
                .ToList();

            var resultado = new List<PendingFiscalDocumentDto>();
            foreach (var doc in pendientes)
            {
                var sale = await _saleRepository.GetByIdAsync(doc.SaleId);

                resultado.Add(new PendingFiscalDocumentDto
                {
                    FiscalDocumentId = doc.Id,
                    SaleId = doc.SaleId,
                    SaleDate = sale?.DateAndTime ?? doc.CreatedAt,
                    Total = sale?.Total ?? 0,
                    AttemptCount = doc.AttemptCount,
                    LastAttemptAt = doc.LastAttemptAt,
                    LastError = doc.LastError
                });
            }

            return resultado;
        }
    }
}
