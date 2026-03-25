using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmSanPham : Form
    {
        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;

        public FrmSanPham()
        {
            InitializeComponent();

            Load += FrmSanPham_Load;
            FormClosed += FrmSanPham_FormClosed;

            btnLamMoi.Click += btnLamMoi_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTim.Click += btnTim_Click;

            dgvSanPham.CellClick += dgvSanPham_CellClick;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        }

        private void FrmSanPham_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();

            TaiLoaiSanPhamVaoCombo();
            TaiDuLieuLenGrid();
            LamMoiNhapLieu();
        }

        private void FrmSanPham_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void TaiLoaiSanPhamVaoCombo()
        {
            const string sql = "SELECT MaLoaiSP, TenLoaiSP FROM LoaiSanPham ORDER BY TenLoaiSP";

            using (var da = new SqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);

                cboLoaiSP.DataSource = dt;
                cboLoaiSP.DisplayMember = "TenLoaiSP";
                cboLoaiSP.ValueMember = "MaLoaiSP";
                cboLoaiSP.SelectedIndex = -1;
            }
        }

        private void TaiDuLieuLenGrid(string tuKhoa = "")
        {
            const string sql = @"
SELECT sp.MaSP,
       sp.TenSP,
       lsp.TenLoaiSP,
       sp.ChatLieu,
       sp.ThuongHieu,
       sp.SoLuongTon,
       sp.MauSac,
       sp.KichThuoc,
       sp.DonGiaBan,
       sp.NgaySX,
       sp.MoTa
FROM SanPham sp
LEFT JOIN LoaiSanPham lsp ON sp.MaLoaiSP = lsp.MaLoaiSP
WHERE (@TuKhoa = N'')
   OR (sp.MaSP LIKE N'%' + @TuKhoa + N'%')
   OR (sp.TenSP LIKE N'%' + @TuKhoa + N'%')
   OR (sp.ChatLieu LIKE N'%' + @TuKhoa + N'%')
   OR (sp.ThuongHieu LIKE N'%' + @TuKhoa + N'%')
   OR (sp.MauSac LIKE N'%' + @TuKhoa + N'%')
   OR (sp.KichThuoc LIKE N'%' + @TuKhoa + N'%')
ORDER BY sp.MaSP;";

            using (var da = new SqlDataAdapter(sql, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@TuKhoa", tuKhoa ?? string.Empty);

                var dt = new DataTable();
                da.Fill(dt);
                dgvSanPham.DataSource = dt;
            }

            dgvSanPham.ClearSelection();
            dgvSanPham.CurrentCell = null;
        }

        private string TaoMaSPMoi()
        {
            const string sql = "SELECT TOP 1 MaSP FROM SanPham ORDER BY MaSP DESC";

            using (var cmd = new SqlCommand(sql, conn))
            {
                var ketQua = cmd.ExecuteScalar();
                if (ketQua == null || ketQua == DBNull.Value)
                {
                    return "SP0000001";
                }

                var maCu = ketQua.ToString();
                var so = 0;
                if (!string.IsNullOrWhiteSpace(maCu) && maCu.Length > 2)
                {
                    int.TryParse(maCu.Substring(2), out so);
                }

                return "SP" + (so + 1).ToString("D7");
            }
        }

        private void LamMoiNhapLieu()
        {
            txtMaSP.Text = TaoMaSPMoi();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = -1;
            txtChatLieu.Clear();
            txtThuongHieu.Clear();
            txtSoLuongTon.Clear();
            txtMauSac.Clear();
            txtKichThuoc.Clear();
            txtDonGiaBan.Clear();
            dtpNgaySX.Value = DateTime.Today;
            txtMoTa.Clear();
            txtTimKiem.Clear();
            txtTenSP.Focus();
        }

        private bool KiemTraDuLieuNhap(out int soLuongTon, out decimal donGiaBan)
        {
            soLuongTon = 0;
            donGiaBan = 0;

            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm.");
                txtTenSP.Focus();
                return false;
            }

            if (cboLoaiSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm.");
                cboLoaiSP.Focus();
                return false;
            }

            if (!int.TryParse(txtSoLuongTon.Text.Trim(), out soLuongTon) || soLuongTon < 0)
            {
                MessageBox.Show("Số lượng tồn phải là số nguyên >= 0.");
                txtSoLuongTon.Focus();
                return false;
            }

            if (!decimal.TryParse(txtDonGiaBan.Text.Trim(), out donGiaBan) || donGiaBan < 0)
            {
                MessageBox.Show("Đơn giá bán phải là số >= 0.");
                txtDonGiaBan.Focus();
                return false;
            }

            return true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            TaiDuLieuLenGrid();
            LamMoiNhapLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int soLuongTon;
            decimal donGiaBan;
            if (!KiemTraDuLieuNhap(out soLuongTon, out donGiaBan))
            {
                return;
            }

            const string sql = @"
INSERT INTO SanPham
(
    MaSP, TenSP, MaLoaiSP, ChatLieu, ThuongHieu, SoLuongTon,
    MauSac, KichThuoc, DonGiaBan, NgaySX, MoTa
)
VALUES
(
    @MaSP, @TenSP, @MaLoaiSP, @ChatLieu, @ThuongHieu, @SoLuongTon,
    @MauSac, @KichThuoc, @DonGiaBan, @NgaySX, @MoTa
);";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text.Trim());
                cmd.Parameters.AddWithValue("@MaLoaiSP", cboLoaiSP.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ChatLieu", string.IsNullOrWhiteSpace(txtChatLieu.Text) ? (object)DBNull.Value : txtChatLieu.Text.Trim());
                cmd.Parameters.AddWithValue("@ThuongHieu", string.IsNullOrWhiteSpace(txtThuongHieu.Text) ? (object)DBNull.Value : txtThuongHieu.Text.Trim());
                cmd.Parameters.AddWithValue("@SoLuongTon", soLuongTon);
                cmd.Parameters.AddWithValue("@MauSac", string.IsNullOrWhiteSpace(txtMauSac.Text) ? (object)DBNull.Value : txtMauSac.Text.Trim());
                cmd.Parameters.AddWithValue("@KichThuoc", string.IsNullOrWhiteSpace(txtKichThuoc.Text) ? (object)DBNull.Value : txtKichThuoc.Text.Trim());
                cmd.Parameters.AddWithValue("@DonGiaBan", donGiaBan);
                cmd.Parameters.AddWithValue("@NgaySX", dtpNgaySX.Value.Date);
                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrWhiteSpace(txtMoTa.Text) ? (object)DBNull.Value : txtMoTa.Text.Trim());

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sản phẩm thành công.");
                    TaiDuLieuLenGrid();
                    LamMoiNhapLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm thất bại: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int soLuongTon;
            decimal donGiaBan;
            if (!KiemTraDuLieuNhap(out soLuongTon, out donGiaBan))
            {
                return;
            }

            const string sql = @"
UPDATE SanPham
SET TenSP = @TenSP,
    MaLoaiSP = @MaLoaiSP,
    ChatLieu = @ChatLieu,
    ThuongHieu = @ThuongHieu,
    SoLuongTon = @SoLuongTon,
    MauSac = @MauSac,
    KichThuoc = @KichThuoc,
    DonGiaBan = @DonGiaBan,
    NgaySX = @NgaySX,
    MoTa = @MoTa
WHERE MaSP = @MaSP;";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text.Trim());
                cmd.Parameters.AddWithValue("@MaLoaiSP", cboLoaiSP.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ChatLieu", string.IsNullOrWhiteSpace(txtChatLieu.Text) ? (object)DBNull.Value : txtChatLieu.Text.Trim());
                cmd.Parameters.AddWithValue("@ThuongHieu", string.IsNullOrWhiteSpace(txtThuongHieu.Text) ? (object)DBNull.Value : txtThuongHieu.Text.Trim());
                cmd.Parameters.AddWithValue("@SoLuongTon", soLuongTon);
                cmd.Parameters.AddWithValue("@MauSac", string.IsNullOrWhiteSpace(txtMauSac.Text) ? (object)DBNull.Value : txtMauSac.Text.Trim());
                cmd.Parameters.AddWithValue("@KichThuoc", string.IsNullOrWhiteSpace(txtKichThuoc.Text) ? (object)DBNull.Value : txtKichThuoc.Text.Trim());
                cmd.Parameters.AddWithValue("@DonGiaBan", donGiaBan);
                cmd.Parameters.AddWithValue("@NgaySX", dtpNgaySX.Value.Date);
                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrWhiteSpace(txtMoTa.Text) ? (object)DBNull.Value : txtMoTa.Text.Trim());

                var soDong = cmd.ExecuteNonQuery();                                             
                MessageBox.Show(soDong > 0 ? "Sửa thành công." : "Không tìm thấy mã sản phẩm.");
                TaiDuLieuLenGrid();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maSP = txtMaSP.Text.Trim();
            if (string.IsNullOrWhiteSpace(maSP))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
                return;
            }

            var xacNhan = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
            {
                return;
            }

            const string sql = "DELETE FROM SanPham WHERE MaSP = @MaSP";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", maSP);

                try
                {
                    var soDong = cmd.ExecuteNonQuery();
                    MessageBox.Show(soDong > 0 ? "Xóa thành công." : "Không tìm thấy mã sản phẩm.");
                    TaiDuLieuLenGrid();
                    LamMoiNhapLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại: " + ex.Message);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            TaiDuLieuLenGrid(txtTimKiem.Text.Trim());
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TaiDuLieuLenGrid(txtTimKiem.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvSanPham.Rows[e.RowIndex];

            txtMaSP.Text = row.Cells["colMaSP"].Value == null ? string.Empty : row.Cells["colMaSP"].Value.ToString();
            txtTenSP.Text = row.Cells["colTenSP"].Value == null ? string.Empty : row.Cells["colTenSP"].Value.ToString();
            txtChatLieu.Text = row.Cells["colChatLieu"].Value == null ? string.Empty : row.Cells["colChatLieu"].Value.ToString();
            txtThuongHieu.Text = row.Cells["colThuongHieu"].Value == null ? string.Empty : row.Cells["colThuongHieu"].Value.ToString();
            txtSoLuongTon.Text = row.Cells["colSoLuongTon"].Value == null ? string.Empty : row.Cells["colSoLuongTon"].Value.ToString();
            txtMauSac.Text = row.Cells["colMauSac"].Value == null ? string.Empty : row.Cells["colMauSac"].Value.ToString();
            txtKichThuoc.Text = row.Cells["colKichThuoc"].Value == null ? string.Empty : row.Cells["colKichThuoc"].Value.ToString();
            txtDonGiaBan.Text = row.Cells["colDonGiaBan"].Value == null ? string.Empty : row.Cells["colDonGiaBan"].Value.ToString();
            txtMoTa.Text = row.Cells["colMoTa"].Value == null ? string.Empty : row.Cells["colMoTa"].Value.ToString();

            if (row.Cells["colNgaySX"].Value != null)
            {
                DateTime ngay;
                if (DateTime.TryParse(row.Cells["colNgaySX"].Value.ToString(), out ngay))
                {
                    dtpNgaySX.Value = ngay;
                }
            }

            if (row.Cells["colTenLoaiSP"].Value != null)
            {
                cboLoaiSP.Text = row.Cells["colTenLoaiSP"].Value.ToString();
            }
        }

        private void btnTim_Click_1(object sender, EventArgs e)
        {

        }

        private void txtMauSac_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
