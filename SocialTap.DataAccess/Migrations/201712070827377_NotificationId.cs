namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.NotificationUsers", name: "UserAccount_Id", newName: "UserAccountId");
            RenameIndex(table: "dbo.NotificationUsers", name: "IX_UserAccount_Id", newName: "IX_UserAccountId");
            DropPrimaryKey("dbo.NotificationUsers");
            AddPrimaryKey("dbo.NotificationUsers", new[] { "UserAccountId", "NotificationId" });
            DropColumn("dbo.NotificationUsers", "AccountUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotificationUsers", "AccountUserID", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.NotificationUsers");
            AddPrimaryKey("dbo.NotificationUsers", new[] { "AccountUserID", "NotificationId" });
            RenameIndex(table: "dbo.NotificationUsers", name: "IX_UserAccountId", newName: "IX_UserAccount_Id");
            RenameColumn(table: "dbo.NotificationUsers", name: "UserAccountId", newName: "UserAccount_Id");
        }
    }
}
