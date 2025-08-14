
using Store_X.DAL;
using System.Data;

namespace Store_X.BLL
{
    public class WarehouseHistoryBLL
    {
        // Sử dụng mẫu Singleton
        private static WarehouseHistoryBLL _instance;
        public static WarehouseHistoryBLL Instance => _instance ?? (_instance = new WarehouseHistoryBLL());
        private WarehouseHistoryBLL() { }

        /// <summary>
        /// Lấy về một DataTable chứa toàn bộ lịch sử giao dịch kho (nhập và xuất).
        /// Dữ liệu đã được JOIN để lấy tên sản phẩm và tên nhân viên.
        /// </summary>
        /// <returns>DataTable chứa lịch sử kho.</returns>
        public DataTable GetAllWarehouseHistory()
        {
            return WarehouseHistoryDAL.Instance.GetAllWarehouseHistory();
        }

        /// <summary>
        /// Lấy lịch sử các giao dịch NHẬP kho gần đây.
        /// Dùng cho FrmWarehouse.
        /// </summary>
        public DataTable GetRecentImportHistory()
        {
            // Giả sử bạn có phương thức này trong DAL
            return WarehouseHistoryDAL.Instance.GetRecentImportHistory();
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ và gọi DAL để tạo một giao dịch kho mới (Nhập hoặc Xuất).
        /// Thao tác này sẽ cập nhật số lượng tồn kho và ghi lại lịch sử trong một transaction duy nhất.
        /// </summary>
        /// <param name="productId">ID của sản phẩm.</param>
        /// <param name="employeeId">ID của nhân viên thực hiện.</param>
        /// <param name="transactionType">Loại giao dịch ("Nhập" hoặc "Xuất").</param>
        /// <param name="quantity">Số lượng.</param>
        /// <param name="notes">Ghi chú cho giao dịch.</param>
        /// <returns>True nếu giao dịch thành công, ngược lại False.</returns>
        public bool CreateWarehouseTransaction(int productId, int employeeId, string transactionType, int quantity, string notes)
        {
            // --- Logic nghiệp vụ ---
            // Các ID phải hợp lệ, số lượng phải lớn hơn 0, và loại giao dịch phải là "Nhập" hoặc "Xuất".
            if (productId <= 0 || employeeId <= 0 || quantity <= 0 || (transactionType != "Nhập" && transactionType != "Xuất"))
            {
                return false;
            }

            // Gọi xuống DAL để thực thi Stored Procedure, đảm bảo an toàn dữ liệu
            return WarehouseHistoryDAL.Instance.CreateWarehouseTransaction(productId, employeeId, transactionType, quantity, notes);
        }
    }
}