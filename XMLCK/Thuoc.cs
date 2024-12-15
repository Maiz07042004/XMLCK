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
using System.Xml;

namespace XMLCK
{
    public partial class Thuoc : Form
    {
        public Thuoc()
        {
            InitializeComponent();
            SaveDataToXML();
            LoadDataFromXML();
            Display2();
        }

        private void SaveDataToXML()
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    string query = "SELECT * FROM ThongTinThuoc";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dt.TableName = "Thuoc"; // Đặt tên cho bảng

                            if (dt.Rows.Count > 0)
                            {
                                string filePath = "ThongTinThuoc.xml";
                                dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
                            }
                            else
                            {
                                MessageBox.Show("Không có dữ liệu từ bảng Thuoc.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        public void Display2()
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // SQL Query để lấy thông tin
                    string queryMaNhomThuoc = "SELECT DISTINCT maNhomThuoc FROM NhomThuoc";
                    using (SqlCommand cmd = new SqlCommand(queryMaNhomThuoc, conn))
                    {

                        SqlDataReader readerMaNV = cmd.ExecuteReader();

                        // Xóa các mục hiện tại trong ComboBox1
                        ComboBox1.Items.Clear();

                        // Duyệt qua các dữ liệu và thêm vào ComboBox1
                        while (readerMaNV.Read())
                        {
                            ComboBox1.Items.Add(readerMaNV["maNhomThuoc"].ToString());
                        }

                        readerMaNV.Close();
                        conn.Close();
                    }

                }
                // Mở kết nối

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LoadDataFromXML()
        {
            try
            {
                DataTable dt = new DataTable();

                dt.ReadXml("ThongTinThuoc.xml");

                TableThuoc.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void AddIntoSQL(string maThuoc, string tenThuoc, DateTime ngaySX, DateTime ngayHH, string congDung, int gia, string maNhomThuoc)
        {
            string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = @"
        INSERT INTO ThongTinThuoc (maThuoc, tenThuoc, ngaySX, ngayHH, congDung, gia, maNhomThuoc)
        VALUES (@maThuoc, @tenThuoc, @ngaySX, @ngayHH, @congDung, @gia, @maNhomThuoc);";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@maThuoc", maThuoc);
                    cmd.Parameters.AddWithValue("@tenThuoc", tenThuoc);
                    cmd.Parameters.AddWithValue("@ngaySX", ngaySX);
                    cmd.Parameters.AddWithValue("@ngayHH", ngayHH);
                    cmd.Parameters.AddWithValue("@congDung", congDung);
                    cmd.Parameters.AddWithValue("@gia", gia);
                    cmd.Parameters.AddWithValue("@maNhomThuoc", maNhomThuoc);
                    // Thêm thông tin mới vào cơ sở dữ liệu
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private void AddNewRow(string maThuoc, string tenThuoc, DateTime ngaySX, DateTime ngayHH, string congDung, int gia, string maNhomThuoc)
        {
            DataTable dt = (DataTable)TableThuoc.DataSource;

            DataRow newRow = dt.NewRow();
            newRow["maThuoc"] = maThuoc;
            newRow["tenThuoc"] = tenThuoc;
            newRow["ngaySX"] = ngaySX;
            newRow["ngayHH"] = ngayHH;
            newRow["congDung"] = congDung;
            newRow["gia"] = gia;
            newRow["maNhomThuoc"] = maNhomThuoc;

            dt.Rows.Add(newRow);

            TableThuoc.DataSource = dt;

            dt.WriteXml("ThongTinThuoc.xml");
        }


        private void UpdateRowInSQL(string maThuoc, string tenThuoc, DateTime ngaySX, DateTime ngayHH, string congDung, int gia, string maNhomThuoc)
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    string query = @"
        UPDATE ThongTinThuoc 
        SET tenThuoc = @tenThuoc, 
            ngaySX = @ngaySX, 
            ngayHH = @ngayHH, 
            congDung = @congDung, 
            gia = @gia, 
            maNhomThuoc = @maNhomThuoc 
        WHERE maThuoc = @maThuoc;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số an toàn
                        cmd.Parameters.AddWithValue("@maThuoc", maThuoc);
                        cmd.Parameters.AddWithValue("@tenThuoc", tenThuoc);
                        cmd.Parameters.AddWithValue("@ngaySX", ngaySX);
                        cmd.Parameters.AddWithValue("@ngayHH", ngayHH);
                        cmd.Parameters.AddWithValue("@congDung", congDung);
                        cmd.Parameters.AddWithValue("@gia", gia);
                        cmd.Parameters.AddWithValue("@maNhomThuoc", maNhomThuoc);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin thuốc thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin thuốc để cập nhật hoặc có lỗi xảy ra.");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                QuanLiThuoc quanLiThuocForm = new QuanLiThuoc();
                quanLiThuocForm.Show();
                this.Hide();
            }
        }



        private void Thuoc_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string maThuoc = TextBox6.Text.Trim();
                string tenThuoc = TextBox1.Text.Trim();
                DateTime ngaySX = DateTimePicker1.Value;
                DateTime ngayHH = DateTimePicker2.Value;
                string congDung = TextBox3.Text.Trim();
                int gia = int.TryParse(TextBox4.Text.Trim(), out int parsedGia) ? parsedGia : 0;
                string maNhomThuoc = ComboBox1.SelectedItem?.ToString() ?? "";

                if (string.IsNullOrEmpty(maThuoc) || string.IsNullOrEmpty(tenThuoc) || string.IsNullOrEmpty(maNhomThuoc))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.");
                    return;
                }
                AddIntoSQL(maThuoc, tenThuoc, ngaySX, ngayHH, congDung, gia, maNhomThuoc);
                AddNewRow(maThuoc, tenThuoc, ngaySX, ngayHH, congDung, gia, maNhomThuoc);
                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TableThuoc.SelectedRows.Count > 0)
                {
                    int selectedIndex = TableThuoc.SelectedRows[0].Index;

                    string maThuoc = TextBox6.Text.Trim();
                    string tenThuoc = TextBox1.Text.Trim();
                    DateTime ngaySX = DateTimePicker1.Value;
                    DateTime ngayHH = DateTimePicker2.Value;
                    string congDung = TextBox3.Text.Trim();
                    int gia = int.TryParse(TextBox4.Text.Trim(), out int parsedGia) ? parsedGia : 0;
                    string maNhomThuoc = ComboBox1.SelectedItem?.ToString() ?? "";

                    UpdateRowInSQL(maThuoc, tenThuoc, ngaySX, ngayHH, congDung, gia, maNhomThuoc);
                    DataTable dt = (DataTable)TableThuoc.DataSource;

                    DataRow row = dt.Rows[selectedIndex];
                    row["maThuoc"] = maThuoc;
                    row["tenThuoc"] = tenThuoc;
                    row["ngaySX"] = ngaySX;
                    row["ngayHH"] = ngayHH;
                    row["congDung"] = congDung;
                    row["gia"] = gia;
                    row["maNhomThuoc"] = maNhomThuoc;

                    dt.WriteXml("ThongTinThuoc.xml");

                    TableThuoc.DataSource = dt;

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thuốc để sửa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (TableThuoc.SelectedRows.Count > 0)
                {
                    int selectedIndex = TableThuoc.SelectedRows[0].Index;
                    string maThuoc = TextBox6.Text.Trim();
                    DataTable dt = (DataTable)TableThuoc.DataSource;

                    dt.Rows[selectedIndex].Delete();

                    dt.WriteXml("ThongTinThuoc.xml");

                    TableThuoc.DataSource = dt;

                    string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();

                        string query = "DELETE FROM ThongTinThuoc WHERE maThuoc = @maThuoc";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@maThuoc", maThuoc);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            MessageBox.Show("Xoá thành công.");

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bác sĩ để xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void TableThuoc_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (TableThuoc.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = TableThuoc.SelectedRows[0];

                    // Lấy và gán mã thuốc
                    TextBox6.Text = selectedRow.Cells["maThuoc"].Value?.ToString() ?? "";

                    // Lấy và gán tên thuốc
                    TextBox1.Text = selectedRow.Cells["tenThuoc"].Value?.ToString() ?? "";

                    // Chuyển đổi và gán ngày sản xuất
                    if (DateTime.TryParse(selectedRow.Cells["ngaySX"].Value?.ToString(), out DateTime ngaySX))
                    {
                        DateTimePicker1.Value = ngaySX;
                    }
                    else
                    {
                        DateTimePicker1.Value = DateTime.Now; // Hoặc một ngày hợp lý mặc định
                    }

                    // Chuyển đổi và gán ngày hết hạn
                    if (DateTime.TryParse(selectedRow.Cells["ngayHH"].Value?.ToString(), out DateTime ngayHH))
                    {
                        DateTimePicker2.Value = ngayHH;
                    }
                    else
                    {
                        DateTimePicker2.Value = DateTime.Now; // Hoặc một ngày hợp lý mặc định
                    }

                    // Gán công dụng thuốc
                    TextBox3.Text = selectedRow.Cells["congDung"].Value?.ToString() ?? "";
                    TextBox4.Text = selectedRow.Cells["gia"].Value.ToString();

                    // Gán thông tin nhóm thuốc vào ComboBox
                    string maNhomThuoc = selectedRow.Cells["maNhomThuoc"].Value?.ToString() ?? "";
                    if (ComboBox1.Items.Contains(maNhomThuoc))
                    {
                        ComboBox1.SelectedItem = maNhomThuoc;
                    }
                    else
                    {
                        ComboBox1.SelectedIndex = -1; // Không tìm thấy dữ liệu phù hợp, bỏ chọn
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XuatTatCaThuoc();
        }

        private void XuatTatCaThuoc()
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "ThongTinThuoc.xml"; // Đảm bảo tên file XML là chính xác

                // Tạo một đối tượng XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(Application.StartupPath, fileXML));

                // Kiểm tra nếu root node không phải "NewDataSet"
                if (xmlDoc.DocumentElement.Name != "NewDataSet")
                {
                    XmlElement newRoot = xmlDoc.CreateElement("NewDataSet");

                    // Di chuyển tất cả các child nodes của root node cũ vào newRoot
                    foreach (XmlNode child in xmlDoc.DocumentElement.ChildNodes)
                    {
                        newRoot.AppendChild(child.CloneNode(true));
                    }

                    // Thay thế root node cũ bằng newRoot
                    xmlDoc.ReplaceChild(newRoot, xmlDoc.DocumentElement);
                }

                // Lấy tất cả các node Thuoc
                XmlNodeList nodes = xmlDoc.SelectNodes("/NewDataSet/Thuoc");

                // Kiểm tra nếu có dữ liệu
                if (nodes.Count > 0)
                {
                    // Sử dụng StringBuilder để tạo nội dung HTML
                    StringBuilder htmlContent = new StringBuilder();

                    // Bắt đầu HTML
                    htmlContent.Append("<html><body>");
                    htmlContent.Append("<h1>Danh sách tất cả các thuốc</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Mã Thuốc</th><th>Tên Thuốc</th><th>Ngày Sản Xuất</th><th>Ngày Hết Hạn</th><th>Công Dụng</th><th>Giá</th><th>Mã Nhóm Thuốc</th></tr>");

                    // Duyệt qua các node Thuoc và thêm vào nội dung HTML
                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("tenThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("ngaySX")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("ngayHH")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("congDung")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("gia")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maNhomThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("</tr>");
                    }

                    // Kết thúc bảng và HTML
                    htmlContent.Append("</table>");
                    htmlContent.Append("</body></html>");

                    // Ghi nội dung HTML ra một file tạm thời
                    string tempHtmlFile = Path.Combine(Application.StartupPath, "DanhSachThuoc.html");
                    File.WriteAllText(tempHtmlFile, htmlContent.ToString());

                    // Mở file HTML bằng trình duyệt mặc định
                    System.Diagnostics.Process.Start(tempHtmlFile);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu thuốc trong file XML.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất danh sách thuốc: {ex.Message}");
            }
        }






    }


}
