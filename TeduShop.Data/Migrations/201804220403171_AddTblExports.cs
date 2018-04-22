namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblExports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exports",
                c => new
                    {
                        ExportId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        ReferenceCode = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        DayCreatedVoucher = c.DateTime(nullable: false),
                        Reason = c.String(maxLength: 100),
                        Note = c.String(maxLength: 100),
                        Debt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierId = c.Int(nullable: false),
                        SupplierCode = c.String(maxLength: 20),
                        SupplierName = c.String(maxLength: 200),
                        UserId = c.Int(nullable: false),
                        Censorship = c.Boolean(nullable: false),
                        WarehouseCode = c.String(),
                    })
                .PrimaryKey(t => t.ExportId);
            
            AddColumn("dbo.Imports", "SupplierId", c => c.Int(nullable: false));
            AddColumn("dbo.Imports", "SupplierCode", c => c.String(maxLength: 20));
            AddColumn("dbo.Imports", "SupplierName", c => c.String(maxLength: 200));
            DropColumn("dbo.Imports", "CustomerId");
            DropColumn("dbo.Imports", "CustomerCode");
            DropColumn("dbo.Imports", "CustomerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Imports", "CustomerName", c => c.String(maxLength: 200));
            AddColumn("dbo.Imports", "CustomerCode", c => c.String(maxLength: 20));
            AddColumn("dbo.Imports", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Imports", "SupplierName");
            DropColumn("dbo.Imports", "SupplierCode");
            DropColumn("dbo.Imports", "SupplierId");
            DropTable("dbo.Exports");
        }
    }
}
