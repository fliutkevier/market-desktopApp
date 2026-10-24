using Application.AuditLogs.Dtos;
using Application.AuditLogs.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.AuditLogs
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repository;
        private readonly IUserSessionService _session; //user

        public AuditLogService(IAuditLogRepository repository, IUserSessionService session)
        {
            _repository = repository;
            _session = session;
        }

        public async Task LogAsync(string action, string details)
        {
            var log = new AuditLog
            {
                DateTime = DateTime.Now,
                Username = _session.Username ?? "Sistema",
                Action = action,
                Details = details
            };

            await _repository.AddAsync(log);
        }

        public async Task<IEnumerable<AuditLogGridDto>> GetLogsAsync(DateTime? from, DateTime? to, string? user)
        {
            //var filter = PredicateBuilder.New<AuditLog>(true)
            var filter = PredicateBuilder.New<AuditLog>(true);

            //filtro
            if (from.HasValue) filter = filter.And(x => x.DateTime >= from.Value);
            if (to.HasValue) filter = filter.And(x => x.DateTime <= to.Value);
            if (!string.IsNullOrEmpty(user)) filter = filter.And(x => x.Username == user);

            var logs = await _repository.GetAsync(filter);

            return logs.OrderByDescending(x => x.DateTime)
                        .Select(x => new AuditLogGridDto
                        {
                            Date = x.DateTime,
                            Username = x.Username,
                            Action = x.Action,
                            Details = x.Details
                        });
        }
    }
}
