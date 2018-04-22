using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("Exports")]
    public class Export
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExportId { get; set; }
        public string Code { get; set; }
        [StringLength(100)]
        public string ReferenceCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DayCreatedVoucher { get; set; }
        [StringLength(100)]
        public string Reason { get; set; }
        [StringLength(100)]
        public string Note { get; set; }
        public decimal Debt { get; set; }
        public decimal Payment { get; set; }
        public decimal Total { get; set; }
        public int SupplierId { get; set; }
        [StringLength(20)]
        public string SupplierCode { get; set; }
        [StringLength(200)]
        public string SupplierName { get; set; }
        public int UserId { get; set; }
        public bool Censorship { get; set; }
        public string WarehouseCode { get; set; }
    }
}
