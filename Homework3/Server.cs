using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

class Dish
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TimeToCook { get; set; }
}

class Order
{
    public int Id { get; set; }
    public string RestaurantName { get; set; }
    public Dish Dish { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } = "В процессе";
}

class Server
{
    private static TcpListener listener;
    private static ConcurrentDictionary<int, Order> orders = new();
    private static BlockingCollection<Order> orderQueue = new();
    private static List<Dish> menu = new()
    {
        new Dish { Id = 1, Name = "Пицца", TimeToCook = 30 },
        new Dish { Id = 2, Name = "Бургер", TimeToCook = 20 },
        new Dish { Id = 3, Name = "Салат", TimeToCook = 10 }
    };
    private static int IdCounter = 1;
    private static JsonSerializerOptions jsonOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true
    };

    static void Main()
    {
        listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Сервер запущен");

        Thread orderProcessor = new Thread(ProcessOrders);
        orderProcessor.Start();

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            new Thread(() => HandleClient(client)).Start();
        }
    }

    static void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[4096];

        while (true)
        {
            int byteCount = stream.Read(buffer, 0, buffer.Length);
            if (byteCount == 0) break;

            string request = Encoding.UTF8.GetString(buffer, 0, byteCount);
            var command = JsonSerializer.Deserialize<Dictionary<string, string>>(request);

            string response = "";

            switch (command["action"])
            {
                case "newOrder":
                    int dishId = int.Parse(command["dishId"]);
                    int quantity = int.Parse(command["quantity"]);
                    Dish selectedDish = menu.Find(d => d.Id == dishId);
                    if (selectedDish == null)
                    {
                        response = "Блюдо не найдено";
                        break;
                    }
                    var order = new Order
                    {
                        Id = IdCounter++,
                        RestaurantName = command["restaurant"],
                        Dish = new Dish
                        {
                            Id = selectedDish.Id,
                            Name = selectedDish.Name,
                            TimeToCook = selectedDish.TimeToCook * quantity
                        },
                        Quantity = quantity
                    };
                    orders[order.Id] = order;
                    orderQueue.Add(order);
                    response = $"Заказ принят. ID: {order.Id}";
                    break;

                case "checkStatus":
                    int id = int.Parse(command["orderId"]);
                    if (orders.TryGetValue(id, out var foundOrder))
                        response = JsonSerializer.Serialize(new
                        {
                            ID = foundOrder.Id,
                            Ресторан = foundOrder.RestaurantName,
                            Блюдо = foundOrder.Dish.Name,
                            Количество = foundOrder.Quantity,
                            Статус = foundOrder.Status
                        }, jsonOptions);
                    else
                        response = "Заказ не найден";
                    break;

                case "allStatuses":
                    var list = new List<object>();
                    foreach (var o in orders.Values)
                    {
                        list.Add(new { ID = o.Id, Ресторан = o.RestaurantName, Блюдо = o.Dish.Name, Количество = o.Quantity, Статус = o.Status });
                    }
                    response = JsonSerializer.Serialize(list, jsonOptions);
                    break;

                case "cancelOrder":
                    int cancelId = int.Parse(command["orderId"]);
                    if (orders.TryGetValue(cancelId, out var orderToCancel)){
                        if (orderToCancel.Status != "Готов")
                        {
                            orderToCancel.Status = "Отменен";
                            response = "Заказ отменен";
                        }
                        else
                            response = "Заказ уже готов";
                    }
                    else
                        response = "Заказ не найден";
                    break;
            }

            byte[] respBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(respBytes, 0, respBytes.Length);
        }
    }

    static void ProcessOrders()
    {
        while (true)
        {
            foreach (var order in orders.Values)
            {
                if (order.Status == "В процессе")
                {
                    TimeSpan elapsed = DateTime.Now - order.CreatedAt;
                    if (elapsed.TotalSeconds >= order.Dish.TimeToCook)
                        order.Status = "Готов";
                }
            }
            Thread.Sleep(1000);
        }
    }
}