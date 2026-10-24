using Application.PaymentMethods.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PaymentMethods.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodGridDto>> GetAllForGridAsync();
        Task CreateAsync(CreatePaymentMethodDto dto);
        Task<PaymentMethodDetailDto?> GetForEditAsync(int id);
        Task UpdateAsync(UpdatePaymentMethodDto dto);
        Task SoftDeleteAsync(int id);
        Task<IEnumerable<PaymentMethodGridDto>> GetDeletedAsync();
        Task RestoreAsync(int id);
    }
}
