using System.Net.Mail;
using System.Net;

class Program
{
    static void Main()
    {
        Console.Write("Введите ваш email: ");
        string fromEmail = Console.ReadLine();

        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();

        Console.Write("Введите email получателей (через запятую): ");
        string toEmails = Console.ReadLine();

        Console.Write("Введите тему письма: ");
        string subject = Console.ReadLine();

        Console.WriteLine("Введите текст письма:");
        string body = Console.ReadLine();

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);

            foreach (var address in toEmails.Split(','))
            {
                mail.To.Add(address.Trim());
            }

            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(fromEmail, password);
            smtp.EnableSsl = true;

            smtp.Send(mail);
            Console.WriteLine("Письмо успешно отправлено");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка отправки: " + ex.Message);
        }
    }
}