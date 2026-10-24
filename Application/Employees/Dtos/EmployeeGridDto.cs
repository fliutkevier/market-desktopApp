using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Dtos
{
    public class EmployeeGridDto
    {
        public string Dni { get; set; } = string.Empty;
        public string Cuit { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime DateHired { get; set; }
        public string? LinkedUser { get; set; }
    }
}
