using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sudoku.Models.ViewModels
{
    public class PlaySudokuVM
    {
        public int Id { get; set; }
        public char[,] Board { get; set; }
        public char[,] InitialBoard { get; set; }
        public double AverageRating { get; set; }
        public int CurrentUserRating { get; set; }

        public string GetSimpleRating()
        {
            if (AverageRating == 0)
            {
                return "0";
            }
            else
            {
                string result = AverageRating.ToString("0.0");
                if (result.EndsWith("0"))
                {
                    result = result[0].ToString();
                }

                return result;
            }
        }
    }
}