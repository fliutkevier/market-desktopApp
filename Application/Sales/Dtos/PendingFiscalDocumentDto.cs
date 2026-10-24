using System;

namespace Application.Sales.Dtos
{
    public class PendingFiscalDocumentDto
    {
        public int FiscalDocumentId { get; set; }
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
        public int AttemptCount { get; set; }
        public DateTime? LastAttemptAt { get; set; }
        public string? LastError { get; set; }
    }
}
