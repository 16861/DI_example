namespace Server.Config
{

    public class ConnectionStringSection {
        public string SqlLite3 {get; set;}
    }

    public class AppConfig
    {
        public ConnectionStringSection ConnectionStrings {get; set;}
        public string EncryptionKey {get; set;}
    }
}