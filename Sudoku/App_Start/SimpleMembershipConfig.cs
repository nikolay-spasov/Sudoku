using WebMatrix.WebData;
namespace Sudoku.App_Start
{
    public static class SimpleMembershipConfig
    {
        public static void Initialize()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", 
                "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
    }
}