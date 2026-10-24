using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sales.Dtos
{
    public class SaleResultDto
    {
        public int SaleId { get; set; }
        public FiscalDocument? FiscalData { get; set; }
    }
}
