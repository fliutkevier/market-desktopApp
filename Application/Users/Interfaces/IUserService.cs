using Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserGridDto>> GetAllAsync();
        Task<bool> AnyUserAsync();
        Task CreateUserAsync(CreateUserDto dto);
        Task DeleteUserAsync(string username);
        Task ChangePasswordAsync(string username, string newPassword);
        Task<IEnumerable<UserGridDto>> GetDeletedAsync();
        Task RestoreAsync(string username);
    }
}
