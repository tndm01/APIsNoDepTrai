namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TblColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Colors", "ColorCode", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Colors", "ColorCode");
        }
    }
}
