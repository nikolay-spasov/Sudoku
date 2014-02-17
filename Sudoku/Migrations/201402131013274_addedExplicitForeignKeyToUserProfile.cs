namespace Sudoku.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedExplicitForeignKeyToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavedGames", "UserProfileId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavedGames", "UserProfileId");
        }
    }
}
