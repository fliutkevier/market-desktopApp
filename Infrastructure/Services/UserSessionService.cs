using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserSessionService : IUserSessionService
    {
        public string? Username { get; private set; }
        public char? Role { get; private set; }

        public void StartSession(string username, char role)
        {
            Username = username;
            Role = role;
        }

        public void EndSession()
        {
            Username = null;
            Role = null;
        }
    }
}
