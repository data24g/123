
using Store_X.DAL;
using System.Data;

namespace Store_X.BLL
{
    public class CategoryBLL
    {
        // Sử dụng mẫu Singleton để đảm bảo chỉ có một thể hiện của lớp
        private static CategoryBLL _instance;
        public static CategoryBLL Instance => _instance ?? (_instance = new CategoryBLL());
        private CategoryBLL() { }

        /// <summary>
        /// Lấy về một DataTable chứa tất cả các danh mục sản phẩm.
        /// Được sử dụng để điền dữ liệu vào các ComboBox.
        /// </summary>
        /// <returns>Một DataTable với các cột CategoryID và CategoryName.</returns>
        public DataTable GetAllCategories()
        {
            // Đơn giản là gọi phương thức tương ứng từ lớp DAL
            return CategoryDAL.Instance.GetAllCategories();
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ và gọi DAL để thêm một danh mục mới, sau đó lấy về ID của nó.
        /// Chức năng này phục vụ cho việc người dùng tự gõ tên danh mục mới vào ComboBox.
        /// </summary>
        /// <param name="categoryName">Tên danh mục mới cần thêm.</param>
        /// <returns>ID của danh mục vừa được tạo. Trả về 0 nếu thêm thất bại hoặc tên không hợp lệ.</returns>
        public int AddCategoryAndGetId(string categoryName)
        {
            // --- Logic nghiệp vụ cơ bản ---
            // Không cho phép thêm tên rỗng hoặc chỉ chứa toàn khoảng trắng.
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return 0; // Trả về 0 để báo hiệu thất bại
            }

            // Gọi xuống lớp DAL để thực hiện thao tác với cơ sở dữ liệu.
            // .Trim() để loại bỏ các khoảng trắng thừa ở đầu và cuối chuỗi.
            return CategoryDAL.Instance.AddCategoryAndGetId(categoryName.Trim());
        }

        // Trong tương lai, bạn có thể thêm các phương thức BLL khác cho Category ở đây
        // Ví dụ:
        // public bool UpdateCategory(Category category) { ... }
        // public bool DeleteCategory(int categoryId) { ... }
    }
}