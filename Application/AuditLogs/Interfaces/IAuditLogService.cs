using Application.AuditLogs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AuditLogs.Interfaces
{
    public interface IAuditLogService
    {
        Task LogAsync(string action, string details);
        Task<IEnumerable<AuditLogGridDto>> GetLogsAsync(DateTime? from, DateTime? to, string? user);
    }
}
