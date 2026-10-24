using Application.AuditLogs.Interfaces;
using Application.Providers.Dtos;
using Application.Providers.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Providers
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditLogService _auditLogService;

        public ProviderService(IProviderRepository repository, IUnitOfWork unitOfWork, IAuditLogService auditLogService)
        {
            _providerRepository = repository;
            _unitOfWork = unitOfWork;
            _auditLogService = auditLogService;
        }

        public async Task CreateAsync(CreateProviderDto dto)
        {
            DtoValidator.Validate(dto);

            var exists = await _providerRepository.GetByIdAsync(dto.Cuit);
            if (exists != null)
            {
                throw new InvalidOperationException($"Ya existe un proveedor con el CUIT '{dto.Cuit}'.");
            }

            var nameExists = await _providerRepository.GetByNameAsync(dto.Name);
            if (nameExists != null)
            {
                throw new InvalidOperationException($"Ya existe un proveedor con el nombre '{dto.Name}'.");
            }

            var entity = new Provider
            {
                CUIT = dto.Cuit,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                DateAdded = DateTime.Now,
                IsActive = true
            };

            await _providerRepository.AddAsync(entity);
            await _auditLogService.LogAsync("PROVEEDOR Creado", $"CUIT: {entity.CUIT}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProviderGridDto>> GetAllForGridAsync()
        {
            var providers = await _providerRepository.GetAsync(x => x.IsActive);

            return providers.Select(x => new ProviderGridDto
            {
                Cuit = x.CUIT,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                DateAdded = x.DateAdded
            });
        }

        public async Task<ProviderDetailDto?> GetForEditAsync(string cuit)
        {
            var provider = await _providerRepository.GetByIdAsync(cuit);
            if (provider == null) return null;

            return new ProviderDetailDto
            {
                Cuit = provider.CUIT,
                Name = provider.Name,
                Email = provider.Email,
                Phone = provider.Phone,
                Address = provider.Address,
                DateAdded = provider.DateAdded,
                IsActive = provider.IsActive
            };
        }

        public async Task SoftDeleteAsync(string cuit)
        {
            var provider = await _providerRepository.GetByIdAsync(cuit);
            if (provider == null)
            {
                throw new InvalidOperationException($"No se encontró el proveedor con CUIT {cuit}.");
            }

            provider.IsActive = false;

            _providerRepository.Update(provider);
            await _auditLogService.LogAsync("PROVEEDOR Dado de baja", $"CUIT: {provider.CUIT}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateProviderDto dto)
        {
            DtoValidator.Validate(dto);

            var provider = await _providerRepository.GetByIdAsync(dto.Cuit);
            if (provider == null)
            {
                throw new InvalidOperationException($"No se encontró el proveedor con CUIT {dto.Cuit}.");
            }

            var nameExists = await _providerRepository.GetByNameAsync(dto.Name);
            if (nameExists != null && nameExists.CUIT != dto.Cuit)
            {
                throw new InvalidOperationException($"Ya existe otro proveedor con el nombre '{dto.Name}'.");
            }

            provider.Name = dto.Name;
            provider.Email = dto.Email;
            provider.Phone = dto.Phone;
            provider.Address = dto.Address;
            provider.IsActive = dto.IsActive;

            _providerRepository.Update(provider);
            await _auditLogService.LogAsync("PROVEEDOR Modificado", $"CUIT: {provider.CUIT}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProviderGridDto>> GetDeletedAsync()
        {
            var providers = await _providerRepository.GetAsync(pm => !pm.IsActive);

            return providers.Select(p => new ProviderGridDto
            {
                Cuit = p.CUIT,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                DateAdded = p.DateAdded
            });
        }

        public async Task RestoreAsync(string cuit)
        {
            var p = await _providerRepository.GetByIdAsync(cuit);

            if (p == null) throw new InvalidOperationException("Proveedor no encontrado.");

            p.IsActive = true;

            _providerRepository.Update(p);
            await _auditLogService.LogAsync("PROVEEDOR Restaurado", $"CUIT: {p.CUIT}");
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
