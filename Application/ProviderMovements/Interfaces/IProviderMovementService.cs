using Application.ProviderMovements.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProviderMovements.Interfaces
{
    public interface IProviderMovementService
    {
        Task CreateMovementAsync(CreateProviderMovementDto dto);
        Task<IEnumerable<ProviderMovementGridDto>> GetHistoryAsync(DateOnly? from = null, DateOnly? to = null, string? providerCuit = null);
        Task SoftDeleteAsync(int id);
        Task UpdateMovementAsync(UpdateProviderMovementDto dto);
        Task<UpdateProviderMovementDto?> GetForEditAsync(int id);
        Task<string?> GetFilePathAsync(int id);
        Task<IEnumerable<ProviderMovementGridDto>> GetDeletedAsync();
        Task RestoreAsync(int id);
    }
}
