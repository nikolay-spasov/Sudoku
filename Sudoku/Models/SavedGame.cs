using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sudoku.Models
{
    public class SavedGame
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SavedAt { get; set; }

        public int BoardId { get; set; }
        public virtual Board InitialBoard { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}