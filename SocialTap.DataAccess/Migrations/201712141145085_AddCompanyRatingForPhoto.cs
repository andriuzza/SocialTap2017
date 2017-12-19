namespace SocialTap.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyRatingForPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "RatingOfCapture", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "RatingOfCapture");
        }
    }
}
