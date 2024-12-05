using System.Security.Cryptography;
using System.Text;

namespace Business.Helpers
{
    public class HashingHelper
    {
        // Şifreyi hash'leme metodu
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Şifreyi hash'leyip Base64 formatında geri döner
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Şifre doğrulama metodu
        public static bool VerifyPassword(string hashedPassword, string enteredPassword)
        {
            // Girilen şifreyi hash'leyip, mevcut hash ile karşılaştırır
            var hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedPassword == hashedEnteredPassword;
        }
    }
}
