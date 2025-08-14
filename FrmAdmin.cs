
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Store_X.BLL;
using Store_X.DAL;
using Store_X.DTO;

namespace Store_X
{
    public partial class FrmAdmin : Form
    {
        #region Constructor & Main Form Events

        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            // Khi Form được tải lần đầu, chỉ cần tải dữ liệu cho tab mặc định (Products)
            LoadProductTab();
        }

        private void tabControlAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Khi người dùng chuyển tab, tải dữ liệu cho tab tương ứng
            TabPage selectedTab = tabControlAdmin.SelectedTab;

            if (selectedTab == tabProducts) LoadProductTab();
            else if (selectedTab == tabEmployees) LoadEmployeeTab();
            //else if (selectedTab == tabAccounts) LoadAccountTab();
            else if (selectedTab == tabInvoices) LoadRecentInvoices();
            else if (selectedTab == tabWarehouses) LoadWarehouseTab();
            else if (selectedTab == tabStakeholders) LoadSupplierTab(); // Giả sử Stakeholders là Suppliers
            else if (selectedTab == tabReport) LoadReportTab();
            else if (selectedTab == tabRoleManagement) tabRoleManagement_Enter(null, null); // Gọi sự kiện Enter của tab
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        #endregion

        #region Product Management Tab

        void LoadProductTab()
        {
            try
            {
                dgvProducts.DataSource = ProductBLL.Instance.GetProductsForDisplay();
                cboProductCategory.DataSource = CategoryBLL.Instance.GetAllCategories();
                cboProductCategory.DisplayMember = "CategoryName";
                cboProductCategory.ValueMember = "CategoryID";
                cboProductSupplier.DataSource = SupplierBLL.Instance.GetAllSuppliers();
                cboProductSupplier.DisplayMember = "SupplierName";
                cboProductSupplier.ValueMember = "SupplierID";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải tab Sản phẩm: " + ex.Message); }
        }

        void ClearProductFields()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            txtProductPrice.Clear();
            cboProductCategory.SelectedIndex = -1;
            cboProductSupplier.SelectedIndex = -1;
            if (this.Controls.Find("txtProductFind", true).Length > 0) txtProductFind.Clear();
            txtProductName.Focus();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                txtProductID.Text = Convert.ToString(row.Cells["ProductID"].Value);
                txtProductName.Text = Convert.ToString(row.Cells["ProductName"].Value);
                txtProductPrice.Text = Convert.ToString(row.Cells["Price"].Value);
                cboProductCategory.SelectedValue = row.Cells["CategoryID"].Value ?? 0;
                cboProductSupplier.SelectedValue = row.Cells["SupplierID"].Value ?? 0;
            }
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtProductPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên và Giá sản phẩm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int categoryId;
                if (cboProductCategory.SelectedValue != null) categoryId = (int)cboProductCategory.SelectedValue;
                else
                {
                    categoryId = CategoryBLL.Instance.AddCategoryAndGetId(cboProductCategory.Text.Trim());
                    if (categoryId == 0) { MessageBox.Show("Không thể thêm loại sản phẩm mới."); return; }
                }

                int supplierId;
                if (cboProductSupplier.SelectedValue != null) supplierId = (int)cboProductSupplier.SelectedValue;
                else
                {
                    supplierId = SupplierBLL.Instance.AddSupplierAndGetId(cboProductSupplier.Text.Trim());
                    if (supplierId == 0) { MessageBox.Show("Không thể thêm nhà cung cấp mới."); return; }
                }

                Product newProduct = new Product
                {
                    ProductName = txtProductName.Text.Trim(),
                    Price = Convert.ToDecimal(txtProductPrice.Text),
                    CategoryID = categoryId,
                    SupplierID = supplierId,
                    StockQuantity = 0,
                    DateAdded = DateTime.Now
                };

                if (ProductBLL.Instance.AddProduct(newProduct))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductTab();
                    ClearProductFields();
                }
                else { MessageBox.Show("Thêm sản phẩm thất bại."); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message); }
        }

        private void btnProductUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để cập nhật.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int categoryId;
                if (cboProductCategory.SelectedValue != null) categoryId = (int)cboProductCategory.SelectedValue;
                else
                {
                    categoryId = CategoryBLL.Instance.AddCategoryAndGetId(cboProductCategory.Text.Trim());
                    if (categoryId == 0) { MessageBox.Show("Không thể thêm loại sản phẩm mới."); return; }
                }
                int supplierId;
                if (cboProductSupplier.SelectedValue != null) supplierId = (int)cboProductSupplier.SelectedValue;
                else
                {
                    supplierId = SupplierBLL.Instance.AddSupplierAndGetId(cboProductSupplier.Text.Trim());
                    if (supplierId == 0) { MessageBox.Show("Không thể thêm nhà cung cấp mới."); return; }
                }

                Product productToUpdate = new Product
                {
                    ProductID = Convert.ToInt32(txtProductID.Text),
                    ProductName = txtProductName.Text.Trim(),
                    Price = Convert.ToDecimal(txtProductPrice.Text),
                    CategoryID = categoryId,
                    SupplierID = supplierId,
                    StockQuantity = Convert.ToInt32(dgvProducts.CurrentRow.Cells["StockQuantity"].Value),
                };

                if (ProductBLL.Instance.UpdateProduct(productToUpdate))
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductTab();
                    ClearProductFields();
                }
                else { MessageBox.Show("Cập nhật sản phẩm thất bại."); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi cập nhật sản phẩm: " + ex.Message); }
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string confirmMessage = $"Bạn có chắc chắn muốn xóa sản phẩm này không?\nID: {txtProductID.Text}\nTên: {txtProductName.Text}";
            DialogResult result = MessageBox.Show(confirmMessage, "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int productId = Convert.ToInt32(txtProductID.Text);
                    if (ProductBLL.Instance.DeleteProduct(productId))
                    {
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProductTab();
                        ClearProductFields();
                    }
                    else { MessageBox.Show("Xóa thất bại! Sản phẩm có thể đã được sử dụng."); }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message); }
            }
        }

        private void btnProductFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductFind.Text))
            {
                MessageBox.Show("Vui lòng nhập ID sản phẩm cần tìm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int idToFind = Convert.ToInt32(txtProductFind.Text);
                bool found = false;
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.Cells["ProductID"].Value != null && Convert.ToInt32(row.Cells["ProductID"].Value) == idToFind)
                    {
                        dgvProducts.ClearSelection();
                        row.Selected = true;
                        dgvProducts.FirstDisplayedScrollingRowIndex = row.Index;
                        dgvProducts_CellClick(null, new DataGridViewCellEventArgs(0, row.Index)); // Đảm bảo tên sự kiện này đúng
                        found = true;
                        break;
                    }
                }
                if (!found) { MessageBox.Show("Không tìm thấy sản phẩm với ID: " + idToFind); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi tìm sản phẩm: " + ex.Message); }
        }

        #endregion

        #region Employee Management Tab

        void LoadEmployeeTab()
        {
            try
            {
                dgvEmployees.DataSource = EmployeeBLL.Instance.GetAllEmployees();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải tab Nhân viên: " + ex.Message); }
        }

        void ClearEmployeeFields()
        {
            txtEmpID.Clear();
            txtEmpName.Clear();
            txtEmpRole.Clear();
            txtEmpAddress.Clear();
            txtEmpPhone.Clear();
            txtEmpSalary.Clear();
            if (this.Controls.Find("txtEmpFind", true).Length > 0) txtEmpFind.Clear();
            dtpEmpStartDate.Value = DateTime.Now;
            txtEmpName.Focus();
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmployees.Rows[e.RowIndex];
                txtEmpID.Text = Convert.ToString(row.Cells["EmployeeID"].Value);
                txtEmpName.Text = Convert.ToString(row.Cells["FullName"].Value);
                txtEmpAddress.Text = Convert.ToString(row.Cells["Address"].Value);
                txtEmpPhone.Text = Convert.ToString(row.Cells["Phone"].Value);
                txtEmpSalary.Text = Convert.ToString(row.Cells["Salary"].Value);
                if (row.Cells["StartDate"].Value != DBNull.Value)
                    dtpEmpStartDate.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);
            }
        }

        private void btnEmpAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmpName.Text) || string.IsNullOrWhiteSpace(txtEmpSalary.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên và Lương nhân viên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Employee newEmployee = new Employee
                {
                    FullName = txtEmpName.Text.Trim(),
                    Address = txtEmpAddress.Text.Trim(),
                    Phone = txtEmpPhone.Text.Trim(),
                    StartDate = dtpEmpStartDate.Value,
                    Salary = Convert.ToDecimal(txtEmpSalary.Text)
                };

                if (EmployeeBLL.Instance.AddEmployee(newEmployee))
                {
                    MessageBox.Show("Thêm nhân viên thành công!\nHãy sang tab 'Accounts' để tạo tài khoản.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeTab();
                    ClearEmployeeFields();
                }
                else { MessageBox.Show("Thêm nhân viên thất bại."); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message); }
        }

        // ... Thêm code cho btnEmpUpdate, btnEmpDelete, btnEmpFind ...

        #endregion

        #region Account Management Tab

        void LoadAccountTab()
        {
            //try
            //{
            //    dgvAccounts.DataSource = AccountBLL.Instance.GetAllAccountsForDisplay();
            //    dgvAccounts.Columns["AccountID"].HeaderText = "Mã TK";
            //    dgvAccounts.Columns["Username"].HeaderText = "Tên đăng nhập";
            //    dgvAccounts.Columns["RoleName"].HeaderText = "Vai trò";
            //    dgvAccounts.Columns["CreateDate"].HeaderText = "Ngày tạo";
            //    dgvAccounts.Columns["IsActive"].HeaderText = "Trạng thái";
            //    dgvAccounts.Columns["FullName"].HeaderText = "Tên nhân viên";
            //    dgvAccounts.Columns["Phone"].HeaderText = "SĐT Nhân viên";
            //}
            //catch (Exception ex) { MessageBox.Show("Lỗi tải tab Tài khoản: " + ex.Message); }
        }

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //    if (e.RowIndex >= 0)
            //    {
            //        DataGridViewRow row = dgvAccounts.Rows[e.RowIndex];
            //        txtAccountID.Text = Convert.ToString(row.Cells["AccountID"].Value);
            //        txtAccountUsername.Text = Convert.ToString(row.Cells["Username"].Value);
            //        txtAccountRole.Text = Convert.ToString(row.Cells["RoleName"].Value);
            //        txtAccountPhone.Text = Convert.ToString(row.Cells["Phone"].Value);
            //        if (row.Cells["CreateDate"].Value != DBNull.Value) dtpAccountCreateDate.Value = Convert.ToDateTime(row.Cells["CreateDate"].Value);
            //        bool isActive = Convert.ToBoolean(row.Cells["IsActive"].Value ?? false);
            //        cboAccountStatus.SelectedItem = isActive ? "Còn hoạt động" : "Không hoạt động";
            //    }
        }

        // ... Thêm code cho các nút của tab Account ...

        #endregion

        #region Invoice Management Tab

        void LoadRecentInvoices()
        {
            try
            {
                dgvInvoices.DataSource = InvoiceBLL.Instance.GetInvoicesByDateRange(DateTime.Now.AddDays(-30), DateTime.Now);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải tab Hóa đơn: " + ex.Message); }
        }

        void DisplayInvoiceDetails(int invoiceId)
        {
            try
            {// Lấy kết quả là một DataTable// Lấy kết quả trực tiếp là một DataRow
                DataRow header = InvoiceBLL.Instance.GetInvoiceHeaderById(invoiceId);

                // Chỉ cần kiểm tra xem header có null không
                if (header != null)
                {
                    // Phần code còn lại giữ nguyên
                    txtInvoiceID.Text = header["InvoiceID"].ToString();
                    txtInvoiceCustomerName.Text = header["CustomerName"].ToString();
                    txtInvoiceEmployeeName.Text = header["EmployeeName"].ToString();
                    dtpInvoiceDate.Value = Convert.ToDateTime(header["InvoiceDate"]);
                    txtInvoiceTotal.Text = Convert.ToDecimal(header["TotalAmount"]).ToString("N0");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hiển thị chi tiết hóa đơn: " + ex.Message); }
        }

        // ... Thêm code cho các nút và sự kiện của tab Invoice ...

        #endregion

        #region Warehouse Management Tab

        void LoadWarehouseTab()
        {
            try
            {
                dgvWarehouseHistory.DataSource = WarehouseHistoryBLL.Instance.GetAllWarehouseHistory();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải tab Kho: " + ex.Message); }
        }

        void ClearWarehouseFields()
        {
            txtWarehouseProductID.Clear();
            txtWarehouseProductName.Clear();
            txtWarehousePrice.Clear();
            txtWarehouseSupplier.Clear();
            txtWarehouseQuantity.Clear();
        }

        // ... Thêm code cho các nút của tab Warehouse ...

        #endregion

        #region Supplier Management Tab (Stakeholders)

        void LoadSupplierTab()
        {
            try
            {
                dgvSuppliers.DataSource = SupplierBLL.Instance.GetAllSuppliers();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải tab Nhà cung cấp: " + ex.Message); }
        }

        void ClearSupplierFields()
        {
            txtSupplierID.Clear();
            txtSupplierName.Clear();
            txtSupplierAddress.Clear();
            txtSupplierPhone.Clear();
            if (this.Controls.Find("txtSupplierFind", true).Length > 0) txtSupplierFind.Clear();
            dtpSupplierCoopDate.Value = DateTime.Now;
            txtSupplierName.Focus();
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSuppliers.Rows[e.RowIndex];
                txtSupplierID.Text = Convert.ToString(row.Cells["SupplierID"].Value);
                txtSupplierName.Text = Convert.ToString(row.Cells["SupplierName"].Value);
                txtSupplierAddress.Text = Convert.ToString(row.Cells["Address"].Value);
                txtSupplierPhone.Text = Convert.ToString(row.Cells["Phone"].Value);
                if (row.Cells["CooperationDate"].Value != DBNull.Value) dtpSupplierCoopDate.Value = Convert.ToDateTime(row.Cells["CooperationDate"].Value);
            }
        }

        // ... Thêm code cho các nút của tab Supplier ...

        #endregion

        #region Report Management Tab

        void LoadReportTab()
        {
            // Logic tải dữ liệu cho tab Report
        }

        #endregion

        #region Role Management Tab

        private void tabRoleManagement_Enter(object sender, EventArgs e)
        {
            LoadPendingRoles();
            LoadAllPossiblePermissions();
            btnApprove.Enabled = false;
            btnReject.Enabled = false;
        }

        void LoadPendingRoles()
        {
            try
            {
                lstPendingRoles.DataSource = RoleBLL.Instance.GetPendingRoles();
                lstPendingRoles.DisplayMember = "RequestedRole";
                lstPendingRoles.ValueMember = "RequestedRole";

                // << THÊM DÒNG NÀY VÀO >>
                // Sau khi tải dữ liệu xong, ta kiểm tra xem có item nào không
                // Nếu có, ta giả lập một sự kiện "SelectedIndexChanged" để cập nhật trạng thái các nút
                //if (lstPendingRoles.Items.Count > 0)
                //{
                //    lstPendingRoles_SelectedIndexChanged(null, null);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vai trò chờ duyệt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadAllPossiblePermissions()
        {
            chkPermissions.Items.Clear();
            chkPermissions.Items.Add("VIEW_SALE_FORM", true);
            chkPermissions.Items.Add("VIEW_WAREHOUSE_FORM", true);
            // ... thêm các quyền khác
        }

        private void lstPendingRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    // Tải dữ liệu cần thiết cho tab
        //    LoadPendingRoles();
        //    LoadAllPossiblePermissions();

        //    // << LOGIC PHÂN QUYỀN VÀ KÍCH HOẠT NÚT ĐƯỢC ĐẶT Ở ĐÂY >>
        //    // BƯỚC 1: KIỂM TRA VAI TRÒ
        //    if (CurrentUserSession.Role == "Admin")
        //    {
        //        // BƯỚC 2: NẾU LÀ ADMIN, KIỂM TRA XEM CÓ VAI TRÒ NÀO ĐỂ DUYỆT KHÔNG
        //        if (lstPendingRoles.Items.Count > 0)
        //        {
        //            // Nếu có, KÍCH HOẠT các nút
        //            btnApprove.Enabled = true;
        //            btnReject.Enabled = true;
        //        }
        //        else
        //        {
        //            // Nếu không có vai trò nào chờ duyệt, VÔ HIỆU HÓA các nút
        //            btnApprove.Enabled = false;
        //            btnReject.Enabled = false;
        //        }
        //    }
        //    else // Nếu không phải là Admin
        //    {
        //        // Luôn luôn VÔ HIỆU HÓA các nút
        //        btnApprove.Enabled = false;
        //        btnReject.Enabled = false;
        //    }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (lstPendingRoles.SelectedItem == null) return;
            string roleNameToApprove = lstPendingRoles.SelectedValue.ToString();
            var grantedPermissions = new List<string>();
            foreach (var item in chkPermissions.CheckedItems)
            {
                grantedPermissions.Add(item.ToString());
            }

            if (grantedPermissions.Count == 0)
            {
                MessageBox.Show("Phải cấp ít nhất một quyền.");
                return;
            }

            if (RoleBLL.Instance.ApproveRoleAndSetPermissions(roleNameToApprove, grantedPermissions))
            {
                MessageBox.Show("Phê duyệt thành công.");
                LoadPendingRoles();
            }
            else { MessageBox.Show("Phê duyệt thất bại."); }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (lstPendingRoles.SelectedItem == null) return;
            string roleNameToReject = lstPendingRoles.SelectedValue.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn từ chối và xóa vai trò '{roleNameToReject}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (RoleBLL.Instance.RejectRole(roleNameToReject))
                {
                    MessageBox.Show("Từ chối thành công.");
                    LoadPendingRoles();
                }
                else { MessageBox.Show("Từ chối thất bại."); }
            }
        }
        public DataTable GetPendingRoles()
        {
            // Câu lệnh này phải lấy ra một cột có tên là "RequestedRole"
            string query = @"SELECT DISTINCT RequestedRole 
                     FROM Accounts 
                     WHERE RoleID IS NULL AND RequestedRole IS NOT NULL";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        #endregion

        private void btnProductRefresh_Click(object sender, EventArgs e)
        {
            LoadProductTab();
            ClearProductFields();
        }

        private void btnEmpFind_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtProductFind.Text))
            {
                MessageBox.Show("Vui lòng nhập ID sản phẩm cần tìm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int idToFind = Convert.ToInt32(txtProductFind.Text);
                bool found = false;
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.Cells["ProductID"].Value != null && Convert.ToInt32(row.Cells["ProductID"].Value) == idToFind)
                    {
                        dgvProducts.ClearSelection();
                        row.Selected = true;
                        dgvProducts.FirstDisplayedScrollingRowIndex = row.Index;
                        dgvProducts_CellClick(null, new DataGridViewCellEventArgs(0, row.Index));
                        found = true;
                        break;
                    }
                }
                if (!found) { MessageBox.Show("Không tìm thấy sản phẩm với ID: " + idToFind, "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi tìm sản phẩm: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEmpDelete_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtEmpID.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Chưa chọn nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string confirmMessage = $"Bạn có chắc chắn muốn xóa nhân viên này không?\nID: {txtEmpID.Text}\nTên: {txtEmpName.Text}";
            DialogResult result = MessageBox.Show(confirmMessage, "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int employeeId = Convert.ToInt32(txtEmpID.Text);
                    if (EmployeeBLL.Instance.DeleteEmployee(employeeId))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployeeTab();
                        ClearEmployeeFields();
                    }
                    else { MessageBox.Show("Xóa thất bại! Nhân viên có thể đang liên kết với hóa đơn hoặc tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnEmpUpdate_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtEmpID.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để cập nhật.", "Chưa chọn nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Employee employeeToUpdate = new Employee
                {
                    EmployeeID = Convert.ToInt32(txtEmpID.Text),
                    FullName = txtEmpName.Text.Trim(),
                    Address = txtEmpAddress.Text.Trim(),
                    Phone = txtEmpPhone.Text.Trim(),
                    StartDate = dtpEmpStartDate.Value,
                    Salary = Convert.ToDecimal(txtEmpSalary.Text)
                };

                if (EmployeeBLL.Instance.UpdateEmployee(employeeToUpdate))
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeTab();
                    ClearEmployeeFields();
                }
                else { MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEmpRefresh_Click(object sender, EventArgs e)
        {
            LoadEmployeeTab();
            ClearEmployeeFields();
        }

        private void btnInvoiceFind_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtInvoiceFind.Text))
            {
                MessageBox.Show("Vui lòng nhập ID hóa đơn cần tìm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int idToFind = Convert.ToInt32(txtInvoiceFind.Text);
                DisplayInvoiceDetails(idToFind);
            }
            catch (FormatException) { MessageBox.Show("ID hóa đơn phải là một con số.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnReportByDay_Click(object sender, EventArgs e)
        {

            try
            {
                DateTime selectedDate = dtpDailyStats.Value.Date;
                DataRow stats = InvoiceBLL.Instance.GetDailyStatistics(selectedDate);
                if (stats != null)
                {
                    txtStatInvoiceCount.Text = stats["InvoiceCount"].ToString();
                    txtStatTotalAmount.Text = (stats["TotalRevenue"] != DBNull.Value) ? Convert.ToDecimal(stats["TotalRevenue"]).ToString("N0") : "0";
                }
                else
                {
                    txtStatInvoiceCount.Text = "0";
                    txtStatTotalAmount.Text = "0";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi lấy thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnWarehouseImport_Click(object sender, EventArgs e)
        {
            ExecuteWarehouseTransaction("Nhập");
        }

        private void btnWarehouseExport_Click(object sender, EventArgs e)
        {
            ExecuteWarehouseTransaction("Xuất");
        }
        private void ExecuteWarehouseTransaction(string transactionType)
        {
            if (string.IsNullOrWhiteSpace(txtWarehouseProductID.Text) || string.IsNullOrWhiteSpace(txtWarehouseQuantity.Text))
            {
                MessageBox.Show($"Vui lòng nhập ID Sản phẩm và Số lượng để {transactionType.ToLower()} kho.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int productId = Convert.ToInt32(txtWarehouseProductID.Text);
                int quantity = Convert.ToInt32(txtWarehouseQuantity.Text);
                int employeeId = 1; // Tạm thời hard-code, nên lấy từ phiên đăng nhập

                if (quantity <= 0) { MessageBox.Show("Số lượng phải lớn hơn 0.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (WarehouseHistoryBLL.Instance.CreateWarehouseTransaction(productId, employeeId, transactionType, quantity, $"Giao dịch từ form Admin"))
                {
                    MessageBox.Show($"{transactionType} kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWarehouseTab();
                    ClearWarehouseFields();
                }
                else
                {
                    MessageBox.Show($"{transactionType} kho thất bại! Vui lòng kiểm tra lại ID sản phẩm và số lượng tồn kho.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi khi {transactionType.ToLower()} kho: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnShowHistoryDetail_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtHistoryID.Text))
            {
                MessageBox.Show("Vui lòng chọn một dòng trong lịch sử để xem chi tiết.", "Chưa chọn giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Logic hiển thị chi tiết bằng MessageBox
            string message = $"Mã GD: {txtHistoryID.Text}\nLoại GD: {cboHistoryType.SelectedItem}\nNhân viên: {txtHistoryEmployeeName.Text}";
            MessageBox.Show(message, "Chi tiết giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSupplierAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Nhà cung cấp.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Supplier newSupplier = new Supplier
                {
                    SupplierName = txtSupplierName.Text.Trim(),
                    Address = txtSupplierAddress.Text.Trim(),
                    Phone = txtSupplierPhone.Text.Trim(),
                    CooperationDate = dtpSupplierCoopDate.Value
                };

                if (SupplierBLL.Instance.AddSupplier(newSupplier))
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplierTab();
                    ClearSupplierFields();
                }
                else { MessageBox.Show("Thêm nhà cung cấp thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi thêm NCC: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSupplierDelete_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtSupplierID.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa.", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này không?\nTên NCC: " + txtSupplierName.Text, "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int supplierId = Convert.ToInt32(txtSupplierID.Text);
                if (SupplierBLL.Instance.DeleteSupplier(supplierId))
                {
                    MessageBox.Show("Xóa nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplierTab();
                    ClearSupplierFields();
                }
                else { MessageBox.Show("Xóa thất bại! NCC có thể đang liên kết với một sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnSupplierUpdate_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtSupplierID.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để cập nhật.", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Supplier supplierToUpdate = new Supplier
                {
                    SupplierID = Convert.ToInt32(txtSupplierID.Text),
                    SupplierName = txtSupplierName.Text.Trim(),
                    Address = txtSupplierAddress.Text.Trim(),
                    Phone = txtSupplierPhone.Text.Trim(),
                    CooperationDate = dtpSupplierCoopDate.Value
                };

                if (SupplierBLL.Instance.UpdateSupplier(supplierToUpdate))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplierTab();
                    ClearSupplierFields();
                }
                else { MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi cập nhật NCC: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSupplierRefresh_Click(object sender, EventArgs e)
        {
            LoadSupplierTab();
            ClearSupplierFields();
        }

        private void btnSupplierFind_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtSupplierFind.Text))
            {
                MessageBox.Show("Vui lòng nhập ID nhà cung cấp cần tìm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int idToFind = Convert.ToInt32(txtSupplierFind.Text);
                bool found = false;
                foreach (DataGridViewRow row in dgvSuppliers.Rows)
                {
                    if (row.Cells["SupplierID"].Value != null && Convert.ToInt32(row.Cells["SupplierID"].Value) == idToFind)
                    {
                        dgvSuppliers.ClearSelection();
                        row.Selected = true;
                        dgvSuppliers.FirstDisplayedScrollingRowIndex = row.Index;
                        dgvSuppliers_CellClick(null, new DataGridViewCellEventArgs(0, row.Index));
                        found = true;
                        break;
                    }
                }
                if (!found) { MessageBox.Show("Không tìm thấy NCC với ID: " + idToFind, "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi tìm NCC: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnReportRefresh_Click(object sender, EventArgs e)
        {

            if (cboReportType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một loại báo cáo để xem.", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedReport = cboReportType.SelectedItem.ToString();
            DataTable reportData = new DataTable();
            try
            {
                switch (selectedReport)
                {
                    case "Doanh thu theo tháng":
                        reportData = ReportBLL.Instance.GetMonthlyRevenue();
                        break;
                    case "Top 5 sản phẩm bán chạy":
                        reportData = ReportBLL.Instance.GetTopSellingProducts();
                        break;
                    case "Doanh thu theo nhân viên":
                        reportData = ReportBLL.Instance.GetRevenueByEmployee();
                        break;
                    default:
                        MessageBox.Show("Loại báo cáo không được hỗ trợ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                dgvReport.DataSource = reportData;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            LoadWarehouseTab();
        }
    }
}