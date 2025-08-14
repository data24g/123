using Store_X.BLL;
using System;
using System.Windows.Forms;

namespace Store_X
{
    public partial class FrmHistoryWareholder : Form
    {
        public FrmHistoryWareholder()
        {
            InitializeComponent();
        }

        private void FrmHistoryWareholder_Load(object sender, EventArgs e)
        {
            // Load warehouse history as soon as the form opens
            LoadWarehouseHistory();
        }

        /// <summary>
        /// Loads the entire warehouse transaction history into the DataGridView.
        /// </summary>
        private void LoadWarehouseHistory()
        {
            try
            {
                dgvWarehouseHistory.DataSource = WarehouseHistoryBLL.Instance.GetAllWarehouseHistory();
                // Format columns for better readability
                dgvWarehouseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (dgvWarehouseHistory.Columns["HistoryID"] != null)
                    dgvWarehouseHistory.Columns["HistoryID"].HeaderText = "Transaction ID";
                if (dgvWarehouseHistory.Columns["ProductName"] != null)
                    dgvWarehouseHistory.Columns["ProductName"].HeaderText = "Product Name";
                if (dgvWarehouseHistory.Columns["EmployeeName"] != null)
                    dgvWarehouseHistory.Columns["EmployeeName"].HeaderText = "Employee";
                if (dgvWarehouseHistory.Columns["TransactionType"] != null)
                    dgvWarehouseHistory.Columns["TransactionType"].HeaderText = "Transaction Type";
                if (dgvWarehouseHistory.Columns["Quantity"] != null)
                    dgvWarehouseHistory.Columns["Quantity"].HeaderText = "Quantity";
                if (dgvWarehouseHistory.Columns["TransactionDate"] != null)
                    dgvWarehouseHistory.Columns["TransactionDate"].HeaderText = "Time";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading warehouse history: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// "refresh" button: Reloads the history list.
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            LoadWarehouseHistory();
            MessageBox.Show("Warehouse history has been refreshed.", "Notification",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// CellClick event to view details of a transaction.
        /// </summary>
        private void dgvWarehouseHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvWarehouseHistory.Rows[e.RowIndex];
                string message = $"--- Warehouse Transaction Details ---\n\n" +
                                 $"Transaction ID: {row.Cells["HistoryID"].Value}\n" +
                                 $"Product: {row.Cells["ProductName"].Value}\n" +
                                 $"Transaction Type: {row.Cells["TransactionType"].Value}\n" +
                                 $"Quantity: {row.Cells["Quantity"].Value}\n" +
                                 $"Performed by: {row.Cells["EmployeeName"].Value}\n" +
                                 $"Time: {Convert.ToDateTime(row.Cells["TransactionDate"].Value):dd/MM/yyyy HH:mm:ss}";

                MessageBox.Show(message, "Transaction Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}