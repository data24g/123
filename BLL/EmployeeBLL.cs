
using Store_X.DAL;
using Store_X.DTO;
using System.Data;

namespace Store_X.BLL
{
    public class EmployeeBLL
    {
        // Sử dụng mẫu Singleton
        private static EmployeeBLL _instance;
        public static EmployeeBLL Instance => _instance ?? (_instance = new EmployeeBLL());
        private EmployeeBLL() { }

        /// <summary>
        /// Lấy về một DataTable chứa thông tin của tất cả nhân viên.
        /// </summary>
        /// <returns>DataTable chứa danh sách nhân viên.</returns>
        public DataTable GetAllEmployees()
        {
            return EmployeeDAL.Instance.GetAllEmployees();
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để thêm một nhân viên mới.
        /// </summary>
        /// <param name="employee">Đối tượng Employee chứa thông tin cần thêm.</param>
        /// <returns>True nếu thêm thành công, ngược lại False.</returns>
        public bool AddEmployee(Employee employee)
        {
            // --- Logic nghiệp vụ ---
            // Tên và lương là bắt buộc
            if (employee == null || string.IsNullOrWhiteSpace(employee.FullName) || employee.Salary < 0)
            {
                return false;
            }

            return EmployeeDAL.Instance.AddEmployee(employee);
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để cập nhật thông tin một nhân viên.
        /// </summary>
        /// <param name="employee">Đối tượng Employee chứa thông tin cần cập nhật.</param>
        /// <returns>True nếu cập nhật thành công, ngược lại False.</returns>
        public bool UpdateEmployee(Employee employee)
        {
            // --- Logic nghiệp vụ ---
            // Phải có EmployeeID để biết cập nhật ai
            // Tên và lương không được rỗng/âm
            if (employee == null || employee.EmployeeID <= 0 || string.IsNullOrWhiteSpace(employee.FullName) || employee.Salary < 0)
            {
                return false;
            }

            return EmployeeDAL.Instance.UpdateEmployee(employee);
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để xóa một nhân viên.
        /// </summary>
        /// <param name="employeeId">ID của nhân viên cần xóa.</param>
        /// <returns>True nếu xóa thành công, ngược lại False.</returns>
        public bool DeleteEmployee(int employeeId)
        {
            // --- Logic nghiệp vụ ---
            // ID phải hợp lệ
            if (employeeId <= 0)
            {
                return false;
            }

            // Ghi chú: Một logic phức tạp hơn có thể được thêm ở đây,
            // ví dụ: kiểm tra xem nhân viên có đang quản lý hóa đơn nào không trước khi xóa,
            // hoặc tự động xóa cả tài khoản (Account) liên kết.
            // Hiện tại, chúng ta để logic này ở tầng DAL (thông qua ràng buộc khóa ngoại).

            return EmployeeDAL.Instance.DeleteEmployee(employeeId);
        }
    }
}