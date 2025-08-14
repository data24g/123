
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Store_X.BLL;
using Store_X.DTO;

namespace Store_X
{
    public partial class FrmCustomer : Form
    {
        // === BIẾN THÀNH VIÊN ===
        private int _currentCustomerId;
        private DataTable _shoppingCart; // Sử dụng DataTable để làm giỏ hàng, dễ dàng binding với DataGridView

        // === HÀM KHỞI TẠO ===
        public FrmCustomer(int customerId)
        {
            InitializeComponent();
            _currentCustomerId = customerId;
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            InitializeShoppingCart();
            LoadAllProducts();
            LoadPurchaseHistory();
            LoadCategories();
        }

        #region Helper Methods (Các phương thức hỗ trợ)

        void InitializeShoppingCart()
        {
            _shoppingCart = new DataTable();
            _shoppingCart.Columns.Add("ProductID", typeof(int));
            _shoppingCart.Columns.Add("ProductName", typeof(string));
            _shoppingCart.Columns.Add("Quantity", typeof(int));
            _shoppingCart.Columns.Add("UnitPrice", typeof(decimal));
            _shoppingCart.Columns.Add("Total", typeof(decimal), "Quantity * UnitPrice");

            // Gán giỏ hàng cho DataGridView bên phải (giả sử tên là dgvCart)
            dgvCart.DataSource = _shoppingCart;
        }

        void LoadAllProducts()
        {
            try { dgvProducts.DataSource = ProductBLL.Instance.GetProductsForDisplay(); }
            catch (Exception ex) { MessageBox.Show("Lỗi tải danh sách sản phẩm: " + ex.Message); }
        }

        void LoadPurchaseHistory()
        {
            try { dgvPurchaseHistory.DataSource = InvoiceBLL.Instance.GetInvoicesByCustomerId(_currentCustomerId); }
            catch (Exception ex) { MessageBox.Show("Lỗi tải lịch sử mua hàng: " + ex.Message); }
        }

        void LoadCategories()
        {
            try
            {
                cboCategory.DataSource = CategoryBLL.Instance.GetAllCategories();
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryID";
                cboCategory.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải danh mục: " + ex.Message); }
        }

        #endregion

        #region Button and Control Events

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            string name = txtProductNameSearch.Text; // Giả sử tên ô tìm kiếm
            int categoryId = (cboCategory.SelectedValue != null) ? Convert.ToInt32(cboCategory.SelectedValue) : 0;
            dgvProducts.DataSource = ProductBLL.Instance.SearchProducts(name, categoryId);
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để thêm vào giỏ.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvProducts.CurrentRow;
            int productId = Convert.ToInt32(selectedRow.Cells["ProductID"].Value);
            string productName = selectedRow.Cells["ProductName"].Value.ToString();
            decimal unitPrice = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
            int stock = Convert.ToInt32(selectedRow.Cells["StockQuantity"].Value);
            int quantityToAdd = (int)numQuantity.Value;

            if (quantityToAdd <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0.", "Số lượng không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            DataRow existingItem = _shoppingCart.AsEnumerable().FirstOrDefault(row => (int)row["ProductID"] == productId);
            int quantityInCart = (existingItem != null) ? (int)existingItem["Quantity"] : 0;

            if (quantityToAdd + quantityInCart > stock)
            {
                MessageBox.Show($"Không đủ hàng trong kho.\nTồn kho: {stock}\nĐã có trong giỏ: {quantityInCart}", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (existingItem != null)
            {
                // Cập nhật số lượng nếu sản phẩm đã có
                existingItem["Quantity"] = (int)existingItem["Quantity"] + quantityToAdd;
            }
            else
            {
                // Thêm mới nếu chưa có
                _shoppingCart.Rows.Add(productId, productName, quantityToAdd, unitPrice);
            }

            MessageBox.Show($"Đã thêm '{productName}' vào giỏ hàng.", "Thêm thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Nút "btnViewCart" không còn cần thiết vì giỏ hàng đã hiển thị trực tiếp trên dgvCart

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            if (_shoppingCart.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm trước khi thanh toán.", "Giỏ hàng trống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Invoice mainInvoice = new Invoice
            {
                CustomerID = _currentCustomerId,
                EmployeeID = null, // Giao dịch online không có nhân viên
                InvoiceDate = DateTime.Now,
                TotalAmount = (decimal)_shoppingCart.Compute("SUM(Total)", "")
            };

            List<InvoiceDetail> details = new List<InvoiceDetail>();
            foreach (DataRow row in _shoppingCart.Rows)
            {
                details.Add(new InvoiceDetail
                {
                    ProductID = (int)row["ProductID"],
                    Quantity = (int)row["Quantity"],
                    UnitPrice = (decimal)row["UnitPrice"]
                });
            }

            // << SỬA LỖI: Chuyển kiểu biến nhận kết quả từ int thành bool >>
            bool success = InvoiceBLL.Instance.CreateInvoice(mainInvoice, details);

            if (success)
            {
                MessageBox.Show($"Cảm ơn bạn đã mua hàng! Hóa đơn của bạn đã được tạo thành công.", "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InitializeShoppingCart(); // Xóa giỏ hàng
                LoadAllProducts();      // Cập nhật lại tồn kho
                LoadPurchaseHistory();  // Cập nhật lịch sử mua hàng
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại. Số lượng sản phẩm trong kho có thể đã thay đổi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshHistory_Click(object sender, EventArgs e)
        {
            LoadPurchaseHistory();
        }

        private void btnViewProductDetail_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            DataGridViewRow row = dgvProducts.CurrentRow;
            string message = $"Tên: {row.Cells["ProductName"].Value}\nGiá: {Convert.ToDecimal(row.Cells["Price"].Value):N0} VNĐ\nTồn kho: {row.Cells["StockQuantity"].Value}";
            MessageBox.Show(message, "Chi tiết sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSearchProduct_Click_1(object sender, EventArgs e)
        {

            try
            {
                string name = txtProductNameSearch.Text; // Giả sử tên ô tìm kiếm theo tên
                int categoryId = (cboCategory.SelectedValue != null) ? Convert.ToInt32(cboCategory.SelectedValue) : 0;

                // Gọi BLL để thực hiện tìm kiếm và cập nhật lại DataGridView sản phẩm
                dgvProducts.DataSource = ProductBLL.Instance.SearchProducts(name, categoryId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewProductDetail_Click_1(object sender, EventArgs e)
        {

            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xem chi tiết.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvProducts.CurrentRow;
            string message = $"--- Chi tiết sản phẩm ---\n\n" +
                             $"Tên: {row.Cells["ProductName"].Value}\n" +
                             $"Giá: {Convert.ToDecimal(row.Cells["Price"].Value):N0} VNĐ\n" +
                             $"Tồn kho: {row.Cells["StockQuantity"].Value}\n" +
                             $"Loại: {row.Cells["CategoryName"].Value}\n" +
                             $"Nhà cung cấp: {row.Cells["SupplierName"].Value}";

            MessageBox.Show(message, "Chi tiết sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAddToCart_Click_1(object sender, EventArgs e)
        {

            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để thêm vào giỏ.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvProducts.CurrentRow;
            int productId = Convert.ToInt32(selectedRow.Cells["ProductID"].Value);
            string productName = selectedRow.Cells["ProductName"].Value.ToString();
            decimal unitPrice = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
            int stock = Convert.ToInt32(selectedRow.Cells["StockQuantity"].Value);
            int quantityToAdd = (int)numQuantity.Value;

            if (quantityToAdd <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0.", "Số lượng không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow existingItem = _shoppingCart.AsEnumerable().FirstOrDefault(row => (int)row["ProductID"] == productId);
            int quantityInCart = (existingItem != null) ? (int)existingItem["Quantity"] : 0;

            if (quantityToAdd + quantityInCart > stock)
            {
                MessageBox.Show($"Không đủ hàng trong kho.\n\nTồn kho hiện tại: {stock}\nĐã có trong giỏ: {quantityInCart}", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (existingItem != null)
            {
                existingItem["Quantity"] = (int)existingItem["Quantity"] + quantityToAdd;
            }
            else
            {
                _shoppingCart.Rows.Add(productId, productName, quantityToAdd, unitPrice);
            }

            MessageBox.Show($"Đã thêm {quantityToAdd} sản phẩm '{productName}' vào giỏ hàng.", "Thêm thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {

            if (_shoppingCart.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal totalAmount = (decimal)_shoppingCart.Compute("SUM(Total)", "");
            string message = $"Tổng số sản phẩm trong giỏ: {_shoppingCart.Rows.Count}\n" +
                             $"Tổng tiền tạm tính: {totalAmount:N0} VNĐ\n\n" +
                             "Bạn có muốn tiến hành thanh toán không?";

            DialogResult result = MessageBox.Show(message, "Thông tin giỏ hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                btnPurchase.PerformClick(); // Tự động nhấn nút Mua hàng
            }
        }

        private void btnPurchase_Click_1(object sender, EventArgs e)
        {

            if (_shoppingCart.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang trống.", "Không thể thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn thanh toán cho các sản phẩm trong giỏ hàng không?", "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                Invoice mainInvoice = new Invoice
                {
                    CustomerID = _currentCustomerId,
                    EmployeeID = null, // Giao dịch online không có nhân viên cụ thể
                    InvoiceDate = DateTime.Now,
                    TotalAmount = (decimal)_shoppingCart.Compute("SUM(Total)", "")
                };

                List<InvoiceDetail> details = new List<InvoiceDetail>();
                foreach (DataRow row in _shoppingCart.Rows)
                {
                    details.Add(new InvoiceDetail
                    {
                        ProductID = (int)row["ProductID"],
                        Quantity = (int)row["Quantity"],
                        UnitPrice = (decimal)row["UnitPrice"]
                    });
                }

                if (InvoiceBLL.Instance.CreateInvoice(mainInvoice, details))
                {
                    MessageBox.Show($"Cảm ơn bạn đã mua hàng! Hóa đơn của bạn đã được tạo thành công.", "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitializeShoppingCart(); // Xóa giỏ hàng và tạo mới
                    LoadAllProducts();      // Tải lại danh sách sản phẩm để cập nhật tồn kho
                    LoadPurchaseHistory();  // Tải lại lịch sử mua hàng
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại. Số lượng sản phẩm trong kho có thể đã thay đổi. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi hệ thống khi thanh toán: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshPurchaseHistory_Click(object sender, EventArgs e)
        {
            LoadPurchaseHistory();
            MessageBox.Show("Đã làm mới lịch sử mua hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}