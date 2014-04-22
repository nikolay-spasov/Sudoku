namespace Sudoku.Migrations
{
    using Sudoku.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;

    public sealed class Configuration : DbMigrationsConfiguration<Sudoku.Models.SudokuDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Sudoku.Models.SudokuDbContext context)
        {

        }
    }
}
