namespace Sudoku.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedRatingsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.Ratings", new[] { "UserProfile_UserId" });
            AddForeignKey("dbo.Ratings", "UserProfileId", "dbo.UserProfile", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Ratings", "UserProfileId");
            DropColumn("dbo.Ratings", "UserProfile_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "UserProfile_UserId", c => c.Int());
            DropIndex("dbo.Ratings", new[] { "UserProfileId" });
            DropForeignKey("dbo.Ratings", "UserProfileId", "dbo.UserProfile");
            CreateIndex("dbo.Ratings", "UserProfile_UserId");
            AddForeignKey("dbo.Ratings", "UserProfile_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
