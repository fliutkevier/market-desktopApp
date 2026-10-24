using Application.AuditLogs.Interfaces;
using Application.PaymentMethods.Dtos;
using Application.PaymentMethods.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PaymentMethods
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditLogService _auditLogService;
        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, IUnitOfWork unitOfWork, IAuditLogService auditLogService)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _unitOfWork = unitOfWork;
            _auditLogService = auditLogService;
        }

        public async Task CreateAsync(CreatePaymentMethodDto dto)
        {
            DtoValidator.Validate(dto);

            var exists = await _paymentMethodRepository.GetByNameAsync(dto.Name);

            if (exists != null)
            {
                throw new InvalidOperationException($"El método de pago '{dto.Name}' ya existe.");
            }

            var paymentMethod = new PaymentMethod
            {
                Name = dto.Name,
                Recharge = dto.Recharge,
                Discount = dto.Discount,
                IsActive = true
            };

            await _paymentMethodRepository.AddAsync(paymentMethod);
            await _auditLogService.LogAsync("MÉTODO PAGO Creado", $"Nombre: {paymentMethod.Name}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentMethodGridDto>> GetAllForGridAsync()
        {
            var paymentMethods = await _paymentMethodRepository.GetAsync(pm => pm.IsActive);

            return paymentMethods.Select(pm => new PaymentMethodGridDto
            {
                Id = pm.Id,
                Name = pm.Name,
                Recharge = pm.Recharge,
                Discount = pm.Discount,
            });
        }

        public async Task<PaymentMethodDetailDto?> GetForEditAsync(int id)
        {
            var entity = await _paymentMethodRepository.GetByIdAsync(id);
            if (entity == null) return null;

            return new PaymentMethodDetailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Recharge = entity.Recharge,
                Discount = entity.Discount,
                IsActive = entity.IsActive
            };
        }

        public async Task SoftDeleteAsync(int id)
        {
            var pm = await _paymentMethodRepository.GetByIdAsync(id);
            if (pm == null)
            {
                throw new InvalidOperationException($"No se encontró el método de pago {pm.Name}.");
            }

            pm.IsActive = false;

            _paymentMethodRepository.Update(pm);
            await _auditLogService.LogAsync("MÉTODO PAGO Dado de baja", $"Nombre: {pm.Name}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdatePaymentMethodDto dto)
        {
            DtoValidator.Validate(dto);

            var pm = await _paymentMethodRepository.GetByIdAsync(dto.Id);
            if (pm == null)
            {
                throw new InvalidOperationException($"No se encontró el método de pago con ID {dto.Id}.");
            }

            var existingWithName = await _paymentMethodRepository.GetByNameAsync(dto.Name);

            if (existingWithName != null && existingWithName.Id != dto.Id)
            {
                throw new InvalidOperationException($"Ya existe otro método de pago con el nombre '{dto.Name}'.");
            }

            pm.Name = dto.Name;
            pm.Recharge = dto.Recharge;
            pm.Discount = dto.Discount;
            pm.IsActive = dto.IsActive;

            _paymentMethodRepository.Update(pm);
            await _auditLogService.LogAsync("MÉTODO PAGO Modificado", $"Nombre: {pm.Name}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentMethodGridDto>> GetDeletedAsync()
        {
            var paymentMethods = await _paymentMethodRepository.GetAsync(pm => !pm.IsActive);

            return paymentMethods.Select(pm => new PaymentMethodGridDto
            {
                Id = pm.Id,
                Name = pm.Name,
                Recharge = pm.Recharge,
                Discount = pm.Discount
            });
        }

        public async Task RestoreAsync(int id)
        {
            var pm = await _paymentMethodRepository.GetByIdAsync(id);

            if (pm == null) throw new InvalidOperationException("Método de Pago no encontrado.");

            pm.IsActive = true;

            _paymentMethodRepository.Update(pm);
            await _auditLogService.LogAsync("MÉTODO PAGO Restaurado", $"Nombre: {pm.Name}");
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
