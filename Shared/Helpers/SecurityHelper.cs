using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;

namespace Shared.Helpers;

public class SecurityHelper
{
    private static readonly char[] Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()".ToCharArray();
    public static string GenerateHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool ValidateHash(string password, string actualPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, actualPassword);
    }

    public static string GenerateFileUrl(string filePath, DateTime expiry)
    {
        var expiryString = expiry.ToString("o");
        var encodedExpiry = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(expiryString));
        return $"{filePath}?token={encodedExpiry}";
    }

    public static string GeneratePassword()
    {
        int length = 8;
        if (length <= 0)
        {
            throw new ArgumentException("Password length must be greater than 0", nameof(length));
        }

        StringBuilder password = new StringBuilder(length);
        byte[] randomBytes = new byte[length];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        for (int i = 0; i < length; i++)
        {
            password.Append(Characters[randomBytes[i] % Characters.Length]);
        }

        return password.ToString();
    }
}
