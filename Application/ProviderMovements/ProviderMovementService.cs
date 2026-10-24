using Application.AuditLogs.Interfaces;
using Application.ProviderMovements.Dtos;
using Application.ProviderMovements.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProviderMovements
{
    public class ProviderMovementService : IProviderMovementService
    {
        private readonly IProviderMovementRepository _movementRepo;
        private readonly IProviderRepository _providerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditLogService _auditLogService;

        public ProviderMovementService(
            IProviderMovementRepository movementRepo,
            IProviderRepository providerRepo,
            IUnitOfWork unitOfWork,
            IAuditLogService auditLogService)
        {
            _movementRepo = movementRepo;
            _providerRepo = providerRepo;
            _unitOfWork = unitOfWork;
            _auditLogService = auditLogService;
        }

        public async Task CreateMovementAsync(CreateProviderMovementDto dto)
        {
            DtoValidator.Validate(dto);

            var provider = await _providerRepo.GetByIdAsync(dto.ProviderCuit);
            if (provider == null) throw new InvalidOperationException("Proveedor no encontrado.");

            // Guardar archivo
            string? savedPath = CopyFileToStorage(dto.SourceFilePath, dto.ProviderCuit);

            var movement = new ProviderMovement
            {
                ProviderCUIT = dto.ProviderCuit,
                Date = DateOnly.FromDateTime(dto.Date),
                Total = dto.Total,
                UrlPhoto = savedPath,
            };

            await _movementRepo.AddAsync(movement);
            await _auditLogService.LogAsync("COMPRA PROVEEDOR Creada", $"Proveedor: {movement.ProviderCUIT}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateMovementAsync(UpdateProviderMovementDto dto)
        {
            DtoValidator.Validate(dto);

            var movement = await _movementRepo.GetByIdAsync(dto.Id);
            if (movement == null) throw new InvalidOperationException("Movimiento no encontrado.");

            // Si viene un archivo nuevo, guarda y acutaliza la ruta.
            // Si es null/empty, ruta vieja.
            if (!string.IsNullOrEmpty(dto.SourceFilePath))
            {
                string? newPath = CopyFileToStorage(dto.SourceFilePath, movement.ProviderCUIT);
                movement.UrlPhoto = newPath;
            }

            movement.Date = DateOnly.FromDateTime(dto.Date);
            movement.Total = dto.Total;

            _movementRepo.Update(movement);
            await _auditLogService.LogAsync("COMPRA PROVEEDOR Modificada", $"Compra: {dto.Id}");
            await _unitOfWork.SaveChangesAsync();
        }
        /*
        public async Task SoftDeleteMovementAsync(int id)
        {
            var movement = await _movementRepo.GetByIdAsync(id);
            if (movement == null) throw new InvalidOperationException("Movimiento no encontrado.");

            movement.IsActive = false;

            _movementRepo.Update(movement);
            await _unitOfWork.SaveChangesAsync();
        }*/

        public async Task<IEnumerable<ProviderMovementGridDto>> GetHistoryAsync(DateOnly? from = null, DateOnly? to = null, string? providerCuit = null)
        {
            var list = await _movementRepo.GetAllWithProviderAsync(from, to, providerCuit);

            // Filtramos solo activos
            //list = list.Where(m => m.IsActive);

            return list.Select(m => new ProviderMovementGridDto
            {
                Id = m.Id,
                Date = m.Date,
                Total = m.Total,
                ProviderName = m.Provider?.Name ?? "Desconocido",
                ProviderCuit = m.ProviderCUIT,
                FilePath = m.UrlPhoto,
                HasFile = !string.IsNullOrEmpty(m.UrlPhoto) && File.Exists(m.UrlPhoto)
            });
        }

        public async Task<UpdateProviderMovementDto?> GetForEditAsync(int id)
        {
            var m = await _movementRepo.GetByIdAsync(id);
            if (m == null) return null;

            return new UpdateProviderMovementDto
            {
                Id = m.Id,
                Date = m.Date.ToDateTime(TimeOnly.MinValue),
                ProviderCuit = m.ProviderCUIT,
                Total = m.Total,
                SourceFilePath = null // No devolvemos la ruta local, solo servimos para que suban uno nuevo
            };
        }

        // ARCHIVOS
        private string? CopyFileToStorage(string? sourcePath, string cuit)
        {
            if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath)) return null;

            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string storageFolder = Path.Combine(baseDir, "FacturasProveedores");
                if (!Directory.Exists(storageFolder)) Directory.CreateDirectory(storageFolder);

                string ext = Path.GetExtension(sourcePath);
                string fileName = $"{cuit}_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString().Substring(0, 4)}{ext}";
                string destPath = Path.Combine(storageFolder, fileName);

                File.Copy(sourcePath, destPath);
                return destPath;
            }
            catch
            {
                // Loguear error si es necesario
                return null;
            }
        }

        public async Task<string?> GetFilePathAsync(int id)
        {
            var movement = await _movementRepo.GetByIdAsync(id);
            return movement?.UrlPhoto;
        }

        public async Task SoftDeleteAsync(int id)
        {
            var movement = await _movementRepo.GetByIdAsync(id);
            if (movement == null)
            {
                throw new InvalidOperationException($"No se encontró el el movimiento con ID {id}.");
            }

            movement.IsActive = false;

            _movementRepo.Update(movement);
            await _auditLogService.LogAsync("COMPRA PROVEEDOR Dada de baja", $"Proveedor: {movement.Id}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProviderMovementGridDto>> GetDeletedAsync()
        {
            var providerMovements = await _movementRepo.GetAsync(pm => !pm.IsActive);

            return providerMovements.Select(pm => new ProviderMovementGridDto
            {
                Id = pm.Id,
                Date = pm.Date,
                Total = pm.Total,
                ProviderCuit = pm.ProviderCUIT,
                FilePath = pm.UrlPhoto,
                HasFile = !string.IsNullOrEmpty(pm.UrlPhoto) && File.Exists(pm.UrlPhoto)
            });
        }
        public async Task RestoreAsync(int id)
        {
            var pm = await _movementRepo.GetByIdAsync(id);

            if (pm == null) throw new InvalidOperationException("Movimiento de Proveedor no encontrado.");

            pm.IsActive = true;

            _movementRepo.Update(pm);
            await _auditLogService.LogAsync("COMPRA PROVEEDOR Restaurado", $"Número: {pm.Id}");
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
