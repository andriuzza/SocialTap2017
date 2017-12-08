namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrinkRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrinkId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drinks", t => t.DrinkId, cascadeDelete: true)
                .Index(t => t.DrinkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DrinkRatings", "DrinkId", "dbo.Drinks");
            DropIndex("dbo.DrinkRatings", new[] { "DrinkId" });
            DropTable("dbo.DrinkRatings");
        }
    }
}
