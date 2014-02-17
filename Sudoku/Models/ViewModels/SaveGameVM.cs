using Sudoku.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sudoku.Models.ViewModels
{
    public class SaveGameVM
    {
        public int InitialBoardId { get; set; }

        [ValidSudokuBoard]
        public string Content { get; set; }
    }
}