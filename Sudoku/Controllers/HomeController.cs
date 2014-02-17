using Sudoku.Models;
using Sudoku.Models.ViewModels;
using Sudoku.SudokuLogic;
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
        private SudokuDbContext db = new SudokuDbContext();

        public ActionResult Index(string difficulty = "All")
        {
            var games = GetGames(0, 12, difficulty);

            List<SelectListItem> diffList = new List<SelectListItem>
            {
               new SelectListItem
               {
                   Text = "All",
                   Value = "All",
                   Selected = difficulty == "All"
               }
            };
            Enum.GetNames(typeof(Difficulty)).ToList().ForEach(x =>
                {
                    diffList.Add(new SelectListItem
                    {
                        Text = x,
                        Value = x,
                        Selected = difficulty == x
                    });
                });

            TempData["DifficultyList"] = diffList;

            return View(games);
        }

        [HttpPost]
        public ActionResult GetMoreGames(int skip, int take, string difficulty = "All")
        {
            var games = GetGames(skip, take, difficulty);

            return PartialView("_Games", games);
        }

        private List<IndexVM> GetGames(int skip, int take, string difficulty)
        {
            List<Board> games = new List<Board>();
            if (difficulty == null || difficulty == "All")
            {
                games = db.Boards.Include("Rated")
                    .OrderBy(x => x.Id)
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            else
            {
                Difficulty diff = (Difficulty)Enum.Parse(typeof(Difficulty), difficulty);
                games = db.Boards.Include("Rated")
                    .Where(x => x.Difficulty == diff)
                    .OrderBy(x => x.Id)
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }

            var result = new List<IndexVM>();
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

            string content = board.Content;
            if (TempData["SavedGameContent"] != null)
            {
                content = TempData["SavedGameContent"].ToString();
            }

            var viewModel = new PlaySudokuVM
            {
                Id = board.Id,
                Board = ParseBoard(content),
                InitialBoard = ParseBoard(board.Content),
                AverageRating = averageRating,
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

        [Authorize]
        [HttpPost]
        public ActionResult SaveGame(SaveGameVM game)
        {
            if (ModelState.IsValid)
            {
                var currentUser = db.UserProfiles.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId);
                //var gameToSave = db.SavedGames.Create();
                //gameToSave.SavedAt = DateTime.Now;
                //gameToSave.BoardId = game.InitialBoardId;
                //gameToSave.Content = game.Content;

                //currentUser.SavedGames.Add(gameToSave);

                //db.SaveChanges();

                

                SavedGame savedGame = new SavedGame()
                {
                    BoardId = game.InitialBoardId,
                    Content = game.Content,
                    SavedAt = DateTime.Now,
                };
                currentUser.SavedGames.Add(savedGame);
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
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
