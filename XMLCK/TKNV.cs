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
    public partial class TKNV : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        private DataSet ds;
        public TKNV()
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
                    string query = "SELECT * FROM NhanVien";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu từ bảng NhanVien.");
                                return;
                            }

                            // Thiết lập tên cho DataTable nếu chưa có
                            dt.TableName = "TimKiemNV";

                            // Đảm bảo thư mục tồn tại
                            string filePath = "TimKiemNV.xml";
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

                dt.ReadXml("TimKiemNV.xml");

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

        private void TKNV_Load(object sender, EventArgs e)
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

                string maNV = TextBox1.Text.Trim();
                string tenNV = Textbox2.Text.Trim();

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

                    if (!string.IsNullOrEmpty(maNV))
                    {
                        string cellMaNV = row.Cells["maNV"].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(cellMaNV) ||
                            !cellMaNV.Equals(maNV, StringComparison.OrdinalIgnoreCase))
                        {
                            isMatch = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(tenNV))
                    {
                        string cellTenNV = row.Cells["tenNV"].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(cellTenNV) ||
                                                    !cellTenNV.StartsWith(tenNV, StringComparison.OrdinalIgnoreCase))
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
            string maNV = TextBox1.Text.Trim();
            string tenNV = Textbox2.Text.Trim();  // Assuming TextBox2 is for 'tenNV'

            // Kiểm tra nếu TextBox trống
            if (string.IsNullOrEmpty(maNV) && string.IsNullOrEmpty(tenNV))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên hoặc tên nhân viên để tìm.");
                return;
            }

            // Tìm kiếm trong XML
            TimKiemNV(maNV, tenNV);
        }

        private void TimKiemNV(string maNV, string tenNV)
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "TimKiemNV.xml"; // Đảm bảo tên file XML là chính xác

                // Tạo một đối tượng XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(Application.StartupPath, fileXML));

                // Dùng XPath để tìm các nhân viên theo mã nhân viên và tên nhân viên
                string xpathQuery = "/NewDataSet/TimKiemNV";

                // Add conditions for both maNV and tenNV
                bool hasMaNV = !string.IsNullOrEmpty(maNV);
                bool hasTenNV = !string.IsNullOrEmpty(tenNV);

                if (hasMaNV)
                {
                    xpathQuery += $"[normalize-space(maNV)='{maNV.Trim()}']";
                }

                if (hasTenNV)
                {
                    if (hasMaNV)
                    {
                        xpathQuery += " and ";
                    }
                    xpathQuery += $"[normalize-space(tenNV)='{tenNV.Trim()}']";
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
                    htmlContent.Append("<h1>Thông tin nhân viên</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Mã NV</th><th>Tên NV</th><th>Giới Tính</th><th>Công Việc</th></tr>");

                    // Duyệt qua các nhân viên tìm được và thêm vào nội dung HTML
                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("maNV")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("tenNV")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("gioiTinh")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("tenCV")?.InnerText.Trim() + "</td>");
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
                    MessageBox.Show("Không tìm thấy nhân viên phù hợp.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm thông tin nhân viên: {ex.Message}");
            }
        }

    }
}
