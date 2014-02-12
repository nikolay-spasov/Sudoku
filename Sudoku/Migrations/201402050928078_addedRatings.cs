namespace Sudoku.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserProfileId = c.Int(nullable: false),
                        BoardId = c.Int(nullable: false),
                        RatingValue = c.Int(nullable: false),
                        UserProfile_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserId)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.UserProfile_UserId)
                .Index(t => t.BoardId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ratings", new[] { "BoardId" });
            DropIndex("dbo.Ratings", new[] { "UserProfile_UserId" });
            DropForeignKey("dbo.Ratings", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Ratings", "UserProfile_UserId", "dbo.UserProfile");
            DropTable("dbo.Ratings");
        }
    }
}
