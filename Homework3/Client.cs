using System.Net.Sockets;
using System.Text;
using System.Text.Json;

class Client
{
    static void Main()
    {
        TcpClient client = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = client.GetStream();

        while (true)
        {
            Console.WriteLine("1 - Новый заказ\n2 - Проверить статус заказа\n3 - Статусы всех заказов\n4 - Отмена заказа\n0 - Выход");
            string choice = Console.ReadLine();
            if (choice == "0") break;

            Dictionary<string, string> command = new();
            switch (choice)
            {
                case "1":
                    Console.Write("Название ресторана: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Выберите блюдо: 1 - Пицца, 2 - Бургер, 3 - Салат");
                    string dishId = Console.ReadLine();
                    Console.Write("Количество: ");
                    string quantity = Console.ReadLine();
                    command["action"] = "newOrder";
                    command["restaurant"] = name;
                    command["dishId"] = dishId;
                    command["quantity"] = quantity;
                    break;

                case "2":
                    Console.Write("ID заказа: ");
                    string id = Console.ReadLine();
                    command["action"] = "checkStatus";
                    command["orderId"] = id;
                    break;

                case "3":
                    command["action"] = "allStatuses";
                    break;

                case "4":
                    Console.Write("ID заказа: ");
                    string cancelId = Console.ReadLine();
                    command["action"] = "cancelOrder";
                    command["orderId"] = cancelId;
                    break;

                default:
                    continue;
            }

            string json = JsonSerializer.Serialize(command);
            byte[] data = Encoding.UTF8.GetBytes(json);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[4096];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            Console.WriteLine("Ответ: " + Encoding.UTF8.GetString(buffer, 0, bytesRead));
            Console.WriteLine();
        }
        stream.Close();
        client.Close();
    }
}
