
using Store_X.DTO;
using System.Data;
using System;
using System.Collections.Generic;

namespace Store_X.DAL
{
    public class AccountDAL
    {
        // Sử dụng mẫu Singleton
        private static AccountDAL _instance;
        public static AccountDAL Instance => _instance ?? (_instance = new AccountDAL());
        private AccountDAL() { }

        /// <summary>
        /// Lấy thông tin chi tiết của một tài khoản dựa trên username.
        /// </summary>
        public DataRow GetAccountByUsername(string username)
        {
            string query = @"SELECT a.*, r.RoleName, e.FullName AS EmployeeName
                             FROM Accounts AS a
                             LEFT JOIN Roles AS r ON a.RoleID = r.RoleID
                             LEFT JOIN Employees e ON a.EmployeeID = e.EmployeeID
                             WHERE a.Username = @Username";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username });
            return result.Rows.Count > 0 ? result.Rows[0] : null;
        }

        /// <summary>
        /// << ĐÃ THỐNG NHẤT: Chỉ nhận 2 tham số >>
        /// Gọi Stored Procedure để thêm một nhân viên và tài khoản tương ứng.
        /// </summary>
        public bool CreateEmployeeAccount(Employee emp, Account acc)
        {
            try
            {
                // Gọi Stored Procedure đã được tạo trước đó
                DataProvider.Instance.ExecuteNonQueryStoredProcedure("sp_CreateEmployeeAccount", new Dictionary<string, object>
                {
                    { "@FullName", emp.FullName },
                    { "@Address", emp.Address },
                    { "@Phone", emp.Phone },
                    { "@StartDate", emp.StartDate },
                    { "@Salary", emp.Salary },
                    { "@Username", acc.Username },
                    { "@PasswordHash", acc.PasswordHash },
                    { "@RoleID", (object)acc.RoleID ?? DBNull.Value },
                    { "@RequestedRole", (object)acc.RequestedRole ?? DBNull.Value }
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// << ĐÃ THỐNG NHẤT: Chỉ nhận 2 tham số >>
        /// Gọi Stored Procedure để thêm một khách hàng và tài khoản tương ứng.
        /// </summary>
        public bool CreateCustomerAccount(Customer cus, Account acc)
        {
            try
            {
                // Gọi Stored Procedure đã được tạo trước đó
                DataProvider.Instance.ExecuteNonQueryStoredProcedure("sp_CreateCustomerAccount", new Dictionary<string, object>
                 {
                    { "@CustomerName", cus.CustomerName },
                    { "@Phone", cus.Phone },
                    { "@Username", acc.Username },
                    { "@PasswordHash", acc.PasswordHash },
                    { "@RoleID", (object)acc.RoleID ?? DBNull.Value },
                    { "@RequestedRole", (object)acc.RequestedRole ?? DBNull.Value }
                 });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}