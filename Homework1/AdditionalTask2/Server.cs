using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

class Server
{
    private static List<TcpClient> clients = new List<TcpClient>();

    static void Main()
    {
        int port = 8888;
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine("Сервер запущен...");

        Thread clientThread = new Thread(AcceptClients);
        clientThread.Start(listener);

        while (true)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            BroadcastTime(currentTime);
            Thread.Sleep(1000);
        }
    }

    static void AcceptClients(object obj)
    {
        TcpListener listener = (TcpListener)obj;
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            lock (clients)
            {
                clients.Add(client);
            }
            Console.WriteLine("Клиент подключен");
        }
    }

    static void BroadcastTime(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);

        lock (clients)
        {
            for (int i = clients.Count - 1; i >= 0; i--)
            {
                try
                {
                    NetworkStream stream = clients[i].GetStream();
                    stream.Write(data, 0, data.Length);
                }
                catch
                {
                    Console.WriteLine("Клиент отключен");
                    clients.RemoveAt(i);
                }
            }
        }
    }
}
