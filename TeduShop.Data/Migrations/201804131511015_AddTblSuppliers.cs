namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblSuppliers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Address = c.String(maxLength: 500),
                        Email = c.String(maxLength: 100),
                        Mobile = c.String(maxLength: 20),
                        Note = c.String(maxLength: 1000),
                        Tax = c.String(maxLength: 50),
                        Created = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Suppliers");
        }
    }
}
