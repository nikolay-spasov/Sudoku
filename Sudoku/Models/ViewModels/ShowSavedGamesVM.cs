using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sudoku.Models.ViewModels
{
    public class ShowSavedGamesVM
    {
        public int Id { get; set; }
        public int InitialBoardId { get; set; }
        public char[,] Content { get; set; }
        public DateTime SavedAt { get; set; }
    }
}