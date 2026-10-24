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
    public class SaleDetailRepository : SqlRepository<SaleDetail>, ISaleDetailRepository
    {
        public SaleDetailRepository(AppDbContext context) : base(context)
        {
        }
    }
}
