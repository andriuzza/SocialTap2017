namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAwards", "Awards_Id", "dbo.LocationAwards");
            DropIndex("dbo.UserAwards", new[] { "Awards_Id" });
            DropColumn("dbo.UserAwards", "AwardId");
            RenameColumn(table: "dbo.UserAwards", name: "Awards_Id", newName: "AwardId");
            AlterColumn("dbo.UserAwards", "AwardId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserAwards", "AwardId");
            AddForeignKey("dbo.UserAwards", "AwardId", "dbo.LocationAwards", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAwards", "AwardId", "dbo.LocationAwards");
            DropIndex("dbo.UserAwards", new[] { "AwardId" });
            AlterColumn("dbo.UserAwards", "AwardId", c => c.Int());
            RenameColumn(table: "dbo.UserAwards", name: "AwardId", newName: "Awards_Id");
            AddColumn("dbo.UserAwards", "AwardId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserAwards", "Awards_Id");
            AddForeignKey("dbo.UserAwards", "Awards_Id", "dbo.LocationAwards", "Id");
        }
    }
}
