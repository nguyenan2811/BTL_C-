using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmDangNhap : Form
    {
        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;

        public FrmDangNhap()
        {
            InitializeComponent();

            chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
            txtMatKhau.UseSystemPasswordChar = true;

            Load += FrmDangNhap_Load;
            FormClosed += FrmDangNhap_FormClosed;
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
        }

        private void FrmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            var tenDangNhap = txtTenDangNhap.Text.Trim();
            var matKhau = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.");
                return;
            }

            const string sql = @"SELECT COUNT(1)
                                 FROM TaiKhoan
                                 WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                var ketQua = Convert.ToInt32(cmd.ExecuteScalar());

                if (ketQua > 0)
                {
                    MessageBox.Show("Đăng nhập thành công.");
                    Hide();
                    using (var f = new Form1())
                    {
                        f.ShowDialog();
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.");
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }
    }
}
