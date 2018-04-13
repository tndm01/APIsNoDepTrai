using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Mobile { get; set; }
        [StringLength(1000)]
        public string Note { get; set; }
        [StringLength(50)]
        public string Tax { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
    }
}
