// =====================================================================
// === FrmLogin.cs (PHIÊN BẢN HOÀN CHỈNH ĐÃ SỬA LỖI VÀ TỐI ƯU HÓA) ===
// =====================================================================

using Store_X.BLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace Store_X
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // Optional: You can add initial logic here
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // STEP 1: GET AND VALIDATE INPUT
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // STEP 2: CALL BLL TO HANDLE LOGIN LOGIC
                DataRow loginResult = AccountBLL.Instance.Login(username, password);

                // STEP 3: PROCESS THE LOGIN RESULT
                if (loginResult != null)
                {
                    // --- LOGIN SUCCESSFUL ---

                    // 3.1. Save session information to the static class
                    CurrentUserSession.AccountID = Convert.ToInt32(loginResult["AccountID"]);
                    CurrentUserSession.Username = loginResult["Username"].ToString();
                    CurrentUserSession.Role = loginResult["RoleName"].ToString();
                    CurrentUserSession.EmployeeID = loginResult["EmployeeID"] as int?; 
                    CurrentUserSession.CustomerID = loginResult["CustomerID"] as int?;
                    string employeeName = loginResult["EmployeeName"].ToString();

                    // 3.2. Hide the current login form
                    this.Hide();

                    // 3.3. Authorize and open the corresponding Form based on the role
                    switch (CurrentUserSession.Role)
                    {
                        case "Admin":
                            FrmAdmin fAdmin = new FrmAdmin();
                            fAdmin.ShowDialog();
                            break;

                        case "Sale":
                            if (CurrentUserSession.EmployeeID.HasValue)
                            {
                                FrmSale fSale = new FrmSale(CurrentUserSession.EmployeeID.Value);
                                fSale.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Error: Sale staff account does not have a valid ID.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Show(); 
                                return; 
                            }
                            break;

                        case "Warehouse":
                            if (CurrentUserSession.EmployeeID.HasValue)
                            {
                                FrmWarehouse fWarehouse = new FrmWarehouse(CurrentUserSession.EmployeeID.Value, employeeName);
                                fWarehouse.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Error: Warehouse staff account does not have a valid ID.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Show();
                                return;
                            }
                            break;

                        case "Customer":
                            if (CurrentUserSession.CustomerID.HasValue)
                            {
                                FrmCustomer fCustomer = new FrmCustomer(CurrentUserSession.CustomerID.Value);
                                fCustomer.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Error: Customer account does not have a valid ID.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Show();
                                return;
                            }
                            break;

                        default:
                            MessageBox.Show($"The role '{CurrentUserSession.Role}' is not supported or has not been approved.", "Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Show();
                            CurrentUserSession.Clear();
                            return;
                    }

                    // 3.4. After the main Form is closed, also close the login Form to end the application
                    this.Close();
                }
                else
                {
                    // --- LOGIN FAILED ---
                    MessageBox.Show("Incorrect username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors (e.g., DB connection loss)
                MessageBox.Show("An error occurred during login: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Open the account creation form
            FrmCreateAccount fCreate = new FrmCreateAccount();
            fCreate.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Exit the application
            Application.Exit();
        }
    }
}