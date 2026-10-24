using Application.Sessions.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sessions.Interfaces
{
    public interface ISessionService
    {
        Task<bool> HasOpenSessionAsync(string username);
        Task<SessionStatusDto?> GetCurrentSessionAsync(string username);
        Task OpenSessionAsync(string username);
        Task CloseSessionAsync(string username);
        Task<IEnumerable<SessionGridDto>> GetAllForGridAsync(DateOnly? fromDate = null, DateOnly? toDate = null);
    }
}
