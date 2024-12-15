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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMLCK
{
    public partial class TKBanGiao : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        private DataSet ds;
        public TKBanGiao()
        {
            InitializeComponent();
            LoadDataFromXML();
            SaveDataToXML();
        }

        private void SaveDataToXML()
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // Updated SQL query to select from the BanGiaoThuoc table
                    string query = "SELECT * FROM BanGiaoThuoc";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu từ bảng BanGiaoThuoc.");
                                return;
                            }

                            // Thiết lập tên cho DataTable nếu chưa có
                            dt.TableName = "TKBanGiaoThuoc";

                            // Đảm bảo thư mục tồn tại
                            string filePath = "TKBanGiaoThuoc.xml";
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

                // Load data from the updated XML file "BanGiaoThuoc.xml"
                dt.ReadXml("TKBanGiaoThuoc.xml");

                // Set the DataTable as the DataSource of your DataGrid control
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

        private void TKBanGiao_Load(object sender, EventArgs e)
        {
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)DataGrid1.DataSource;

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để tìm kiếm.");
                    return;
                }

                string maBanGiao = TextBox1.Text.Trim();
                string maNhanVien = TextBox3.Text.Trim();
                string tenPhong = Textbox2.Text.Trim();  // Fixed variable name

                bool found = false;

                // Reset the background color of all rows
                foreach (DataGridViewRow row in DataGrid1.Rows)
                {
                    if (row.IsNewRow) continue;
                    row.DefaultCellStyle.BackColor = SystemColors.Window;
                }

                // Now filter rows based on input criteria
                foreach (DataGridViewRow row in DataGrid1.Rows)
                {
                    if (row.IsNewRow) continue;

                    bool isMatch = true;

                    // Match maThuoc (if provided)
                    if (!string.IsNullOrEmpty(maBanGiao))
                    {
                        string cellMaThuoc = row.Cells["maBG"]?.Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(cellMaThuoc) || !cellMaThuoc.Equals(maBanGiao, StringComparison.OrdinalIgnoreCase))
                        {
                            isMatch = false;
                        }
                    }

                    // Match maNhanVien (if provided)
                    if (!string.IsNullOrEmpty(maNhanVien))
                    {
                        string cellMaNhanVien = row.Cells["maNV"]?.Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(cellMaNhanVien) || !cellMaNhanVien.Equals(maNhanVien, StringComparison.OrdinalIgnoreCase))
                        {
                            isMatch = false;
                        }
                    }

                    // Match tenPhong (if provided)
                    if (!string.IsNullOrEmpty(tenPhong))
                    {
                        string cellPhong = row.Cells["phong"]?.Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(cellPhong) || !cellPhong.StartsWith(tenPhong, StringComparison.OrdinalIgnoreCase))
                        {
                            isMatch = false;
                        }
                    }

                    // If the row matches, highlight it
                    if (isMatch)
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        if (!found)
                        {
                            // Scroll to the first matched row
                            DataGrid1.FirstDisplayedScrollingRowIndex = row.Index;
                        }
                        found = true;
                    }
                }

                // If no match found, show a message
                if (!found)
                {
                    MessageBox.Show("Không tìm thấy thuốc phù hợp.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Lấy số bàn từ TextBox
            string maBG = TextBox1.Text.Trim();
            string maNV = TextBox3.Text.Trim();
            string tenPhong = Textbox2.Text.Trim();  // TextBox for "tenPhong"

            // Kiểm tra nếu TextBox trống
            if (string.IsNullOrEmpty(maBG) && string.IsNullOrEmpty(maNV) && string.IsNullOrEmpty(tenPhong))
            {
                MessageBox.Show("Vui lòng nhập mã bàn giao, mã nhân viên hoặc phòng để tìm.");
                return;
            }

            // Tìm kiếm trong XML
            TimKiemMaBanGiao(maBG, maNV, tenPhong);
        }

        private void TimKiemMaBanGiao(string maBG, string maNV, string tenPhong)
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "TKBanGiaoThuoc.xml"; // Đảm bảo đường dẫn chính xác đến file XML

                // Tạo một đối tượng XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(Application.StartupPath, fileXML));

                // Dùng XPath để tìm các bản ghi BanGiaoThuoc thuộc các tiêu chí nhập vào
                string xpathQuery = "/NewDataSet/TKBanGiaoThuoc";

                bool hasMaBG = !string.IsNullOrEmpty(maBG);
                bool hasMaNV = !string.IsNullOrEmpty(maNV);
                bool hasPhong = !string.IsNullOrEmpty(tenPhong);

                // Build the XPath query based on the input criteria
                if (hasMaBG)
                {
                    xpathQuery += $"[normalize-space(maBG)='{maBG.Trim()}']";
                }

                if (hasMaNV)
                {
                    if (hasMaBG) xpathQuery += " and ";
                    xpathQuery += $"[normalize-space(maNV)='{maNV.Trim()}']";
                }

                if (hasPhong)
                {
                    if (hasMaBG || hasMaNV) xpathQuery += " and ";
                    xpathQuery += $"[normalize-space(phong)='{tenPhong.Trim()}']";
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
                    htmlContent.Append("<h1>Danh sách thông tin bàn giao</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Mã BG</th><th>Mã Nhân Viên</th><th>Mã Thuốc</th><th>Phòng</th><th>Ngày Bàn Giao</th></tr>");

                    // Duyệt qua các bản ghi tìm được và thêm vào nội dung HTML
                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maBG")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maNV")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("phong")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("ngayBanGiao")?.InnerText + "</td>");
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
                    MessageBox.Show("Không tìm thấy thông tin bàn giao với các tiêu chí này.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm thông tin bàn giao: {ex.Message}");
            }
        }


    }
}
