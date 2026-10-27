using Microsoft.AspNetCore.Mvc;
using Web.Dtos;
using Web.Services;

namespace Web.Controllers
{
    // GET: /health
    // Sin PIN (ver PinMiddleware). El celular lo usa para saber si la PC responde antes de sincronizar.
    [ApiController]
    [Route("[controller]")]
    public class Health : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { Status = "ok", ServerTime = DateTime.Now });
    }

    // GET: /catalog
    // Productos, medios de pago y cajas, completos. El celular reemplaza su copia con esto.
    [ApiController]
    [Route("[controller]")]
    public class Catalog : ControllerBase
    {
        private readonly MobileSyncService _syncService;

        public Catalog(MobileSyncService syncService)
        {
            _syncService = syncService;
        }

        [HttpGet]
        public async Task<ActionResult<CatalogDto>> Get() => Ok(await _syncService.GetCatalogAsync());
    }

    [ApiController]
    [Route("[controller]")]
    public class Sales : ControllerBase
    {
        private readonly MobileSyncService _syncService;

        public Sales(MobileSyncService syncService)
        {
            _syncService = syncService;
        }

        // GET: /sales?afterId=120&take=1000
        // Ventas nuevas (Id > afterId) con sus ítems, de a páginas.
        [HttpGet]
        public async Task<ActionResult<SalesPageDto>> Get([FromQuery] int afterId = 0, [FromQuery] int take = 1000)
            => Ok(await _syncService.GetSalesAsync(afterId, take));

        // POST: /sales/fiscal-status   body: [12, 15, 40]
        // Es POST para no armar una URL gigante si hay muchas pendientes; no modifica nada.
        [HttpPost("fiscal-status")]
        public async Task<ActionResult<List<FiscalStatusDto>>> FiscalStatus([FromBody] List<int> saleIds)
        {
            if (saleIds.Count > MobileSyncService.MaxPageSize)
                return BadRequest($"Se pueden consultar hasta {MobileSyncService.MaxPageSize} ventas por vez.");

            return Ok(await _syncService.GetFiscalStatusAsync(saleIds));
        }
    }
}
