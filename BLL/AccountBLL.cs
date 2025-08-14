
using Store_X.DAL;
using Store_X.DTO;
using System.Data;

namespace Store_X.BLL
{
    public class AccountBLL
    {
        // Sử dụng mẫu Singleton
        private static AccountBLL _instance;
        public static AccountBLL Instance => _instance ?? (_instance = new AccountBLL());
        private AccountBLL() { }

        /// <summary>
        /// Xử lý logic đăng nhập, bao gồm xác thực và so sánh mật khẩu đã băm.
        /// </summary>
        public DataRow Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;

            DataRow accountData = AccountDAL.Instance.GetAccountByUsername(username);
            if (accountData == null) return null;

            string hashedPasswordFromDb = accountData["PasswordHash"].ToString();

            // << THAY ĐỔI Ở ĐÂY: Sử dụng PasswordHelper thay vì BCrypt >>
            if (PasswordHelper.VerifyPassword(password, hashedPasswordFromDb))
            {
                return accountData;
            }

            return null;
        }

        /// <summary>
        /// << ĐÃ THỐNG NHẤT: Chỉ nhận 2 tham số >>
        /// Xử lý logic nghiệp vụ để tạo tài khoản cho một nhân viên mới.
        /// </summary>
        public bool CreateEmployeeAccount(Employee emp, Account acc)
        {
            // Kiểm tra các thông tin bắt buộc
            if (emp == null || acc == null || string.IsNullOrWhiteSpace(emp.FullName) || string.IsNullOrWhiteSpace(acc.Username) || string.IsNullOrWhiteSpace(acc.PasswordHash))
            {
                return false;
            }
            return AccountDAL.Instance.CreateEmployeeAccount(emp, acc);
        }

        /// <summary>
        /// << ĐÃ THỐNG NHẤT: Chỉ nhận 2 tham số >>
        /// Xử lý logic nghiệp vụ để tạo tài khoản cho một khách hàng mới.
        /// </summary>
        public bool CreateCustomerAccount(Customer cus, Account acc)
        {
            // Kiểm tra các thông tin bắt buộc
            if (cus == null || acc == null || string.IsNullOrWhiteSpace(cus.CustomerName) || string.IsNullOrWhiteSpace(acc.Username) || string.IsNullOrWhiteSpace(acc.PasswordHash))
            {
                return false;
            }
            return AccountDAL.Instance.CreateCustomerAccount(cus, acc);
        }
    }
}