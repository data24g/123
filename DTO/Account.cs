
using System;

namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một Tài khoản người dùng.
    /// Nó được sử dụng để truyền dữ liệu giữa các tầng kiến trúc của ứng dụng.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Mã định danh duy nhất của tài khoản (Khóa chính).
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// Tên đăng nhập của người dùng.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Chuỗi mật khẩu đã được băm (hashed) để lưu trữ an toàn.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Ngày tạo tài khoản.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Trạng thái của tài khoản (true = đang hoạt động, false = đã bị khóa).
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến bảng Employees (có thể là null).
        /// </summary>
        public int? EmployeeID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến bảng Customers (có thể là null).
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến bảng Roles (có thể là null, đặc biệt khi tài khoản đang chờ duyệt).
        /// </summary>
        public int? RoleID { get; set; }

        /// <summary>
        /// Tên vai trò mà người dùng yêu cầu khi đăng ký (dùng cho chức năng Role Management).
        /// </summary>
        public string RequestedRole { get; set; }
    }
}