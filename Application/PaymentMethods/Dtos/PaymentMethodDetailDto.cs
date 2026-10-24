using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PaymentMethods.Dtos
{
    public class PaymentMethodDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Recharge { get; set; }
        public decimal Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
