using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProviderMovements.Dtos
{
    public class CreateProviderMovementDto
    {
        [Required(ErrorMessage = "Debe seleccionar un proveedor")]
        public string ProviderCuit { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
        public decimal Total { get; set; }

        public string? SourceFilePath { get; set; }
    }
}
