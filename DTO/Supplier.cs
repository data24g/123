
using System;

namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một Nhà Cung Cấp.
    /// Nó được sử dụng để truyền dữ liệu giữa các tầng kiến trúc của ứng dụng.
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Mã định danh duy nhất của nhà cung cấp (Khóa chính).
        /// </summary>
        public int SupplierID { get; set; }

        /// <summary>
        /// Tên của nhà cung cấp.
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Địa chỉ của nhà cung cấp (có thể là null).
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số điện thoại liên hệ của nhà cung cấp (có thể là null).
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Ngày bắt đầu hợp tác với nhà cung cấp (có thể là null).
        /// Chúng ta dùng kiểu DateTime? (nullable DateTime) để nó có thể nhận giá trị null,
        /// tương ứng với cột trong CSDL cho phép NULL.
        /// </summary>
        public DateTime? CooperationDate { get; set; }
    }
}