using System.Security.Cryptography;
using System.Text;

namespace Business.Helpers
{
    public class NotificationHelper
    {
        public static void SendSmsNotification(string phoneNumber, string message)
        {
            Console.WriteLine($"[SMS] Gönderilen Numara: {phoneNumber}, Mesaj: {message}");
        }

        public static void SendEmailNotification(string email, string message)
        {
            Console.WriteLine($"[E-posta] Gönderilen Adres: {email}, Mesaj: {message}");
        }

        public static void SendNotification(string contact, string message, bool isEmail = true)
        {
            if (isEmail)
            {
                SendEmailNotification(contact, message);
            }
            else
            {
                SendSmsNotification(contact, message);
            }
        }
    }
}
