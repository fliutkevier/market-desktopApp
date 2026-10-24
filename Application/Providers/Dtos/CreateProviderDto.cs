using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Providers.Dtos
{
    public class CreateProviderDto
    {
        [Required(ErrorMessage = "El CUIT es obligatorio")]
        public string Cuit { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;


        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string? Email { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
