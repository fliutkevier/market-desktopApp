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
    public class SaleRepository : SqlRepository<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sale>> GetSalesWithDetailsAsync(DateOnly? fromDate = null, DateOnly? toDate = null)
        {
            var query = _dbSet
                .Include(s => s.PaymentMethod)
                .Include(s => s.Session)
                    .ThenInclude(sess => sess.User)
                .Include(s => s.SaleDetails)
                .AsQueryable();

            if (fromDate != null)
            {
                var fromDt = fromDate.Value.ToDateTime(TimeOnly.MinValue);
                query = query.Where(s => s.DateAndTime >= fromDt);
            }

            if (toDate != null)
            {
                var toDt = toDate.Value.ToDateTime(TimeOnly.MaxValue);
                query = query.Where(s => s.DateAndTime <= toDt);
            }

            return await query
                .OrderByDescending(s => s.DateAndTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesBySessionIdAsync(int sessionId)
        {
            return await _dbSet
                .Include(s => s.PaymentMethod)
                .Include(s => s.SaleDetails)
                .Include(s => s.Session) 
                    .ThenInclude(sess => sess.User)
                .Where(s => s.SessionId == sessionId)
                .OrderByDescending(s => s.DateAndTime)
                .ToListAsync();
        }

        public async Task<Sale?> GetSaleWithRelationsByIdAsync(int id)
        {
            return await _dbSet
                .Include(s => s.PaymentMethod)
                .Include(s => s.Session)
                    .ThenInclude(sess => sess.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
