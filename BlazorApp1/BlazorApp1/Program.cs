using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp1.Data;
using BlazorApp1.Webside;
using BlazorApp1.Userside;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<SubscriptionKey>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("/api/sample", () => new List<string> { "Value 1", "Value 2" });
app.MapPost("/api/sample", (string data) => {
    Console.WriteLine("Received data: " + data);
    return Results.Ok();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run(); 
