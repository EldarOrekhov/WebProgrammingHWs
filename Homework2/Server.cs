using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    private static Dictionary<string, double> exchangeRates = new Dictionary<string, double>
    {
        { "USD/EUR", 0.92 }, { "EUR/USD", 1.09 },
        { "USD/UAH", 39.50 }, { "UAH/USD", 0.0253 },
        { "EUR/UAH", 42.90 }, { "UAH/EUR", 0.0233 }
    };

    static void Main()
    {
        int port = 8888;
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine("Сервер запущен...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Thread clientThread = new Thread(HandleClient);
            clientThread.Start(client);
        }
    }

    static void HandleClient(object obj)
    {
        TcpClient client = (TcpClient)obj;
        IPEndPoint clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
        Console.WriteLine($"Клиент {clientEndPoint} подключился в {DateTime.Now.ToShortTimeString()}");

        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[256];

        while (true)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                Console.WriteLine($"Запрос от {clientEndPoint}: {request}");

                string response = GetExchangeRate(request);
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                Console.WriteLine($"Ответ: {response}");
            }
            catch
            {
                break;
            }
        }

        Console.WriteLine($"Клиент {clientEndPoint} отключился в {DateTime.Now.ToShortTimeString()}");
        client.Close();
    }

    static string GetExchangeRate(string request)
    {
        if (exchangeRates.TryGetValue(request.ToUpper(), out double rate))
        {
            return $"Курс {request}: {rate}";
        }
        return "Ошибка: курс не найден!";
    }
}
