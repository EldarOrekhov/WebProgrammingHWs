using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/currencyHub")
            .WithAutomaticReconnect()
            .Build();

        connection.On<string>("ReceiveRates", (json) =>
        {
            var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(json)!;
            Console.Clear();
            Console.WriteLine("Текущий курс валют:\n");
            foreach (var rate in rates)
            {
                Console.WriteLine($"{rate.Key}/USD: {rate.Value:F4}");
            }
        });

        await connection.StartAsync();
        Console.WriteLine("Подключено к серверу. Ожидание обновления курсов...");
        await Task.Delay(-1);
    }
}
