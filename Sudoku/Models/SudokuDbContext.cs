using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Sudoku.Migrations;

namespace Sudoku.Models
{
    public class SudokuDbContext : DbContext
    {
        public SudokuDbContext() 
            : this("DefaultConnection") { }

        public SudokuDbContext(string connectionString)
            : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SudokuDbContext, Configuration>());
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<SavedGame> SavedGames { get; set; }
    }
}