using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sudoku.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public int BoardId { get; set; }

        [Range(0, 5)]
        public int RatingValue { get; set; }
    }
}