using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models.Supplier
{
    public class SupplierViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Note { get; set; }
        public string Tax { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
    }
}