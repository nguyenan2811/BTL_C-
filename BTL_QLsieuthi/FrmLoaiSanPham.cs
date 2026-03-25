using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmLoaiSanPham : Form
    {
        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;

        public FrmLoaiSanPham()
        {
            InitializeComponent();

            Load += FrmLoaiSanPham_Load;
            FormClosed += FrmLoaiSanPham_FormClosed;

            btnLamMoi.Click += btnLamMoi_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTim.Click += btnTim_Click;

            dgvLoaiSanPham.CellClick += dgvLoaiSanPham_CellClick;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        }

        private void FrmLoaiSanPham_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();

            TaiDuLieuLenGrid();
            LamMoiNhapLieu();
        }

        private void FrmLoaiSanPham_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void TaiDuLieuLenGrid(string tuKhoa = "")
        {
            const string sql = @"
SELECT MaLoaiSP AS MaLoai, TenLoaiSP AS TenLoai, MoTa
FROM LoaiSanPham
WHERE (@TuKhoa = N'')
   OR (MaLoaiSP LIKE N'%' + @TuKhoa + N'%')
   OR (TenLoaiSP LIKE N'%' + @TuKhoa + N'%')
   OR (MoTa LIKE N'%' + @TuKhoa + N'%')
ORDER BY MaLoaiSP;";

            using (var da = new SqlDataAdapter(sql, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@TuKhoa", tuKhoa ?? string.Empty);

                var dt = new DataTable();
                da.Fill(dt);
                dgvLoaiSanPham.DataSource = dt;
            }

            dgvLoaiSanPham.ClearSelection();
            dgvLoaiSanPham.CurrentCell = null;
        }

        private void LamMoiNhapLieu()
        {
            txtMaLoai.Text = TaoMaLoaiMoi();
            txtTenLoai.Clear();
            txtMoTa.Clear();
            txtTimKiem.Clear();
            txtTenLoai.Focus();
        }

        private string TaoMaLoaiMoi()
        {
            const string sql = "SELECT TOP 1 MaLoaiSP FROM LoaiSanPham ORDER BY MaLoaiSP DESC";

            using (var cmd = new SqlCommand(sql, conn))
            {
                var ketQua = cmd.ExecuteScalar();
                if (ketQua == null || ketQua == DBNull.Value)
                {
                    return "LSP0000001";
                }

                var maCu = ketQua.ToString();
                var so = 0;
                if (!string.IsNullOrWhiteSpace(maCu) && maCu.Length > 3)
                {
                    int.TryParse(maCu.Substring(3), out so);
                }

                return "LSP" + (so + 1).ToString("D7");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            TaiDuLieuLenGrid();
            LamMoiNhapLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var maLoai = txtMaLoai.Text.Trim();
            var tenLoai = txtTenLoai.Text.Trim();
            var moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLoai) || string.IsNullOrWhiteSpace(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập tên loại.");
                return;
            }

            const string sql = @"
INSERT INTO LoaiSanPham (MaLoaiSP, TenLoaiSP, MoTa)
VALUES (@MaLoai, @TenLoai, @MoTa);";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrWhiteSpace(moTa) ? (object)DBNull.Value : moTa);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công.");
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
            var maLoai = txtMaLoai.Text.Trim();
            var tenLoai = txtTenLoai.Text.Trim();
            var moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLoai) || string.IsNullOrWhiteSpace(tenLoai))
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa và nhập tên loại.");
                return;
            }

            const string sql = @"
UPDATE LoaiSanPham
SET TenLoaiSP = @TenLoai, MoTa = @MoTa
WHERE MaLoaiSP = @MaLoai;";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrWhiteSpace(moTa) ? (object)DBNull.Value : moTa);

                var soDong = cmd.ExecuteNonQuery();
                MessageBox.Show(soDong > 0 ? "Sửa thành công." : "Không tìm thấy mã loại.");
                TaiDuLieuLenGrid();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maLoai = txtMaLoai.Text.Trim();
            if (string.IsNullOrWhiteSpace(maLoai))
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm cần xóa.");
                return;
            }

            var xacNhan = MessageBox.Show(
                "Bạn có chắc muốn xóa loại sản phẩm này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
            {
                return;
            }

            const string sql = "DELETE FROM LoaiSanPham WHERE MaLoaiSP = @MaLoai;";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaLoai", maLoai);

                try
                {
                    var soDong = cmd.ExecuteNonQuery();
                    MessageBox.Show(soDong > 0 ? "Xóa thành công." : "Không tìm thấy mã loại.");
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

        private void dgvLoaiSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvLoaiSanPham.Rows[e.RowIndex];

            txtMaLoai.Text = row.Cells["colMaLoai"].Value == null
                ? string.Empty
                : row.Cells["colMaLoai"].Value.ToString();

            txtTenLoai.Text = row.Cells["colTenLoai"].Value == null
                ? string.Empty
                : row.Cells["colTenLoai"].Value.ToString();

            txtMoTa.Text = row.Cells["colMoTa"].Value == null
                ? string.Empty
                : row.Cells["colMoTa"].Value.ToString();
        }
    }
}
