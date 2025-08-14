
namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một Vai trò người dùng trong hệ thống.
    /// Nó được sử dụng để truyền dữ liệu giữa các tầng kiến trúc của ứng dụng.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Mã định danh duy nhất của vai trò (Khóa chính).
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Tên của vai trò (ví dụ: "Admin", "Sale", "Warehouse", "Customer").
        /// </summary>
        public string RoleName { get; set; }
    }
}