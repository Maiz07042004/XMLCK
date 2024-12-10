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
            Form3 Form3 = new Form3();
            Form3.Show();
            this.Hide();
        }

        private void menu2_Click(object sender, EventArgs e)
        {
            Form4 Form3 = new Form4();
            Form3.Show();
            this.Hide();
        }

        private void menu3_Click(object sender, EventArgs e)
        {
            Form5 Form3 = new Form5();
            Form3.Show();
            this.Hide();
        }

        private void menu4_Click(object sender, EventArgs e)
        {
            Form6 Form3 = new Form6();
            Form3.Show();
            this.Hide();
        }

        private void TìmKiếmThuốcNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 Form3 = new Form7();
            Form3.Show();
            this.Hide();
        }

        private void TìmKiếmNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 Form3 = new Form8();
            Form3.Show();
            this.Hide();
        }

        private void TìmKiếmThôngTinThuốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 Form3 = new Form9();
            Form3.Show();
            this.Hide();
        }

        private void TìmKiếmThuốcBànGiaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 Form3 = new Form10();
            Form3.Show();
            this.Hide();
        }
    }
}
