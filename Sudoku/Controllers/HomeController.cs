using Sudoku.Models;
using Sudoku.Models.ViewModels;
using Sudoku.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Sudoku.Controllers
{
    public class HomeController : Controller
    {
        // TODO: Make it DI friendly
        private SudokuDbContext db = new SudokuDbContext();

        public ActionResult Index()
        {
            var games = GetGames(0, 12);

            return View(games);
        }

        [HttpPost]
        public ActionResult GetMoreGames(int skip, int take)
        {
            var games = GetGames(skip, take);

            return PartialView("_Games", games);
        }

        private List<IndexVM> GetGames(int skip, int take)
        {
            var games = db.Boards.Include("Rated")
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .ToList();

            var result = new List<IndexVM>(games.Count);
            games.ForEach(x =>
                {
                    var current = new IndexVM();
                    current.BoardId = x.Id;
                    current.Board = ParseBoard(x.Content);
                    current.Difficulty = x.Difficulty.ToString();
                    current.Rating = (x.Rated.Count == 0) ? 0 : x.Rated.Average(r => r.RatingValue);
                    result.Add(current);
                });

            return result;
        }

        public ActionResult PlaySudoku(int id)
        {
            var board = db.Boards.Include("Rated").FirstOrDefault(x => x.Id == id);
            double averageRating = (board.Rated.Count() > 0) ? board.Rated.Average(x => x.RatingValue) : 0;
            int currentUserRating = 0;
            if (WebSecurity.IsAuthenticated && board.Rated.Any(x => x.UserProfileId == WebSecurity.CurrentUserId))
            {
                currentUserRating = board.Rated.Single(x => x.UserProfileId == WebSecurity.CurrentUserId).RatingValue;
            }

            var viewModel = new PlaySudokuVM
            {
                Id = board.Id,
                Board = ParseBoard(board.Content),
                AverageRating = averageRating,
                UserIsAuthenticated = WebSecurity.IsAuthenticated,
                CurrentUserRating = currentUserRating
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SubmitSolution(SubmitSolutionVM data)
        {
            string initial = db.Boards.FirstOrDefault(x => x.Id == data.Id).Content;

            var verifier = new SudokuSolutionVerifier(data.Solution, initial);
            var result = verifier.Verify();

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        public ActionResult RateBoard(int boardId, int ratingValue)
        {
            if (ratingValue < 1) ratingValue = 1;
            if (ratingValue > 5) ratingValue = 5;

            var game = db.Boards.FirstOrDefault(x => x.Id == boardId);

            if (game.Rated.Any(x => x.UserProfileId == WebSecurity.CurrentUserId))
            {
                game.Rated.Single(x => x.UserProfileId == WebSecurity.CurrentUserId).RatingValue = ratingValue;
                db.SaveChanges();
            }
            else
            {
                game.Rated.Add(new Rating
                {
                    BoardId = boardId,
                    UserProfileId = WebSecurity.CurrentUserId,
                    RatingValue = ratingValue
                });
                db.SaveChanges();
            }

            double totalRating = game.Rated.Average(x => x.RatingValue);

            return Json(new { rating = totalRating.ToString("0.0") }, JsonRequestBehavior.DenyGet);
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
