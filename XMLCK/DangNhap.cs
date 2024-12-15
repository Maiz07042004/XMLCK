using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMLCK
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
            SaveDataToXML();
            TextBox2.PasswordChar = '*';
        }

        private void SaveDataToXML()
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Admin";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu từ bảng Admin.");
                                return;
                            }

                            // Thiết lập tên cho DataTable nếu chưa có
                            dt.TableName = "DangNhap";

                            // Đảm bảo thư mục tồn tại
                            string filePath = "DangNhap.xml";
                            dt.WriteXml(filePath, XmlWriteMode.WriteSchema);

                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
            }
        }
        private bool CheckLogin(string username, string password)
        {
            try
            {
                string filePath = "DangNhap.xml";

                // Kiểm tra nếu tệp XML tồn tại
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Tệp DangNhap.xml không tồn tại.");
                    return false;
                }

                DataTable dt = new DataTable();
                dt.ReadXml(filePath);

                foreach (DataRow row in dt.Rows)
                {
                    // Kiểm tra thông tin đăng nhập
                    if (row["tenDangNhap"].ToString().Trim() == username && row["password"].ToString().Trim() == password)
                    {
                        return true; // Thông tin hợp lệ
                    }
                }

                return false; // Thông tin không hợp lệ
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc tệp XML: {ex.Message}");
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String tenDangNhap = TextBox1.Text.Trim();
            String password = TextBox2.Text.Trim();
            if (CheckLogin(tenDangNhap, password))
            {
                QuanLiThuoc quanLiThuocForm = new QuanLiThuoc();
                quanLiThuocForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}
