
using System.Data;

namespace Store_X.DAL
{
    public class CustomerDAL
    {
        // Sử dụng mẫu Singleton
        private static CustomerDAL _instance;
        public static CustomerDAL Instance => _instance ?? (_instance = new CustomerDAL());
        private CustomerDAL() { }

        /// <summary>
        /// Lấy thông tin của một khách hàng dựa vào ID của họ.
        /// </summary>
        /// <param name="customerId">ID của khách hàng cần tìm.</param>
        /// <returns>Một DataTable chứa thông tin của khách hàng nếu tìm thấy.</returns>
        public DataTable GetCustomerById(int customerId)
        {
            // Câu lệnh SELECT để lấy các thông tin cần thiết của khách hàng
            string query = "SELECT CustomerID, CustomerName, Phone FROM Customers WHERE CustomerID = @id";

            // Thực thi truy vấn và trả về kết quả
            return DataProvider.Instance.ExecuteQuery(query, new object[] { customerId });
        }

        // Trong tương lai, bạn có thể thêm các phương thức DAL khác cho Customer ở đây
        // Ví dụ:
        // public DataTable GetAllCustomers() { ... }
        // public bool AddCustomer(Customer customer) { ... } // (Đã được tích hợp trong sp_CreateCustomerAccount)
        // public bool UpdateCustomer(Customer customer) { ... }
        // public bool DeleteCustomer(int customerId) { ... }
    }
}