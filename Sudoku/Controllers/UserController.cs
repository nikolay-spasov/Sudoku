using Sudoku.Models;
using Sudoku.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Sudoku.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private SudokuDbContext db = new SudokuDbContext();

        public ActionResult ShowSavedGames()
        {
            var savedGames = db.SavedGames.Include("UserProfile")
                .Where(x => x.UserProfile.UserId == WebSecurity.CurrentUserId)
                .OrderByDescending(x => x.SavedAt)
                .ToList();

            var viewModel = new List<ShowSavedGamesVM>();
            savedGames.ForEach(x =>
                {
                    ShowSavedGamesVM current = new ShowSavedGamesVM()
                    {
                        Id = x.Id,
                        InitialBoardId = x.InitialBoard.Id,
                        Content = ParseBoard(x.Content),
                        SavedAt = x.SavedAt
                    };
                    viewModel.Add(current);
                });

            return View(viewModel);
        }

        public ActionResult LoadGame(int id)
        {
            var gameToLoad = db.SavedGames.Include("InitialBoard").FirstOrDefault(x => x.Id == id);
            TempData["SavedGameContent"] = gameToLoad.Content;

            return RedirectToAction("PlaySudoku", "Home", new { id = gameToLoad.BoardId });
        }

        private static char[,] ParseBoard(string board)
        {
            var fixedBoard = board.Replace('.', ' ');
            char[,] result = new char[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    result[row, col] = fixedBoard[row * 9 + col];
                }
            }

            return result;
        }

    }
}
