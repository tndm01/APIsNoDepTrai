using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("ImportDetails")]
    public class ImportDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImportDetailId { get; set; }
        public int ProductId { get; set; }
        public int ImportId { get; set; }
        [StringLength(200)]
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
        [StringLength(50)]
        public string ComponentCode { get; set; }
    }
}
