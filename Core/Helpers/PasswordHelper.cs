using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers;

public class PasswordHelper
{
    public static byte[] HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static byte[] GenerateSalt()
    {
        using var hmac = new HMACSHA512();
        return hmac.Key;
    }
    
}