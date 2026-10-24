using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProviderMovements.Dtos
{
    public class ProviderMovementDto
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string ProviderCuit { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string UrlPhoto { get; set; }
    }
}
