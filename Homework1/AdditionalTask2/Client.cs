using System;
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
                byte[] buffer = new byte[256];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string receivedTime = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.Clear();
                    Console.WriteLine("Текущее время: " + receivedTime);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
