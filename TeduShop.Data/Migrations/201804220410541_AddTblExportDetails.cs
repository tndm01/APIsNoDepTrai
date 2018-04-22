namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblExportDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExportDetails",
                c => new
                    {
                        ExportDetailId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ExportId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WareHouseId = c.Int(nullable: false),
                        ColorCode = c.String(maxLength: 10),
                        SizeCode = c.String(maxLength: 10),
                        ColorId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ExportDetailId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExportDetails");
        }
    }
}
