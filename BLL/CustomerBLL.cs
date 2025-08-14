
using Store_X.DAL;
using System.Data;

namespace Store_X.BLL
{
    public class CustomerBLL
    {
        // Sử dụng mẫu Singleton để đảm bảo chỉ có một thể hiện của lớp
        private static CustomerBLL _instance;
        public static CustomerBLL Instance => _instance ?? (_instance = new CustomerBLL());
        private CustomerBLL() { }

        /// <summary>
        /// Lấy thông tin của một khách hàng dựa vào ID.
        /// Thường được sử dụng trong Form Bán hàng (FrmSale) để tra cứu.
        /// </summary>
        /// <param name="customerId">ID của khách hàng cần tìm.</param>
        /// <returns>Một DataTable chứa thông tin của khách hàng nếu tìm thấy, ngược lại trả về null.</returns>
        public DataTable GetCustomerById(int customerId)
        {
            // --- Logic nghiệp vụ cơ bản ---
            // ID của khách hàng phải là một số dương.
            if (customerId <= 0)
            {
                return null;
            }

            // Gọi xuống lớp DAL để thực hiện truy vấn
            return CustomerDAL.Instance.GetCustomerById(customerId);
        }

        // Trong tương lai, bạn có thể thêm các phương thức BLL khác cho Customer ở đây
        // Ví dụ:
        // public DataTable GetAllCustomers() { ... }
        // public bool AddCustomer(Customer customer) { ... }
        // public bool UpdateCustomer(Customer customer) { ... }
        // public bool DeleteCustomer(int customerId) { ... }
    }
}