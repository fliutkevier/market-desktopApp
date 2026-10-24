using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(string username, string password);
    }
}
