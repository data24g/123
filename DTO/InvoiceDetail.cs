
namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một dòng Chi tiết Hóa đơn.
    /// Mỗi đối tượng của lớp này tương ứng với một sản phẩm trong một hóa đơn cụ thể.
    /// </summary>
    public class InvoiceDetail
    {
        /// <summary>
        /// </summary>
        public int InvoiceDetailID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến hóa đơn (Invoice) mà chi tiết này thuộc về.
        /// </summary>
        public int InvoiceID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến sản phẩm (Product) đã được bán.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Số lượng sản phẩm đã được bán trong dòng chi tiết này.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Đơn giá của sản phẩm tại thời điểm bán.
        /// Ghi lại đơn giá ở đây là rất quan trọng, vì giá sản phẩm trong bảng Products có thể thay đổi trong tương lai.
        /// </summary>
        public decimal UnitPrice { get; set; }

        // --- Thuộc tính bổ sung (không có trong CSDL) ---
        // Thuộc tính này hữu ích khi hiển thị giỏ hàng hoặc chi tiết hóa đơn trên giao diện,
        // nhưng không cần thiết khi lưu vào CSDL.
        public string ProductName { get; set; }
    }
}