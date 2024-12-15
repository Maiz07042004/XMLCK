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
    public partial class Kho : Form
    {
        public Kho()
        {
            InitializeComponent();
            SaveDataToXML();
            LoadDataFromXML();
            Display2();
            TextBox5.Text = TinhTongTienChuaThanhToan().ToString();
        }

        private void SaveDataToXML()
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ThuocNhap";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu từ bảng ThuocNhap.");
                                return;
                            }

                            // Thiết lập tên cho DataTable nếu chưa có
                            dt.TableName = "ThuocNhap";

                            // Đảm bảo thư mục tồn tại
                            string filePath = "ThuocNhap.xml";
                            dt.WriteXml(filePath, XmlWriteMode.WriteSchema);

                            MessageBox.Show($"Dữ liệu đã lưu thành công tại: {filePath}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
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
                    string queryMaNhomThuoc = "SELECT DISTINCT maThuoc FROM ThongTinThuoc";
                    using (SqlCommand cmd = new SqlCommand(queryMaNhomThuoc, conn))
                    {

                        SqlDataReader readerMaNV = cmd.ExecuteReader();

                        // Xóa các mục hiện tại trong ComboBox1
                        ComboBox1.Items.Clear();

                        // Duyệt qua các dữ liệu và thêm vào ComboBox1
                        while (readerMaNV.Read())
                        {
                            ComboBox1.Items.Add(readerMaNV["maThuoc"].ToString());
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

                dt.ReadXml("ThuocNhap.xml");

                DataGrid1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }





        private void AddIntoSQL(string maNhap, string maThuoc, DateTime ngayNhap, int soLuong, int giaBan, int thanhTien, string trangThai)
        {
            string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = @"
        INSERT INTO ThuocNhap (maNhap,maThuoc, ngayNhap, soLuong, giaBan, thanhTien, trangThai)
        VALUES (@maNhap, @maThuoc, @ngayNhap, @soLuong, @giaBan, @thanhTien, @trangThai);";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@maNhap", maNhap);
                    cmd.Parameters.AddWithValue("@maThuoc", maThuoc);
                    cmd.Parameters.AddWithValue("@ngayNhap", ngayNhap);
                    cmd.Parameters.AddWithValue("@soLuong", soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", giaBan);
                    cmd.Parameters.AddWithValue("@thanhTien", thanhTien);
                    cmd.Parameters.AddWithValue("@trangThai", trangThai);

                    // Thêm thông tin mới vào cơ sở dữ liệu
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private void AddNewRow(string maNhap, string maThuoc, DateTime ngayNhap, int soLuong, int giaBan, int thanhTien, string trangThai)
        {
            DataTable dt = (DataTable)DataGrid1.DataSource;

            DataRow newRow = dt.NewRow();
            newRow["maNhap"] = maNhap;
            newRow["maThuoc"] = maThuoc;
            newRow["ngayNhap"] = ngayNhap;
            newRow["soLuong"] = soLuong;
            newRow["giaBan"] = giaBan;
            newRow["thanhTien"] = thanhTien;
            newRow["trangThai"] = trangThai;

            dt.Rows.Add(newRow);

            DataGrid1.DataSource = dt;

            dt.WriteXml("ThuocNhap.xml");
        }


        private void UpdateRowInSQL(string maNhap, string maThuoc, DateTime ngayNhap, int soLuong, int giaBan, int thanhTien, string trangThai)
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    string query = @"
    UPDATE ThuocNhap 
    SET maThuoc = @maThuoc, 
        ngayNhap = @ngayNhap, 
        soLuong = @soLuong, 
        giaBan = @giaBan, 
        thanhTien = @thanhTien, 
        trangThai = @trangThai 
    WHERE maNhap = @maNhap;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số an toàn
                        cmd.Parameters.AddWithValue("@maThuoc", maThuoc);
                        cmd.Parameters.AddWithValue("@maNhap", maNhap);
                        cmd.Parameters.AddWithValue("@ngayNhap", ngayNhap);
                        cmd.Parameters.AddWithValue("@soLuong", soLuong);
                        cmd.Parameters.AddWithValue("@giaBan", giaBan);
                        cmd.Parameters.AddWithValue("@thanhTien", thanhTien);
                        cmd.Parameters.AddWithValue("@trangThai", trangThai);

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

        private void DeleteRowFromSQL(string maNhap)
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    string query = "DELETE FROM ThuocNhap WHERE maNhap = @maNhap";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@maNhap", maNhap);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xoá thành công từ cơ sở dữ liệu.");
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin cần xoá trong cơ sở dữ liệu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xoá dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }


        public decimal TinhTongTienChuaThanhToan()
        {
            decimal tongTien = 0;

            // Kết nối Cơ sở dữ liệu
            using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;"))
            {
                con.Open();

                // Truy vấn dữ liệu
                string query = "SELECT SUM(thanhTien) AS TongTien FROM ThuocNhap WHERE trangThai = N'Chưa thanh toán'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();

                    // Kiểm tra kết quả và gán cho biến tongTien
                    if (result != DBNull.Value && result != null)
                    {
                        tongTien = Convert.ToDecimal(result);
                    }
                }

                // Kết nối sẽ được tự động đóng khi dùng `using`
            }

            // Trả về tổng tiền
            return tongTien;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                QuanLiThuoc quanLiThuocForm = new QuanLiThuoc();
                quanLiThuocForm.Show();
                this.Hide();
            }
        }

        private void Kho_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string maPhieuNhap = TextBox4.Text.Trim();
                string maThuoc = ComboBox1.SelectedItem.ToString();
                DateTime ngayNhap = DateTimePicker1.Value;
                int soLuong = int.TryParse(TextBox3.Text.Trim(), out int parsedSoLuong) ? parsedSoLuong : 0;
                int donGia = int.TryParse(TextBox2.Text.Trim(), out int parsedDonGia) ? parsedDonGia : 0;

                if (string.IsNullOrEmpty(maPhieuNhap) || string.IsNullOrEmpty(maThuoc))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.");
                    return;
                }
                AddIntoSQL(maPhieuNhap, maThuoc, ngayNhap, soLuong, donGia, 0, "Chưa thanh toán");
                AddNewRow(maPhieuNhap, maThuoc, ngayNhap, soLuong, donGia, soLuong * donGia, "Chưa thanh toán");
                TextBox5.Text = TinhTongTienChuaThanhToan().ToString();
                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGrid1.SelectedRows.Count > 0)
                {
                    // Lấy chỉ mục dòng được chọn
                    int selectedIndex = DataGrid1.SelectedRows[0].Index;

                    // Lấy giá trị từ các TextBox và ComboBox
                    string maNhap = TextBox4.Text.Trim();
                    string maThuoc = ComboBox1.SelectedItem.ToString();
                    DateTime ngayNhap = DateTimePicker1.Value;
                    int soLuong = int.TryParse(TextBox3.Text.Trim(), out int parsedSoLuong) ? parsedSoLuong : 0;
                    int giaBan = int.TryParse(TextBox2.Text.Trim(), out int parsedGiaBan) ? parsedGiaBan : 0;

                    if (string.IsNullOrEmpty(maNhap) || string.IsNullOrEmpty(maThuoc))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.");
                        return;
                    }

                    // Tính toán thanh tiền
                    int thanhTien = soLuong * giaBan;

                    // Cập nhật vào cơ sở dữ liệu
                    UpdateRowInSQL(maNhap, maThuoc, ngayNhap, soLuong, giaBan, thanhTien, "Chưa thanh toán");

                    // Cập nhật lại DataGridView
                    DataTable dt = (DataTable)DataGrid1.DataSource;
                    DataRow row = dt.Rows[selectedIndex];
                    row["maThuoc"] = maThuoc;
                    row["ngayNhap"] = ngayNhap;
                    row["soLuong"] = soLuong;
                    row["giaBan"] = giaBan;
                    row["thanhTien"] = thanhTien;

                    // Cập nhật lại DataGridView
                    DataGrid1.DataSource = dt;

                    // Lưu dữ liệu vào XML
                    dt.WriteXml("ThuocNhap.xml", XmlWriteMode.WriteSchema);

                    MessageBox.Show("Cập nhật thông tin thuốc thành công!");
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

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGrid1.SelectedRows.Count > 0) // Kiểm tra nếu người dùng đã chọn một dòng
                {
                    int selectedIndex = DataGrid1.SelectedRows[0].Index;
                    string maNhap = DataGrid1.SelectedRows[0].Cells["maNhap"].Value.ToString();

                    // Xoá thông tin từ cơ sở dữ liệu
                    DeleteRowFromSQL(maNhap);

                    // Xoá thông tin khỏi DataTable
                    DataTable dt = (DataTable)DataGrid1.DataSource;
                    dt.Rows[selectedIndex].Delete();

                    // Ghi lại thông tin mới vào XML
                    dt.WriteXml("ThuocNhap.xml");

                    // Cập nhật giao diện
                    DataGrid1.DataSource = dt;

                    MessageBox.Show("Xoá thông tin thành công.");
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin cần xoá.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;"))
                {
                    con.Open();

                    // Cập nhật trạng thái các đơn hàng từ "Chưa thanh toán" sang "Đã thanh toán"
                    string query = "UPDATE ThuocNhap SET trangThai = N'Đã thanh toán' WHERE trangThai = N'Chưa thanh toán'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật trạng thái thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Không có đơn hàng nào cần cập nhật.");
                        }
                    }
                }

                // Cập nhật dữ liệu XML sau khi cập nhật trạng thái trong cơ sở dữ liệu
                SaveDataToXML();

                // Gọi lại các chức năng giao diện để làm mới thông tin
                LoadDataFromXML();
                Display2();
                TextBox5.Text = TinhTongTienChuaThanhToan().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void DataGrid1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (DataGrid1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = DataGrid1.SelectedRows[0];

                    // Lấy và gán mã thuốc
                    TextBox4.Text = selectedRow.Cells["maNhap"].Value.ToString();

                    // Gán ComboBox1.SelectedItem
                    ComboBox1.SelectedItem = selectedRow.Cells["maThuoc"].Value.ToString();

                    // Chuyển đổi và gán ngày nhập
                    DateTimePicker1.Value = DateTime.Parse(selectedRow.Cells["ngayNhap"].Value.ToString());

                    // Gán số lượng
                    TextBox3.Text = selectedRow.Cells["soLuong"].Value.ToString();

                    // Gán giá bán
                    TextBox2.Text = selectedRow.Cells["giaBan"].Value.ToString();
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Dữ liệu ngày không hợp lệ: " + fe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            XuatTatCaThuocNhap();
        }

        private void XuatTatCaThuocNhap()
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "ThuocNhap.xml"; // Đảm bảo tên file XML là chính xác

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

                // Lấy tất cả các node ThuocNhap
                XmlNodeList nodes = xmlDoc.SelectNodes("/NewDataSet/ThuocNhap");

                // Kiểm tra nếu có dữ liệu
                if (nodes.Count > 0)
                {
                    // Sử dụng StringBuilder để tạo nội dung HTML
                    StringBuilder htmlContent = new StringBuilder();

                    // Bắt đầu HTML
                    htmlContent.Append("<html><body>");
                    htmlContent.Append("<h1>Danh sách tất cả thuốc nhập</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Mã Nhập</th><th>Mã Thuốc</th><th>Ngày Nhập</th><th>Số Lượng</th><th>Giá Bán</th><th>Thành Tiền</th><th>Trạng Thái</th></tr>");

                    // Duyệt qua các node ThuocNhap và thêm vào nội dung HTML
                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maNhap")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("ngayNhap")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("soLuong")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("giaBan")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("thanhTien")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("trangThai")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("</tr>");
                    }

                    // Kết thúc bảng và HTML
                    htmlContent.Append("</table>");
                    htmlContent.Append("</body></html>");

                    // Ghi nội dung HTML ra một file tạm thời
                    string tempHtmlFile = Path.Combine(Application.StartupPath, "DanhSachThuocNhap.html");
                    File.WriteAllText(tempHtmlFile, htmlContent.ToString());

                    // Mở file HTML bằng trình duyệt mặc định
                    System.Diagnostics.Process.Start(tempHtmlFile);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu thuốc nhập trong file XML.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất danh sách thuốc nhập: {ex.Message}");
            }
        }

    }
}
