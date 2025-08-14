
using Store_X.DTO;
using System.Data;
using System;

namespace Store_X.DAL
{
    public class SupplierDAL
    {
        // Sử dụng mẫu Singleton
        private static SupplierDAL _instance;
        public static SupplierDAL Instance => _instance ?? (_instance = new SupplierDAL());
        private SupplierDAL() { }

        /// <summary>
        /// Lấy về một DataTable chứa thông tin của tất cả các nhà cung cấp.
        /// </summary>
        public DataTable GetAllSuppliers()
        {
            string query = "SELECT SupplierID, SupplierName, Address, Phone, CooperationDate FROM Suppliers ORDER BY SupplierName";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Thêm một nhà cung cấp mới và trả về ID của nhà cung cấp vừa được tạo.
        /// </summary>
        public int AddSupplierAndGetId(string supplierName)
        {
            // Mặc định ngày hợp tác là ngày hiện tại khi thêm nhanh
            string query = "INSERT INTO Suppliers (SupplierName, CooperationDate) VALUES ( @name , GETDATE()); SELECT SCOPE_IDENTITY();";
            try
            {
                object result = DataProvider.Instance.ExecuteScalar(query, new object[] { supplierName });
                return Convert.ToInt32(result ?? 0);
            }
            catch
            {
                return 0; // Trả về 0 nếu có lỗi (ví dụ: tên bị trùng)
            }
        }

        /// <summary>
        /// Thêm một nhà cung cấp mới với đầy đủ thông tin.
        /// </summary>
        public bool AddSupplier(Supplier supplier)
        {
            string query = "INSERT INTO Suppliers (SupplierName, Address, Phone, CooperationDate) VALUES ( @name , @address , @phone , @date )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {
                supplier.SupplierName,
                supplier.Address,
                supplier.Phone,
                (object)supplier.CooperationDate ?? DBNull.Value // Xử lý an toàn nếu ngày tháng là null
            });
            return result > 0;
        }

        /// <summary>
        /// Cập nhật thông tin của một nhà cung cấp.
        /// </summary>
        public bool UpdateSupplier(Supplier supplier)
        {
            string query = @"UPDATE Suppliers 
                             SET SupplierName = @name, 
                                 Address = @address, 
                                 Phone = @phone, 
                                 CooperationDate = @date 
                             WHERE SupplierID = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {
                supplier.SupplierName,
                supplier.Address,
                supplier.Phone,
                (object)supplier.CooperationDate ?? DBNull.Value, // Xử lý an toàn nếu ngày tháng là null
                supplier.SupplierID
            });
            return result > 0;
        }

        /// <summary>
        /// Xóa một nhà cung cấp khỏi cơ sở dữ liệu.
        /// </summary>
        public bool DeleteSupplier(int supplierId)
        {
            // Thao tác này có thể thất bại nếu có ràng buộc khóa ngoại từ bảng Products.
            string query = "DELETE FROM Suppliers WHERE SupplierID = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { supplierId });
            return result > 0;
        }
    }
}