using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class FrmPhieuNhap : Form
    {
        private readonly string chuoiketnoi =
            ConfigurationManager.ConnectionStrings["QuanLySieuThi"].ConnectionString;

        private SqlConnection conn = null;
        private DataTable dtChiTietTam = null;
        private readonly List<string> danhSachMaPn = new List<string>();
        private int viTriPhieuNhap = -1;

        public FrmPhieuNhap()
        {
            InitializeComponent();

            Load += FrmPhieuNhap_Load;
            FormClosed += FrmPhieuNhap_FormClosed;

            btnLamMoi.Click += btnLamMoi_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;

            btnThemCT.Click += btnThemCT_Click;
            btnXoaCT.Click += btnXoaCT_Click;

            btnTruoc.Click += btnTruoc_Click;
            btnSau.Click += btnSau_Click;

            cboSanPham.SelectedIndexChanged += cboSanPham_SelectedIndexChanged;
            dgvChiTiet.CellClick += dgvChiTiet_CellClick;
        }

        private void FrmPhieuNhap_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();

            KhoiTaoBangChiTietTam();
            TaiNhanVienVaoCombo();
            TaiNhaCungCapVaoCombo();
            TaiSanPhamVaoCombo();
            TaiDanhSachMaPhieuNhap();
            LamMoiPhieuNhap();
        }

        private void FrmPhieuNhap_FormClosed(object sender, FormClosedEventArgs e)
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
            dtChiTietTam.Columns.Add("DonGiaNhap", typeof(decimal));
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

        private void TaiNhaCungCapVaoCombo()
        {
            const string sql = "SELECT MaNCC, TenNCC FROM NhaCungCap ORDER BY TenNCC";
            using (var da = new SqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);

                cboNhaCungCap.DataSource = dt;
                cboNhaCungCap.DisplayMember = "TenNCC";
                cboNhaCungCap.ValueMember = "MaNCC";
                cboNhaCungCap.SelectedIndex = -1;
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

        private void TaiDanhSachMaPhieuNhap()
        {
            danhSachMaPn.Clear();
            const string sql = "SELECT MaPN FROM PhieuNhap ORDER BY MaPN";
            using (var cmd = new SqlCommand(sql, conn))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    danhSachMaPn.Add(rd["MaPN"].ToString());
                }
            }
        }

        private string TaoMaPNMoi()
        {
            const string sql = "SELECT TOP 1 MaPN FROM PhieuNhap ORDER BY MaPN DESC";
            using (var cmd = new SqlCommand(sql, conn))
            {
                var ketQua = cmd.ExecuteScalar();
                if (ketQua == null || ketQua == DBNull.Value)
                {
                    return "PN0000001";
                }

                var maCu = ketQua.ToString();
                var so = 0;
                if (!string.IsNullOrWhiteSpace(maCu) && maCu.Length > 2)
                {
                    int.TryParse(maCu.Substring(2), out so);
                }

                return "PN" + (so + 1).ToString("D7");
            }
        }

        private void LamMoiPhieuNhap()
        {
            txtMaPN.Text = TaoMaPNMoi();
            dtpNgayLap.Value = DateTime.Today;
            cboNhanVien.SelectedIndex = -1;
            cboNhaCungCap.SelectedIndex = -1;

            cboSanPham.SelectedIndex = -1;
            txtSoLuong.Clear();
            txtDonGiaNhap.Clear();

            dtChiTietTam.Rows.Clear();
            txtTongTien.Text = "0";
            viTriPhieuNhap = -1;
        }

        private void TaiPhieuNhapTheoMa(string maPn)
        {
            const string sqlPn = @"
SELECT MaPN, NgayLap, MaNV, MaNCC, TongTien
FROM PhieuNhap
WHERE MaPN = @MaPN;";

            using (var cmd = new SqlCommand(sqlPn, conn))
            {
                cmd.Parameters.AddWithValue("@MaPN", maPn);
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read())
                    {
                        return;
                    }

                    txtMaPN.Text = rd["MaPN"].ToString();
                    if (rd["NgayLap"] != DBNull.Value)
                    {
                        dtpNgayLap.Value = Convert.ToDateTime(rd["NgayLap"]);
                    }

                    cboNhanVien.SelectedValue = rd["MaNV"] == DBNull.Value ? null : rd["MaNV"].ToString();
                    cboNhaCungCap.SelectedValue = rd["MaNCC"] == DBNull.Value ? null : rd["MaNCC"].ToString();
                }
            }

            dtChiTietTam.Rows.Clear();

            const string sqlCt = @"
SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.DonGiaNhap, ct.ThanhTien
FROM ChiTietPhieuNhap ct
INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
WHERE ct.MaPN = @MaPN
ORDER BY ct.MaSP;";

            using (var da = new SqlDataAdapter(sqlCt, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@MaPN", maPn);
                var dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    dtChiTietTam.Rows.Add(
                        row["MaSP"].ToString(),
                        row["TenSP"].ToString(),
                        Convert.ToInt32(row["SoLuong"]),
                        Convert.ToDecimal(row["DonGiaNhap"]),
                        Convert.ToDecimal(row["ThanhTien"]));
                }
            }

            TinhTongTien();
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null)
            {
                txtDonGiaNhap.Clear();
                return;
            }

            const string sql = @"
SELECT TOP 1 DonGiaNhap
FROM ChiTietPhieuNhap
WHERE MaSP = @MaSP
ORDER BY MaPN DESC;";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", cboSanPham.SelectedValue.ToString());
                var giaNhap = cmd.ExecuteScalar();
                if (giaNhap != null && giaNhap != DBNull.Value)
                {
                    txtDonGiaNhap.Text = Convert.ToDecimal(giaNhap).ToString("0");
                    return;
                }
            }

            const string sqlGiaBan = "SELECT DonGiaBan FROM SanPham WHERE MaSP = @MaSP";
            using (var cmd = new SqlCommand(sqlGiaBan, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", cboSanPham.SelectedValue.ToString());
                var giaBan = cmd.ExecuteScalar();
                txtDonGiaNhap.Text = giaBan == null || giaBan == DBNull.Value
                    ? "0"
                    : Convert.ToDecimal(giaBan).ToString("0");
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

            decimal donGiaNhap;
            if (!decimal.TryParse(txtDonGiaNhap.Text.Trim(), out donGiaNhap) || donGiaNhap < 0)
            {
                MessageBox.Show("Đơn giá nhập không hợp lệ.");
                txtDonGiaNhap.Focus();
                return;
            }

            var maSp = cboSanPham.SelectedValue.ToString();
            var tenSp = cboSanPham.Text;
            var thanhTien = soLuong * donGiaNhap;

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
                dtChiTietTam.Rows.Add(maSp, tenSp, soLuong, donGiaNhap, thanhTien);
            }
            else
            {
                var slCu = Convert.ToInt32(rowTimThay["SoLuong"]);
                var slMoi = slCu + soLuong;
                rowTimThay["SoLuong"] = slMoi;
                rowTimThay["ThanhTien"] = slMoi * donGiaNhap;
            }

            txtSoLuong.Clear();
            TinhTongTien();
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null || dgvChiTiet.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn dòng chi tiết cần xóa.");
                return;
            }

            dtChiTietTam.Rows.RemoveAt(dgvChiTiet.CurrentRow.Index);
            TinhTongTien();
        }

        private void dgvChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvChiTiet.Rows[e.RowIndex];
            txtSoLuong.Text = row.Cells["colSoLuong"].Value == null ? string.Empty : row.Cells["colSoLuong"].Value.ToString();
            txtDonGiaNhap.Text = row.Cells["colDonGiaNhap"].Value == null ? string.Empty : row.Cells["colDonGiaNhap"].Value.ToString();

            if (row.Cells["colMaSP"].Value != null)
            {
                cboSanPham.SelectedValue = row.Cells["colMaSP"].Value.ToString();
            }
        }

        private void TinhTongTien()
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
        }

        private bool KiemTraDuLieuPhieuNhap()
        {
            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.");
                cboNhanVien.Focus();
                return false;
            }

            if (cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp.");
                cboNhaCungCap.Focus();
                return false;
            }

            if (dtChiTietTam.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 sản phẩm vào chi tiết phiếu nhập.");
                return false;
            }

            return true;
        }

        private DataTable LayChiTietPhieuNhapTheoMa(string maPn, SqlTransaction tran)
        {
            const string sql = @"
SELECT MaSP, SoLuong
FROM ChiTietPhieuNhap
WHERE MaPN = @MaPN;";

            using (var da = new SqlDataAdapter(sql, conn))
            {
                da.SelectCommand.Transaction = tran;
                da.SelectCommand.Parameters.AddWithValue("@MaPN", maPn);

                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private Dictionary<string, int> TaoBangSoLuong(DataTable dt, string tenCotMa, string tenCotSoLuong)
        {
            var ketQua = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow row in dt.Rows)
            {
                var ma = row[tenCotMa].ToString();
                var sl = Convert.ToInt32(row[tenCotSoLuong]);
                if (ketQua.ContainsKey(ma))
                {
                    ketQua[ma] += sl;
                }
                else
                {
                    ketQua[ma] = sl;
                }
            }

            return ketQua;
        }

        private void CapNhatTonKho(SqlTransaction tran, string maSp, int chenhlech)
        {
            if (chenhlech == 0)
            {
                return;
            }

            if (chenhlech > 0)
            {
                const string sqlCong = @"
UPDATE SanPham
SET SoLuongTon = SoLuongTon + @SoLuong
WHERE MaSP = @MaSP;";

                using (var cmd = new SqlCommand(sqlCong, conn, tran))
                {
                    cmd.Parameters.AddWithValue("@MaSP", maSp);
                    cmd.Parameters.AddWithValue("@SoLuong", chenhlech);

                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("Không tìm thấy sản phẩm: " + maSp);
                    }
                }
            }
            else
            {
                var slGiam = -chenhlech;
                const string sqlTru = @"
UPDATE SanPham
SET SoLuongTon = SoLuongTon - @SoLuong
WHERE MaSP = @MaSP
  AND SoLuongTon >= @SoLuong;";

                using (var cmd = new SqlCommand(sqlTru, conn, tran))
                {
                    cmd.Parameters.AddWithValue("@MaSP", maSp);
                    cmd.Parameters.AddWithValue("@SoLuong", slGiam);

                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("Không thể giảm tồn kho sản phẩm " + maSp + " vì số lượng tồn không đủ.");
                    }
                }
            }
        }

        private void LuuPhieuNhap(bool laThem)
        {
            if (!KiemTraDuLieuPhieuNhap())
            {
                return;
            }

            var maPn = txtMaPN.Text.Trim();
            var maNv = cboNhanVien.SelectedValue.ToString();
            var maNcc = cboNhaCungCap.SelectedValue.ToString();

            decimal tongTien;
            if (!decimal.TryParse(txtTongTien.Text.Trim(), out tongTien))
            {
                tongTien = 0;
            }

            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    var soLuongMoi = TaoBangSoLuong(dtChiTietTam, "MaSP", "SoLuong");

                    if (laThem)
                    {
                        const string sqlThemPn = @"
INSERT INTO PhieuNhap (MaPN, NgayLap, MaNV, MaNCC, PhanTramChietKhau, TongTien)
VALUES (@MaPN, @NgayLap, @MaNV, @MaNCC, @PhanTramChietKhau, @TongTien);";

                        using (var cmd = new SqlCommand(sqlThemPn, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaPN", maPn);
                            cmd.Parameters.AddWithValue("@NgayLap", dtpNgayLap.Value.Date);
                            cmd.Parameters.AddWithValue("@MaNV", maNv);
                            cmd.Parameters.AddWithValue("@MaNCC", maNcc);
                            cmd.Parameters.AddWithValue("@PhanTramChietKhau", 0);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            cmd.ExecuteNonQuery();
                        }

                        foreach (var item in soLuongMoi)
                        {
                            CapNhatTonKho(tran, item.Key, item.Value);
                        }
                    }
                    else
                    {
                        var dtCu = LayChiTietPhieuNhapTheoMa(maPn, tran);
                        var soLuongCu = TaoBangSoLuong(dtCu, "MaSP", "SoLuong");

                        var tatCaSanPham = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                        foreach (var item in soLuongCu)
                        {
                            tatCaSanPham[item.Key] = 0;
                        }

                        foreach (var item in soLuongMoi)
                        {
                            tatCaSanPham[item.Key] = 0;
                        }

                        foreach (var item in tatCaSanPham)
                        {
                            var maSp = item.Key;
                            var slCu = soLuongCu.ContainsKey(maSp) ? soLuongCu[maSp] : 0;
                            var slMoi = soLuongMoi.ContainsKey(maSp) ? soLuongMoi[maSp] : 0;
                            var chenhlech = slMoi - slCu;
                            CapNhatTonKho(tran, maSp, chenhlech);
                        }

                        const string sqlSuaPn = @"
UPDATE PhieuNhap
SET NgayLap = @NgayLap,
    MaNV = @MaNV,
    MaNCC = @MaNCC,
    PhanTramChietKhau = @PhanTramChietKhau,
    TongTien = @TongTien
WHERE MaPN = @MaPN;";

                        using (var cmd = new SqlCommand(sqlSuaPn, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaPN", maPn);
                            cmd.Parameters.AddWithValue("@NgayLap", dtpNgayLap.Value.Date);
                            cmd.Parameters.AddWithValue("@MaNV", maNv);
                            cmd.Parameters.AddWithValue("@MaNCC", maNcc);
                            cmd.Parameters.AddWithValue("@PhanTramChietKhau", 0);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM ChiTietPhieuNhap WHERE MaPN = @MaPN", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaPN", maPn);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    const string sqlThemCt = @"
INSERT INTO ChiTietPhieuNhap (MaPN, MaSP, SoLuong, DonGiaNhap, ThanhTien)
VALUES (@MaPN, @MaSP, @SoLuong, @DonGiaNhap, @ThanhTien);";

                    foreach (DataRow row in dtChiTietTam.Rows)
                    {
                        using (var cmd = new SqlCommand(sqlThemCt, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaPN", maPn);
                            cmd.Parameters.AddWithValue("@MaSP", row["MaSP"].ToString());
                            cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row["SoLuong"]));
                            cmd.Parameters.AddWithValue("@DonGiaNhap", Convert.ToDecimal(row["DonGiaNhap"]));
                            cmd.Parameters.AddWithValue("@ThanhTien", Convert.ToDecimal(row["ThanhTien"]));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    MessageBox.Show(laThem ? "Thêm phiếu nhập thành công." : "Sửa phiếu nhập thành công.");

                    TaiDanhSachMaPhieuNhap();
                    viTriPhieuNhap = danhSachMaPn.IndexOf(maPn);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Lưu phiếu nhập thất bại: " + ex.Message);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LuuPhieuNhap(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (viTriPhieuNhap < 0)
            {
                MessageBox.Show("Vui lòng mở phiếu nhập đã lưu để sửa.");
                return;
            }

            LuuPhieuNhap(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maPn = txtMaPN.Text.Trim();
            if (string.IsNullOrWhiteSpace(maPn))
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần xóa.");
                return;
            }

            var xacNhan = MessageBox.Show(
                "Bạn có chắc muốn xóa phiếu nhập này?",
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
                    var dtCu = LayChiTietPhieuNhapTheoMa(maPn, tran);
                    foreach (DataRow row in dtCu.Rows)
                    {
                        CapNhatTonKho(tran, row["MaSP"].ToString(), -Convert.ToInt32(row["SoLuong"]));
                    }

                    using (var cmd = new SqlCommand("DELETE FROM ChiTietPhieuNhap WHERE MaPN = @MaPN", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPn);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("DELETE FROM PhieuNhap WHERE MaPN = @MaPN", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPn);
                        var soDong = cmd.ExecuteNonQuery();
                        if (soDong == 0)
                        {
                            throw new Exception("Không tìm thấy mã phiếu nhập.");
                        }
                    }

                    tran.Commit();
                    MessageBox.Show("Xóa phiếu nhập thành công.");

                    TaiDanhSachMaPhieuNhap();
                    LamMoiPhieuNhap();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Xóa phiếu nhập thất bại: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiPhieuNhap();
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            if (danhSachMaPn.Count == 0)
            {
                return;
            }

            if (viTriPhieuNhap < 0)
            {
                viTriPhieuNhap = danhSachMaPn.Count - 1;
            }
            else
            {
                viTriPhieuNhap--;
                if (viTriPhieuNhap < 0)
                {
                    viTriPhieuNhap = danhSachMaPn.Count - 1;
                }
            }

            TaiPhieuNhapTheoMa(danhSachMaPn[viTriPhieuNhap]);
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            if (danhSachMaPn.Count == 0)
            {
                return;
            }

            if (viTriPhieuNhap < 0)
            {
                viTriPhieuNhap = 0;
            }
            else
            {
                viTriPhieuNhap++;
                if (viTriPhieuNhap >= danhSachMaPn.Count)
                {
                    viTriPhieuNhap = 0;
                }
            }

            TaiPhieuNhapTheoMa(danhSachMaPn[viTriPhieuNhap]);
        }
    }
}
