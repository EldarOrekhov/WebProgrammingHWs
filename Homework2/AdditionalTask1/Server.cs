using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    private static Dictionary<string, string> users = new Dictionary<string, string>();

    static void Main()
    {
        Console.WriteLine("Сервер запущен.");
        Console.WriteLine("Добавьте пользователей (логин пароль), введите 'start' для запуска сервера.");

        while (true)
        {
            Console.Write("Добавить пользователя: ");
            string input = Console.ReadLine();
            if (input.ToLower() == "start") break;

            string[] parts = input.Split(' ');
            if (parts.Length == 2)
            {
                users[parts[0]] = parts[1];
                Console.WriteLine($"Пользователь {parts[0]} добавлен.");
            }
            else
            {
                Console.WriteLine("Ошибка. Введите: логин пароль");
            }
        }

        StartServer();
    }

    public static void StartServer()
    {
        int port = 8888;
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine("Ожидание подключений...");

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
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[256];
        int bytesRead;

        try
        {
            bytesRead = stream.Read(buffer, 0, buffer.Length);
            string credentials = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            string[] parts = credentials.Split(':');

            if (parts.Length != 2 || !users.ContainsKey(parts[0]) || users[parts[0]] != parts[1])
            {
                Console.WriteLine($"Ошибка авторизации: {parts[0]}");
                byte[] errorMsg = Encoding.UTF8.GetBytes("Ошибка авторизации");
                stream.Write(errorMsg, 0, errorMsg.Length);
                client.Close();
                return;
            }

            Console.WriteLine($"Пользователь {parts[0]} авторизован.");
            byte[] successMsg = Encoding.UTF8.GetBytes("Успешная авторизация");
            stream.Write(successMsg, 0, successMsg.Length);

            bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Сообщение от {parts[0]}: {message}");

            client.Close();
        }
        catch
        {
            client.Close();
        }
    }
}
