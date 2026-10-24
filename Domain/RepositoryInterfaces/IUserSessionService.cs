using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IUserSessionService
    {
        string? Username { get; }
        char? Role { get; }

        void StartSession(string username, char role);

        void EndSession();
    }
}
