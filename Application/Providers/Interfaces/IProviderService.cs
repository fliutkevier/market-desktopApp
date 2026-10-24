using Application.PaymentMethods.Dtos;
using Application.Providers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Providers.Interfaces
{
    public interface IProviderService
    {
        Task<IEnumerable<ProviderGridDto>> GetAllForGridAsync();
        Task CreateAsync(CreateProviderDto dto);
        Task<ProviderDetailDto?> GetForEditAsync(string cuit);
        Task UpdateAsync(UpdateProviderDto dto);
        Task SoftDeleteAsync(string cuit);
        Task<IEnumerable<ProviderGridDto>> GetDeletedAsync();
        Task RestoreAsync(string cuit);
    }
}
