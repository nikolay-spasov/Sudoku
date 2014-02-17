using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sudoku.Validation
{
    public class ValidSudokuBoardAttribute : ValidationAttribute
    {
        public ValidSudokuBoardAttribute()
        {
            this.ErrorMessage = "Board contains not allowed characters";
        }

        public override bool IsValid(object value)
        {
            string val = value.ToString();

            return val.Length == 81 && ContainsOnlyAllowedChars(val);
        }

        private static bool ContainsOnlyAllowedChars(string board)
        {
            foreach (var ch in board)
            {
                if (!char.IsDigit(ch) && ch != '.')
                {
                    return false;
                }
            }

            return true;
        }
    }
}