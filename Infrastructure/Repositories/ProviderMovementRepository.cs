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
    public class ProviderMovementRepository : SqlRepository<ProviderMovement>, IProviderMovementRepository
    {
        public ProviderMovementRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProviderMovement>> GetAllWithProviderAsync(DateOnly? from = null, DateOnly? to = null, string? providerCuit = null)
        {
            var query = _dbSet.Include(m => m.Provider).AsQueryable();

            if (from != null)
            {
                var f = from.Value; // Si la entidad usa DateOnly
                query = query.Where(m => m.Date >= f);
            }

            if (to != null)
            {
                var t = to.Value;
                query = query.Where(m => m.Date <= t);
            }

            if (!string.IsNullOrEmpty(providerCuit))
            {
                query = query.Where(m => m.ProviderCUIT == providerCuit);
            }

            return await query.OrderByDescending(m => m.Date).ToListAsync();
        }
    }
}
