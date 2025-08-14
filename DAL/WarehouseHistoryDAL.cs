
using System.Data;
using System.Collections.Generic; // Cần cho Dictionary
using System;

namespace Store_X.DAL
{
    public class WarehouseHistoryDAL
    {
        // Sử dụng mẫu Singleton
        private static WarehouseHistoryDAL _instance;
        public static WarehouseHistoryDAL Instance => _instance ?? (_instance = new WarehouseHistoryDAL());
        private WarehouseHistoryDAL() { }

        /// <summary>
        /// Lấy về một DataTable chứa toàn bộ lịch sử giao dịch kho (nhập và xuất).
        /// </summary>
        /// <returns>DataTable chứa lịch sử kho.</returns>
        public DataTable GetAllWarehouseHistory()
        {
            // Câu lệnh JOIN các bảng để lấy thông tin tường minh
            string query = @"SELECT 
                                h.HistoryID, 
                                p.ProductName, 
                                e.FullName AS EmployeeName, 
                                h.TransactionType, 
                                h.Quantity, 
                                h.TransactionDate 
                             FROM WarehouseHistory h
                             LEFT JOIN Products p ON h.ProductID = p.ProductID
                             LEFT JOIN Employees e ON h.EmployeeID = e.EmployeeID
                             ORDER BY h.TransactionDate DESC"; // Sắp xếp theo ngày mới nhất lên đầu

            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy lịch sử các giao dịch NHẬP kho gần đây (ví dụ: 50 giao dịch cuối).
        /// </summary>
        public DataTable GetRecentImportHistory()
        {
            string query = @"SELECT TOP 50
                                h.HistoryID, 
                                p.ProductName, 
                                e.FullName AS EmployeeName, 
                                h.TransactionType, 
                                h.Quantity, 
                                h.TransactionDate 
                             FROM WarehouseHistory h
                             LEFT JOIN Products p ON h.ProductID = p.ProductID
                             LEFT JOIN Employees e ON h.EmployeeID = e.EmployeeID
                             WHERE h.TransactionType = N'Nhập'
                             ORDER BY h.TransactionDate DESC";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Gọi Stored Procedure để tạo một giao dịch kho mới (Nhập hoặc Xuất).
        /// </summary>
        /// <returns>True nếu giao dịch thành công.</returns>
        public bool CreateWarehouseTransaction(int productId, int employeeId, string transactionType, int quantity, string notes)
        {
            try
            {
                // Gọi phương thức thực thi Stored Procedure trong DataProvider
                // Chúng ta dùng ExecuteNonQuery vì Stored Procedure này không trả về giá trị
                DataProvider.Instance.ExecuteNonQueryStoredProcedure("sp_CreateWarehouseTransaction", new Dictionary<string, object>
                {
                    { "@ProductID", productId },
                    { "@EmployeeID", employeeId },
                    { "@TransactionType", transactionType },
                    { "@Quantity", quantity },
                    { "@Notes", (object)notes ?? DBNull.Value }
                });
                return true; // Nếu không có lỗi nào được ném ra, coi như thành công
            }
            catch
            {
                // Nếu Stored Procedure bị lỗi và ROLLBACK, C# sẽ bắt được exception
                return false;
            }
        }
    }
}