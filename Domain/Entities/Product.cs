using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public DateTime LastStockUpdate { get; set; }
        public bool IsOpenPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
