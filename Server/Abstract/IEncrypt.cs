namespace Server.Abstract
{
    public interface IEncrypt
    {
        string Encrypt(string input);
        string Decrypt(string encrypted);
    }
}