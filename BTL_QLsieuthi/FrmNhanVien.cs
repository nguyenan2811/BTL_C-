using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmNhanVien : Form
    {
        private static readonly string[] DanhSachChucVuHopLe = { "Giám đốc", "Quản lý", "NVBH" };

        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;

        public FrmNhanVien()
        {
            InitializeComponent();

            Load += FrmNhanVien_Load;
            FormClosed += FrmNhanVien_FormClosed;

            btnLamMoi.Click += btnLamMoi_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTim.Click += btnTim_Click;

            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();

            var autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(DanhSachChucVuHopLe);
            cboChucVu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboChucVu.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboChucVu.AutoCompleteCustomSource = autoSource;

            TaiDuLieuLenGrid();
            LamMoiNhapLieu();
        }

        private void FrmNhanVien_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void TaiDuLieuLenGrid(string tuKhoa = "")
        {
            const string sql = @"
SELECT MaNV,
       ChucVu,
       HoTen,
       NgaySinh,
       GioiTinh,
       DienThoai
FROM NhanVien
WHERE (@TuKhoa = N'')
   OR (MaNV LIKE N'%' + @TuKhoa + N'%')
   OR (HoTen LIKE N'%' + @TuKhoa + N'%')
   OR (GioiTinh LIKE N'%' + @TuKhoa + N'%')
   OR (DienThoai LIKE N'%' + @TuKhoa + N'%')
   OR (ChucVu LIKE N'%' + @TuKhoa + N'%')
ORDER BY MaNV;";

            using (var da = new SqlDataAdapter(sql, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@TuKhoa", tuKhoa ?? string.Empty);

                var dt = new DataTable();
                da.Fill(dt);
                dgvNhanVien.DataSource = dt;
            }

            dgvNhanVien.ClearSelection();
            dgvNhanVien.CurrentCell = null;
        }

        private string TaoMaNVMoi()
        {
            const string sql = "SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV DESC";

            using (var cmd = new SqlCommand(sql, conn))
            {
                var ketQua = cmd.ExecuteScalar();
                if (ketQua == null || ketQua == DBNull.Value)
                {
                    return "NV0000001";
                }

                var maCu = ketQua.ToString();
                var so = 0;
                if (!string.IsNullOrWhiteSpace(maCu) && maCu.Length > 2)
                {
                    int.TryParse(maCu.Substring(2), out so);
                }

                return "NV" + (so + 1).ToString("D7");
            }
        }

        private void LamMoiNhapLieu()
        {
            txtMaNV.Text = TaoMaNVMoi();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            rdoNam.Checked = true;
            txtDienThoai.Clear();
            cboChucVu.Text = "NVBH";
            txtTimKiem.Clear();
            txtHoTen.Focus();
        }

        private bool KiemTraDuLieuNhap(out string gioiTinh, out string chucVu)
        {
            gioiTinh = rdoNu.Checked ? "Nữ" : "Nam";
            chucVu = cboChucVu.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.");
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(chucVu))
            {
                MessageBox.Show("Vui lòng nhập chức vụ.");
                cboChucVu.Focus();
                return false;
            }

            var hopLe = false;
            for (var i = 0; i < DanhSachChucVuHopLe.Length; i++)
            {
                if (string.Equals(chucVu, DanhSachChucVuHopLe[i], StringComparison.OrdinalIgnoreCase))
                {
                    chucVu = DanhSachChucVuHopLe[i];
                    hopLe = true;
                    break;
                }
            }

            if (!hopLe)
            {
                MessageBox.Show("Chức vụ chỉ nhận: Giám đốc, Quản lý, NVBH.");
                cboChucVu.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập điện thoại.");
                txtDienThoai.Focus();
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
            string gioiTinh;
            string chucVu;
            if (!KiemTraDuLieuNhap(out gioiTinh, out chucVu))
            {
                return;
            }

            const string sql = @"
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, DienThoai, ChucVu)
VALUES (@MaNV, @HoTen, @NgaySinh, @GioiTinh, @DienThoai, @ChucVu);";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text.Trim());
                cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@ChucVu", chucVu);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công.");
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
            string gioiTinh;
            string chucVu;
            if (!KiemTraDuLieuNhap(out gioiTinh, out chucVu))
            {
                return;
            }

            const string sql = @"
UPDATE NhanVien
SET HoTen = @HoTen,
    NgaySinh = @NgaySinh,
    GioiTinh = @GioiTinh,
    DienThoai = @DienThoai,
    ChucVu = @ChucVu
WHERE MaNV = @MaNV;";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text.Trim());
                cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@ChucVu", chucVu);

                var soDong = cmd.ExecuteNonQuery();
                MessageBox.Show(soDong > 0 ? "Sửa thành công." : "Không tìm thấy mã nhân viên.");
                TaiDuLieuLenGrid();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maNV = txtMaNV.Text.Trim();
            if (string.IsNullOrWhiteSpace(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.");
                return;
            }

            var xacNhan = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân viên này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
            {
                return;
            }

            const string sql = "DELETE FROM NhanVien WHERE MaNV = @MaNV";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                try
                {
                    var soDong = cmd.ExecuteNonQuery();
                    MessageBox.Show(soDong > 0 ? "Xóa thành công." : "Không tìm thấy mã nhân viên.");
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

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvNhanVien.Rows[e.RowIndex];

            txtMaNV.Text = row.Cells["colMaNV"].Value == null ? string.Empty : row.Cells["colMaNV"].Value.ToString();
            txtHoTen.Text = row.Cells["colHoTen"].Value == null ? string.Empty : row.Cells["colHoTen"].Value.ToString();
            txtDienThoai.Text = row.Cells["colDienThoai"].Value == null ? string.Empty : row.Cells["colDienThoai"].Value.ToString();
            cboChucVu.Text = row.Cells["colChucVu"].Value == null ? string.Empty : row.Cells["colChucVu"].Value.ToString();

            var gioiTinh = row.Cells["colGioiTinh"].Value == null ? string.Empty : row.Cells["colGioiTinh"].Value.ToString();
            rdoNam.Checked = gioiTinh.Equals("Nam", StringComparison.OrdinalIgnoreCase);
            rdoNu.Checked = !rdoNam.Checked;

            if (row.Cells["colNgaySinh"].Value != null)
            {
                DateTime ngay;
                if (DateTime.TryParse(row.Cells["colNgaySinh"].Value.ToString(), out ngay))
                {
                    dtpNgaySinh.Value = ngay;
                }
            }
        }
    }
}
