
namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một Danh mục sản phẩm.
    /// Nó được sử dụng để truyền dữ liệu giữa các tầng kiến trúc của ứng dụng.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Mã định danh duy nhất của danh mục (Khóa chính).
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Tên của danh mục.
        /// </summary>
        public string CategoryName { get; set; }
    }
}