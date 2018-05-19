using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    public class Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitId { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(100)]
        public string UnitCode { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
