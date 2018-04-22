using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("ExportDetails")]
    public class ExportDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExportDetailId { get; set; }
        public int ProductId { get; set; }
        public int ExportId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int WareHouseId { get; set; }
        [StringLength(10)]
        public string ColorCode { get; set; }
        [StringLength(10)]
        public string SizeCode { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string Note { get; set; }
    }
}
