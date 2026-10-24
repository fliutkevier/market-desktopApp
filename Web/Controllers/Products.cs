using Application.Products.Dtos;
using Application.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Products : ControllerBase
    {
        // Constante con el código exacto de tu producto comodín
        private const string CODIGO_VARIOS = "VARIOS";
        private readonly IProductService _productService;

        // servicio de winforms
        public Products(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products/779123456789
        // El celular escanea y llama a este método para ver si el producto existe
        [HttpGet("{code}")]
        public async Task<IActionResult> GetProduct(string code)
        {
            // 2. BLOQUEO BÚSQUEDA DIRECTA: Si intentan escanear o buscar "VARIOS" a mano, fingimos que no existe
            if (code == CODIGO_VARIOS)
                return NotFound();

            var product = await _productService.GetProductForEditAsync(code);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // PUT: api/products/779123456789
        // El celular modifica el stock o el precio y manda la actualización
        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateProduct(string code, [FromBody] UpdateProductDto dto)
        {
            if (code != dto.Code)
                return BadRequest("El código no coincide");

            try
            {
                await _productService.UpdateProductAsync(dto);
                return NoContent(); // 204: Todo salió bien, no hay nada más que decir
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Le manda el error de validación al celular
            }
        }

        //POST: api/products
        // El celular escanea un nuevo producto y lo manda para agregarlo a la base de datos
        [HttpPost("{code}")]
        public async Task<IActionResult> NewProduct(string code, [FromBody] CreateProductDto dto)
        {
            var productExists = await _productService.GetProductForEditAsync(code);
            if(productExists != null)
            {
                if (productExists.Code == code)
                    return BadRequest($"El producto ya existe {productExists.Name}");
            }

            try
            {
                await _productService.CreateProductAsync(dto);
                return NoContent(); // 204: Todo salió bien, no hay nada más que decir
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Le manda el error de validación al celular
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllForGridAsync();

            // 1. FILTRO: Quitamos "VARIOS" de la lista para que no viaje al celular
            var filteredProducts = products.Where(p => p.Code != CODIGO_VARIOS);

            return Ok(filteredProducts);
        }
    }
}
