using Application.AuditLogs.Interfaces;
using Application.Users.Dtos;
using Application.Users.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditLogService _auditLogService;

        public UserService(
            IUserRepository userRepository,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            IAuditLogService auditLogService)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _auditLogService = auditLogService;
        }

        public async Task<IEnumerable<UserGridDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAsync(u => u.IsActive);

            return users.Select(u => new UserGridDto
            {
                Username = u.Username,
                Role = u.Role == 'A' ? "Administrador" : "Empleado",
            });
        }

        /// <summary>
        /// Cuenta también los dados de baja: si existe alguno, el sistema ya fue configurado y se
        /// recupera desde la papelera, no creando un administrador nuevo desde el arranque.
        /// </summary>
        public async Task<bool> AnyUserAsync()
        {
            var users = await _userRepository.GetAsync(u => true);
            return users.Any();
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            DtoValidator.Validate(dto);

            // Validar que no exista el username
            var existing = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existing != null)
            {
                throw new InvalidOperationException($"El usuario '{dto.Username}' ya existe.");
            }

            char role = 'A'; // Por defecto Admin

            //Si se seleccionó un empleado
            if (!string.IsNullOrEmpty(dto.EmployeeDni))
            {
                var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeDni);
                if (employee == null)
                {
                    throw new InvalidOperationException("El empleado seleccionado no existe.");
                }
                if (!string.IsNullOrEmpty(employee.Username))
                {
                    throw new InvalidOperationException($"El empleado {employee.Name} ya tiene un usuario asignado ({employee.Username}).");
                }

                //Rol de empleado
                role = 'E';

                employee.Username = dto.Username;
                _employeeRepository.Update(employee);
            }

            //Crear Usuario
            var newUser = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Role = role,
                IsActive = true
            };

            await _userRepository.AddAsync(newUser);
            await _auditLogService.LogAsync("USUARIO Creado", $"Usuario: {dto.Username}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string username)
        {
            var user = await _userRepository.GetByIdAsync(username);
            if (user == null) throw new InvalidOperationException("Usuario no encontrado.");

            user.IsActive = false;

            _userRepository.Update(user);
            await _auditLogService.LogAsync("Usuario dado de baja", $"Usuario: {username}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(string username, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(username);
            if (user == null) throw new InvalidOperationException("Usuario no encontrado.");

            user.Password = newPassword;

            _userRepository.Update(user);
            await _auditLogService.LogAsync("USUARIO Contraseña Modificada", $"Usuario: {username}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserGridDto>> GetDeletedAsync()
        {
            var users = await _userRepository.GetAsync(u => !u.IsActive);

            return users.Select(u => new UserGridDto
            {
                Username = u.Username,
                Role = u.Role == 'A' ? "Administrador" : "Empleado",
            });
        }

        public async Task RestoreAsync(string username)
        {
            var u = await _userRepository.GetByIdAsync(username);

            if (u == null) throw new InvalidOperationException("Usuario no encontrado.");

            u.IsActive = true;

            _userRepository.Update(u);
            await _auditLogService.LogAsync("USUARIO Restaurado", $"Usuario: {u.Username}");
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
