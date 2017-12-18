namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredPropLocation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Locations", "Address", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Address", c => c.String(maxLength: 50));
            AlterColumn("dbo.Locations", "Name", c => c.String(maxLength: 50));
        }
    }
}
