namespace BTL_QLsieuthi
{
    partial class FrmTaiKhoan
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
            this.grbTaiKhoan = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkHien = new System.Windows.Forms.CheckBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.gbDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvTaiKhoan = new System.Windows.Forms.DataGridView();
            this.colTenDangNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grbTaiKhoan.SuspendLayout();
            this.gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).BeginInit();
            this.SuspendLayout();
            // 
            // grbTaiKhoan
            // 
            this.grbTaiKhoan.Controls.Add(this.button4);
            this.grbTaiKhoan.Controls.Add(this.button3);
            this.grbTaiKhoan.Controls.Add(this.button2);
            this.grbTaiKhoan.Controls.Add(this.button1);
            this.grbTaiKhoan.Controls.Add(this.chkHien);
            this.grbTaiKhoan.Controls.Add(this.txtMatKhau);
            this.grbTaiKhoan.Controls.Add(this.lblMatKhau);
            this.grbTaiKhoan.Controls.Add(this.txtTenDangNhap);
            this.grbTaiKhoan.Controls.Add(this.lblTenDangNhap);
            this.grbTaiKhoan.Location = new System.Drawing.Point(10, 30);
            this.grbTaiKhoan.Name = "grbTaiKhoan";
            this.grbTaiKhoan.Size = new System.Drawing.Size(810, 130);
            this.grbTaiKhoan.TabIndex = 0;
            this.grbTaiKhoan.TabStop = false;
            this.grbTaiKhoan.Text = "Tài khoản";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(703, 92);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 32);
            this.button4.TabIndex = 8;
            this.button4.Text = "Xóa";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(608, 92);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 32);
            this.button3.TabIndex = 7;
            this.button3.Text = "Sửa";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(512, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 32);
            this.button2.TabIndex = 6;
            this.button2.Text = "Thêm";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(417, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 32);
            this.button1.TabIndex = 5;
            this.button1.Text = "Làm mới";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkHien
            // 
            this.chkHien.AutoSize = true;
            this.chkHien.Location = new System.Drawing.Point(340, 69);
            this.chkHien.Name = "chkHien";
            this.chkHien.Size = new System.Drawing.Size(57, 20);
            this.chkHien.TabIndex = 4;
            this.chkHien.Text = "Hiện";
            this.chkHien.UseVisualStyleBackColor = true;
            this.chkHien.CheckedChanged += new System.EventHandler(this.chkHien_CheckedChanged);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(130, 67);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(200, 22);
            this.txtMatKhau.TabIndex = 3;
            this.txtMatKhau.UseSystemPasswordChar = true;
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new System.Drawing.Point(20, 70);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(62, 16);
            this.lblMatKhau.TabIndex = 2;
            this.lblMatKhau.Text = "Mật Khẩu";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(130, 32);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(200, 22);
            this.txtTenDangNhap.TabIndex = 1;
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Location = new System.Drawing.Point(20, 35);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(98, 16);
            this.lblTenDangNhap.TabIndex = 0;
            this.lblTenDangNhap.Text = "Tên đăng nhập";
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.Controls.Add(this.dgvTaiKhoan);
            this.gbDanhSach.Location = new System.Drawing.Point(10, 170);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(810, 310);
            this.gbDanhSach.TabIndex = 1;
            this.gbDanhSach.TabStop = false;
            this.gbDanhSach.Text = "Danh sách tài khoản";
            // 
            // dgvTaiKhoan
            // 
            this.dgvTaiKhoan.AllowUserToAddRows = false;
            this.dgvTaiKhoan.AllowUserToDeleteRows = false;
            this.dgvTaiKhoan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTaiKhoan.CausesValidation = false;
            this.dgvTaiKhoan.ColumnHeadersHeight = 29;
            this.dgvTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTaiKhoan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTenDangNhap,
            this.colQuyen});
            this.dgvTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaiKhoan.Location = new System.Drawing.Point(3, 18);
            this.dgvTaiKhoan.MultiSelect = false;
            this.dgvTaiKhoan.Name = "dgvTaiKhoan";
            this.dgvTaiKhoan.ReadOnly = true;
            this.dgvTaiKhoan.RowHeadersVisible = false;
            this.dgvTaiKhoan.RowHeadersWidth = 51;
            this.dgvTaiKhoan.RowTemplate.Height = 24;
            this.dgvTaiKhoan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaiKhoan.Size = new System.Drawing.Size(804, 289);
            this.dgvTaiKhoan.TabIndex = 0;
            // 
            // colTenDangNhap
            // 
            this.colTenDangNhap.HeaderText = "Tên đăng nhập";
            this.colTenDangNhap.MinimumWidth = 6;
            this.colTenDangNhap.Name = "colTenDangNhap";
            this.colTenDangNhap.ReadOnly = true;
            this.colTenDangNhap.Width = 127;
            // 
            // colQuyen
            // 
            this.colQuyen.HeaderText = "Quyền";
            this.colQuyen.MinimumWidth = 6;
            this.colQuyen.Name = "colQuyen";
            this.colQuyen.ReadOnly = true;
            this.colQuyen.Width = 75;
            // 
            // FrmTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 493);
            this.Controls.Add(this.gbDanhSach);
            this.Controls.Add(this.grbTaiKhoan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmTaiKhoan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Siêu Thị MAD - [Tài khoản]";
            this.Load += new System.EventHandler(this.FrmTaiKhoan_Load);
            this.grbTaiKhoan.ResumeLayout(false);
            this.grbTaiKhoan.PerformLayout();
            this.gbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbTaiKhoan;
        private System.Windows.Forms.GroupBox gbDanhSach;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.CheckBox chkHien;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenDangNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuyen;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}