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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace XMLCK
{
    public partial class ThongKeTienNhapThuocTheoThang : Form
    {
        private DataSet ds = new DataSet();
        private readonly string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
        private readonly string xmlFilePath = "ThongKeTienNhapThuocTheoThang.xml";
        public ThongKeTienNhapThuocTheoThang()
        {
            InitializeComponent();
            SaveDataToXML();
            LoadData();
        }

        

        private void SaveDataToXML()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"SELECT MONTH(ngayNhap) AS Thang, YEAR(ngayNhap) AS Nam, SUM(thanhTien) AS TongTienNhap
                             FROM ThuocNhap
                             GROUP BY MONTH(ngayNhap), YEAR(ngayNhap)
                             ORDER BY YEAR(ngayNhap), MONTH(ngayNhap);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu trong cơ sở dữ liệu.");
                                return;
                            }

                            dt.TableName = "ThongKeTienNhapThuocTheoThang";
                            dt.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);
                            MessageBox.Show($"Dữ liệu đã lưu thành công vào: {xmlFilePath}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu vào XML: {ex.Message}");
            }
        }
        private void LoadData()
        {
            try
            {
                if (!File.Exists(xmlFilePath))
                {
                    MessageBox.Show("File XML không tồn tại. Vui lòng lưu dữ liệu trước khi thực hiện thao tác.");
                    return;
                }

                DataSet ds = new DataSet();
                ds.ReadXml(xmlFilePath);

                if (ds.Tables.Contains("ThongKeTienNhapThuocTheoThang"))
                {
                    DataTable dt = ds.Tables["ThongKeTienNhapThuocTheoThang"];
                    DataGridView1.DataSource = dt; // Hiển thị dữ liệu trong DataGridView
                    DrawChart(dt);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu trong XML.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc dữ liệu từ XML: {ex.Message}");
            }
        }


        private void DrawChart(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị biểu đồ.");
                    return;
                }

                // Clear biểu đồ trước khi vẽ mới
                Chart1.Series.Clear();

                // Tạo Series mới
                Series series = new Series
                {
                    Name = "ThongKeTienNhap",
                    ChartType = SeriesChartType.Column
                };

                foreach (DataRow row in dt.Rows)
                {
                    string monthYearLabel = $"{row["Thang"]}/{row["Nam"]}";
                    double totalMoney = Convert.ToDouble(row["TongTienNhap"]);
                    series.Points.AddXY(monthYearLabel, totalMoney);
                }

                Chart1.Series.Add(series);
                Chart1.ChartAreas[0].AxisX.Title = "Tháng/Năm";
                Chart1.ChartAreas[0].AxisY.Title = "Tổng Tiền Nhập";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi vẽ biểu đồ: {ex.Message}");
            }
        }

        private void ThongKeTienNhapThuocTheoThang_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            XuatThongKeTienNhapThuocTheoThang();
        }

        private void XuatThongKeTienNhapThuocTheoThang()
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "ThongKeTienNhapThuocTheoThang.xml"; // Đảm bảo tên file XML là chính xác

                // Tạo một đối tượng XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(Application.StartupPath, fileXML));

                // Lấy tất cả các node ThongKeTienNhapThuocTheoThang
                XmlNodeList nodes = xmlDoc.SelectNodes("/NewDataSet/ThongKeTienNhapThuocTheoThang");

                // Kiểm tra nếu có dữ liệu
                if (nodes.Count > 0)
                {
                    // Sử dụng StringBuilder để tạo nội dung HTML
                    StringBuilder htmlContent = new StringBuilder();

                    // Bắt đầu HTML
                    htmlContent.Append("<html><body>");
                    htmlContent.Append("<h1>Thống kê tiền nhập thuốc theo tháng</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Tháng</th><th>Năm</th><th>Tổng Tiền Nhập</th></tr>");

                    // Duyệt qua các node ThongKeTienNhapThuocTheoThang và thêm vào nội dung HTML
                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("Thang")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("Nam")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("TongTienNhap")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("</tr>");
                    }

                    // Kết thúc bảng và HTML
                    htmlContent.Append("</table>");
                    htmlContent.Append("</body></html>");

                    // Ghi nội dung HTML ra một file tạm thời
                    string tempHtmlFile = Path.Combine(Application.StartupPath, "ThongKeTienNhapThuocTheoThang.html");
                    File.WriteAllText(tempHtmlFile, htmlContent.ToString());

                    // Mở file HTML bằng trình duyệt mặc định
                    System.Diagnostics.Process.Start(tempHtmlFile);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu thống kê tiền nhập thuốc theo tháng trong file XML.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất thống kê tiền nhập thuốc theo tháng: {ex.Message}");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SaveDataToXML();
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                QuanLiThuoc quanLiThuocForm = new QuanLiThuoc();
                quanLiThuocForm.Show();
                this.Hide();
            }
        }
    }
}
