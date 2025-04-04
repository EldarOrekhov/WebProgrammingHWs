using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        UdpClient udpClient = new UdpClient();
        udpClient.Connect("127.0.0.1", 8080);

        Console.WriteLine("Введите название комплектующего (например, процессор), либо 'exit' для выхода:");

        while (true)
        {
            string input = Console.ReadLine();
            byte[] requestData = Encoding.UTF8.GetBytes(input);
            udpClient.Send(requestData, requestData.Length);

            if (input.ToLower() == "exit") break;

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] response = udpClient.Receive(ref serverEndPoint);
            Console.WriteLine("Ответ: " + Encoding.UTF8.GetString(response));
        }
    }
}
