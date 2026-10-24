using Application.Users.Interfaces;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionService _sessionService;

        public AuthService(IUserRepository userRepository, IUserSessionService sessionService)
        {
            _userRepository = userRepository;
            _sessionService = sessionService;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
            {
                return false;
            }

            bool isValidPassword = (user.Password == password);

            if (!isValidPassword)
            {
                return false;
            }

            _sessionService.StartSession(user.Username, user.Role);

            return true;
        }
    }
}
