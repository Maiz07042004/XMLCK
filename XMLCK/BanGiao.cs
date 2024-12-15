using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMLCK
{
    public partial class BanGiao : Form
    {
        public BanGiao()
        {
            InitializeComponent();
            LoadDataFromXML();
            SaveDataToXML();
            Display2();
            Display1();
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


        private void SaveDataToXML()
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // Truy vấn bảng BanGiaoThuoc
                    string query = "SELECT * FROM BanGiaoThuoc";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dt.TableName = "BanGiaoThuoc"; // Đặt tên cho bảng

                            if (dt.Rows.Count > 0)
                            {
                                string filePath = "BanGiaoThuoc.xml";
                                dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
                            }
                            else
                            {
                                MessageBox.Show("Không có dữ liệu từ bảng BanGiaoThuoc.");
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


        private void LoadDataFromXML()
        {
            try
            {
                DataTable dt = new DataTable();

                // Đọc dữ liệu từ file BanGiaoThuoc.xml
                dt.ReadXml("BanGiaoThuoc.xml");

                // Gán dữ liệu vào DataGridView
                TableBGT.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void AddIntoSQL(string maBG, string maNV, string maThuoc, string phong, DateTime ngayBanGiao)
        {
            string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = "INSERT INTO BanGiaoThuoc (maBG, maNV, maThuoc, phong, ngayBanGiao) VALUES (@maBG, @maNV, @maThuoc, @phong, @ngayBanGiao)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số với giá trị tương ứng
                    cmd.Parameters.AddWithValue("@maBG", maBG);
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    cmd.Parameters.AddWithValue("@maThuoc", maThuoc);
                    cmd.Parameters.AddWithValue("@phong", phong);
                    cmd.Parameters.AddWithValue("@ngayBanGiao", ngayBanGiao);

                    // Thực thi câu lệnh INSERT
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Kiểm tra kết quả
                    if (rowsAffected > 0)
                    {
                        
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu được thêm.");
                    }
                }
            }
        }

        private void AddNewRow(string maBG, string maNV, string maThuoc, string phong, DateTime ngayBanGiao)
        {
            try
            {
                // Kiểm tra nếu DataSource của TableBGT là DataTable
                if (TableBGT.DataSource is DataTable dt)
                {
                    // Tạo một hàng mới và gán giá trị
                    DataRow newRow = dt.NewRow();
                    newRow["maBG"] = maBG;
                    newRow["maNV"] = maNV;
                    newRow["maThuoc"] = maThuoc;
                    newRow["phong"] = phong;
                    newRow["ngayBanGiao"] = ngayBanGiao;

                    // Thêm hàng mới vào DataTable
                    dt.Rows.Add(newRow);

                    // Lưu DataTable vào XML (tùy chọn, nếu muốn lưu trạng thái)
                    dt.WriteXml("BanGiaoThuoc.xml", XmlWriteMode.WriteSchema);

                    // Cập nhật lại DataSource cho DataGridView
                    TableBGT.DataSource = dt;

                    
                    
                }
                else
                {
                    MessageBox.Show("Không thể thêm dữ liệu vì DataSource chưa được khởi tạo hoặc không đúng kiểu DataTable.");
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
                    string queryMaNhomThuoc = "SELECT DISTINCT maNV FROM NhanVien";
                    using (SqlCommand cmd = new SqlCommand(queryMaNhomThuoc, conn))
                    {

                        SqlDataReader readerMaNV = cmd.ExecuteReader();

                        // Xóa các mục hiện tại trong ComboBox1
                        MaNV.Items.Clear();

                        // Duyệt qua các dữ liệu và thêm vào ComboBox1
                        while (readerMaNV.Read())
                        {
                            MaNV.Items.Add(readerMaNV["maNV"].ToString());
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


        public void Display1()
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // SQL Query để lấy thông tin
                    string queryMaNhomThuoc = "SELECT DISTINCT maThuoc FROM ThongTinThuoc";
                    using (SqlCommand cmd = new SqlCommand(queryMaNhomThuoc, conn))
                    {

                        SqlDataReader readerMaNV = cmd.ExecuteReader();

                        // Xóa các mục hiện tại trong ComboBox1
                        MaThuoc.Items.Clear();

                        // Duyệt qua các dữ liệu và thêm vào ComboBox1
                        while (readerMaNV.Read())
                        {
                            MaThuoc.Items.Add(readerMaNV["maThuoc"].ToString());
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

        private void UpdateRowInSQL(string maBG, string maNV, string maThuoc, string phong, DateTime ngayBanGiao)
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    string query = @"
                UPDATE BanGiaoThuoc 
                SET maNV = @maNV, maThuoc = @maThuoc, phong = @phong, ngayBanGiao = @ngayBanGiao 
                WHERE maBG = @maBG";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Gắn tham số cho câu lệnh SQL
                        cmd.Parameters.AddWithValue("@maBG", maBG);
                        cmd.Parameters.AddWithValue("@maNV", maNV);
                        cmd.Parameters.AddWithValue("@maThuoc", maThuoc);
                        cmd.Parameters.AddWithValue("@phong", phong);
                        cmd.Parameters.AddWithValue("@ngayBanGiao", ngayBanGiao);

                        // Thực thi câu lệnh
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy bản ghi bàn giao thuốc để cập nhật.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }






        private void BanGiao_Load(object sender, EventArgs e)
        {
            
        }

        private void TableBGT_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (TableBGT.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = TableBGT.SelectedRows[0];

                    // Gán giá trị từ cột của bảng BanGiaoThuoc vào các điều khiển (TextBox, Label...)
                    MaBG.Text = selectedRow.Cells["maBG"].Value?.ToString() ?? "";
                    string maNV = selectedRow.Cells["maNV"].Value?.ToString() ?? "";
                    if (MaNV.Items.Contains(maNV))
                    {
                        MaNV.SelectedItem = maNV;
                    }
                    else
                    {
                        MaNV.SelectedIndex = -1; // Không tìm thấy dữ liệu phù hợp, bỏ chọn
                    }

                    
                    string maThuoc = selectedRow.Cells["maThuoc"].Value?.ToString() ?? "";
                    if (MaThuoc.Items.Contains(maThuoc))
                    {
                        MaThuoc.SelectedItem = maThuoc;
                    }
                    else
                    {
                        MaThuoc.SelectedIndex = -1; // Không tìm thấy dữ liệu phù hợp, bỏ chọn
                    }

                    Phong.Text = selectedRow.Cells["phong"].Value?.ToString() ?? "";
                    NgayBanGiao.Text = selectedRow.Cells["ngayBanGiao"].Value?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Them_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các ô nhập
                string maBG = MaBG.Text; // Mã bàn giao
                string maNV = MaNV.Text; // Mã nhân viên
                string maThuoc = MaThuoc.Text; // Mã thuốc
                string phong = Phong.Text; // Phòng
                DateTime ngayBanGiao = NgayBanGiao.Value; // Ngày bàn giao (định dạng yyyy-MM-dd)

                // Gọi hàm thêm vào DataGridView và SQL
                AddNewRow(maBG, maNV, maThuoc, phong, ngayBanGiao);
                AddIntoSQL(maBG, maNV, maThuoc, phong, ngayBanGiao);

                // Hiển thị thông báo thành công
                MessageBox.Show("Thêm bàn giao thuốc thành công!");
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (TableBGT.SelectedRows.Count > 0)
                {
                    // Lấy chỉ số dòng được chọn
                    int selectedIndex = TableBGT.SelectedRows[0].Index;

                    // Lấy mã bàn giao từ ô nhập
                    string maBG = MaBG.Text;

                    // Lấy DataTable từ DataSource
                    if (TableBGT.DataSource is DataTable dt)
                    {
                        // Xóa dòng từ DataTable
                        dt.Rows[selectedIndex].Delete();

                        // Lưu lại DataTable vào file XML
                        dt.WriteXml("BanGiaoThuoc.xml", XmlWriteMode.WriteSchema);

                        // Cập nhật DataGridView
                        TableBGT.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ.");
                        return;
                    }

                    // Xóa bàn giao thuốc khỏi cơ sở dữ liệu
                    string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();

                        string query = "DELETE FROM BanGiaoThuoc WHERE maBG = @maBG";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@maBG", maBG);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa bàn giao thuốc thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy bàn giao thuốc để xóa trong cơ sở dữ liệu.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bàn giao thuốc để xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (TableBGT.SelectedRows.Count > 0)
                {
                    // Lấy chỉ số dòng được chọn
                    int selectedIndex = TableBGT.SelectedRows[0].Index;

                    // Lấy dữ liệu từ các TextBox
                    string maBG = MaBG.Text;
                    string maNV = MaNV.Text;
                    string maThuoc = MaThuoc.Text;
                    string phong = Phong.Text;
                    DateTime ngayBanGiao = NgayBanGiao.Value;

                    // Lấy DataTable từ DataSource
                    DataTable dt = (DataTable)TableBGT.DataSource;

                    // Cập nhật dữ liệu trong hàng được chọn
                    DataRow row = dt.Rows[selectedIndex];
                    row["maBG"] = maBG;
                    row["maNV"] = maNV;
                    row["maThuoc"] = maThuoc;
                    row["phong"] = phong;
                    row["ngayBanGiao"] = ngayBanGiao;

                    // Ghi dữ liệu mới vào file XML
                    dt.WriteXml("BanGiaoThuoc.xml", XmlWriteMode.WriteSchema);

                    // Cập nhật lại DataSource
                    TableBGT.DataSource = dt;

                    // Gọi hàm cập nhật vào cơ sở dữ liệu
                    UpdateRowInSQL(maBG, maNV, maThuoc, phong, ngayBanGiao);

                    MessageBox.Show("Sửa thông tin bàn giao thuốc thành công!");
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi để sửa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XuatDanhSachBanGiaoThuoc();
        }

        private void XuatDanhSachBanGiaoThuoc()
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "BanGiaoThuoc.xml"; // Thay bằng tên file XML của bạn

                // Tạo một đối tượng XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(Application.StartupPath, fileXML));

                // Kiểm tra nếu root node không phải là "NewDataSet"
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

                // Lấy tất cả các node BanGiaoThuoc
                XmlNodeList nodes = xmlDoc.SelectNodes("/NewDataSet/BanGiaoThuoc");

                // Kiểm tra nếu có dữ liệu
                if (nodes.Count > 0)
                {
                    // Sử dụng StringBuilder để tạo nội dung HTML
                    StringBuilder htmlContent = new StringBuilder();

                    // Bắt đầu HTML
                    htmlContent.Append("<html><body>");
                    htmlContent.Append("<h1>Danh sách bàn giao thuốc</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Mã Bàn Giao</th><th>Mã Nhân Viên</th><th>Mã Thuốc</th><th>Phòng</th><th>Ngày Bàn Giao</th></tr>");

                    // Duyệt qua các node BanGiaoThuoc và thêm vào nội dung HTML
                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maBG")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maNV")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("phong")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("ngayBanGiao")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("</tr>");
                    }

                    // Kết thúc bảng và HTML
                    htmlContent.Append("</table>");
                    htmlContent.Append("</body></html>");

                    // Ghi nội dung HTML ra file tạm thời
                    string tempHtmlFile = Path.Combine(Application.StartupPath, "DanhSachBanGiaoThuoc.html");
                    File.WriteAllText(tempHtmlFile, htmlContent.ToString());

                    // Mở file HTML bằng trình duyệt mặc định
                    System.Diagnostics.Process.Start(tempHtmlFile);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu bàn giao thuốc trong file XML.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất danh sách bàn giao thuốc: {ex.Message}");
            }
        }

    }
}
