
using System;

namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một bản ghi Lịch sử Kho.
    /// Mỗi đối tượng của lớp này tương ứng với một giao dịch Nhập hoặc Xuất kho.
    /// </summary>
    public class WarehouseHistory
    {
        /// <summary>
        /// Mã định danh duy nhất của giao dịch (Khóa chính).
        /// </summary>
        public int HistoryID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến sản phẩm (Product) đã được giao dịch.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến nhân viên (Employee) đã thực hiện giao dịch.
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Loại giao dịch (ví dụ: "Nhập" hoặc "Xuất").
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Số lượng sản phẩm trong giao dịch.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Ngày và giờ giao dịch được thực hiện.
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Ghi chú cho giao dịch (có thể là null).
        /// </summary>
        public string Notes { get; set; }


        // --- CÁC THUỘC TÍNH BỔ SUNG (KHÔNG CÓ TRONG BẢNG) ---
        // Được dùng để hiển thị dữ liệu đã JOIN từ các bảng khác.

        /// <summary>
        /// Tên của sản phẩm (lấy từ bảng Products).
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Tên của nhân viên thực hiện (lấy từ bảng Employees).
        /// </summary>
        public string EmployeeName { get; set; }
    }
}