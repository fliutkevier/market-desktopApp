using Application.Sales.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// Cada 20 minutos intenta facturar en ARCA las ventas que quedaron "Pending" (ARCA caído,
    /// timeout, etc.). También corre una vez al arrancar la app, por si quedaron pendientes de
    /// una sesión anterior.
    /// </summary>
    public class FiscalQueueWorker : BackgroundService
    {
        private static readonly TimeSpan Interval = TimeSpan.FromMinutes(20);
        private readonly IServiceScopeFactory _scopeFactory;

        public FiscalQueueWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Interval);

            await ProcesarPendientesAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                await ProcesarPendientesAsync(stoppingToken);
            }
        }

        private async Task ProcesarPendientesAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var fiscalQueueService = scope.ServiceProvider.GetRequiredService<IFiscalQueueService>();
                await fiscalQueueService.ProcessPendingAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // La app se está cerrando, no hay nada más que hacer.
            }
            catch
            {
                // Un error inesperado (ej. DB no disponible) no debe tumbar el worker:
                // se reintenta solo en el próximo ciclo. Los errores por venta ya quedan
                // registrados en FiscalDocument.LastError dentro de FiscalQueueService.
            }
        }
    }
}
