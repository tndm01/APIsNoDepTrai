using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { set; get; }
        public DateTime Created { get; set; }
        [StringLength(500)]
        public string Content { get; set; }
    }
}
