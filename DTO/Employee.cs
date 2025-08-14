
using System;

namespace Store_X.DTO
{
    /// <summary>
    /// Lớp này đại diện cho cấu trúc dữ liệu của một Nhân viên.
    /// Nó được sử dụng để truyền dữ liệu giữa các tầng kiến trúc của ứng dụng.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Mã định danh duy nhất của nhân viên (Khóa chính).
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Họ và tên đầy đủ của nhân viên.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Địa chỉ của nhân viên (có thể là null).
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số điện thoại liên hệ của nhân viên (có thể là null).
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Ngày bắt đầu làm việc của nhân viên.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Mức lương của nhân viên.
        /// </summary>
        public decimal Salary { get; set; }
    }
}