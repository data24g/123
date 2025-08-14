
using System; // Cần thêm để sử dụng DateTime

namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một Khách hàng.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Mã định danh duy nhất của khách hàng (Khóa chính).
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Tên của khách hàng.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Số điện thoại liên hệ của khách hàng.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Địa chỉ của khách hàng (có thể là null).
        /// << ĐÃ THÊM >>
        /// </summary>
        public string Address { get; set; }

        // Ghi chú: Thuộc tính CreateDate nên nằm trong đối tượng Account 
        // vì nó liên quan đến ngày tạo TÀI KHOẢN, không phải ngày tạo HỒ SƠ khách hàng.
        // Tuy nhiên, để sửa lỗi biên dịch trước mắt, chúng ta tạm thời thêm vào đây.
        // Trong một thiết kế lý tưởng, bạn sẽ gán CreateDate cho đối tượng Account.
        // public DateTime CreateDate { get; set; }
    }
}