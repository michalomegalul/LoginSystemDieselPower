using System;
using System.IO.Pipes;
using System.Management;
using System.Text;

public class Program
{
    public static async Task Main()
    {
        await UserManagment.Pipse();
        var hardwareId = GetMotherboardId();
        var subscriptionDuration = TimeSpan.FromDays(30); //How long the subscription is valid for

        var key = SubscriptionKey.GenerateKey(hardwareId);
        Console.WriteLine($"Generated Key: {key}");

        var isValid = SubscriptionKey.ValidateKey(key, hardwareId, subscriptionDuration); //Validate the key
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
    public static string GetPCName()
    {
        string result = string.Empty;
        try
        {
            var mos = new ManagementObjectSearcher("Select * FROM Win32_ComputerSystem");
            foreach (var mo in mos.Get())
            {
                result = mo["Name"].ToString();
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

public class UserManagment
{
    public static async Task Pipse()
    {
        using (var server = new NamedPipeServerStream("MyNamedPipe"))
        {
            Console.WriteLine("Waiting for connection...");
            await server.WaitForConnectionAsync();
            Console.WriteLine("Connected!");

            var buffer = new byte[4096];
            var bytesRead = await server.ReadAsync(buffer, 0, buffer.Length);
            var requestData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Received data: {requestData}");

            // Process the received data as needed

            // Send response if required
            var responseData = "Response from C#";
            var responseBytes = Encoding.UTF8.GetBytes(responseData);
            await server.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
public class User
{
    public string Desktop_Name { get; set; } = Program.GetPCName();
    public string Motherboard_ID { get; set; } = Program.GetMotherboardId();


}