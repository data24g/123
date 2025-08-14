
using Store_X.DTO;
using System.Data;

namespace Store_X.DAL
{
    public class EmployeeDAL
    {
        // Sử dụng mẫu Singleton
        private static EmployeeDAL _instance;
        public static EmployeeDAL Instance => _instance ?? (_instance = new EmployeeDAL());
        private EmployeeDAL() { }

        /// <summary>
        /// Lấy về một DataTable chứa thông tin của tất cả nhân viên.
        /// </summary>
        /// <returns>DataTable chứa danh sách nhân viên.</returns>
        public DataTable GetAllEmployees()
        {
            string query = "SELECT EmployeeID, FullName, Address, Phone, StartDate, Salary FROM Employees";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Thêm một nhân viên mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="employee">Đối tượng Employee chứa thông tin cần thêm.</param>
        /// <returns>True nếu thêm thành công (có 1 dòng bị ảnh hưởng), ngược lại False.</returns>
        public bool AddEmployee(Employee employee)
        {
            string query = "INSERT INTO Employees (FullName, Address, Phone, StartDate, Salary) VALUES ( @FullName , @Address , @Phone , @StartDate , @Salary )";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {
                employee.FullName,
                employee.Address,
                employee.Phone,
                employee.StartDate,
                employee.Salary
            });

            return result > 0;
        }

        /// <summary>
        /// Cập nhật thông tin của một nhân viên trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="employee">Đối tượng Employee chứa thông tin cần cập nhật.</param>
        /// <returns>True nếu cập nhật thành công, ngược lại False.</returns>
        public bool UpdateEmployee(Employee employee)
        {
            string query = @"UPDATE Employees 
                             SET FullName = @FullName , 
                                 Address = @Address , 
                                 Phone = @Phone , 
                                 StartDate = @StartDate , 
                                 Salary = @Salary 
                             WHERE EmployeeID = @EmployeeID";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {
                employee.FullName,
                employee.Address,
                employee.Phone,
                employee.StartDate,
                employee.Salary,
                employee.EmployeeID
            });

            return result > 0;
        }

        /// <summary>
        /// Xóa một nhân viên khỏi cơ sở dữ liệu.
        /// </summary>
        /// <param name="employeeId">ID của nhân viên cần xóa.</param>
        /// <returns>True nếu xóa thành công, ngược lại False.</returns>
        public bool DeleteEmployee(int employeeId)
        {
            // Lưu ý: Nếu có ràng buộc khóa ngoại từ bảng Accounts đến Employees,
            // bạn cần xử lý việc xóa tài khoản trước, hoặc thiết lập ON DELETE CASCADE trong CSDL.
            // Giả sử logic xóa tài khoản được xử lý ở BLL hoặc Stored Procedure.
            string query = "DELETE FROM Employees WHERE EmployeeID = @id";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { employeeId });

            return result > 0;
        }
    }
}