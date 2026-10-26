using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sales.Dtos
{
    public class SaleDetailViewDto
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty; 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        // Va después de UnitPrice: la grilla del detalle de venta genera las columnas en este orden.
        public decimal CostPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
