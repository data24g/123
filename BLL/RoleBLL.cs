
using Store_X.DAL;
using System.Collections.Generic;
using System.Data;

namespace Store_X.BLL
{
    public class RoleBLL
    {
        // Sử dụng mẫu Singleton
        private static RoleBLL _instance;
        public static RoleBLL Instance => _instance ?? (_instance = new RoleBLL());
        private RoleBLL() { }

        /// <summary>
        /// Lấy danh sách các vai trò đang chờ được phê duyệt.
        /// Về cơ bản, đây là danh sách các 'RequestedRole' duy nhất từ bảng Accounts
        /// mà chưa được gán RoleID.
        /// </summary>
        /// <returns>Một DataTable chứa cột RequestedRole.</returns>
        public DataTable GetPendingRoles()
        {
            return RoleDAL.Instance.GetPendingRoles();
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để phê duyệt một vai trò mới, cập nhật các tài khoản liên quan
        /// và gán các quyền được chỉ định.
        /// </summary>
        /// <param name="roleNameToApprove">Tên của vai trò cần phê duyệt.</param>
        /// <param name="permissions">Danh sách các quyền (dạng chuỗi) được cấp cho vai trò này.</param>
        /// <returns>True nếu toàn bộ quá trình thành công, ngược lại False.</returns>
        public bool ApproveRoleAndSetPermissions(string roleNameToApprove, List<string> permissions)
        {
            // --- Logic nghiệp vụ ---
            // Tên vai trò và danh sách quyền không được rỗng.
            if (string.IsNullOrWhiteSpace(roleNameToApprove) || permissions == null || permissions.Count == 0)
            {
                return false;
            }

            // Toàn bộ quá trình phức tạp này (thêm vai trò, cập nhật tài khoản, gán quyền)
            // nên được xử lý trong một transaction duy nhất ở tầng DAL để đảm bảo toàn vẹn dữ liệu.
            return RoleDAL.Instance.ApproveRoleAndSetPermissions(roleNameToApprove, permissions);
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để từ chối một vai trò đang chờ duyệt.
        /// Thao tác này sẽ xóa tất cả các tài khoản đang chờ với vai trò được yêu cầu đó.
        /// </summary>
        /// <param name="roleNameToReject">Tên vai trò cần từ chối.</param>
        /// <returns>True nếu xóa thành công, ngược lại False.</returns>
        public bool RejectRole(string roleNameToReject)
        {
            // --- Logic nghiệp vụ ---
            if (string.IsNullOrWhiteSpace(roleNameToReject))
            {
                return false;
            }

            return RoleDAL.Instance.RejectRole(roleNameToReject);
        }
    }
}