using System.Net;

class Program
{
    static void Main()
    {
        string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/78/Image.jpg";

        using (WebClient client = new WebClient())
        {
            try
            {
                client.DownloadFile(imageUrl, @"D:\image.jpg");
                Console.WriteLine("Изображение скачано");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при скачивании: " + ex.Message);
            }
        }
    }
}
