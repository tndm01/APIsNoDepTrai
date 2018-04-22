namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblImportDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportDetails",
                c => new
                    {
                        ImportDetailId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImportId = c.Int(nullable: false),
                        ProductName = c.String(maxLength: 200),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WareHouseId = c.Int(nullable: false),
                        ColorCode = c.String(maxLength: 10),
                        SizeCode = c.String(maxLength: 10),
                        ColorId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        ComponentCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ImportDetailId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImportDetails");
        }
    }
}
