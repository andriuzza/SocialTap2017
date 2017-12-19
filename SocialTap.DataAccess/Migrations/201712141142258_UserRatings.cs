namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserAccountId = c.String(maxLength: 128),
                        LocationId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAccountId)
                .Index(t => t.UserAccountId)
                .Index(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRatings", "UserAccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserRatings", "LocationId", "dbo.Locations");
            DropIndex("dbo.UserRatings", new[] { "LocationId" });
            DropIndex("dbo.UserRatings", new[] { "UserAccountId" });
            DropTable("dbo.UserRatings");
        }
    }
}
