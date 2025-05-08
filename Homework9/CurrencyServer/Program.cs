using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

var app = builder.Build();
app.UseCors();

app.MapHub<CurrencyHub>("/currencyHub");

var hubContext = app.Services.GetRequiredService<IHubContext<CurrencyHub>>();
var random = new Random();
var currencies = new[] { "EUR", "UAH" };

_ = Task.Run(async () =>
{
    while (true)
    {
        var rates = currencies.ToDictionary(c => c, c => (decimal)(random.NextDouble() * (1.5 - 0.5) + 0.5));
        var json = JsonSerializer.Serialize(rates);
        await hubContext.Clients.All.SendAsync("ReceiveRates", json);
        await Task.Delay(1000);
    }
});

app.Run();
