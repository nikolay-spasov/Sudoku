using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sudoku.Models.ViewModels
{
    public class IndexVM
    {
        public int BoardId { get; set; }
        public char[,] Board { get; set; }
        public string Difficulty { get; set; }
        public double Rating { get; set; }

        public string GetSimpleRating()
        {
            if (Rating == 0)
            {
                return "0";
            }
            else
            {
                string result = Rating.ToString("0.0");
                if (result.EndsWith("0"))
                {
                    result = result[0].ToString();
                }

                return result;
            }
        }
    }
}