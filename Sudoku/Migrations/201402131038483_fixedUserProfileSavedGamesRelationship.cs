namespace Sudoku.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedUserProfileSavedGamesRelationship : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SavedGames", "UserProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavedGames", "UserProfileId", c => c.Int(nullable: false));
        }
    }
}
