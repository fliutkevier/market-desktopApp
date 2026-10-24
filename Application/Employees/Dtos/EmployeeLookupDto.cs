using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Dtos
{
    public class EmployeeLookupDto
    {
        public string Dni { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
