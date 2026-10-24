using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AuditLogs.Dtos
{
    public class AuditLogGridDto
    {
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
    }
}
