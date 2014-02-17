namespace Sudoku.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSaveGame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        SavedAt = c.DateTime(nullable: false),
                        InitialBoard_Id = c.Int(),
                        UserProfile_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.InitialBoard_Id)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserId)
                .Index(t => t.InitialBoard_Id)
                .Index(t => t.UserProfile_UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SavedGames", new[] { "UserProfile_UserId" });
            DropIndex("dbo.SavedGames", new[] { "InitialBoard_Id" });
            DropForeignKey("dbo.SavedGames", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.SavedGames", "InitialBoard_Id", "dbo.Boards");
            DropTable("dbo.SavedGames");
        }
    }
}
