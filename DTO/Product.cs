
using System;

namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một Sản phẩm.
    /// Nó được sử dụng để truyền dữ liệu giữa các tầng kiến trúc của ứng dụng.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Mã định danh duy nhất của sản phẩm (Khóa chính).
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Tên của sản phẩm.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Đơn giá của sản phẩm.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Số lượng sản phẩm hiện có trong kho.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Ngày sản phẩm được thêm vào hệ thống.
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến bảng Categories (có thể là null).
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// Khóa ngoại, liên kết đến bảng Suppliers (có thể là null).
        /// </summary>
        public int? SupplierID { get; set; }


        // --- CÁC THUỘC TÍNH BỔ SUNG (KHÔNG CÓ TRONG BẢNG PRODUCTS) ---
        // Các thuộc tính này được dùng để hiển thị dữ liệu đã JOIN từ các bảng khác lên DataGridView.

        /// <summary>
        /// Tên của danh mục (lấy từ bảng Categories).
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Tên của nhà cung cấp (lấy từ bảng Suppliers).
        /// </summary>
        public string SupplierName { get; set; }
    }
}