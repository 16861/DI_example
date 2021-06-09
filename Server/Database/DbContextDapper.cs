using System.Data;
using Dapper;
using Server.Abstract;
using System.Collections.Generic;
using Server.Models;
using System.Linq;

namespace Server.Database {
   public class DbContextDapper : IDbContext {
       readonly IDbConnection conn;
       public DbContextDapper(IDbConnection dbcon)
       {
           conn = dbcon;
           conn.Open();
       }

       public string GetVersion() {
           return conn.QueryFirst("select SQLITE_VERSION() AS Version").Version;
       }

       public IEnumerable<MessageModel> GetAllMessages(long requestTime) {
           return conn.Query<MessageModel>($"select * from Messages where time > {requestTime}");
       } 

       public void SaveNewMessage(MessageModel message) {
           string insertQuery = "INSERT INTO Messages(name, text, time) values(@name, @text, @time)";
           conn.Execute(insertQuery, new {message.Name, message.Text, message.Time});
       }

        public VersionsModel GetVersions()
        {
            var ret = conn.Query<VersionsModel>("select * from Versions").FirstOrDefault();
            if (ret == null)
                throw new System.Exception("Could not ");
            return ret;
        }

        public void UpdateVersion(string time)
        {
            string updateQuery = "UPDATE Versions SET Version = @time";
            conn.Execute(updateQuery, new { time });
        }
    }
}