using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        Console.WriteLine("Выберите запрос:");
        Console.WriteLine("1 - Время");
        Console.WriteLine("2 - Дата");
        Console.Write("Введите номер запроса: ");
        string request = Console.ReadLine();

        try
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 8888))
            using (NetworkStream stream = client.GetStream())
            {
                byte[] requestData = Encoding.UTF8.GetBytes(request);
                stream.Write(requestData, 0, requestData.Length);

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Ответ от сервера: " + response);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
