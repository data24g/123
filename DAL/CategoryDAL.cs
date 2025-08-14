
using System.Data;
using System;

namespace Store_X.DAL
{
    public class CategoryDAL
    {
        // Sử dụng mẫu Singleton
        private static CategoryDAL _instance;
        public static CategoryDAL Instance => _instance ?? (_instance = new CategoryDAL());
        private CategoryDAL() { }

        /// <summary>
        /// Lấy về một DataTable chứa tất cả các danh mục sản phẩm từ CSDL.
        /// </summary>
        /// <returns>Một DataTable với các cột CategoryID và CategoryName.</returns>
        public DataTable GetAllCategories()
        {
            string query = "SELECT CategoryID, CategoryName FROM Categories ORDER BY CategoryName";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Thêm một danh mục mới vào CSDL và trả về ID của nó.
        /// Sử dụng SCOPE_IDENTITY() để lấy ID của dòng vừa được chèn một cách an toàn.
        /// </summary>
        /// <param name="categoryName">Tên danh mục mới.</param>
        /// <returns>ID của danh mục vừa được tạo, hoặc 0 nếu thất bại.</returns>
        public int AddCategoryAndGetId(string categoryName)
        {
            // Câu lệnh INSERT để thêm danh mục mới, sau đó
            // SELECT SCOPE_IDENTITY() để lấy ra ID (khóa chính tự tăng) của dòng vừa được chèn.
            string query = "INSERT INTO Categories (CategoryName) VALUES ( @name ); SELECT SCOPE_IDENTITY();";
            try
            {
                // ExecuteScalar là lựa chọn hoàn hảo để thực thi một câu lệnh trả về một giá trị duy nhất.
                object result = DataProvider.Instance.ExecuteScalar(query, new object[] { categoryName });

                // Chuyển đổi kết quả về kiểu int. Nếu result là null hoặc không hợp lệ, trả về 0.
                return Convert.ToInt32(result ?? 0);
            }
            catch
            {
                // Trả về 0 nếu có lỗi xảy ra (ví dụ: tên danh mục bị trùng và có ràng buộc UNIQUE)
                return 0;
            }
        }

        // Trong tương lai, bạn có thể thêm các phương thức DAL khác cho Category ở đây
        // Ví dụ: UpdateCategory, DeleteCategory...
    }
}