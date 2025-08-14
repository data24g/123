
using Store_X.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Store_X.DAL
{
    public class InvoiceDAL
    {
        // Sử dụng mẫu Singleton
        private static InvoiceDAL _instance;
        public static InvoiceDAL Instance => _instance ?? (_instance = new InvoiceDAL());
        private InvoiceDAL() { }

        /// <summary>
        /// Gọi Stored Procedure để tạo một hóa đơn mới (bao gồm header và details) trong một transaction.
        /// </summary>
        /// <returns>ID của hóa đơn mới nếu thành công, ngược lại trả về 0.</returns>
        public int CreateInvoice(Invoice invoiceHeader, List<InvoiceDetail> invoiceDetailsList)
        {
            // Tạo một DataTable từ danh sách chi tiết để truyền vào Stored Procedure
            DataTable detailsTable = new DataTable();
            detailsTable.Columns.Add("ProductID", typeof(int));
            detailsTable.Columns.Add("Quantity", typeof(int));
            detailsTable.Columns.Add("UnitPrice", typeof(decimal));

            foreach (var item in invoiceDetailsList)
            {
                detailsTable.Rows.Add(item.ProductID, item.Quantity, item.UnitPrice);
            }

            try
            {
                // Gọi phương thức đặc biệt trong DataProvider để thực thi Stored Procedure
                object result = DataProvider.Instance.ExecuteScalarStoredProcedure("sp_CreateInvoice", new Dictionary<string, object>
                {
                    { "@EmployeeID", (object)invoiceHeader.EmployeeID ?? DBNull.Value },
                    { "@CustomerID", (object)invoiceHeader.CustomerID ?? DBNull.Value },
                    { "@InvoiceDate", invoiceHeader.InvoiceDate },
                    { "@TotalAmount", invoiceHeader.TotalAmount },
                    { "@Details", detailsTable } // Truyền vào DataTable
                });

                return Convert.ToInt32(result ?? 0);
            }
            catch
            {
                // Nếu Stored Procedure ném lỗi (ví dụ: tồn kho không đủ), C# sẽ bắt được ở đây
                return 0;
            }
        }

        /// <summary>
        /// Lấy 20 hóa đơn gần đây nhất.
        /// </summary>
        public DataTable GetRecentInvoices()
        {
            string query = @"SELECT TOP 20 i.InvoiceID, i.InvoiceDate, i.TotalAmount, 
                                    ISNULL(c.CustomerName, 'Khách lẻ') AS CustomerName, 
                                    e.FullName AS EmployeeName
                             FROM Invoices i
                             LEFT JOIN Customers c ON i.CustomerID = c.CustomerID
                             LEFT JOIN Employees e ON i.EmployeeID = e.EmployeeID
                             ORDER BY i.InvoiceDate DESC";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy danh sách hóa đơn trong một khoảng thời gian.
        /// </summary>
        public DataTable GetInvoicesByDateRange(DateTime fromDate, DateTime toDate)
        {
            string query = @"SELECT i.InvoiceID, i.InvoiceDate, i.TotalAmount, 
                                     ISNULL(c.CustomerName, 'Khách lẻ') AS CustomerName, 
                                     e.FullName AS EmployeeName
                              FROM Invoices i
                              LEFT JOIN Customers c ON i.CustomerID = c.CustomerID
                              LEFT JOIN Employees e ON i.EmployeeID = e.EmployeeID
                              WHERE i.InvoiceDate BETWEEN @fromDate AND @toDate
                              ORDER BY i.InvoiceDate DESC";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        }

        /// <summary>
        /// Lấy thông tin chính (header) của một hóa đơn bằng ID.
        /// </summary>
        public DataTable GetInvoiceHeaderById(int invoiceId)
        {
            string query = @"SELECT i.InvoiceID, i.InvoiceDate, i.TotalAmount, 
                                     ISNULL(c.CustomerName, 'Khách lẻ') AS CustomerName, 
                                     e.FullName AS EmployeeName
                              FROM Invoices i
                              LEFT JOIN Customers c ON i.CustomerID = c.CustomerID
                              LEFT JOIN Employees e ON i.EmployeeID = e.EmployeeID
                              WHERE i.InvoiceID = @id";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { invoiceId });
        }

        /// <summary>
        /// Lấy danh sách chi tiết (các sản phẩm) của một hóa đơn bằng ID.
        /// </summary>
        public DataTable GetInvoiceDetailsById(int invoiceId)
        {
            string query = @"SELECT d.ProductID, p.ProductName, d.Quantity, d.UnitPrice, (d.Quantity * d.UnitPrice) AS TotalLineAmount
                             FROM InvoiceDetails d
                             JOIN Products p ON d.ProductID = p.ProductID
                             WHERE d.InvoiceID = @id";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { invoiceId });
        }

        /// <summary>
        /// Lấy danh sách hóa đơn của một khách hàng cụ thể.
        /// </summary>
        public DataTable GetInvoicesByCustomerId(int customerId)
        {
            string query = @"SELECT InvoiceID, InvoiceDate, TotalAmount 
                              FROM Invoices 
                              WHERE CustomerID = @id 
                              ORDER BY InvoiceDate DESC";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { customerId });
        }

        /// <summary>
        /// Lấy dữ liệu thống kê trong một ngày.
        /// </summary>
        public DataTable GetDailyStatistics(DateTime date)
        {
            string query = @"SELECT COUNT(*) AS InvoiceCount, SUM(TotalAmount) AS TotalRevenue 
                             FROM Invoices 
                             WHERE CONVERT(date, InvoiceDate) = @date";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { date.Date });
        }
    }
}