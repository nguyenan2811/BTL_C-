namespace BTL_QLsieuthi
{
    partial class FrmSanPham
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
            this.grbThongTin = new System.Windows.Forms.GroupBox();
            this.dtpNgaySX = new System.Windows.Forms.DateTimePicker();
            this.cboLoaiSP = new System.Windows.Forms.ComboBox();
            this.txtThuongHieu = new System.Windows.Forms.TextBox();
            this.txtChatLieu = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.txtDonGiaBan = new System.Windows.Forms.TextBox();
            this.txtKichThuoc = new System.Windows.Forms.TextBox();
            this.txtMauSac = new System.Windows.Forms.TextBox();
            this.txtSoLuongTon = new System.Windows.Forms.TextBox();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.lblThuongHieu = new System.Windows.Forms.Label();
            this.lblChatLieu = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblNgaySX = new System.Windows.Forms.Label();
            this.lblDonGiaBan = new System.Windows.Forms.Label();
            this.lblKichThuoc = new System.Windows.Forms.Label();
            this.lblMauSac = new System.Windows.Forms.Label();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.lblLoaiSP = new System.Windows.Forms.Label();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblMaSP = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.grbTimKiem = new System.Windows.Forms.GroupBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.grbDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenLoaiSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChatLieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThuongHieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongTon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMauSac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKichThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbThongTin.SuspendLayout();
            this.grbTimKiem.SuspendLayout();
            this.grbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // grbThongTin
            // 
            this.grbThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbThongTin.Controls.Add(this.dtpNgaySX);
            this.grbThongTin.Controls.Add(this.cboLoaiSP);
            this.grbThongTin.Controls.Add(this.txtThuongHieu);
            this.grbThongTin.Controls.Add(this.txtChatLieu);
            this.grbThongTin.Controls.Add(this.txtMoTa);
            this.grbThongTin.Controls.Add(this.txtDonGiaBan);
            this.grbThongTin.Controls.Add(this.txtKichThuoc);
            this.grbThongTin.Controls.Add(this.txtMauSac);
            this.grbThongTin.Controls.Add(this.txtSoLuongTon);
            this.grbThongTin.Controls.Add(this.txtTenSP);
            this.grbThongTin.Controls.Add(this.txtMaSP);
            this.grbThongTin.Controls.Add(this.lblThuongHieu);
            this.grbThongTin.Controls.Add(this.lblChatLieu);
            this.grbThongTin.Controls.Add(this.lblMoTa);
            this.grbThongTin.Controls.Add(this.lblNgaySX);
            this.grbThongTin.Controls.Add(this.lblDonGiaBan);
            this.grbThongTin.Controls.Add(this.lblKichThuoc);
            this.grbThongTin.Controls.Add(this.lblMauSac);
            this.grbThongTin.Controls.Add(this.lblSoLuongTon);
            this.grbThongTin.Controls.Add(this.lblLoaiSP);
            this.grbThongTin.Controls.Add(this.lblTenSP);
            this.grbThongTin.Controls.Add(this.lblMaSP);
            this.grbThongTin.Controls.Add(this.btnXoa);
            this.grbThongTin.Controls.Add(this.btnSua);
            this.grbThongTin.Controls.Add(this.btnThem);
            this.grbThongTin.Controls.Add(this.btnLamMoi);
            this.grbThongTin.Location = new System.Drawing.Point(12, 12);
            this.grbThongTin.Name = "grbThongTin";
            this.grbThongTin.Size = new System.Drawing.Size(1158, 202);
            this.grbThongTin.TabIndex = 0;
            this.grbThongTin.TabStop = false;
            this.grbThongTin.Text = "Thông tin sản phẩm";
            // 
            // dtpNgaySX
            // 
            this.dtpNgaySX.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySX.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySX.Location = new System.Drawing.Point(423, 128);
            this.dtpNgaySX.Name = "dtpNgaySX";
            this.dtpNgaySX.Size = new System.Drawing.Size(180, 22);
            this.dtpNgaySX.TabIndex = 10;
            // 
            // cboLoaiSP
            // 
            this.cboLoaiSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSP.FormattingEnabled = true;
            this.cboLoaiSP.Location = new System.Drawing.Point(93, 62);
            this.cboLoaiSP.Name = "cboLoaiSP";
            this.cboLoaiSP.Size = new System.Drawing.Size(180, 24);
            this.cboLoaiSP.TabIndex = 3;
            // 
            // txtThuongHieu
            // 
            this.txtThuongHieu.Location = new System.Drawing.Point(721, 62);
            this.txtThuongHieu.Name = "txtThuongHieu";
            this.txtThuongHieu.Size = new System.Drawing.Size(180, 22);
            this.txtThuongHieu.TabIndex = 6;
            // 
            // txtChatLieu
            // 
            this.txtChatLieu.Location = new System.Drawing.Point(423, 62);
            this.txtChatLieu.Name = "txtChatLieu";
            this.txtChatLieu.Size = new System.Drawing.Size(180, 22);
            this.txtChatLieu.TabIndex = 5;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(93, 162);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(808, 22);
            this.txtMoTa.TabIndex = 11;
            // 
            // txtDonGiaBan
            // 
            this.txtDonGiaBan.Location = new System.Drawing.Point(93, 128);
            this.txtDonGiaBan.Name = "txtDonGiaBan";
            this.txtDonGiaBan.Size = new System.Drawing.Size(180, 22);
            this.txtDonGiaBan.TabIndex = 9;
            // 
            // txtKichThuoc
            // 
            this.txtKichThuoc.Location = new System.Drawing.Point(721, 95);
            this.txtKichThuoc.Name = "txtKichThuoc";
            this.txtKichThuoc.Size = new System.Drawing.Size(180, 22);
            this.txtKichThuoc.TabIndex = 8;
            // 
            // txtMauSac
            // 
            this.txtMauSac.Location = new System.Drawing.Point(423, 95);
            this.txtMauSac.Name = "txtMauSac";
            this.txtMauSac.Size = new System.Drawing.Size(180, 22);
            this.txtMauSac.TabIndex = 7;
            this.txtMauSac.TextChanged += new System.EventHandler(this.txtMauSac_TextChanged);
            // 
            // txtSoLuongTon
            // 
            this.txtSoLuongTon.Location = new System.Drawing.Point(93, 95);
            this.txtSoLuongTon.Name = "txtSoLuongTon";
            this.txtSoLuongTon.Size = new System.Drawing.Size(180, 22);
            this.txtSoLuongTon.TabIndex = 4;
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(423, 30);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(478, 22);
            this.txtTenSP.TabIndex = 2;
            // 
            // txtMaSP
            // 
            this.txtMaSP.Location = new System.Drawing.Point(93, 30);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.ReadOnly = true;
            this.txtMaSP.Size = new System.Drawing.Size(180, 22);
            this.txtMaSP.TabIndex = 1;
            // 
            // lblThuongHieu
            // 
            this.lblThuongHieu.AutoSize = true;
            this.lblThuongHieu.Location = new System.Drawing.Point(643, 65);
            this.lblThuongHieu.Name = "lblThuongHieu";
            this.lblThuongHieu.Size = new System.Drawing.Size(50, 16);
            this.lblThuongHieu.TabIndex = 0;
            this.lblThuongHieu.Text = "T. Hiệu";
            // 
            // lblChatLieu
            // 
            this.lblChatLieu.AutoSize = true;
            this.lblChatLieu.Location = new System.Drawing.Point(343, 65);
            this.lblChatLieu.Name = "lblChatLieu";
            this.lblChatLieu.Size = new System.Drawing.Size(47, 16);
            this.lblChatLieu.TabIndex = 0;
            this.lblChatLieu.Text = "C. Liệu";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(22, 165);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(40, 16);
            this.lblMoTa.TabIndex = 0;
            this.lblMoTa.Text = "Mô tả";
            // 
            // lblNgaySX
            // 
            this.lblNgaySX.AutoSize = true;
            this.lblNgaySX.Location = new System.Drawing.Point(343, 131);
            this.lblNgaySX.Name = "lblNgaySX";
            this.lblNgaySX.Size = new System.Drawing.Size(60, 16);
            this.lblNgaySX.TabIndex = 0;
            this.lblNgaySX.Text = "Ngày SX";
            // 
            // lblDonGiaBan
            // 
            this.lblDonGiaBan.AutoSize = true;
            this.lblDonGiaBan.Location = new System.Drawing.Point(22, 131);
            this.lblDonGiaBan.Name = "lblDonGiaBan";
            this.lblDonGiaBan.Size = new System.Drawing.Size(43, 16);
            this.lblDonGiaBan.TabIndex = 0;
            this.lblDonGiaBan.Text = "Đ. Giá";
            // 
            // lblKichThuoc
            // 
            this.lblKichThuoc.AutoSize = true;
            this.lblKichThuoc.Location = new System.Drawing.Point(643, 98);
            this.lblKichThuoc.Name = "lblKichThuoc";
            this.lblKichThuoc.Size = new System.Drawing.Size(59, 16);
            this.lblKichThuoc.TabIndex = 0;
            this.lblKichThuoc.Text = "K. Thước";
            // 
            // lblMauSac
            // 
            this.lblMauSac.AutoSize = true;
            this.lblMauSac.Location = new System.Drawing.Point(343, 98);
            this.lblMauSac.Name = "lblMauSac";
            this.lblMauSac.Size = new System.Drawing.Size(48, 16);
            this.lblMauSac.TabIndex = 0;
            this.lblMauSac.Text = "M. Sắc";
            // 
            // lblSoLuongTon
            // 
            this.lblSoLuongTon.AutoSize = true;
            this.lblSoLuongTon.Location = new System.Drawing.Point(22, 98);
            this.lblSoLuongTon.Name = "lblSoLuongTon";
            this.lblSoLuongTon.Size = new System.Drawing.Size(50, 16);
            this.lblSoLuongTon.TabIndex = 0;
            this.lblSoLuongTon.Text = "SL Tồn";
            // 
            // lblLoaiSP
            // 
            this.lblLoaiSP.AutoSize = true;
            this.lblLoaiSP.Location = new System.Drawing.Point(22, 65);
            this.lblLoaiSP.Name = "lblLoaiSP";
            this.lblLoaiSP.Size = new System.Drawing.Size(54, 16);
            this.lblLoaiSP.TabIndex = 0;
            this.lblLoaiSP.Text = "Loại SP";
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Location = new System.Drawing.Point(343, 33);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(52, 16);
            this.lblTenSP.TabIndex = 0;
            this.lblTenSP.Text = "Tên SP";
            // 
            // lblMaSP
            // 
            this.lblMaSP.AutoSize = true;
            this.lblMaSP.Location = new System.Drawing.Point(22, 33);
            this.lblMaSP.Name = "lblMaSP";
            this.lblMaSP.Size = new System.Drawing.Size(47, 16);
            this.lblMaSP.TabIndex = 0;
            this.lblMaSP.Text = "Mã SP";
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(947, 131);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(144, 28);
            this.btnXoa.TabIndex = 15;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(947, 96);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(144, 28);
            this.btnSua.TabIndex = 14;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(947, 61);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(144, 28);
            this.btnThem.TabIndex = 13;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(947, 26);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(144, 28);
            this.btnLamMoi.TabIndex = 12;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // grbTimKiem
            // 
            this.grbTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTimKiem.Controls.Add(this.btnTim);
            this.grbTimKiem.Controls.Add(this.txtTimKiem);
            this.grbTimKiem.Controls.Add(this.lblTimKiem);
            this.grbTimKiem.Location = new System.Drawing.Point(12, 220);
            this.grbTimKiem.Name = "grbTimKiem";
            this.grbTimKiem.Size = new System.Drawing.Size(1158, 70);
            this.grbTimKiem.TabIndex = 1;
            this.grbTimKiem.TabStop = false;
            this.grbTimKiem.Text = "Tìm kiếm";
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(947, 24);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(144, 28);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click_1);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(346, 28);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(555, 22);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(256, 31);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(86, 16);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Nhập từ khóa";
            // 
            // grbDanhSach
            // 
            this.grbDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbDanhSach.Controls.Add(this.dgvSanPham);
            this.grbDanhSach.Location = new System.Drawing.Point(12, 296);
            this.grbDanhSach.Name = "grbDanhSach";
            this.grbDanhSach.Size = new System.Drawing.Size(1158, 342);
            this.grbDanhSach.TabIndex = 2;
            this.grbDanhSach.TabStop = false;
            this.grbDanhSach.Text = "Danh sách sản phẩm";
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanPham.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSP,
            this.colTenSP,
            this.colTenLoaiSP,
            this.colChatLieu,
            this.colThuongHieu,
            this.colSoLuongTon,
            this.colMauSac,
            this.colKichThuoc,
            this.colDonGiaBan,
            this.colNgaySX,
            this.colMoTa});
            this.dgvSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSanPham.Location = new System.Drawing.Point(3, 18);
            this.dgvSanPham.MultiSelect = false;
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.RowHeadersWidth = 51;
            this.dgvSanPham.RowTemplate.Height = 24;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(1152, 321);
            this.dgvSanPham.TabIndex = 0;
            // 
            // colMaSP
            // 
            this.colMaSP.DataPropertyName = "MaSP";
            this.colMaSP.HeaderText = "Mã SP";
            this.colMaSP.MinimumWidth = 6;
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.ReadOnly = true;
            // 
            // colTenSP
            // 
            this.colTenSP.DataPropertyName = "TenSP";
            this.colTenSP.HeaderText = "Tên SP";
            this.colTenSP.MinimumWidth = 6;
            this.colTenSP.Name = "colTenSP";
            // 
            // colTenLoaiSP
            // 
            this.colTenLoaiSP.DataPropertyName = "TenLoaiSP";
            this.colTenLoaiSP.HeaderText = "Loại SP";
            this.colTenLoaiSP.MinimumWidth = 6;
            this.colTenLoaiSP.Name = "colTenLoaiSP";
            // 
            // colChatLieu
            // 
            this.colChatLieu.DataPropertyName = "ChatLieu";
            this.colChatLieu.HeaderText = "Chất liệu";
            this.colChatLieu.MinimumWidth = 6;
            this.colChatLieu.Name = "colChatLieu";
            // 
            // colThuongHieu
            // 
            this.colThuongHieu.DataPropertyName = "ThuongHieu";
            this.colThuongHieu.HeaderText = "Thương hiệu";
            this.colThuongHieu.MinimumWidth = 6;
            this.colThuongHieu.Name = "colThuongHieu";
            // 
            // colSoLuongTon
            // 
            this.colSoLuongTon.DataPropertyName = "SoLuongTon";
            this.colSoLuongTon.HeaderText = "SL Tồn";
            this.colSoLuongTon.MinimumWidth = 6;
            this.colSoLuongTon.Name = "colSoLuongTon";
            // 
            // colMauSac
            // 
            this.colMauSac.DataPropertyName = "MauSac";
            this.colMauSac.HeaderText = "Màu sắc";
            this.colMauSac.MinimumWidth = 6;
            this.colMauSac.Name = "colMauSac";
            // 
            // colKichThuoc
            // 
            this.colKichThuoc.DataPropertyName = "KichThuoc";
            this.colKichThuoc.HeaderText = "Kích thước";
            this.colKichThuoc.MinimumWidth = 6;
            this.colKichThuoc.Name = "colKichThuoc";
            // 
            // colDonGiaBan
            // 
            this.colDonGiaBan.DataPropertyName = "DonGiaBan";
            this.colDonGiaBan.HeaderText = "Đơn giá bán";
            this.colDonGiaBan.MinimumWidth = 6;
            this.colDonGiaBan.Name = "colDonGiaBan";
            // 
            // colNgaySX
            // 
            this.colNgaySX.DataPropertyName = "NgaySX";
            this.colNgaySX.HeaderText = "Ngày SX";
            this.colNgaySX.MinimumWidth = 6;
            this.colNgaySX.Name = "colNgaySX";
            // 
            // colMoTa
            // 
            this.colMoTa.DataPropertyName = "MoTa";
            this.colMoTa.HeaderText = "Mô tả";
            this.colMoTa.MinimumWidth = 6;
            this.colMoTa.Name = "colMoTa";
            // 
            // FrmSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 650);
            this.Controls.Add(this.grbDanhSach);
            this.Controls.Add(this.grbTimKiem);
            this.Controls.Add(this.grbThongTin);
            this.MinimumSize = new System.Drawing.Size(1200, 697);
            this.Name = "FrmSanPham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sản phẩm";
            this.grbThongTin.ResumeLayout(false);
            this.grbThongTin.PerformLayout();
            this.grbTimKiem.ResumeLayout(false);
            this.grbTimKiem.PerformLayout();
            this.grbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbThongTin;
        private System.Windows.Forms.DateTimePicker dtpNgaySX;
        private System.Windows.Forms.ComboBox cboLoaiSP;
        private System.Windows.Forms.TextBox txtThuongHieu;
        private System.Windows.Forms.TextBox txtChatLieu;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.TextBox txtDonGiaBan;
        private System.Windows.Forms.TextBox txtKichThuoc;
        private System.Windows.Forms.TextBox txtMauSac;
        private System.Windows.Forms.TextBox txtSoLuongTon;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.Label lblThuongHieu;
        private System.Windows.Forms.Label lblChatLieu;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblNgaySX;
        private System.Windows.Forms.Label lblDonGiaBan;
        private System.Windows.Forms.Label lblKichThuoc;
        private System.Windows.Forms.Label lblMauSac;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.Label lblLoaiSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.GroupBox grbTimKiem;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.GroupBox grbDanhSach;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenLoaiSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChatLieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThuongHieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongTon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMauSac;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKichThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
    }
}