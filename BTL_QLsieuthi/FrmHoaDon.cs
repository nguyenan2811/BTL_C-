using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmHoaDon : Form
    {
        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;
        private DataTable dtChiTietTam = null;
        private readonly List<string> danhSachMaHD = new List<string>();
        private int viTriHoaDon = -1;

        public FrmHoaDon()
        {
            InitializeComponent();

            Load += FrmHoaDon_Load;
            FormClosed += FrmHoaDon_FormClosed;

            btnLamMoi.Click += btnLamMoi_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;

            btnThemCT.Click += btnThemCT_Click;
            btnXoaCT.Click += btnXoaCT_Click;

            btnTruoc.Click += btnTruoc_Click;
            btnSau.Click += btnSau_Click;

            cboSanPham.SelectedIndexChanged += cboSanPham_SelectedIndexChanged;
            txtTienKhach.TextChanged += txtTienKhach_TextChanged;
            dgvChiTiet.CellClick += dgvChiTiet_CellClick;
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();

            KhoiTaoBangChiTietTam();
            TaiNhanVienVaoCombo();
            TaiSanPhamVaoCombo();
            TaiDanhSachMaHoaDon();
            LamMoiHoaDon();
        }

        private void FrmHoaDon_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void KhoiTaoBangChiTietTam()
        {
            dtChiTietTam = new DataTable();
            dtChiTietTam.Columns.Add("MaSP", typeof(string));
            dtChiTietTam.Columns.Add("TenSP", typeof(string));
            dtChiTietTam.Columns.Add("SoLuong", typeof(int));
            dtChiTietTam.Columns.Add("DonGia", typeof(decimal));
            dtChiTietTam.Columns.Add("ThanhTien", typeof(decimal));
            dgvChiTiet.DataSource = dtChiTietTam;
        }

        private void TaiNhanVienVaoCombo()
        {
            const string sql = "SELECT MaNV, HoTen FROM NhanVien ORDER BY HoTen";
            using (var da = new SqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);

                cboNhanVien.DataSource = dt;
                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.SelectedIndex = -1;
            }
        }

        private void TaiSanPhamVaoCombo()
        {
            const string sql = "SELECT MaSP, TenSP FROM SanPham ORDER BY TenSP";
            using (var da = new SqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);

                cboSanPham.DataSource = dt;
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
                cboSanPham.SelectedIndex = -1;
            }
        }

        private void TaiDanhSachMaHoaDon()
        {
            danhSachMaHD.Clear();
            const string sql = "SELECT MaHD FROM HoaDon ORDER BY MaHD";
            using (var cmd = new SqlCommand(sql, conn))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    danhSachMaHD.Add(rd["MaHD"].ToString());
                }
            }
        }

        private string TaoMaHDMoi()
        {
            const string sql = "SELECT TOP 1 MaHD FROM HoaDon ORDER BY MaHD DESC";
            using (var cmd = new SqlCommand(sql, conn))
            {
                var ketQua = cmd.ExecuteScalar();
                if (ketQua == null || ketQua == DBNull.Value)
                {
                    return "HD0000001";
                }

                var maCu = ketQua.ToString();
                var so = 0;
                if (!string.IsNullOrWhiteSpace(maCu) && maCu.Length > 2)
                {
                    int.TryParse(maCu.Substring(2), out so);
                }

                return "HD" + (so + 1).ToString("D7");
            }
        }

        private void LamMoiHoaDon()
        {
            txtMaHD.Text = TaoMaHDMoi();
            dtpNgayLap.Value = DateTime.Today;
            cboNhanVien.SelectedIndex = -1;
            txtTenKhachHang.Clear();
            txtSDTKhachHang.Clear();
            txtGhiChu.Clear();

            txtSoLuong.Clear();
            txtDonGia.Clear();
            cboSanPham.SelectedIndex = -1;

            dtChiTietTam.Rows.Clear();
            txtTongTien.Text = "0";
            lblTienHangValue.Text = "0";
            txtTienKhach.Clear();
            txtTienThua.Text = "0";

            viTriHoaDon = -1;
            txtTenKhachHang.Focus();
        }

        private void TaiHoaDonTheoMa(string maHD)
        {
            const string sqlHd = @"
SELECT MaHD, NgayLap, MaNV, TenKhachHang, SDTKhachHang, GhiChu, TongTien
FROM HoaDon
WHERE MaHD = @MaHD;";

            using (var cmd = new SqlCommand(sqlHd, conn))
            {
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read())
                    {
                        return;
                    }

                    txtMaHD.Text = rd["MaHD"].ToString();
                    if (rd["NgayLap"] != DBNull.Value)
                    {
                        dtpNgayLap.Value = Convert.ToDateTime(rd["NgayLap"]);
                    }

                    cboNhanVien.SelectedValue = rd["MaNV"] == DBNull.Value ? null : rd["MaNV"].ToString();
                    txtTenKhachHang.Text = rd["TenKhachHang"] == DBNull.Value ? string.Empty : rd["TenKhachHang"].ToString();
                    txtSDTKhachHang.Text = rd["SDTKhachHang"] == DBNull.Value ? string.Empty : rd["SDTKhachHang"].ToString();
                    txtGhiChu.Text = rd["GhiChu"] == DBNull.Value ? string.Empty : rd["GhiChu"].ToString();
                }
            }

            dtChiTietTam.Rows.Clear();

            const string sqlCt = @"
SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.DonGia, ct.ThanhTien
FROM ChiTietHoaDon ct
INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
WHERE ct.MaHD = @MaHD
ORDER BY ct.MaSP;";

            using (var da = new SqlDataAdapter(sqlCt, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@MaHD", maHD);
                var dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    dtChiTietTam.Rows.Add(
                        row["MaSP"].ToString(),
                        row["TenSP"].ToString(),
                        Convert.ToInt32(row["SoLuong"]),
                        Convert.ToDecimal(row["DonGia"]),
                        Convert.ToDecimal(row["ThanhTien"]));
                }
            }

            TinhTien();
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null)
            {
                txtDonGia.Clear();
                return;
            }

            const string sql = "SELECT DonGiaBan FROM SanPham WHERE MaSP = @MaSP";
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", cboSanPham.SelectedValue.ToString());
                var gia = cmd.ExecuteScalar();
                if (gia == null || gia == DBNull.Value)
                {
                    txtDonGia.Text = "0";
                }
                else
                {
                    txtDonGia.Text = Convert.ToDecimal(gia).ToString("0");
                }
            }
        }

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm.");
                return;
            }

            int soLuong;
            if (!int.TryParse(txtSoLuong.Text.Trim(), out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên > 0.");
                txtSoLuong.Focus();
                return;
            }

            decimal donGia;
            if (!decimal.TryParse(txtDonGia.Text.Trim(), out donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.");
                txtDonGia.Focus();
                return;
            }

            var maSp = cboSanPham.SelectedValue.ToString();
            var tenSp = cboSanPham.Text;
            var thanhTien = soLuong * donGia;

            DataRow rowTimThay = null;
            foreach (DataRow row in dtChiTietTam.Rows)
            {
                if (string.Equals(row["MaSP"].ToString(), maSp, StringComparison.OrdinalIgnoreCase))
                {
                    rowTimThay = row;
                    break;
                }
            }

            if (rowTimThay == null)
            {
                dtChiTietTam.Rows.Add(maSp, tenSp, soLuong, donGia, thanhTien);
            }
            else
            {
                var slCu = Convert.ToInt32(rowTimThay["SoLuong"]);
                var slMoi = slCu + soLuong;
                rowTimThay["SoLuong"] = slMoi;
                rowTimThay["ThanhTien"] = slMoi * donGia;
            }

            txtSoLuong.Clear();
            TinhTien();
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null || dgvChiTiet.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn dòng chi tiết cần xóa.");
                return;
            }

            dtChiTietTam.Rows.RemoveAt(dgvChiTiet.CurrentRow.Index);
            TinhTien();
        }

        private void dgvChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvChiTiet.Rows[e.RowIndex];
            txtSoLuong.Text = row.Cells["colSoLuong"].Value == null ? string.Empty : row.Cells["colSoLuong"].Value.ToString();
            txtDonGia.Text = row.Cells["colDonGia"].Value == null ? string.Empty : row.Cells["colDonGia"].Value.ToString();

            if (row.Cells["colMaSP"].Value != null)
            {
                cboSanPham.SelectedValue = row.Cells["colMaSP"].Value.ToString();
            }
        }

        private void TinhTien()
        {
            decimal tong = 0;
            foreach (DataRow row in dtChiTietTam.Rows)
            {
                if (row["ThanhTien"] != DBNull.Value)
                {
                    tong += Convert.ToDecimal(row["ThanhTien"]);
                }
            }

            txtTongTien.Text = tong.ToString("0");
            lblTienHangValue.Text = tong.ToString("0");
            TinhTienThua();
        }

        private void txtTienKhach_TextChanged(object sender, EventArgs e)
        {
            TinhTienThua();
        }

        private void TinhTienThua()
        {
            decimal tongTien;
            decimal tienKhach;

            if (!decimal.TryParse(txtTongTien.Text.Trim(), out tongTien))
            {
                tongTien = 0;
            }

            if (!decimal.TryParse(txtTienKhach.Text.Trim(), out tienKhach))
            {
                txtTienThua.Text = "0";
                return;
            }

            txtTienThua.Text = (tienKhach - tongTien).ToString("0");
        }

        private bool KiemTraDuLieuHoaDon()
        {
            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.");
                cboNhanVien.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.");
                txtTenKhachHang.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSDTKhachHang.Text))
            {
                MessageBox.Show("Vui lòng nhập SĐT khách hàng.");
                txtSDTKhachHang.Focus();
                return false;
            }

            if (dtChiTietTam.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 sản phẩm vào chi tiết hóa đơn.");
                return false;
            }

            return true;
        }

        private DataTable LayChiTietHoaDonTheoMa(string maHd, SqlTransaction tran)
        {
            const string sql = @"
SELECT MaSP, SoLuong
FROM ChiTietHoaDon
WHERE MaHD = @MaHD;";

            using (var da = new SqlDataAdapter(sql, conn))
            {
                da.SelectCommand.Transaction = tran;
                da.SelectCommand.Parameters.AddWithValue("@MaHD", maHd);

                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void CongTonKho(SqlTransaction tran, string maSp, int soLuong)
        {
            const string sql = @"
UPDATE SanPham
SET SoLuongTon = SoLuongTon + @SoLuong
WHERE MaSP = @MaSP;";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@MaSP", maSp);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);

                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("Không tìm thấy sản phẩm: " + maSp);
                }
            }
        }

        private void TruTonKho(SqlTransaction tran, string maSp, int soLuong)
        {
            const string sql = @"
UPDATE SanPham
SET SoLuongTon = SoLuongTon - @SoLuong
WHERE MaSP = @MaSP
  AND SoLuongTon >= @SoLuong;";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@MaSP", maSp);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);

                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("Sản phẩm " + maSp + " không đủ số lượng tồn.");
                }
            }
        }

        private void LuuHoaDon(bool laThem)
        {
            if (!KiemTraDuLieuHoaDon())
            {
                return;
            }

            var maHd = txtMaHD.Text.Trim();
            var maNv = cboNhanVien.SelectedValue.ToString();
            var tenKh = txtTenKhachHang.Text.Trim();
            var sdtKh = txtSDTKhachHang.Text.Trim();
            var ghiChu = txtGhiChu.Text.Trim();

            decimal tongTien;
            if (!decimal.TryParse(txtTongTien.Text.Trim(), out tongTien))
            {
                tongTien = 0;
            }

            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    if (laThem)
                    {
                        const string sqlThemHd = @"
INSERT INTO HoaDon (MaHD, NgayLap, MaNV, TenKhachHang, SDTKhachHang, GhiChu, TongTien)
VALUES (@MaHD, @NgayLap, @MaNV, @TenKhachHang, @SDTKhachHang, @GhiChu, @TongTien);";

                        using (var cmd = new SqlCommand(sqlThemHd, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaHD", maHd);
                            cmd.Parameters.AddWithValue("@NgayLap", dtpNgayLap.Value.Date);
                            cmd.Parameters.AddWithValue("@MaNV", maNv);
                            cmd.Parameters.AddWithValue("@TenKhachHang", tenKh);
                            cmd.Parameters.AddWithValue("@SDTKhachHang", sdtKh);
                            cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(ghiChu) ? (object)DBNull.Value : ghiChu);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        var ctCu = LayChiTietHoaDonTheoMa(maHd, tran);
                        foreach (DataRow rowCu in ctCu.Rows)
                        {
                            CongTonKho(tran, rowCu["MaSP"].ToString(), Convert.ToInt32(rowCu["SoLuong"]));
                        }

                        const string sqlSuaHd = @"
UPDATE HoaDon
SET NgayLap = @NgayLap,
    MaNV = @MaNV,
    TenKhachHang = @TenKhachHang,
    SDTKhachHang = @SDTKhachHang,
    GhiChu = @GhiChu,
    TongTien = @TongTien
WHERE MaHD = @MaHD;";

                        using (var cmd = new SqlCommand(sqlSuaHd, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaHD", maHd);
                            cmd.Parameters.AddWithValue("@NgayLap", dtpNgayLap.Value.Date);
                            cmd.Parameters.AddWithValue("@MaNV", maNv);
                            cmd.Parameters.AddWithValue("@TenKhachHang", tenKh);
                            cmd.Parameters.AddWithValue("@SDTKhachHang", sdtKh);
                            cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(ghiChu) ? (object)DBNull.Value : ghiChu);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            cmd.ExecuteNonQuery();
                        }

                        const string sqlXoaCt = "DELETE FROM ChiTietHoaDon WHERE MaHD = @MaHD";
                        using (var cmd = new SqlCommand(sqlXoaCt, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaHD", maHd);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    const string sqlThemCt = @"
INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia, ThanhTien)
VALUES (@MaHD, @MaSP, @SoLuong, @DonGia, @ThanhTien);";

                    foreach (DataRow row in dtChiTietTam.Rows)
                    {
                        var soLuong = Convert.ToInt32(row["SoLuong"]);
                        TruTonKho(tran, row["MaSP"].ToString(), soLuong);

                        using (var cmd = new SqlCommand(sqlThemCt, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaHD", maHd);
                            cmd.Parameters.AddWithValue("@MaSP", row["MaSP"].ToString());
                            cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                            cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(row["DonGia"]));
                            cmd.Parameters.AddWithValue("@ThanhTien", Convert.ToDecimal(row["ThanhTien"]));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    MessageBox.Show(laThem ? "Thêm hóa đơn thành công." : "Sửa hóa đơn thành công.");

                    TaiDanhSachMaHoaDon();
                    viTriHoaDon = danhSachMaHD.IndexOf(maHd);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Lưu hóa đơn thất bại: " + ex.Message);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LuuHoaDon(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (viTriHoaDon < 0)
            {
                MessageBox.Show("Vui lòng mở hóa đơn đã lưu để sửa.");
                return;
            }

            LuuHoaDon(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maHd = txtMaHD.Text.Trim();
            if (string.IsNullOrWhiteSpace(maHd))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.");
                return;
            }

            var xacNhan = MessageBox.Show(
                "Bạn có chắc muốn xóa hóa đơn này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
            {
                return;
            }

            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    var ctCu = LayChiTietHoaDonTheoMa(maHd, tran);
                    foreach (DataRow rowCu in ctCu.Rows)
                    {
                        CongTonKho(tran, rowCu["MaSP"].ToString(), Convert.ToInt32(rowCu["SoLuong"]));
                    }

                    using (var cmd = new SqlCommand("DELETE FROM ChiTietHoaDon WHERE MaHD = @MaHD", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", maHd);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("DELETE FROM HoaDon WHERE MaHD = @MaHD", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", maHd);
                        var soDong = cmd.ExecuteNonQuery();
                        if (soDong == 0)
                        {
                            throw new Exception("Không tìm thấy mã hóa đơn.");
                        }
                    }

                    tran.Commit();
                    MessageBox.Show("Xóa hóa đơn thành công.");

                    TaiDanhSachMaHoaDon();
                    LamMoiHoaDon();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Xóa hóa đơn thất bại: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiHoaDon();
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            if (danhSachMaHD.Count == 0)
            {
                return;
            }

            if (viTriHoaDon < 0)
            {
                viTriHoaDon = danhSachMaHD.Count - 1;
            }
            else
            {
                viTriHoaDon--;
                if (viTriHoaDon < 0)
                {
                    viTriHoaDon = danhSachMaHD.Count - 1;
                }
            }

            TaiHoaDonTheoMa(danhSachMaHD[viTriHoaDon]);
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            if (danhSachMaHD.Count == 0)
            {
                return;
            }

            if (viTriHoaDon < 0)
            {
                viTriHoaDon = 0;
            }
            else
            {
                viTriHoaDon++;
                if (viTriHoaDon >= danhSachMaHD.Count)
                {
                    viTriHoaDon = 0;
                }
            }

            TaiHoaDonTheoMa(danhSachMaHD[viTriHoaDon]);
        }

        private void lblSDTKhachHang_Click(object sender, EventArgs e)
        {

        }
    }
}
