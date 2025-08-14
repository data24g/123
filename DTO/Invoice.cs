
using System;

namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của thông tin chính (header) của một Hóa đơn.
    /// Nó được sử dụng để truyền dữ liệu giữa các tầng kiến trúc của ứng dụng.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Mã định danh duy nhất của hóa đơn (Khóa chính).
        /// </summary>
        public int InvoiceID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến bảng Customers.
        /// Sử dụng kiểu 'int?' (nullable int) để cho phép hóa đơn không có khách hàng (khách lẻ).
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến bảng Employees.
        /// Sử dụng kiểu 'int?' (nullable int) để cho phép hóa đơn không có nhân viên (ví dụ: khách hàng tự mua online).
        /// </summary>
        public int? EmployeeID { get; set; }

        /// <summary>
        /// Ngày và giờ tạo hóa đơn.
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Tổng số tiền của hóa đơn.
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}