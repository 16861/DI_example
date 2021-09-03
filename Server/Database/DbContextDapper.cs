using System.Data;
using Dapper;
using System.Collections.Generic;
using Server.Models;
using System.Linq;

namespace Server.Database {
   public class DbContextDapper {

       readonly IDbConnection _conn;

       public DbContextDapper(DbHelper dbHelper)
       {
           _conn = dbHelper.GetDbConnection();
           _conn.Open();
       }

       public string GetVersion() {
           return _conn.QueryFirst("select SQLITE_VERSION() AS Version").Version;
       }

       public IEnumerable<MessageModel> GetAllMessages(long requestTime) {
           return _conn.Query<MessageModel>($"select * from Messages where time > {requestTime}");
       } 

       public void SaveNewMessage(MessageModel message) {
           string insertQuery = "INSERT INTO Messages(name, text, time, isencrypted) values(@name, @text, @time, @isencrypted)";
           _conn.Execute(insertQuery, new {message.Name, message.Text, message.Time, message.IsEncrypted});
       }

        public VersionsModel GetVersions()
        {
            var ret = _conn.Query<VersionsModel>("select * from Versions").FirstOrDefault();
            if (ret == null)
                throw new System.Exception("Could not ");
            return ret;
        }

        public void UpdateVersion(string time)
        {
            string updateQuery = "UPDATE Versions SET Version = @time";
            _conn.Execute(updateQuery, new { time });
        }
    }
}