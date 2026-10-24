using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sessions.Dtos
{
    public class SessionStatusDto
    {
        public int Id { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateOnly Date { get; set; }
        public decimal Total { get; set; }
        public bool IsOpen { get; set; }
    }
}
