using System;

namespace TeduShop.Data.Model
{
    public class SupplierMappingModel
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