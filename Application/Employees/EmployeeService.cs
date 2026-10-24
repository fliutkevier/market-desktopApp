using Application.AuditLogs.Interfaces;
using Application.Employees.Dtos;
using Application.Employees.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditLogService _auditLogService;

        public EmployeeService(
            IEmployeeRepository repository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IAuditLogService auditLogService)
        {
            _employeeRepository = repository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _auditLogService = auditLogService;
        }

        public async Task<IEnumerable<EmployeeLookupDto>> GetEmployeesWithoutUserAsync()
        {
            // Filtro activos y que NO tengan usuario asignado
            var employees = await _employeeRepository.GetAsync(e => e.IsActive && e.Username == null);

            return employees.Select(e => new EmployeeLookupDto
            {
                Dni = e.Dni,
                DisplayName = $"{e.Name} {e.LastName} ({e.Dni})"
            });
        }

        public async Task CreateAsync(CreateEmployeeDto dto)
        {
            DtoValidator.Validate(dto);

            var exists = await _employeeRepository.GetByIdAsync(dto.Dni);
            if (exists != null) throw new InvalidOperationException($"Ya existe un empleado con el DNI '{dto.Dni}'.");

            var cuitExists = await _employeeRepository.GetByCuitAsync(dto.Cuit);
            if (cuitExists != null) throw new InvalidOperationException($"Ya existe un empleado con el CUIT '{dto.Cuit}'.");

            if (!string.IsNullOrEmpty(dto.Email))
            {
                var emailExists = await _employeeRepository.GetByEmailAsync(dto.Email);
                if (emailExists != null) throw new InvalidOperationException($"El email '{dto.Email}' ya está asignado.");
            }

            //Creación de Usuario (Si se solicitó)
            if (dto.CreateUser)
            {
                if (string.IsNullOrWhiteSpace(dto.Username))
                    throw new InvalidOperationException("Debe especificar un nombre de usuario.");
                if (string.IsNullOrWhiteSpace(dto.Password))
                    throw new InvalidOperationException("Debe especificar una contraseña.");
                if (string.IsNullOrWhiteSpace(dto.UserRole))
                    throw new InvalidOperationException("Debe especificar un rol.");

                var userExists = await _userRepository.GetByUsernameAsync(dto.Username);
                if (userExists != null)
                {
                    throw new InvalidOperationException($"El nombre de usuario '{dto.Username}' ya está en uso.");
                }

                var newUser = new User
                {
                    Username = dto.Username,
                    Password = dto.Password,
                    Role = dto.UserRole[0],
                    IsActive = true
                };

                await _userRepository.AddAsync(newUser);
            }

            //Crear Empleado
            var entity = new Employee
            {
                Dni = dto.Dni,
                CUIT = dto.Cuit,
                Name = dto.Name,
                LastName = dto.LastName,
                BirthDay = dto.BirthDay,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                DateHired = dto.DateHired,
                Username = dto.Username,
                IsActive = true
            };

            await _employeeRepository.AddAsync(entity);
            await _auditLogService.LogAsync("EMPLEADO Creado", $"DNI: {entity.Dni}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeGridDto>> GetAllForGridAsync()
        {
            var employees = await _employeeRepository.GetAsync(x => x.IsActive);

            return employees.Select(x => new EmployeeGridDto
            {
                Dni = x.Dni,
                Cuit = x.CUIT,
                FullName = $"{x.LastName}, {x.Name}",
                Phone = x.Phone,
                Email = x.Email,
                DateHired = x.DateHired,
                LinkedUser = string.IsNullOrEmpty(x.Username) ? "No" : x.Username
            });
        }

        public async Task<EmployeeDetailDto?> GetForEditAsync(string dni)
        {
            var employee = await _employeeRepository.GetByIdAsync(dni);
            if (employee == null) return null;

            return new EmployeeDetailDto
            {
                Dni = employee.Dni,
                Cuit = employee.CUIT,
                Name = employee.Name,
                LastName = employee.LastName,
                BirthDay = employee.BirthDay,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Address,
                DateHired = employee.DateHired,
                IsActive = employee.IsActive,
                Username = employee.Username
            };
        }

        public async Task SoftDeleteAsync(string dni)
        {
            var employee = await _employeeRepository.GetByIdAsync(dni);
            if (employee == null)
            {
                throw new InvalidOperationException($"No se encontró el empleado con DNI {dni}.");
            }

            employee.IsActive = false;

            _employeeRepository.Update(employee);
            await _auditLogService.LogAsync("EMPLEADO Dado de baja", $"DNI: {dni}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateEmployeeDto dto)
        {
            DtoValidator.Validate(dto);

            var employee = await _employeeRepository.GetByIdAsync(dto.Dni);
            if (employee == null)
            {
                throw new InvalidOperationException($"No se encontró el empleado con DNI {dto.Dni}.");
            }

            var cuitExists = await _employeeRepository.GetByCuitAsync(dto.Cuit);
            if (cuitExists != null && cuitExists.Dni != dto.Dni)
            {
                throw new InvalidOperationException($"El CUIT '{dto.Cuit}' ya está usado por otro empleado.");
            }

            if (!string.IsNullOrEmpty(dto.Email))
            {
                var emailExists = await _employeeRepository.GetByEmailAsync(dto.Email);
                if (emailExists != null && emailExists.Dni != dto.Dni)
                {
                    throw new InvalidOperationException($"El email '{dto.Email}' ya está usado por otro empleado.");
                }
            }

            employee.CUIT = dto.Cuit;
            employee.Name = dto.Name;
            employee.LastName = dto.LastName;
            employee.BirthDay = dto.BirthDay;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.Address = dto.Address;
            employee.Username = dto.Username;
            employee.IsActive = dto.IsActive;

            _employeeRepository.Update(employee);
            await _auditLogService.LogAsync("EMPLEADO Modificado", $"DNI: {dto.Dni}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeGridDto>> GetDeletedAsync()
        {
            var deletedEmployees = await _employeeRepository.GetAsync(p => !p.IsActive);

            return deletedEmployees.Select(e => new EmployeeGridDto
            {
                Dni = e.Dni,
                Cuit = e.CUIT,
                FullName = $"{e.LastName}, {e.Name}",
                Phone = e.Phone,
                Email = e.Email,
                DateHired = e.DateHired,
                LinkedUser = string.IsNullOrEmpty(e.Username) ? "No" : e.Username
            });
        }

        public async Task RestoreAsync(string dni)
        {
            var employee = await _employeeRepository.GetByIdAsync(dni);

            if (employee == null) throw new InvalidOperationException("Empleado no encontrado.");

            employee.IsActive = true;

            _employeeRepository.Update(employee);
            await _auditLogService.LogAsync("EMPLEADO Recuperado", $"DNI: {employee.Dni}");
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
