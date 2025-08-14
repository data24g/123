using System;
using System.Linq;

namespace Store_X
{
    /// <summary>
    /// Lớp trợ giúp để xử lý mật khẩu.
    /// CẢNH BÁO: Hàm băm trong này chỉ là giả lập, KHÔNG AN TOÀN cho sản phẩm thực tế.
    /// </summary>
    public static class PasswordHelper
    {
        // Hàm "giả lập băm" - đơn giản là đảo ngược chuỗi
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;
            char[] charArray = password.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        // Hàm kiểm tra mật khẩu
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword)) return false;

            // Băm mật khẩu người dùng nhập vào và so sánh
            string hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
}