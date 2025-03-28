using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        string serverIp = "127.0.0.1";
        int port = 8888;

        try
        {
            using (TcpClient client = new TcpClient(serverIp, port))
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Подключено к серверу. Введите запрос (например, USD/UAH) или 'exit' для выхода:");

                while (true)
                {
                    Console.Write("Введите валюты: ");
                    string request = Console.ReadLine();
                    if (request.ToLower() == "exit") break;

                    byte[] data = Encoding.UTF8.GetBytes(request);
                    stream.Write(data, 0, data.Length);

                    byte[] buffer = new byte[256];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine("Ответ от сервера: " + response);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
