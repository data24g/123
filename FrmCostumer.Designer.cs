namespace Store_X
{
    partial class FrmCustomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.btnSearchProduct = new System.Windows.Forms.Button();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.btnRefreshPurchaseHistory = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnViewProductDetail = new System.Windows.Forms.Button();
            this.txtProductNameSearch = new System.Windows.Forms.TextBox();
            this.txtCusProductID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnViewCart = new System.Windows.Forms.Button();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnViewReports = new System.Windows.Forms.Button();
            this.dgvPurchaseHistory = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Location = new System.Drawing.Point(187, 221);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(131, 23);
            this.btnAddToCart.TabIndex = 0;
            this.btnAddToCart.Text = "add to cart";
            this.btnAddToCart.UseVisualStyleBackColor = true;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click_1);
            // 
            // btnSearchProduct
            // 
            this.btnSearchProduct.Location = new System.Drawing.Point(42, 221);
            this.btnSearchProduct.Name = "btnSearchProduct";
            this.btnSearchProduct.Size = new System.Drawing.Size(111, 23);
            this.btnSearchProduct.TabIndex = 1;
            this.btnSearchProduct.Text = "find product";
            this.btnSearchProduct.UseVisualStyleBackColor = true;
            this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click_1);
            // 
            // btnPurchase
            // 
            this.btnPurchase.Location = new System.Drawing.Point(594, 221);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(108, 23);
            this.btnPurchase.TabIndex = 2;
            this.btnPurchase.Text = "buy";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click_1);
            // 
            // btnRefreshPurchaseHistory
            // 
            this.btnRefreshPurchaseHistory.Location = new System.Drawing.Point(90, 419);
            this.btnRefreshPurchaseHistory.Name = "btnRefreshPurchaseHistory";
            this.btnRefreshPurchaseHistory.Size = new System.Drawing.Size(158, 23);
            this.btnRefreshPurchaseHistory.TabIndex = 3;
            this.btnRefreshPurchaseHistory.Text = "refresh";
            this.btnRefreshPurchaseHistory.UseVisualStyleBackColor = true;
            this.btnRefreshPurchaseHistory.Click += new System.EventHandler(this.btnRefreshPurchaseHistory_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboCategory);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numQuantity);
            this.groupBox1.Controls.Add(this.btnViewProductDetail);
            this.groupBox1.Controls.Add(this.txtProductNameSearch);
            this.groupBox1.Controls.Add(this.txtCusProductID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(42, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 178);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "product information";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(482, 47);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(121, 24);
            this.cboCategory.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(370, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Quantity";
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(482, 86);
            this.numQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 22);
            this.numQuantity.TabIndex = 12;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnViewProductDetail
            // 
            this.btnViewProductDetail.Location = new System.Drawing.Point(482, 121);
            this.btnViewProductDetail.Name = "btnViewProductDetail";
            this.btnViewProductDetail.Size = new System.Drawing.Size(159, 51);
            this.btnViewProductDetail.TabIndex = 11;
            this.btnViewProductDetail.Text = "add product to invoice list";
            this.btnViewProductDetail.UseVisualStyleBackColor = true;
            this.btnViewProductDetail.Click += new System.EventHandler(this.btnViewProductDetail_Click_1);
            // 
            // txtProductNameSearch
            // 
            this.txtProductNameSearch.Location = new System.Drawing.Point(156, 89);
            this.txtProductNameSearch.Name = "txtProductNameSearch";
            this.txtProductNameSearch.Size = new System.Drawing.Size(100, 22);
            this.txtProductNameSearch.TabIndex = 7;
            // 
            // txtCusProductID
            // 
            this.txtCusProductID.Location = new System.Drawing.Point(149, 47);
            this.txtCusProductID.Name = "txtCusProductID";
            this.txtCusProductID.Size = new System.Drawing.Size(100, 22);
            this.txtCusProductID.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(361, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Product Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product ID";
            // 
            // btnViewCart
            // 
            this.btnViewCart.Location = new System.Drawing.Point(393, 221);
            this.btnViewCart.Name = "btnViewCart";
            this.btnViewCart.Size = new System.Drawing.Size(104, 23);
            this.btnViewCart.TabIndex = 6;
            this.btnViewCart.Text = "view cart";
            this.btnViewCart.UseVisualStyleBackColor = true;
            this.btnViewCart.Click += new System.EventHandler(this.btnViewCart_Click);
            // 
            // dgvProducts
            // 
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(31, 266);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.RowHeadersWidth = 51;
            this.dgvProducts.RowTemplate.Height = 24;
            this.dgvProducts.Size = new System.Drawing.Size(246, 147);
            this.dgvProducts.TabIndex = 7;
            // 
            // dgvCart
            // 
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Location = new System.Drawing.Point(321, 266);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersWidth = 51;
            this.dgvCart.RowTemplate.Height = 24;
            this.dgvCart.Size = new System.Drawing.Size(155, 147);
            this.dgvCart.TabIndex = 8;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(658, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Log out ";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnViewReports
            // 
            this.btnViewReports.Location = new System.Drawing.Point(501, 12);
            this.btnViewReports.Name = "btnViewReports";
            this.btnViewReports.Size = new System.Drawing.Size(139, 23);
            this.btnViewReports.TabIndex = 10;
            this.btnViewReports.Text = "View Reports";
            this.btnViewReports.UseVisualStyleBackColor = true;
            // 
            // dgvPurchaseHistory
            // 
            this.dgvPurchaseHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseHistory.Location = new System.Drawing.Point(541, 265);
            this.dgvPurchaseHistory.Name = "dgvPurchaseHistory";
            this.dgvPurchaseHistory.RowHeadersWidth = 51;
            this.dgvPurchaseHistory.RowTemplate.Height = 24;
            this.dgvPurchaseHistory.Size = new System.Drawing.Size(191, 147);
            this.dgvPurchaseHistory.TabIndex = 11;
            // 
            // FrmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvPurchaseHistory);
            this.Controls.Add(this.btnViewReports);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.btnViewCart);
            this.Controls.Add(this.btnRefreshPurchaseHistory);
            this.Controls.Add(this.btnPurchase);
            this.Controls.Add(this.btnSearchProduct);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAddToCart);
            this.Name = "FrmCustomer";
            this.Text = "FrmCustomer";
            this.Load += new System.EventHandler(this.FrmCustomer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.Button btnSearchProduct;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnRefreshPurchaseHistory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProductNameSearch;
        private System.Windows.Forms.TextBox txtCusProductID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnViewCart;
        private System.Windows.Forms.Button btnViewProductDetail;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnViewReports;
        private System.Windows.Forms.DataGridView dgvPurchaseHistory;
        private System.Windows.Forms.ComboBox cboCategory;
    }
}