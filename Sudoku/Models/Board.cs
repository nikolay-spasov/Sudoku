using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sudoku.Models
{
    public enum Difficulty
    {
        Easy, Hard
    }

    public class Board
    {
        public int Id { get; set; }

        [StringLength(81)]
        public string Content { get; set; }

        public Difficulty Difficulty { get; set; }

        public virtual ICollection<Rating> Rated { get; set; }
    }
}