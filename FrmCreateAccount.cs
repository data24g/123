
using Store_X.BLL;
using Store_X.DTO;
using System;
using System.Windows.Forms;
using BCrypt.Net; // << ĐẢM BẢO CÓ DÒNG NÀY

namespace Store_X
{
    public partial class FrmCreateAccount : Form
    {
        public FrmCreateAccount()
        {
            InitializeComponent();
        }

        private void FrmCreateAccount_Load(object sender, EventArgs e)
        {
            // Thiết lập các giá trị ban đầu cho ComboBox
            cboKindOfAccount.Items.Clear(); // Xóa các item cũ để tránh trùng lặp
            cboKindOfAccount.Items.Add("Customer"); // Dùng tên vai trò trong CSDL
            cboKindOfAccount.Items.Add("Sale");
            cboKindOfAccount.Items.Add("Warehouse");
            // Không nên cho phép tạo tài khoản Admin ở đây

            cboKindOfAccount.SelectedIndex = 0; // Mặc định chọn "Customer"
        }
        

        private void btnCreate_Click(object sender, EventArgs e)
        {
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            try
            {
                // --- 1. Lấy và kiểm tra dữ liệu ---
                if (cboKindOfAccount.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn một loại tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy giá trị từ ComboBox một cách an toàn
                string selectedAccountType = cboKindOfAccount.SelectedItem.ToString();

                string accountName = txtAccountName.Text.Trim();
                string password = txtPassword.Text;
                string checkPassword = txtCheckPassword.Text;
                // ... (các kiểm tra khác cho password, username...)

                string hashedPassword = PasswordHelper.HashPassword(password);

                // --- 2. Tạo đối tượng Account chung ---
                Account accountDto = new Account
                {
                    Username = accountName,
                    PasswordHash = hashedPassword,
                    CreateDate = DateTime.Now,
                    IsActive = false, // Chờ Admin duyệt
                    RoleID = null,
                    RequestedRole = selectedAccountType
                };

                bool success = false;

                // --- 3. Xử lý logic theo loại tài khoản ---
                // Cả "Sale" và "Warehouse" đều là "Nhân viên" (Employee)
                if (selectedAccountType == "Sale" || selectedAccountType == "Warehouse")
                {
                    Employee employeeDto = new Employee
                    {
                        FullName = accountName,
                        Address = txtAddress.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        StartDate = DateTime.Now,
                        Salary = 0 // Lương mặc định
                    };

                    success = AccountBLL.Instance.CreateEmployeeAccount(employeeDto, accountDto);
                }
                else if (selectedAccountType == "Customer")
                {
                    Customer customerDto = new Customer
                    {
                        CustomerName = accountName,
                        Phone = txtPhone.Text.Trim(),
                        Address = txtAddress.Text.Trim()
                    };

                    success = AccountBLL.Instance.CreateCustomerAccount(customerDto, accountDto);
                }
                else
                {
                    MessageBox.Show("Loại tài khoản không được hỗ trợ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // --- 4. Hiển thị kết quả ---
                if (success)
                {
                    MessageBox.Show("Tạo tài khoản thành công! Vui lòng chờ Admin phê duyệt.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tạo tài khoản thất bại. Tên tài khoản có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không mong muốn: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}