using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

class Server
{
    private static Dictionary<string, string> prices = new Dictionary<string, string>
    {
        {"процессор", "150$"},
        {"видеокарта", "450$"},
        {"оперативная память", "80$"},
        {"жесткий диск", "50$"},
        {"материнская плата", "100$"}
    };

    private static Dictionary<string, List<DateTime>> requestLog = new();
    private static Dictionary<string, DateTime> lastActivity = new();
    private static int maxRequestsPerHour = 10;
    private static int maxClients = 10;
    private static TimeSpan inactivityLimit = TimeSpan.FromMinutes(10);

    private static UdpClient udpServer;
    private static IPEndPoint clientEndPoint;

    static void Main()
    {
        udpServer = new UdpClient(8080);
        clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
        Console.WriteLine("Сервер запущен");

        Thread cleanupThread = new Thread(RemoveInactiveClients);
        cleanupThread.Start();

        while (true)
        {
            byte[] receivedData = udpServer.Receive(ref clientEndPoint);
            string clientKey = clientEndPoint.ToString();
            string message = Encoding.UTF8.GetString(receivedData).ToLower();

            lastActivity[clientKey] = DateTime.Now;

            if (!requestLog.ContainsKey(clientKey))
                requestLog[clientKey] = new List<DateTime>();

            requestLog[clientKey].RemoveAll(time => time < DateTime.Now.AddHours(-1));

            if (requestLog.Count > maxClients)
            {
                SendMessage(clientEndPoint, "Сервер занят. Попробуйте позже");
                continue;
            }

            if (requestLog[clientKey].Count >= maxRequestsPerHour)
            {
                SendMessage(clientEndPoint, "Превышен лимит запросов. Попробуйте позже");
                continue;
            }

            if (message == "exit")
            {
                LogClient(clientKey, "Отключился");
                requestLog.Remove(clientKey);
                lastActivity.Remove(clientKey);
                continue;
            }

            requestLog[clientKey].Add(DateTime.Now);

            string response = prices.ContainsKey(message) ?
                $"Цена на {message}: {prices[message]}" :
                "Информация о запрашиваемом товаре не найдена";

            SendMessage(clientEndPoint, response);
            LogClient(clientKey, $"Запрос: {message} | Ответ: {response}");
        }
    }

    private static void SendMessage(IPEndPoint endPoint, string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        udpServer.Send(data, data.Length, endPoint);
    }

    private static void RemoveInactiveClients()
    {
        while (true)
        {
            var now = DateTime.Now;
            var inactiveClients = lastActivity.Where(kvp => now - kvp.Value > inactivityLimit).Select(kvp => kvp.Key).ToList();
            foreach (var client in inactiveClients)
            {
                requestLog.Remove(client);
                lastActivity.Remove(client);
                LogClient(client, "Отключен за неактивность");
                Console.WriteLine($"Клиент {client} отключен за неактивность");
            }
            Thread.Sleep(60000);
        }
    }

    private static void LogClient(string client, string action)
    {
        string logEntry = $"{DateTime.Now} | Клиент: {client} | {action}";
        File.AppendAllText("server_log.txt", logEntry + Environment.NewLine);
        Console.WriteLine(logEntry);
    }
}