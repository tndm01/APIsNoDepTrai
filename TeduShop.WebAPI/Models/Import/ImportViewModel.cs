using System;
using System.Collections.Generic;

namespace TeduShop.Web.Models
{
    public class ImportViewModel
    {
        public int ImportId { get; set; }
        public string Code { get; set; }
        public string ReferenceCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DayCreatedVoucher { get; set; }
        public string Reason { get; set; }
        public string Note { get; set; }
        public decimal Total { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public int UserId { get; set; }
        public bool Censorship { get; set; }
        public string WarehouseCode { get; set; }
        public IEnumerable<ImportDetailModel> ImportDetails { get; set; }
    }

    public class ImportDetailModel
    {
        public int ImportDetailId { get; set; }
        public int ProductId { get; set; }
        public int ImportId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int WareHouseId { get; set; }
        public string ColorCode { get; set; }
        public string SizeCode { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string ComponentCode { get; set; }
    }
}