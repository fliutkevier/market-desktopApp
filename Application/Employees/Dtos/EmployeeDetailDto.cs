using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Dtos
{
    public class EmployeeDetailDto
    {
        public string Dni { get; set; } = string.Empty;
        public string Cuit { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly BirthDay { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime DateHired { get; set; }
        public bool IsActive { get; set; }

        public string? Username { get; set; }
    }
}
