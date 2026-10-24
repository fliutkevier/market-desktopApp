using Domain.RepositoryInterfaces;

namespace Web.Services
{
    public class ApiDummySessionService : IUserSessionService
    {
        public string Username => "AppMovil";

        public char? Role => 'M';

        public void StartSession(string username, char role)
        {
            
        }

        public void EndSession()
        {
            
        }
        /*
        public void Login(string username)
        {
            // No hace nada en la API (Por ahora no hay autenticación JWT)
        }

        public void Logout()
        {
            // No hace nada
        }*/
    }
}
