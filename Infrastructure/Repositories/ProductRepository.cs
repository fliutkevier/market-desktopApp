using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : SqlRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _dbSet.FindAsync(code);
        }
    }
}
