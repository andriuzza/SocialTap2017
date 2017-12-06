namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Drink_Id = c.Int(),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drinks", t => t.Drink_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Drink_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.NotificationUsers",
                c => new
                    {
                        AccountUserID = c.Int(nullable: false),
                        NotificationId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        UserAccount_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AccountUserID, t.NotificationId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAccount_Id)
                .Index(t => t.NotificationId)
                .Index(t => t.UserAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationUsers", "UserAccount_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationUsers", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Notifications", "Drink_Id", "dbo.Drinks");
            DropIndex("dbo.NotificationUsers", new[] { "UserAccount_Id" });
            DropIndex("dbo.NotificationUsers", new[] { "NotificationId" });
            DropIndex("dbo.Notifications", new[] { "Location_Id" });
            DropIndex("dbo.Notifications", new[] { "Drink_Id" });
            DropTable("dbo.NotificationUsers");
            DropTable("dbo.Notifications");
        }
    }
}
