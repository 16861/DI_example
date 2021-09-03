using System.Data.SQLite;
using System.Data;

using Server.Config;

namespace Server.Database
{
    public class DbHelper
    {
        string sqllite3ConnectionString;

        public DbHelper(AppConfig _appConfig)
        {
            sqllite3ConnectionString = _appConfig.ConnectionStrings.SqlLite3;
        }

        public IDbConnection GetDbConnection() {
            return new SQLiteConnection(sqllite3ConnectionString); 
        }
    }
}