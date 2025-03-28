using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        Console.WriteLine("Нажмите на любую кнопку для подключения к серверу");
        Console.ReadLine();
        ConnectToServer();
    }

    static void ConnectToServer()
    {
        string serverIp = "127.0.0.1";
        int port = 8888;

        try
        {
            using (TcpClient client = new TcpClient(serverIp, port))
            using (NetworkStream stream = client.GetStream())
            {
                Console.Write("Введите логин: ");
                string username = Console.ReadLine();

                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();

                string credentials = $"{username}:{password}";
                byte[] data = Encoding.UTF8.GetBytes(credentials);
                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Ответ сервера: " + response);

                if (response != "Успешная авторизация")
                {
                    return;
                }

                Console.Write("Введите сообщение: ");
                string message = Console.ReadLine();
                data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
