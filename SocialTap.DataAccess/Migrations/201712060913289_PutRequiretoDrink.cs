namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PutRequiretoDrink : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drinks", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drinks", "Name", c => c.String(maxLength: 50));
        }
    }
}
