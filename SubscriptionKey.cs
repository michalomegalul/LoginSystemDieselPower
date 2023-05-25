using System.Security.Cryptography;
using System.Text;

public class SubscriptionKey
{
    private const string SecretKey = "A6rUKknreJx!4PFRTEx$jJ7FBtahCnR4M4EaxZMq-Hs++=Q+CT4eB+VT#TJEsN-D&c+f";

    public static string GenerateKey(string hardwareId)
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var data = $"{hardwareId}|{timestamp}";
        var signature = SignData(data, SecretKey);

        return $"{data}|{signature}";
    }

    public static bool ValidateKey(string key, string hardwareId, TimeSpan subscriptionDuration)
    {
        var parts = key.Split('|');
        if (parts.Length != 3)
        {
            return false;
        }

        var timestamp = long.Parse(parts[1]);
        var subscriptionEndDate = DateTimeOffset.FromUnixTimeSeconds(timestamp).Add(subscriptionDuration);
        if (subscriptionEndDate < DateTimeOffset.UtcNow)
        {
            return false;
        }

        var data = $"{hardwareId}|{timestamp}";
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
}
