using Store_X.DAL;
using System.Data;

namespace Store_X.BLL
{
    public class ReportBLL
    {
        // Sử dụng mẫu Singleton
        private static ReportBLL _instance;
        public static ReportBLL Instance => _instance ?? (_instance = new ReportBLL());
        private ReportBLL() { }

        /// <summary>
        /// Lấy dữ liệu báo cáo doanh thu theo từng tháng trong năm hiện tại.
        /// </summary>
        /// <returns>Một DataTable chứa các cột Tháng và TongDoanhThu.</returns>
        public DataTable GetMonthlyRevenue()
        {
            // Lớp BLL có thể thêm logic ở đây, ví dụ: kiểm tra quyền của người dùng
            // trước khi cho phép họ xem báo cáo.
            // Hiện tại, chúng ta chỉ gọi thẳng xuống DAL.
            return ReportDAL.Instance.GetMonthlyRevenue();
        }

        /// <summary>
        /// Lấy dữ liệu báo cáo top 5 sản phẩm bán chạy nhất (dựa trên số lượng đã bán).
        /// </summary>
        /// <returns>Một DataTable chứa các cột TenSanPham và SoLuongDaBan.</returns>
        public DataTable GetTopSellingProducts()
        {
            return ReportDAL.Instance.GetTopSellingProducts();
        }

        /// <summary>
        /// Lấy dữ liệu báo cáo tổng doanh thu theo từng nhân viên.
        /// </summary>
        /// <returns>Một DataTable chứa các cột TenNhanVien và TongDoanhThu.</returns>
        public DataTable GetRevenueByEmployee()
        {
            return ReportDAL.Instance.GetRevenueByEmployee();
        }
    }
}