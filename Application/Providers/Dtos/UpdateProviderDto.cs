using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Providers.Dtos
{
    public class UpdateProviderDto
    {
        [Required(ErrorMessage = "El CUIT es obligatorio para identificar al proveedor")]
        public string Cuit { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }

        public bool IsActive { get; set; }
    }
}
