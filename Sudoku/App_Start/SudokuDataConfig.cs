using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Sudoku.App_Start
{
    public static class SudokuDataConfig
    {
        public static void SetupInitialData()
        {
            string path = Path.Combine(HostingEnvironment.MapPath("~/App_Data"), "top95.txt");

            var db = new SudokuDbContext();
            DeleteBoardsIfExist(db);

            using (StreamReader reader = new StreamReader(path))
            {
                string line = string.Empty;
                int counter = 1;
                while (line != null)
                {
                    line = reader.ReadLine();
                    db.Boards.Add(new Board
                    {
                        Id = counter++,
                        Content = line
                    });
                }
            }
            db.SaveChanges();
        }

        private static void DeleteBoardsIfExist(SudokuDbContext db)
        {
            db.Boards.ToList().ForEach(x => db.Boards.Remove(x));
        }
    }
}