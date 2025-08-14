
using System.Collections.Generic; // QUAN TRỌNG: Thêm dòng using này

namespace Store_X
{
    /// <summary>
    /// Lớp tĩnh để lưu trữ thông tin của người dùng đang đăng nhập trong suốt phiên làm việc.
    /// </summary>
    public static class CurrentUserSession
    {

        public static int? AccountID { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }
        public static int? EmployeeID { get; set; }
        public static int? CustomerID { get; set; }

        /// <summary>
        /// Xóa toàn bộ thông tin phiên khi người dùng đăng xuất.
        /// </summary>
        public static void Clear()
        {
            AccountID = null;
            Username = null;
            Role = null;
            EmployeeID = null;
            CustomerID = null;
        }
        //public static int AccountID { get; private set; }
        //public static string Username { get; private set; }
        //public static string Role { get; private set; }
        //public static int? EmployeeID { get; private set; }
        //public static int? CustomerID { get; private set; }

        // ĐÃ THÊM: Thuộc tính để lưu danh sách các quyền
        public static List<string> Permissions { get; private set; }

        /// <summary>
        /// Gán thông tin cho phiên làm việc sau khi đăng nhập thành công.
        /// </summary>
        public static void SetSession(int accountId, string username, string role, int? employeeId, int? customerId)
        {
            AccountID = accountId;
            Username = username;
            Role = role;
            EmployeeID = employeeId;
            CustomerID = customerId;
            // Khởi tạo danh sách quyền rỗng khi bắt đầu phiên mới
            Permissions = new List<string>();
        }

        // ĐÂY LÀ PHƯƠNG THỨC BỊ THIẾU
        /// <summary>
        /// Gán danh sách các quyền cho phiên làm việc hiện tại.
        /// </summary>
        /// <param name="permissions">Danh sách các key quyền.</param>
        public static void SetPermissions(List<string> permissions)
        {
            Permissions = permissions;
        }

        /// <summary>
        /// Xóa thông tin phiên làm việc khi đăng xuất.
        /// </summary>
        public static void ClearSession()
        {
            AccountID = 0;
            Username = null;
            Role = null;
            EmployeeID = null;
            CustomerID = null;
            // Xóa cả danh sách quyền khi đăng xuất
            Permissions = null;
        }
    }
}