
using Store_X.DTO;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Store_X.DAL
{
    public class ProductDAL
    {
        // Sử dụng mẫu Singleton
        private static ProductDAL _instance;
        public static ProductDAL Instance => _instance ?? (_instance = new ProductDAL());
        private ProductDAL() { }

        /// <summary>
        /// Lấy một DataTable chứa danh sách sản phẩm để hiển thị, bao gồm tên loại và tên nhà cung cấp.
        /// </summary>
        public DataTable GetProductsForDisplay()
        {
            string query = @"SELECT p.ProductID, p.ProductName, p.Price, p.StockQuantity, p.DateAdded,
                                    c.CategoryName, s.SupplierName, p.CategoryID, p.SupplierID
                             FROM Products p
                             LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                             LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một sản phẩm bằng ID, bao gồm cả tên NCC và tên Loại SP.
        /// </summary>
        public DataTable GetProductDetailsById(int productId)
        {
            string query = @"SELECT p.ProductID, p.ProductName, p.Price,
                                    c.CategoryName, s.SupplierName
                             FROM Products p
                             LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                             LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
                             WHERE p.ProductID = @id";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { productId });
        }

        /// <summary>
        /// Lấy thông tin cơ bản của một sản phẩm bằng ID.
        /// </summary>
        public DataTable GetProductById(int productId)
        {
            string query = "SELECT ProductID, ProductName, Price, StockQuantity FROM Products WHERE ProductID = @id";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { productId });
        }

        /// <summary>
        /// Thêm một sản phẩm mới vào cơ sở dữ liệu.
        /// </summary>
        public bool AddProduct(Product product)
        {
            string query = "INSERT INTO Products (ProductName, Price, StockQuantity, DateAdded, CategoryID, SupplierID) VALUES ( @name , @price , @stock , @date , @catId , @supId )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {
                product.ProductName,
                product.Price,
                product.StockQuantity,
                product.DateAdded,
                product.CategoryID,
                product.SupplierID
            });
            return result > 0;
        }

        /// <summary>
        /// Cập nhật thông tin của một sản phẩm trong cơ sở dữ liệu.
        /// </summary>
        public bool UpdateProduct(Product product)
        {
            // Câu lệnh UPDATE này không cập nhật cột DateAdded, giữ lại ngày thêm gốc.
            string query = @"UPDATE Products 
                             SET ProductName = @name, 
                                 Price = @price, 
                                 StockQuantity = @stock, 
                                 CategoryID = @catId, 
                                 SupplierID = @supId 
                             WHERE ProductID = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {
                product.ProductName,
                product.Price,
                product.StockQuantity,
                product.CategoryID,
                product.SupplierID,
                product.ProductID
            });
            return result > 0;
        }

        /// <summary>
        /// Xóa một sản phẩm khỏi cơ sở dữ liệu.
        /// </summary>
        public bool DeleteProduct(int productId)
        {
            // Thao tác này có thể thất bại nếu có ràng buộc khóa ngoại từ InvoiceDetails.
            string query = "DELETE FROM Products WHERE ProductID = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { productId });
            return result > 0;
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo tên và/hoặc ID danh mục.
        /// </summary>
        public DataTable SearchProducts(string name, int categoryId)
        {
            // Xây dựng câu lệnh SQL động để xử lý các trường hợp tìm kiếm khác nhau
            StringBuilder queryBuilder = new StringBuilder(@"
                SELECT p.ProductID, p.ProductName, p.Price, p.StockQuantity, c.CategoryName
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                WHERE 1=1"); // Mệnh đề WHERE 1=1 để dễ dàng nối thêm điều kiện

            List<object> parameters = new List<object>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                queryBuilder.Append(" AND p.ProductName LIKE @name");
                parameters.Add("%" + name + "%"); // Thêm % để tìm kiếm gần đúng
            }

            if (categoryId > 0)
            {
                queryBuilder.Append(" AND p.CategoryID = @catId");
                parameters.Add(categoryId);
            }

            return DataProvider.Instance.ExecuteQuery(queryBuilder.ToString(), parameters.ToArray());
        }
    }
}