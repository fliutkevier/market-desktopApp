using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Dtos
{
    public class ProductLabelDto
    {
        public string ProductCode { get; set; } = string.Empty;
        public string PrintName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
