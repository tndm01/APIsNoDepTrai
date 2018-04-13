namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AppUserId = c.String(maxLength: 128),
                        Created = c.DateTime(nullable: false),
                        Content = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .Index(t => t.AppUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "AppUserId", "dbo.AppUsers");
            DropIndex("dbo.Logs", new[] { "AppUserId" });
            DropTable("dbo.Logs");
        }
    }
}
