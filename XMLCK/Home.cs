using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLCK
{
    public partial class QuanLiThuoc : Form
    {
        public QuanLiThuoc()
        {
            InitializeComponent();
        }

        private void QuanLiThuoc_Load(object sender, EventArgs e)
        {

        }

        private void menu1_Click(object sender, EventArgs e)
        {
            NhanVien Form3 = new NhanVien();
            Form3.Show();
            this.Hide();
        }

        private void menu2_Click(object sender, EventArgs e)
        {
            Thuoc Form3 = new Thuoc();
            Form3.Show();
            this.Hide();
        }

        private void menu3_Click(object sender, EventArgs e)
        {
            Kho Form3 = new Kho();
            Form3.Show();
            this.Hide();
        }

        private void menu4_Click(object sender, EventArgs e)
        {
            BanGiao Form3 = new BanGiao();
            Form3.Show();
            this.Hide();
        }

        private void TìmKiếmThuốcNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TKThuocNhap Form3 = new TKThuocNhap();
            Form3.Show();
            this.Hide();
        }

        private void TìmKiếmNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TKNV Form3 = new TKNV();
            Form3.Show();
            this.Hide();
        }

        private void TìmKiếmThôngTinThuốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TKThuoc Form3 = new TKThuoc();
            Form3.Show();
            this.Hide();
        }

        private void TìmKiếmThuốcBànGiaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TKBanGiao Form3 = new TKBanGiao();
            Form3.Show();
            this.Hide();
        }
    }
}
