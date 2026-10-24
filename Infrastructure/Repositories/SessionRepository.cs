using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SessionRepository : SqlRepository<Session>, ISessionRepository
    {
        public SessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Session?> GetOpenSessionByUserAsync(string username)
        {
            return await _dbSet
                .OrderByDescending(s => s.OpenedAt)
                .FirstOrDefaultAsync(s => s.Username == username && s.ClosedAt == null);
        }
    }
}
