using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorApp1.Userside;

public class CallApi
{
    static async Task Main(string[] args)
    {
        await CallApp();
    }

    public static async Task CallApp()
    {
        var httpClient = new HttpClient();

        // Replace the URL with your BlazorApp's URL
        string apiUrl = "https://localhost:7168/api/sample";

        // Call the GET endpoint
        try
        {
            var result = await httpClient.GetFromJsonAsync<List<string>>(apiUrl);
            Console.WriteLine("GET Endpoint Result:");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"HTTP Request Exception: {e.Message}");
        }

        // Call the POST endpoint
        string dataToSend = "Data from C# Application";
        await httpClient.PostAsJsonAsync(apiUrl, dataToSend);
        Console.WriteLine("\nPOST Endpoint called successfully.");
    }
}
