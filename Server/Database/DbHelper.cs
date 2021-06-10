using System.Data.SQLite;
using Microsoft.Extensions.Options;
using System.Data;


using Server.Abstract;
using Server.Config;

namespace Server.Database
{
    public class DbHelper : IDbHelper
    {
        string sqllite3ConnectionString;

        public DbHelper(IOptions<AppConfig> _appConfig)
        {
            sqllite3ConnectionString = _appConfig.Value.ConnectionStrings.SqlLite3;
        }

        public IDbConnection GetDbConnection() {
            return new SQLiteConnection(sqllite3ConnectionString); 
        }
    }
}