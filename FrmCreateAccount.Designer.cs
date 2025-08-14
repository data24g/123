namespace Store_X
{
    partial class FrmCreateAccount
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            this.txtCheckPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKindOfAccount = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(242, 323);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(116, 62);
            this.btnCreate.TabIndex = 36;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click_1);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dtpCreateDate);
            this.groupBox7.Controls.Add(this.txtCheckPassword);
            this.groupBox7.Controls.Add(this.txtPassword);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.txtAddress);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.cboKindOfAccount);
            this.groupBox7.Controls.Add(this.label36);
            this.groupBox7.Controls.Add(this.txtPhone);
            this.groupBox7.Controls.Add(this.txtAccountName);
            this.groupBox7.Controls.Add(this.txtID);
            this.groupBox7.Controls.Add(this.label37);
            this.groupBox7.Controls.Add(this.label38);
            this.groupBox7.Controls.Add(this.label40);
            this.groupBox7.Controls.Add(this.label41);
            this.groupBox7.Location = new System.Drawing.Point(32, 46);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(718, 247);
            this.groupBox7.TabIndex = 35;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Account Information";
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.Enabled = false;
            this.dtpCreateDate.Location = new System.Drawing.Point(165, 177);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(200, 22);
            this.dtpCreateDate.TabIndex = 19;
            // 
            // txtCheckPassword
            // 
            this.txtCheckPassword.Location = new System.Drawing.Point(451, 122);
            this.txtCheckPassword.Name = "txtCheckPassword";
            this.txtCheckPassword.PasswordChar = '*';
            this.txtCheckPassword.Size = new System.Drawing.Size(100, 22);
            this.txtCheckPassword.TabIndex = 18;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(451, 75);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 22);
            this.txtPassword.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Check your pasword";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Pasword ";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(106, 128);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(100, 22);
            this.txtAddress.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Address";
            // 
            // cboKindOfAccount
            // 
            this.cboKindOfAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKindOfAccount.FormattingEnabled = true;
            this.cboKindOfAccount.Location = new System.Drawing.Point(347, 29);
            this.cboKindOfAccount.Name = "cboKindOfAccount";
            this.cboKindOfAccount.Size = new System.Drawing.Size(143, 24);
            this.cboKindOfAccount.TabIndex = 12;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(244, 32);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(97, 16);
            this.label36.TabIndex = 11;
            this.label36.Text = "Kind of account";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(586, 26);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 22);
            this.txtPhone.TabIndex = 9;
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(106, 72);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(100, 22);
            this.txtAccountName.TabIndex = 6;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(99, 26);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(100, 22);
            this.txtID.TabIndex = 5;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(505, 29);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(46, 16);
            this.label37.TabIndex = 4;
            this.label37.Text = "Phone";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(21, 183);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(77, 16);
            this.label38.TabIndex = 3;
            this.label38.Text = "Create date";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 69);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(92, 16);
            this.label40.TabIndex = 1;
            this.label40.Text = "Account name";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(29, 29);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(20, 16);
            this.label41.TabIndex = 0;
            this.label41.Text = "ID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(425, 323);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 62);
            this.button1.TabIndex = 37;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmCreateAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBox7);
            this.Name = "FrmCreateAccount";
            this.Text = "FrmCreateAccount";
            this.Load += new System.EventHandler(this.FrmCreateAccount_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cboKindOfAccount;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtCheckPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.Button button1;
    }
}