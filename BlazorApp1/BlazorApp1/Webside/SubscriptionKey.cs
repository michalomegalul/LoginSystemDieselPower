using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Webside;


public class SubscriptionKey
{
    private const string SecretKey = "D4jVOga3b7@ggGcUJN3GsF!Ke$5UcFh$PUxHUL!yYJe1xC*h@W";
    public static bool ValidateKey(string key, TimeSpan subscriptionDuration)
    {
        var parts = key.Split('|');
        if (parts.Length != 2)
        {
            return false;
        }

        var timestamp = long.Parse(parts[1]);
        var subscriptionEndDate = DateTimeOffset.FromUnixTimeSeconds(timestamp).Add(subscriptionDuration);
        if (subscriptionEndDate < DateTimeOffset.UtcNow)
        {
            return false;
        }

        var data = Convert.ToString(timestamp);
        var signature = parts[2];
        var expectedSignature = SignData(data, SecretKey);

        return signature == expectedSignature;
    }
    private static string SignData(string data, string secretKey)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToHexString(hash);
    }
    public static string GenerateKey()
    {
        var data = Convert.ToString(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        var signature = SignData(data, SecretKey);
        return $"{data}|{signature}";
    }
}
