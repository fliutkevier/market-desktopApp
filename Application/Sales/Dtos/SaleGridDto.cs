using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sales.Dtos
{
    public class SaleGridDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
        // null = no es venta fiscal. Approved = facturada. Pending = en cola esperando a ARCA.
        public FiscalDocumentStatus? FiscalStatus { get; set; }
        public bool Fiscal => FiscalStatus == FiscalDocumentStatus.Approved;
        public string FiscalStatusText => FiscalStatus switch
        {
            FiscalDocumentStatus.Approved => "Facturado",
            FiscalDocumentStatus.Pending => "Pendiente ARCA",
            _ => ""
        };
        public int ItemCount { get; set; }
        public string User { get; set; } = string.Empty;
    }
}
