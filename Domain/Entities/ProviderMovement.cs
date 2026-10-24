using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProviderMovement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateOnly Date { get; set; }
        public string UrlPhoto { get; set; }
        public bool IsActive { get; set; }
        public string ProviderCUIT { get; set; }

        [ForeignKey("ProviderCUIT")]
        public virtual Provider Provider { get; set; }

    }
}