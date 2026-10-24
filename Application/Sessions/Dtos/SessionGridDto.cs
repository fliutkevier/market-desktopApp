using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sessions.Dtos
{
    public class SessionGridDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Duration { get; set; }
        public decimal Total { get; set; }
    }
}
