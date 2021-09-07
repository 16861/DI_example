using System.Data;
using Dapper;
using Server.Abstract;
using System.Collections.Generic;
using Server.Models;
using System.Linq;
using System;

namespace Server.Database
{

    public class SqlLiteHandler : IMessageHandler {

       readonly IDbConnection conn;
       readonly IEncrypt _encrypt;
       
       public SqlLiteHandler(IDbHelper dbHelper, IEncrypt encrypt)
       {
           conn = dbHelper.GetDbConnection();
           conn.Open();
           _encrypt = encrypt;
       }

       public IEnumerable<MessageModel> GetAllMessages(long requestTime) {
           foreach(var message in conn.Query<MessageModel>($"select * from Messages where time > {requestTime}")) {
                if (message.IsEncrypted == 1)
                    message.Text = _encrypt.Decrypt(message.Text);
                yield return message;
           }
       } 

       public void SaveNewMessage(MessageModel message, bool isEncrypted) {
           string insertQuery = "INSERT INTO Messages(name, text, time, isencrypted) values(@name, @text, @time, @isencrypted)";
           var time = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString();
           if (isEncrypted)
            message.Text = _encrypt.Encrypt(message.Text);
           conn.Execute(insertQuery, new {message.Name, message.Text, time, message.IsEncrypted});

           UpdateVersion(((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString());
       }

        public VersionsModel GetVersions()
        {
            var ret = conn.Query<VersionsModel>("select * from Versions").FirstOrDefault();
            if (ret == null)
                throw new System.Exception("Could not find any version");
            return ret;
        }

        public void UpdateVersion(string time)
        {
            string updateQuery = "UPDATE Versions SET Version = @time";
            conn.Execute(updateQuery, new { time });
        }
    }
}