namespace Store_X
{
    partial class FrmHistoryWareholder
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
            this.button3 = new System.Windows.Forms.Button();
            this.dgvWarehouseHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarehouseHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(32, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(170, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "refresh";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dgvWarehouseHistory
            // 
            this.dgvWarehouseHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWarehouseHistory.Location = new System.Drawing.Point(32, 63);
            this.dgvWarehouseHistory.Name = "dgvWarehouseHistory";
            this.dgvWarehouseHistory.RowHeadersWidth = 51;
            this.dgvWarehouseHistory.RowTemplate.Height = 24;
            this.dgvWarehouseHistory.Size = new System.Drawing.Size(734, 357);
            this.dgvWarehouseHistory.TabIndex = 13;
            this.dgvWarehouseHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWarehouseHistory_CellClick);
            // 
            // FrmHistoryWareholder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvWarehouseHistory);
            this.Controls.Add(this.button3);
            this.Name = "FrmHistoryWareholder";
            this.Text = "Warehouse History";
            this.Load += new System.EventHandler(this.FrmHistoryWareholder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarehouseHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dgvWarehouseHistory;
    }
}