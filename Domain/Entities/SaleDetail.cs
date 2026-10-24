using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SaleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public int SaleId { get; set; }
        public string ProductCode { get; set; }

        [ForeignKey("SaleId")]
        public virtual Sale Sale { get; set; }
        [ForeignKey("ProductCode")]
        public virtual Product Product { get; set; }
    }
}
