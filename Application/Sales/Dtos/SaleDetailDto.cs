using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sales.Dtos
{
    public class SaleDetailDto
    {
        [Required(ErrorMessage = "El código del producto es obligatorio")]
        public string ProductCode { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
