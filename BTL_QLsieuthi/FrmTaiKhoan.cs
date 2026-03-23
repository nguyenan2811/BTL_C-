using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmTaiKhoan : Form
    {
        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;

        public FrmTaiKhoan()
        {
            InitializeComponent();

            Load += FrmTaiKhoan_Load;
            FormClosed += FrmTaiKhoan_FormClosed;

            chkHien.CheckedChanged += chkHien_CheckedChanged;
            button1.Click += button1_Click; // Làm mới
            button2.Click += button2_Click; // Thêm
            button3.Click += button3_Click; // Sửa mật khẩu
            button4.Click += button4_Click; // Xóa

            dgvTaiKhoan.CellClick += dgvTaiKhoan_CellClick;
        }

        private void FrmTaiKhoan_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();

            dgvTaiKhoan.AutoGenerateColumns = false;
            colTenDangNhap.DataPropertyName = "TenDangNhap";
            colQuyen.DataPropertyName = "Quyen";

            TaiDuLieuLenGrid();

            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtTenDangNhap.Focus();
        }

        private void FrmTaiKhoan_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void TaiDuLieuLenGrid()
        {
            const string sql = "SELECT TenDangNhap, Quyen FROM TaiKhoan ORDER BY TenDangNhap";
            using (var da = new SqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                dgvTaiKhoan.DataSource = dt;
            }

            // Không tự chọn dòng
            dgvTaiKhoan.ClearSelection();
            dgvTaiKhoan.CurrentCell = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();

            TaiDuLieuLenGrid();

            txtTenDangNhap.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var tenDangNhap = txtTenDangNhap.Text.Trim();
            var matKhau = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Nhập tên đăng nhập và mật khẩu.");
                return;
            }

            const string sql = @"INSERT INTO TaiKhoan(TenDangNhap, MatKhau, Quyen)
                                 VALUES(@TenDangNhap, @MatKhau, @Quyen)";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd.Parameters.AddWithValue("@Quyen", DBNull.Value);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công.");
                    TaiDuLieuLenGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm thất bại: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tenDangNhap = txtTenDangNhap.Text.Trim();
            var matKhauMoi = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhauMoi))
            {
                MessageBox.Show("Chọn tài khoản và nhập mật khẩu mới.");
                return;
            }

            const string sql = @"UPDATE TaiKhoan
                                 SET MatKhau = @MatKhau
                                 WHERE TenDangNhap = @TenDangNhap";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MatKhau", matKhauMoi);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                var soDong = cmd.ExecuteNonQuery();
                MessageBox.Show(soDong > 0 ? "Sửa thành công." : "Không tìm thấy tài khoản.");
                TaiDuLieuLenGrid();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var tenDangNhap = txtTenDangNhap.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                MessageBox.Show("Chọn tài khoản cần xóa.");
                return;
            }

            var xacNhan = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
            {
                return;
            }

            const string sql = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                var soDong = cmd.ExecuteNonQuery();
                MessageBox.Show(soDong > 0 ? "Xóa thành công." : "Không tìm thấy tài khoản.");
                TaiDuLieuLenGrid();
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var cell = dgvTaiKhoan.Rows[e.RowIndex].Cells["colTenDangNhap"].Value;
            txtTenDangNhap.Text = cell == null ? string.Empty : cell.ToString();
        }

        private void chkHien_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHien.Checked;
        }
    }
}
