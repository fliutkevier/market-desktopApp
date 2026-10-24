using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IProviderMovementRepository : IRepository<ProviderMovement>
    {
        Task<IEnumerable<ProviderMovement>> GetAllWithProviderAsync(DateOnly? from = null, DateOnly? to = null, string? providerCuit = null);
    }
}
