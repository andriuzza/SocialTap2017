namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class awards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocationAwards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RequiredPoints = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            AddColumn("dbo.AspNetUsers", "LocationAward_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "LocationAward_Id");
            AddForeignKey("dbo.AspNetUsers", "LocationAward_Id", "dbo.LocationAwards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "LocationAward_Id", "dbo.LocationAwards");
            DropForeignKey("dbo.LocationAwards", "LocationId", "dbo.Locations");
            DropIndex("dbo.AspNetUsers", new[] { "LocationAward_Id" });
            DropIndex("dbo.LocationAwards", new[] { "LocationId" });
            DropColumn("dbo.AspNetUsers", "LocationAward_Id");
            DropTable("dbo.LocationAwards");
        }
    }
}
