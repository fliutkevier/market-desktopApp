using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        [Key]
        public string Dni { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateOnly BirthDay { get; set; }
        public DateTime DateHired { get; set; }
        public bool IsActive { get; set; }
        
        public string? Username { get; set; }

        [ForeignKey("Username")]
        public virtual User? User { get; set; }
    }
}
