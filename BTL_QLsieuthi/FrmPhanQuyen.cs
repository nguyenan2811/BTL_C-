using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmPhanQuyen : Form
    {
        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;
        private bool dangTai = false;

        public FrmPhanQuyen()
        {
            InitializeComponent();

            Load += FrmPhanQuyen_Load;
            FormClosed += FrmPhanQuyen_FormClosed;

            cboTaiKhoan.SelectedIndexChanged += cboTaiKhoan_SelectedIndexChanged;
            clbQuyen.ItemCheck += clbQuyen_ItemCheck;

            button1.Click += button1_Click; // Đóng
            button2.Click += button2_Click; // Lưu
        }

        private void FrmPhanQuyen_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();

            TaiTaiKhoan();
        }

        private void FrmPhanQuyen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void TaiTaiKhoan()
        {
            const string sql = "SELECT TenDangNhap FROM TaiKhoan ORDER BY TenDangNhap";
            using (var da = new SqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);

                cboTaiKhoan.DataSource = dt;
                cboTaiKhoan.DisplayMember = "TenDangNhap";
                cboTaiKhoan.ValueMember = "TenDangNhap";
            }
        }

        private void cboTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTaiKhoan.SelectedValue == null)
            {
                return;
            }

            const string sql = "SELECT Quyen FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", cboTaiKhoan.SelectedValue.ToString());

                var quyen = cmd.ExecuteScalar() as string;

                dangTai = true;
                for (var i = 0; i < clbQuyen.Items.Count; i++)
                {
                    clbQuyen.SetItemChecked(i, false);
                }

                if (quyen == "ADMIN")
                {
                    clbQuyen.SetItemChecked(0, true); // Quản lý
                }
                else if (quyen == "NVBH")
                {
                    clbQuyen.SetItemChecked(1, true); // Nhân viên bán hàng
                }

                dangTai = false;
            }
        }

        private void clbQuyen_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (dangTai)
            {
                return;
            }

            // chỉ cho chọn 1 quyền
            for (var i = 0; i < clbQuyen.Items.Count; i++)
            {
                if (i != e.Index)
                {
                    clbQuyen.SetItemChecked(i, false);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cboTaiKhoan.SelectedValue == null)
            {
                MessageBox.Show("Chọn tài khoản.");
                return;
            }

            string quyen = null;
            if (clbQuyen.CheckedItems.Count > 0)
            {
                var text = clbQuyen.CheckedItems[0].ToString();
                quyen = text == "Quản lý" ? "ADMIN" : "NVBH";
            }

            const string sql = @"UPDATE TaiKhoan
                                 SET Quyen = @Quyen
                                 WHERE TenDangNhap = @TenDangNhap";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", cboTaiKhoan.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Quyen", (object)quyen ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Lưu phân quyền thành công.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
