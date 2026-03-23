using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLsieuthi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ketQua = MessageBox.Show(
               "Bạn có chắc muốn đăng xuất không?",
               "Xác nhận đăng xuất",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void phânQuyềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmPhanQuyen())
            {
                frm.ShowDialog(this);
            }
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmTaiKhoan())
            {
                frm.ShowDialog(this);
            }
        }
    }
}