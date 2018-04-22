namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblImports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Imports",
                c => new
                    {
                        ImportId = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        ReferenceCode = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        DayCreatedVoucher = c.DateTime(nullable: false),
                        Reason = c.String(maxLength: 100),
                        Note = c.String(maxLength: 100),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(nullable: false),
                        CustomerCode = c.String(maxLength: 20),
                        CustomerName = c.String(maxLength: 200),
                        UserId = c.Int(nullable: false),
                        Censorship = c.Boolean(nullable: false),
                        WarehouseCode = c.String(),
                    })
                .PrimaryKey(t => t.ImportId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Imports");
        }
    }
}
