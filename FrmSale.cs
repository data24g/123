// =====================================================================
// === FrmSale.cs (PHIÊN BẢN ĐÃ SỬA LỖI KIỂU DỮ LIỆU) ===
// =====================================================================

using Store_X.BLL;
using Store_X.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Store_X
{
    public partial class FrmSale : Form
    {
        private int _currentEmployeeId;
        private int? _currentCustomerId = null;
        private DataTable _currentInvoiceDetails;

        public FrmSale(int employeeId)
        {
            InitializeComponent();
            _currentEmployeeId = employeeId;
        }

        private void FrmSale_Load(object sender, EventArgs e)
        {
            InitializeNewInvoice();
            LoadRecentInvoiceHistory();
        }

        private void InitializeNewInvoice()
        {
            _currentInvoiceDetails = new DataTable();
            _currentInvoiceDetails.Columns.Add("ProductID", typeof(int));
            _currentInvoiceDetails.Columns.Add("ProductName", typeof(string));
            _currentInvoiceDetails.Columns.Add("Quantity", typeof(int));
            _currentInvoiceDetails.Columns.Add("UnitPrice", typeof(decimal));
            _currentInvoiceDetails.Columns.Add("Total", typeof(decimal), "Quantity * UnitPrice");
            dgvInvoiceDetails.DataSource = _currentInvoiceDetails;
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            numQuantity.Value = 1;
            txtCustomerID.Clear();
            txtCustomerName.Text = "Retail Customer"; // Khách lẻ
            _currentCustomerId = null;
            if (_currentInvoiceDetails != null) _currentInvoiceDetails.Clear();
            txtProductID.Focus();
        }

        private void LoadRecentInvoiceHistory()
        {
            try
            {
                dgvHistory.DataSource = InvoiceBLL.Instance.GetRecentInvoices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice history: " + ex.Message);
            }
        }

        private void txtProductID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text)) return;
            try
            {
                int productId = Convert.ToInt32(txtProductID.Text);
                DataRow product = ProductBLL.Instance.GetProductById(productId);
                if (product != null)
                {
                    txtProductName.Text = product["ProductName"].ToString();
                    txtPrice.Text = product["Price"].ToString();
                    numQuantity.Focus();
                }
                else
                {
                    MessageBox.Show("Product with this ID not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProductName.Clear();
                    txtPrice.Clear();
                }
            }
            catch (FormatException) { /* Ignore format errors */ }
        }

        private void btnFindCustomer_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerID.Text))
            {
                txtCustomerName.Text = "Retail Customer";
                _currentCustomerId = null;
                return;
            }
            try
            {
                int customerId = Convert.ToInt32(txtCustomerID.Text);
                DataTable customerTable = CustomerBLL.Instance.GetCustomerById(customerId);
                if (customerTable != null && customerTable.Rows.Count > 0)
                {
                    DataRow customer = customerTable.Rows[0];
                    txtCustomerName.Text = customer["CustomerName"].ToString();
                    _currentCustomerId = customerId;
                }
                else
                {
                    MessageBox.Show("Customer with this ID not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCustomerName.Clear();
                    _currentCustomerId = null;
                }
            }
            catch (FormatException) { MessageBox.Show("Customer ID must be a number.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please find a valid product to add to the invoice.", "Missing Product Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(txtProductID.Text);
            string productName = txtProductName.Text;
            int quantity = (int)numQuantity.Value;
            decimal unitPrice = Convert.ToDecimal(txtPrice.Text);

            // Check if the product is already in the invoice
            foreach (DataRow row in _currentInvoiceDetails.Rows)
            {
                if ((int)row["ProductID"] == productId)
                {
                    // If it exists, just update the quantity
                    row["Quantity"] = (int)row["Quantity"] + quantity;
                    goto ClearProductInput; // Jump to the clearing section
                }
            }

            // If the product doesn't exist, add a new row to the DataTable
            _currentInvoiceDetails.Rows.Add(productId, productName, quantity, unitPrice);

        ClearProductInput:
            // Clear product fields for the next entry
            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            numQuantity.Value = 1;
            txtProductID.Focus();
        }
        
        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            if (_currentInvoiceDetails.Rows.Count == 0)
            {
                MessageBox.Show("The invoice must have at least one product.", "Empty Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Invoice invoiceHeader = new Invoice
                {
                    EmployeeID = _currentEmployeeId,
                    CustomerID = _currentCustomerId, // Can be null for retail customers
                    InvoiceDate = DateTime.Now,
                    TotalAmount = (decimal)_currentInvoiceDetails.Compute("SUM(Total)", "")
                };

                List<InvoiceDetail> invoiceDetailsList = new List<InvoiceDetail>();
                foreach (DataRow row in _currentInvoiceDetails.Rows)
                {
                    invoiceDetailsList.Add(new InvoiceDetail
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        UnitPrice = Convert.ToDecimal(row["UnitPrice"])
                    });
                }
                
                if (InvoiceBLL.Instance.CreateInvoice(invoiceHeader, invoiceDetailsList))
                {
                    MessageBox.Show("Invoice created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitializeNewInvoice();
                    LoadRecentInvoiceHistory();
                }
                else
                {
                    MessageBox.Show("Failed to create invoice! The reason may be insufficient stock.", "Business Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A system error occurred while creating the invoice: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnFindInvoice_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is under development.", "Notification");
        }

        private void btnRefreshHistory_Click(object sender, EventArgs e)
        {
            LoadRecentInvoiceHistory();
            MessageBox.Show("Invoice history has been refreshed.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}