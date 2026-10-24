using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sales.Dtos
{
    public class CreateSaleDto
    {
        [Required(ErrorMessage = "Debe seleccionar un método de pago")]
        public int PaymentMethodId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "La venta debe tener al menos un producto")]
        public List<SaleDetailDto> Items { get; set; } = new List<SaleDetailDto>();

        public bool IsFiscal { get; set; } // True = Facturar en AFIP
    }
}
