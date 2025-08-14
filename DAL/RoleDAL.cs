
using System.Collections.Generic;
using System.Data;
using System;

namespace Store_X.DAL
{
    public class RoleDAL
    {
        // Sử dụng mẫu Singleton
        private static RoleDAL _instance;
        public static RoleDAL Instance => _instance ?? (_instance = new RoleDAL());
        private RoleDAL() { }

        /// <summary>
        /// Lấy danh sách các vai trò đang chờ được phê duyệt từ bảng Accounts.
        /// </summary>
        /// <returns>Một DataTable chứa một cột duy nhất là 'RequestedRole'.</returns>
        public DataTable GetPendingRoles()
        {
            // Lấy danh sách các tên vai trò được yêu cầu duy nhất mà chưa được gán RoleID
            string query = @"SELECT DISTINCT RequestedRole 
                             FROM Accounts 
                             WHERE RoleID IS NULL AND RequestedRole IS NOT NULL AND RequestedRole <> ''";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Gọi Stored Procedure để thực hiện việc phê duyệt vai trò và gán quyền trong một transaction.
        /// </summary>
        /// <param name="roleNameToApprove">Tên vai trò cần phê duyệt.</param>
        /// <param name="permissions">Danh sách các quyền được cấp.</param>
        /// <returns>True nếu Stored Procedure thực thi thành công.</returns>
        public bool ApproveRoleAndSetPermissions(string roleNameToApprove, List<string> permissions)
        {
            // Tạo một DataTable từ danh sách quyền để truyền vào Stored Procedure
            DataTable permissionsTable = new DataTable();
            permissionsTable.Columns.Add("PermissionName", typeof(string));
            foreach (var p in permissions)
            {
                permissionsTable.Rows.Add(p);
            }

            try
            {
                // Gọi phương thức đặc biệt trong DataProvider để thực thi Stored Procedure
                // Chúng ta dùng ExecuteNonQuery vì Stored Procedure không trả về giá trị scalar
                DataProvider.Instance.ExecuteNonQueryStoredProcedure("sp_ApproveRoleAndSetPermissions", new Dictionary<string, object>
                {
                    { "@RoleName", roleNameToApprove },
                    { "@Permissions", permissionsTable } // Truyền vào DataTable
                });
                return true; // Nếu không có lỗi nào được ném ra, coi như thành công
            }
            catch
            {
                // Nếu Stored Procedure bị lỗi và ROLLBACK, C# sẽ bắt được exception
                return false;
            }
        }

        /// <summary>
        /// Từ chối một vai trò đang chờ duyệt bằng cách xóa tất cả các tài khoản liên quan.
        /// </summary>
        /// <param name="roleNameToReject">Tên vai trò cần từ chối.</param>
        /// <returns>True nếu có ít nhất một tài khoản bị xóa, ngược lại False.</returns>
        public bool RejectRole(string roleNameToReject)
        {
            // Câu lệnh này sẽ xóa các tài khoản có RequestedRole tương ứng và chưa được gán RoleID
            string query = "DELETE FROM Accounts WHERE RequestedRole = @requestedRole AND RoleID IS NULL";
            try
            {
                int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { roleNameToReject });
                // Trả về true nếu có ít nhất một dòng bị ảnh hưởng (bị xóa)
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}