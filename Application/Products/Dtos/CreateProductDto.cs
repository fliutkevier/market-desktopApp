using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Dtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "El código es obligatorio")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "El precio de venta no puede ser negativo")]
        public decimal SalePrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio de costo no puede ser negativo")]
        public decimal CostPrice { get; set; }

        public int InitialStock { get; set; }
    }
}
