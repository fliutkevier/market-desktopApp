using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum FiscalDocumentStatus
    {
        Pending = 0,
        Approved = 1
    }

    public class FiscalDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SaleId { get; set; }
        [ForeignKey("SaleId")]
        public virtual Sale Sale { get; set; }

        public FiscalDocumentStatus Status { get; set; } = FiscalDocumentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastAttemptAt { get; set; }
        public int AttemptCount { get; set; }
        public string? LastError { get; set; }

        //AFIP/ARCA DATA

        /// <summary>
        /// CUIT con el que se emitió realmente este comprobante. Se guarda por comprobante y no
        /// se lee de la configuración: si el negocio cambia de contribuyente, las facturas viejas
        /// se tienen que poder reimprimir con el CUIT con el que se emitieron.
        /// </summary>
        public string? IssuerCuit { get; set; }

        public int PointOfSale { get; set; } // Punto de Venta
        public int InvoiceType { get; set; } //(1=A, 6=B, etc.)
        public long? InvoiceNumber { get; set; } // Número (ej: 12345), null hasta que se aprueba

        public string? CAE { get; set; }
        public DateTime? CAEExpirationDate { get; set; }

        // Datos del cliente fiscal (si aplica)
        public string? CustomerDocType { get; set; }
        public string? CustomerDocNumber { get; set; }
    }
}
