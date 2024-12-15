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
    public partial class TKThuoc : Form
    {
        public TKThuoc()
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
                    string query = "SELECT * FROM ThongTinThuoc";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu từ bảng ThongTinThuoc.");
                                return;
                            }

                            // Thiết lập tên cho DataTable nếu chưa có
                            dt.TableName = "TimKiemThuoc";

                            // Đảm bảo thư mục tồn tại
                            string filePath = "TimKiemThuoc.xml";
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

                dt.ReadXml("TimKiemThuoc.xml");

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

        private void TKThuoc_Load(object sender, EventArgs e)
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

                string maThuoc = TextBox1.Text.Trim();
                string tenThuoc = Textbox2.Text.Trim();

                bool found = false;

                foreach (DataGridViewRow row in DataGrid1.Rows)
                {
                    if (row.IsNewRow) continue;
                    row.DefaultCellStyle.BackColor = SystemColors.Window;
                }

                foreach (DataGridViewRow row in DataGrid1.Rows)
                {
                    if (row.IsNewRow) continue;

                    bool isMatch = true;

                    if (!string.IsNullOrEmpty(maThuoc))
                    {
                        string cellMaThuoc = row.Cells["maThuoc"].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(cellMaThuoc) ||
                            !cellMaThuoc.Equals(maThuoc, StringComparison.OrdinalIgnoreCase))
                        {
                            isMatch = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(tenThuoc))
                    {
                        string cellTenThuoc = row.Cells["tenThuoc"].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(cellTenThuoc) ||
                            !cellTenThuoc.StartsWith(tenThuoc, StringComparison.OrdinalIgnoreCase))
                        {
                            isMatch = false;
                        }
                    }

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
                    MessageBox.Show("Không tìm thấy thuốc phù hợp.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Lấy số bàn từ TextBox
            string maThuoc = TextBox1.Text.Trim();
            string tenThuoc = Textbox2.Text.Trim();  // Assuming TextBox2 is for 'tenThuoc'

            // Kiểm tra nếu TextBox trống
            if (string.IsNullOrEmpty(maThuoc) && string.IsNullOrEmpty(tenThuoc))
            {
                MessageBox.Show("Vui lòng nhập mã thuốc hoặc tên thuốc để tìm.");
                return;
            }

            // Tìm kiếm trong XML
            TimKiemThuoc(maThuoc, tenThuoc);
        }

        private void TimKiemThuoc(string maThuoc, string tenThuoc)
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "TimKiemThuoc.xml"; // Sửa lại đúng tên file XML của bạn

                // Tạo một đối tượng XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(Application.StartupPath, fileXML));

                // Dùng XPath để tìm các thuốc theo mã thuốc và tên thuốc
                string xpathQuery = "/NewDataSet/TimKiemThuoc";

                // Add conditions for both maThuoc and tenThuoc
                bool hasMaThuoc = !string.IsNullOrEmpty(maThuoc);
                bool hasTenThuoc = !string.IsNullOrEmpty(tenThuoc);

                if (hasMaThuoc)
                {
                    xpathQuery += $"[normalize-space(maThuoc)='{maThuoc.Trim()}']";
                }

                if (hasTenThuoc)
                {
                    if (hasMaThuoc)
                    {
                        xpathQuery += " and ";
                    }
                    xpathQuery += $"[normalize-space(tenThuoc)='{tenThuoc.Trim()}']";
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
                    htmlContent.Append("<h1>Danh sách thuốc</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Mã Thuốc</th><th>Tên Thuốc</th><th>Ngày Sản Xuất</th><th>Ngày Hết Hạn</th><th>Công Dụng</th><th>Giá</th></tr>");

                    // Duyệt qua các thuốc tìm được và thêm vào nội dung HTML
                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("tenThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("ngaySX")?.InnerText + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("ngayHH")?.InnerText + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("congDung")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("gia")?.InnerText + "</td>");
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
                    MessageBox.Show("Không tìm thấy thuốc phù hợp.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm thông tin thuốc: {ex.Message}");
            }
        }
    }
    }
