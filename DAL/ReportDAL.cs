
using System.Data;

namespace Store_X.DAL
{
    public class ReportDAL
    {
        // Sử dụng mẫu Singleton
        private static ReportDAL _instance;
        public static ReportDAL Instance => _instance ?? (_instance = new ReportDAL());
        private ReportDAL() { }

        /// <summary>
        /// Lấy dữ liệu báo cáo doanh thu theo từng tháng trong năm hiện tại.
        /// </summary>
        /// <returns>Một DataTable chứa các cột Tháng (Month) và TongDoanhThu (TotalRevenue).</returns>
        public DataTable GetMonthlyRevenue()
        {
            // Câu lệnh SQL này nhóm các hóa đơn theo tháng của năm hiện tại và tính tổng tiền.
            string query = @"
                SELECT 
                    MONTH(InvoiceDate) AS Thang,
                    SUM(TotalAmount) AS TongDoanhThu
                FROM 
                    Invoices
                WHERE 
                    YEAR(InvoiceDate) = YEAR(GETDATE())
                GROUP BY 
                    MONTH(InvoiceDate)
                ORDER BY 
                    Thang ASC";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy dữ liệu báo cáo top 5 sản phẩm bán chạy nhất (dựa trên tổng số lượng đã bán).
        /// </summary>
        /// <returns>Một DataTable chứa các cột TenSanPham (ProductName) và SoLuongDaBan (TotalQuantitySold).</returns>
        public DataTable GetTopSellingProducts()
        {
            // Câu lệnh này tính tổng số lượng bán ra của mỗi sản phẩm, sau đó sắp xếp giảm dần và lấy 5 sản phẩm đầu tiên.
            string query = @"
                SELECT TOP 5
                    p.ProductName AS TenSanPham,
                    SUM(id.Quantity) AS SoLuongDaBan
                FROM 
                    InvoiceDetails id
                JOIN 
                    Products p ON id.ProductID = p.ProductID
                GROUP BY 
                    p.ProductName
                ORDER BY 
                    SoLuongDaBan DESC";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy dữ liệu báo cáo tổng doanh thu theo từng nhân viên.
        /// </summary>
        /// <returns>Một DataTable chứa các cột TenNhanVien (EmployeeName) và TongDoanhThu (TotalRevenue).</returns>
        public DataTable GetRevenueByEmployee()
        {
            // Câu lệnh này nhóm các hóa đơn theo nhân viên và tính tổng doanh thu của mỗi người.
            string query = @"
                SELECT 
                    e.FullName AS TenNhanVien,
                    SUM(i.TotalAmount) AS TongDoanhThu
                FROM 
                    Invoices i
                JOIN 
                    Employees e ON i.EmployeeID = e.EmployeeID
                GROUP BY 
                    e.FullName
                ORDER BY 
                    TongDoanhThu DESC";

            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}