using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Shared.Helpers;

public class SecurityHelper
{
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
}
