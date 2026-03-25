using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmNhaCungCap : Form
    {
        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;

        public FrmNhaCungCap()
        {
            InitializeComponent();

            Load += FrmNhaCungCap_Load;
            FormClosed += FrmNhaCungCap_FormClosed;

            btnLamMoi.Click += btnLamMoi_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTim.Click += btnTim_Click;

            dgvNhaCungCap.CellClick += dgvNhaCungCap_CellClick;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        }

        private void FrmNhaCungCap_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();

            TaiDuLieuLenGrid();
            LamMoiNhapLieu();
        }

        private void FrmNhaCungCap_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void TaiDuLieuLenGrid(string tuKhoa = "")
        {
            const string sql = @"
SELECT MaNCC, TenNCC, DiaChi, DienThoai
FROM NhaCungCap
WHERE (@TuKhoa = N'')
   OR (MaNCC LIKE N'%' + @TuKhoa + N'%')
   OR (TenNCC LIKE N'%' + @TuKhoa + N'%')
   OR (DiaChi LIKE N'%' + @TuKhoa + N'%')
   OR (DienThoai LIKE N'%' + @TuKhoa + N'%')
ORDER BY MaNCC;";

            using (var da = new SqlDataAdapter(sql, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@TuKhoa", tuKhoa ?? string.Empty);

                var dt = new DataTable();
                da.Fill(dt);
                dgvNhaCungCap.DataSource = dt;
            }

            dgvNhaCungCap.ClearSelection();
            dgvNhaCungCap.CurrentCell = null;
        }

        private string TaoMaNCCMoi()
        {
            const string sql = "SELECT TOP 1 MaNCC FROM NhaCungCap ORDER BY MaNCC DESC";

            using (var cmd = new SqlCommand(sql, conn))
            {
                var ketQua = cmd.ExecuteScalar();
                if (ketQua == null || ketQua == DBNull.Value)
                {
                    return "NCC0000001";
                }

                var maCu = ketQua.ToString();
                var so = 0;
                if (!string.IsNullOrWhiteSpace(maCu) && maCu.Length > 3)
                {
                    int.TryParse(maCu.Substring(3), out so);
                }

                return "NCC" + (so + 1).ToString("D7");
            }
        }

        private void LamMoiNhapLieu()
        {
            txtMaNCC.Text = TaoMaNCCMoi();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtTimKiem.Clear();
            txtTenNCC.Focus();
        }

        private bool KiemTraNhapLieu()
        {
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.");
                txtTenNCC.Focus();
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
            if (!KiemTraNhapLieu())
            {
                return;
            }

            const string sql = @"
INSERT INTO NhaCungCap (MaNCC, TenNCC, DienThoai, DiaChi)
VALUES (@MaNCC, @TenNCC, @DienThoai, @DiaChi);";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@TenNCC", txtTenNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim());

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhà cung cấp thành công.");
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
            if (!KiemTraNhapLieu())
            {
                return;
            }

            const string sql = @"
UPDATE NhaCungCap
SET TenNCC = @TenNCC,
    DienThoai = @DienThoai,
    DiaChi = @DiaChi
WHERE MaNCC = @MaNCC;";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@TenNCC", txtTenNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim());

                var soDong = cmd.ExecuteNonQuery();
                MessageBox.Show(soDong > 0 ? "Sửa thành công." : "Không tìm thấy mã nhà cung cấp.");
                TaiDuLieuLenGrid();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maNcc = txtMaNCC.Text.Trim();
            if (string.IsNullOrWhiteSpace(maNcc))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa.");
                return;
            }

            var xacNhan = MessageBox.Show(
                "Bạn có chắc muốn xóa nhà cung cấp này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
            {
                return;
            }

            const string sql = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNCC", maNcc);

                try
                {
                    var soDong = cmd.ExecuteNonQuery();
                    MessageBox.Show(soDong > 0 ? "Xóa thành công." : "Không tìm thấy mã nhà cung cấp.");
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

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvNhaCungCap.Rows[e.RowIndex];

            txtMaNCC.Text = row.Cells["colMaNCC"].Value == null ? string.Empty : row.Cells["colMaNCC"].Value.ToString();
            txtTenNCC.Text = row.Cells["colTenNCC"].Value == null ? string.Empty : row.Cells["colTenNCC"].Value.ToString();
            txtDiaChi.Text = row.Cells["colDiaChi"].Value == null ? string.Empty : row.Cells["colDiaChi"].Value.ToString();
            txtDienThoai.Text = row.Cells["colDienThoai"].Value == null ? string.Empty : row.Cells["colDienThoai"].Value.ToString();
        }
    }
}
