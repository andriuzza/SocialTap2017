namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNotificationUserID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.NotificationUsers");
            AlterColumn("dbo.NotificationUsers", "AccountUserID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.NotificationUsers", new[] { "AccountUserID", "NotificationId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.NotificationUsers");
            AlterColumn("dbo.NotificationUsers", "AccountUserID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.NotificationUsers", new[] { "AccountUserID", "NotificationId" });
        }
    }
}
