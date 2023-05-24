using System;
using System.Security.Cryptography;
using System.Text;
using System.Management;


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

public class Program
{
    public static void Main()
    {
        var hardwareId = GetMotherboardId();
        var subscriptionDuration = TimeSpan.FromDays(30);

        var key = SubscriptionKey.GenerateKey(hardwareId);
        Console.WriteLine($"Generated Key: {key}");

        var isValid = SubscriptionKey.ValidateKey(key, hardwareId, subscriptionDuration);
        Console.WriteLine($"Key is {(isValid ? "Valid" : "Invalid")}");
    }
    public static string GetMotherboardId()
    {
        string result = string.Empty;
        try
        {
            var mos = new ManagementObjectSearcher("Select * FROM Win32_BaseBoard");
            foreach (var mo in mos.Get())
            {
                result = mo["SerialNumber"].ToString();
                break;
            }
        }
        catch
        {
            result = string.Empty;
        }
        return result;
    }
}
