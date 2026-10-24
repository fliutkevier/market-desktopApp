using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PaymentMethods.Dtos
{
    public class CreatePaymentMethodDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "El recargo debe estar entre 0 y 100%")]
        public decimal Recharge { get; set; }

        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100%")]
        public decimal Discount { get; set; }
    }
}
