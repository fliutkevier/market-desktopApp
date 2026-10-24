using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProviderMovements.Dtos
{
    public class UpdateProviderMovementDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Total { get; set; }
        public string? SourceFilePath { get; set; }
        public string? ProviderCuit { get; set; }

        public bool IsActive { get; set; }
    }
}
