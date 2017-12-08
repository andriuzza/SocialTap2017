namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesOnNotifications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotificationUsers", "UserAccountId", "dbo.AspNetUsers");
            AddForeignKey("dbo.NotificationUsers", "UserAccountId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationUsers", "UserAccountId", "dbo.AspNetUsers");
            AddForeignKey("dbo.NotificationUsers", "UserAccountId", "dbo.AspNetUsers", "Id");
        }
    }
}
