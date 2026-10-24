using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProviderMovements.Dtos
{
    public class ProviderMovementGridDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string ProviderCuit { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public bool HasFile { get; set; }
        public string? FilePath { get; set; }
    }
}
