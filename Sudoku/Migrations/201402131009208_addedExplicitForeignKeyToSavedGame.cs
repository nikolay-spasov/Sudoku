namespace Sudoku.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedExplicitForeignKeyToSavedGame : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SavedGames", "InitialBoard_Id", "dbo.Boards");
            DropIndex("dbo.SavedGames", new[] { "InitialBoard_Id" });
            RenameColumn(table: "dbo.SavedGames", name: "InitialBoard_Id", newName: "BoardId");
            AddForeignKey("dbo.SavedGames", "BoardId", "dbo.Boards", "Id", cascadeDelete: true);
            CreateIndex("dbo.SavedGames", "BoardId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SavedGames", new[] { "BoardId" });
            DropForeignKey("dbo.SavedGames", "BoardId", "dbo.Boards");
            RenameColumn(table: "dbo.SavedGames", name: "BoardId", newName: "InitialBoard_Id");
            CreateIndex("dbo.SavedGames", "InitialBoard_Id");
            AddForeignKey("dbo.SavedGames", "InitialBoard_Id", "dbo.Boards", "Id");
        }
    }
}
