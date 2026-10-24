using Application.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeGridDto>> GetAllForGridAsync();
        Task CreateAsync(CreateEmployeeDto dto);
        Task<EmployeeDetailDto?> GetForEditAsync(string dni);
        Task UpdateAsync(UpdateEmployeeDto dto);
        Task SoftDeleteAsync(string dni);
        Task<IEnumerable<EmployeeLookupDto>> GetEmployeesWithoutUserAsync();
        Task<IEnumerable<EmployeeGridDto>> GetDeletedAsync();
        Task RestoreAsync(string dni);
    }
}
