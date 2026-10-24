using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ISaleRepository : IRepository<Sale>
    {
        Task<IEnumerable<Sale>> GetSalesWithDetailsAsync(DateOnly? fromDate = null, DateOnly? toDate = null);

        Task<IEnumerable<Sale>> GetSalesBySessionIdAsync(int sessionId);

        public Task<Sale?> GetSaleWithRelationsByIdAsync(int id);
    }
}
