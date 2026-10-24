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
    public class PaymentMethodRepository : SqlRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PaymentMethod?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(pm => pm.Name == name);
        }
    }
}
