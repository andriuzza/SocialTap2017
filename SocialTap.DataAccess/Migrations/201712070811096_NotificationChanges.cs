namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "Drink_Id", "dbo.Drinks");
            DropForeignKey("dbo.Notifications", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Notifications", new[] { "Drink_Id" });
            DropIndex("dbo.Notifications", new[] { "Location_Id" });
            AddColumn("dbo.Notifications", "NotificationText", c => c.String());
            DropColumn("dbo.Notifications", "Drink_Id");
            DropColumn("dbo.Notifications", "Location_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Location_Id", c => c.Int());
            AddColumn("dbo.Notifications", "Drink_Id", c => c.Int());
            DropColumn("dbo.Notifications", "NotificationText");
            CreateIndex("dbo.Notifications", "Location_Id");
            CreateIndex("dbo.Notifications", "Drink_Id");
            AddForeignKey("dbo.Notifications", "Location_Id", "dbo.Locations", "Id");
            AddForeignKey("dbo.Notifications", "Drink_Id", "dbo.Drinks", "Id");
        }
    }
}
