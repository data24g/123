
using Store_X.DAL;
using Store_X.DTO;
using System.Data;

namespace Store_X.BLL
{
    public class ProductBLL
    {
        // Sử dụng mẫu Singleton
        private static ProductBLL _instance;
        public static ProductBLL Instance => _instance ?? (_instance = new ProductBLL());
        private ProductBLL() { }

        /// <summary>
        /// Lấy một DataTable chứa danh sách sản phẩm để hiển thị, bao gồm tên loại và tên nhà cung cấp.
        /// </summary>
        public DataTable GetProductsForDisplay()
        {
            return ProductDAL.Instance.GetProductsForDisplay();
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một sản phẩm bằng ID, bao gồm tên NCC và tên Loại SP.
        /// Được sử dụng trong FrmWarehouse để tự động điền thông tin.
        /// </summary>
        public DataTable GetProductDetailsById(int productId)
        {
            if (productId <= 0) return null;
            return ProductDAL.Instance.GetProductDetailsById(productId);
        }

        /// <summary>
        /// Lấy thông tin cơ bản của một sản phẩm bằng ID.
        /// Được sử dụng trong FrmSale.
        /// </summary>
        public DataRow GetProductById(int productId)
        {
            if (productId <= 0) return null;
            DataTable dt = ProductDAL.Instance.GetProductById(productId);
            return (dt != null && dt.Rows.Count > 0) ? dt.Rows[0] : null;
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để thêm một sản phẩm mới.
        /// </summary>
        public bool AddProduct(Product product)
        {
            // --- Logic nghiệp vụ ---
            // Tên, giá, loại và NCC là bắt buộc. Giá không được âm.
            if (product == null
                || string.IsNullOrWhiteSpace(product.ProductName)
                || product.Price < 0
                || product.CategoryID == null
                || product.SupplierID == null)
            {
                return false;
            }

            return ProductDAL.Instance.AddProduct(product);
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để cập nhật một sản phẩm.
        /// </summary>
        public bool UpdateProduct(Product product)
        {
            // --- Logic nghiệp vụ ---
            // Phải có ID để biết cập nhật sản phẩm nào.
            if (product == null
                || product.ProductID <= 0
                || string.IsNullOrWhiteSpace(product.ProductName)
                || product.Price < 0
                || product.CategoryID == null
                || product.SupplierID == null)
            {
                return false;
            }

            return ProductDAL.Instance.UpdateProduct(product);
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để xóa một sản phẩm.
        /// </summary>
        public bool DeleteProduct(int productId)
        {
            // --- Logic nghiệp vụ ---
            if (productId <= 0)
            {
                return false;
            }

            // Logic phức tạp hơn có thể được thêm ở đây, ví dụ:
            // Kiểm tra xem sản phẩm có nằm trong hóa đơn nào không trước khi cho phép xóa.
            // Hiện tại, logic này được xử lý bởi ràng buộc khóa ngoại trong CSDL.

            return ProductDAL.Instance.DeleteProduct(productId);
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo tên và/hoặc ID danh mục.
        /// </summary>
        /// <param name="name">Tên sản phẩm (có thể là một phần).</param>
        /// <param name="categoryId">ID của danh mục (0 nếu không lọc theo danh mục).</param>
        public DataTable SearchProducts(string name, int categoryId)
        {
            // Trim() để loại bỏ các khoảng trắng thừa
            return ProductDAL.Instance.SearchProducts(name.Trim(), categoryId);
        }
    }
}