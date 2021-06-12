using Microsoft.Extensions.Configuration;

namespace Server.Config
{
    public class ConnectionStringSection {
        public string SqlLite3 {get; set;}
    }

    public class AppConfig
    {
        public AppConfig(IConfiguration config) {
            EncryptionKey = config.GetValue<string>("EncryptionKey");
            ConnectionStrings = new ConnectionStringSection() {
                SqlLite3 = config.GetValue<string>("ConnectionStrings:SqlLite3")
            };
        }

        public ConnectionStringSection ConnectionStrings {get; set;}
        public string EncryptionKey {get; set;}
    }
}