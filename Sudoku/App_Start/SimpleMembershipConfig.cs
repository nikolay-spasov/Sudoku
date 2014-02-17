using WebMatrix.WebData;
namespace Sudoku.App_Start
{
    public static class SimpleMembershipConfig
    {
        public static void Initialize()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                    "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }
        }
    }
}