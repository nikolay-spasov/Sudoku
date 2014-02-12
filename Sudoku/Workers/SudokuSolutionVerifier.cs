using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sudoku.Workers
{
    public class SolutionVerifierResult
    {
        public bool IsValid { get; set; }
        public HashSet<int> InvalidIndices { get; set; }
    }

    public class SudokuSolutionVerifier
    {
        private static readonly int[,] indices = 
        {
            // Horizontals
            { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
            { 9, 10, 11, 12, 13, 14, 15, 16, 17 },
            { 18, 19, 20, 21, 22, 23, 24, 25, 26 },
            { 27, 28, 29, 30, 31, 32, 33, 34, 35 },
            { 36, 37, 38, 39, 40, 41, 42, 43, 44 },
            { 45, 46, 47, 48, 49, 50, 51, 52, 53 },
            { 54, 55, 56, 57, 58, 59, 60, 61, 62 },
            { 63, 64, 65, 66, 67, 68, 69, 70, 71 },
            { 72, 73, 74, 75, 76, 77, 78, 79, 80 },

            // Verticals
            { 0, 9, 18, 27, 36, 45, 54, 63, 72 },
            { 1, 10, 19, 28, 37, 46, 55, 64, 73 },
            { 2, 11, 20, 29, 38, 47, 56, 65, 74 },
            { 3, 12, 21, 30, 39, 48, 57, 66, 75 },
            { 4, 13, 22, 31, 40, 49, 58, 67, 76 },
            { 5, 14, 23, 32, 41, 50, 59, 68, 77 },
            { 6, 15, 24, 33, 42, 51, 60, 69, 78 },
            { 7, 16, 25, 34, 43, 52, 61, 70, 79 },
            { 8, 17, 26, 35, 44, 53, 62, 71, 80 },

            // 3 by 3 squares
            { 0, 1, 2, 9, 10, 11, 18, 19, 20 },
            { 3, 4, 5, 12, 13, 14, 21, 22, 23 },
            { 6, 7, 8, 15, 16, 17, 24, 25, 26 },
            { 27, 28, 29, 36, 37, 38, 45, 46, 47 },
            { 30, 31, 32, 39, 40, 41, 48, 49, 50 },
            { 33, 34, 35, 42, 43, 44, 51, 52, 53 },
            { 54, 55, 56, 63, 64, 65, 72, 73, 74 },
            { 57, 58, 59, 66, 67, 68, 75, 76, 77 },
            { 60, 61, 62, 69, 70, 71, 78, 79, 80 },
        };

        private string initialBoard;
        private string solution;

        public SudokuSolutionVerifier(string solution, string initialBoard)
        {
            this.initialBoard = initialBoard;
            this.solution = solution;
        }

        public SolutionVerifierResult Verify()
        {
            var result = new SolutionVerifierResult()
            {
                IsValid = true,
                InvalidIndices = new HashSet<int>(),
            };

            if (!IsSolutionValid())
            {
                result.IsValid = false;
                return result;
            }

            HashSet<char> digits = new HashSet<char>();
            for (int row = 0; row < indices.GetLength(0); row++)
            {
                digits.Clear();
                for (int col = 0; col < indices.GetLength(1); col++)
                {
                    var current = indices[row, col];

                    if (digits.Contains(solution[current]))
                    {
                        result.IsValid = false;
                        result.InvalidIndices.Add(current);
                    }
                    digits.Add(solution[current]);
                }
            }

            return result;
        }

        private bool IsSolutionValid()
        {
            if (solution.Length != 81 || solution.Contains('.'))
            {
                return false;
            }

            for (int i = 0; i < initialBoard.Length; i++)
            {
                if (initialBoard[i] != '.' && solution[i] != initialBoard[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}