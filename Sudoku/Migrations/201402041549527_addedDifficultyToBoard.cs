namespace Sudoku.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDifficultyToBoard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boards", "Difficulty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Boards", "Difficulty");
        }
    }
}
