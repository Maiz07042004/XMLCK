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
    public partial class TKThuocNhap : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        private DataSet ds;
        public TKThuocNhap()
        {
            InitializeComponent();
            SaveDataToXML();
            LoadDataFromXML();
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
                            dt.TableName = "TimKiemThuocNhap";

                            // Đảm bảo thư mục tồn tại
                            string filePath = "TimKiemThuocNhap.xml";
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

        private void LoadDataFromXML()
        {
            try
            {
                DataTable dt = new DataTable();

                dt.ReadXml("TimKiemThuocNhap.xml");

                DataGrid1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                QuanLiThuoc quanLiThuocForm = new QuanLiThuoc();
                quanLiThuocForm.Show();
                this.Hide();
            }
        }

        private void TKThuocNhap_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Lấy DataSource từ DataGridView
                    DataTable dt = (DataTable)DataGrid1.DataSource;

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để tìm kiếm.");
                        return;
                    }

                    string maNhap = TextBox1.Text.Trim();
                    string maThuoc = TextBox2.Text.Trim();
                    

                    bool found = false;

                    // Reset màu nền của các hàng
                    foreach (DataGridViewRow row in DataGrid1.Rows)
                    {
                        if (row.IsNewRow) continue;
                        row.DefaultCellStyle.BackColor = SystemColors.Window;
                    }

                    foreach (DataGridViewRow row in DataGrid1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        bool isMatch = true;

                        // Kiểm tra mã nhập
                        if (!string.IsNullOrEmpty(maNhap))
                        {
                            string cellMaNhap = row.Cells["manhap"].Value?.ToString().Trim();
                            if (string.IsNullOrEmpty(cellMaNhap) ||
                                !cellMaNhap.Equals(maNhap, StringComparison.OrdinalIgnoreCase))
                            {
                                isMatch = false;
                            }
                        }

                        // Kiểm tra mã thuốc
                        if (!string.IsNullOrEmpty(maThuoc))
                        {
                            string cellMaThuoc = row.Cells["mathuoc"].Value?.ToString().Trim();
                            if (string.IsNullOrEmpty(cellMaThuoc) ||
                                !cellMaThuoc.Equals(maThuoc, StringComparison.OrdinalIgnoreCase))
                            {
                                isMatch = false;
                            }
                        }

                        // Kiểm tra tháng nhập
                       

                        // Nếu tất cả điều kiện thỏa mãn, đổi màu hàng
                        if (isMatch)
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            if (!found)
                            {
                                DataGrid1.FirstDisplayedScrollingRowIndex = row.Index;
                            }
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        MessageBox.Show("Không tìm thấy kết quả phù hợp.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Lấy số bàn từ TextBox
            string maNhap = TextBox1.Text.Trim();
            string maThuoc = TextBox2.Text.Trim();
            

            // Kiểm tra nếu TextBox trống
            if (string.IsNullOrEmpty(maNhap) && string.IsNullOrEmpty(maThuoc) )
            {
                MessageBox.Show("Vui lòng nhập mã nhập, mã thuốc hoặc tháng nhập để tìm.");
                return;
            }

            // Tìm kiếm trong XML
            TimKiemThuocNhap(maNhap, maThuoc);
        }

        private void TimKiemThuocNhap(string maNhap, string maThuoc)
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "TimKiemThuocNhap.xml"; // Đảm bảo tên file XML là chính xác

                // Tạo một đối tượng XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(Application.StartupPath, fileXML));

                // Dùng XPath để tìm các thuốc nhập theo các tiêu chí
                string xpathQuery = "/NewDataSet/TimKiemThuocNhap";

                bool hasMaNhap = !string.IsNullOrEmpty(maNhap);
                bool hasMaThuoc = !string.IsNullOrEmpty(maThuoc);
                

                // Xây dựng câu truy vấn XPath theo các tiêu chí
                if (hasMaNhap)
                {
                    xpathQuery += $"[normalize-space(maNhap)='{maNhap.Trim()}']";
                }

                if (hasMaThuoc)
                {
                    if (hasMaNhap) xpathQuery += " and ";
                    xpathQuery += $"[normalize-space(maThuoc)='{maThuoc.Trim()}']";
                }

                

                // Thực hiện tìm kiếm theo XPath
                XmlNodeList nodes = xmlDoc.SelectNodes(xpathQuery);

                // Kiểm tra nếu có kết quả tìm thấy
                if (nodes.Count > 0)
                {
                    // Sử dụng StringBuilder để tạo nội dung HTML
                    StringBuilder htmlContent = new StringBuilder();

                    // Bắt đầu HTML
                    htmlContent.Append("<html><body>");
                    htmlContent.Append("<h1>Thông tin thuốc nhập</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Mã Nhập</th><th>Mã Thuốc</th><th>Ngày Nhập</th><th>Số Lượng</th><th>Giá Bán</th><th>Thành Tiền</th><th>Trạng Thái</th></tr>");

                    // Duyệt qua các thuốc nhập tìm được và thêm vào nội dung HTML
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
                    string tempHtmlFile = Path.Combine(Application.StartupPath, "temp_result.html");
                    File.WriteAllText(tempHtmlFile, htmlContent.ToString());

                    // Mở file HTML bằng trình duyệt mặc định
                    System.Diagnostics.Process.Start(tempHtmlFile);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thuốc với các tiêu chí này.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm thông tin thuốc: {ex.Message}");
            }
        }



    }
}
