using Application.AuditLogs.Interfaces;
using Application.Sessions.Dtos;
using Application.Sessions.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sessions
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditLogService _auditLogService;

        public SessionService(ISessionRepository sessionRepository, IUnitOfWork unitOfWork, IAuditLogService audit)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _auditLogService = audit;
        }

        public async Task CloseSessionAsync(string username)
        {
            var activeSession = await _sessionRepository.GetOpenSessionByUserAsync(username);

            if (activeSession == null)
            {
                throw new InvalidOperationException($"El usuario {username} no tiene ninguna caja abierta para cerrar.");
            }

            activeSession.ClosedAt = DateTime.Now;

            TimeSpan duracion = activeSession.ClosedAt.Value - activeSession.OpenedAt;

            activeSession.TotalDuration = duracion.TotalHours;

            _sessionRepository.Update(activeSession);
            await _auditLogService.LogAsync("CAJA Cerrada", $"Número de caja: {activeSession.Id}");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SessionGridDto>> GetAllForGridAsync(DateOnly? fromDate = null, DateOnly? toDate = null)
        {
            Expression<Func<Session, bool>>? filter = null;

            if (fromDate != null && toDate != null)
            {
                filter = s => s.Date >= fromDate && s.Date <= toDate;
            }
            else if (fromDate != null)
            {
                filter = s => s.Date >= fromDate;
            }

            var sessions = await _sessionRepository.GetAsync(filter);

            return sessions
                .OrderByDescending(s => s.OpenedAt)
                .Select(s => new SessionGridDto
                {
                    Id = s.Id,
                    Username = s.Username,
                    Date = s.Date,
                    OpenedAt = s.OpenedAt,
                    ClosedAt = s.ClosedAt,
                    Duration = s.TotalDuration.HasValue
                           ? FormatDuration(s.TotalDuration.Value)
                           : "-",
                    Total = s.Total
                });
        }

        private string FormatDuration(double totalHours)
        {
            TimeSpan ts = TimeSpan.FromHours(totalHours);
            int horas = (int)ts.TotalHours;
            return $"{horas}hs {ts.Minutes}m";
        }

        public async Task<SessionStatusDto?> GetCurrentSessionAsync(string username)
        {
            var session = await _sessionRepository.GetOpenSessionByUserAsync(username);

            if (session == null) return null;

            return new SessionStatusDto
            {
                Id = session.Id,
                OpenedAt = session.OpenedAt,
                Date = session.Date,
                Total = session.Total,
                IsOpen = session.ClosedAt == null
            };
        }

        public async Task<bool> HasOpenSessionAsync(string username)
        {
            var session = await _sessionRepository.GetOpenSessionByUserAsync(username);
            return session != null;
        }

        public async Task OpenSessionAsync(string username)
        {
            var activeSession = await _sessionRepository.GetOpenSessionByUserAsync(username);
            if (activeSession != null)
            {
                throw new InvalidOperationException($"El usuario {username} ya tiene una caja abierta iniciada el {activeSession.OpenedAt}.");
            }

            var newSession = new Session
            {
                Username = username,
                OpenedAt = DateTime.Now,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Total = 0,
                ClosedAt = null
            };

            await _sessionRepository.AddAsync(newSession);
            await _auditLogService.LogAsync("CAJA abierta", $"Numero: {newSession.Id}");
            await _unitOfWork.SaveChangesAsync();
        }

        async Task<SessionStatusDto?> ISessionService.GetCurrentSessionAsync(string username)
        {
            var session = await _sessionRepository.GetOpenSessionByUserAsync(username);

            if (session == null) return null;

            return new SessionStatusDto
            {
                Id = session.Id,
                OpenedAt = session.OpenedAt,
                Date = session.Date,
                Total = session.Total,
                IsOpen = session.ClosedAt == null
            };
        }
    }
}
