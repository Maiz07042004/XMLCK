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
    public partial class ThongKeSoLuongTheoNhomThuoc : Form
    {
        private DataSet ds;


        public ThongKeSoLuongTheoNhomThuoc()
        {
            InitializeComponent();
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

        private void LoadDataFromDatabase()
        {
            try
            {
                // Kết nối với cơ sở dữ liệu
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;"))
                {
                    con.Open();

                    // Câu truy vấn SQL để thống kê số lượng thuốc theo nhóm
                    string query = "SELECT tenNhomThuoc, COUNT(maThuoc) AS SoLuong FROM NhomThuoc LEFT JOIN ThongTinThuoc ON NhomThuoc.maNhomThuoc = ThongTinThuoc.maNhomThuoc GROUP BY tenNhomThuoc;";

                    // Tạo SqlDataAdapter
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                    // Tạo DataSet để lưu kết quả
                    ds = new DataSet();
                    adapter.Fill(ds, "ThongKeThuocTheoNhom");

                    // Đưa dữ liệu vào DataGridView
                    DataGridView1.DataSource = ds.Tables["ThongKeThuocTheoNhom"];

                    // Lưu dữ liệu vào XML
                    ds.WriteXml("ThongKeThuocTheoNhom.xml", XmlWriteMode.WriteSchema);

                    MessageBox.Show("Dữ liệu đã được lưu vào file XML thành công.");
                }

                // Vẽ biểu đồ
                DrawChart();
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void LoadDataFromXML()
        {
            try
            {
                // Tải dữ liệu từ file XML
                ds = new DataSet();
                ds.ReadXml("ThongKeThuocTheoNhom.xml");

                // Đưa dữ liệu vào DataGridView
                DataGridView1.DataSource = ds.Tables["ThongKeThuocTheoNhom"];

                // Vẽ biểu đồ
                DrawChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ XML: " + ex.Message);
            }
        }

        private void SaveDataToXML()
        {
            try
            {
                if (ds != null)
                {
                    // Lưu dữ liệu vào file XML
                    ds.WriteXml("ThongKeThuocTheoNhom.xml", XmlWriteMode.WriteSchema);
                    MessageBox.Show("Dữ liệu đã được lưu vào file XML thành công.");
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để lưu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu vào XML: " + ex.Message);
            }
        }

        private void DrawChart()
        {
            try
            {
                // Xóa các series hiện tại trong biểu đồ để tránh trùng lặp
                Chart1.Series.Clear();

                // Tạo series mới
                Series series = new Series
                {
                    ChartType = SeriesChartType.Column,
                    Name = "Thống kê thuốc theo nhóm"
                };

                // Thêm dữ liệu vào series từ DataTable
                foreach (DataRow row in ds.Tables["ThongKeThuocTheoNhom"].Rows)
                {
                    string tenNhomThuoc = row["tenNhomThuoc"].ToString();
                    int count = Convert.ToInt32(row["SoLuong"]);
                    series.Points.AddXY(tenNhomThuoc, count);
                }

                // Thêm series vào biểu đồ
                Chart1.Series.Add(series);

                // Đặt tiêu đề cho trục X và Y
                Chart1.ChartAreas[0].AxisX.Title = "Tên nhóm thuốc";
                Chart1.ChartAreas[0].AxisY.Title = "Số lượng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi vẽ biểu đồ: " + ex.Message);
            }
        }


        private void ThongKeSoLuongTheoNhomThuoc_Load(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu file XML tồn tại thì tải dữ liệu từ XML, ngược lại tải từ database
                if (System.IO.File.Exists("ThongKeThuocTheoNhom.xml"))
                {
                    LoadDataFromXML();
                }
                else
                {
                    LoadDataFromDatabase();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XuatThongKeThuocTheoNhom();
        }

        private void XuatThongKeThuocTheoNhom()
        {
            try
            {
                // Đường dẫn tới file XML
                string fileXML = "ThongKeThuocTheoNhom.xml"; // Đảm bảo tên file XML là chính xác

                // Tạo một đối tượng XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(Application.StartupPath, fileXML));

                // Lấy tất cả các node ThongKeThuocTheoNhom
                XmlNodeList nodes = xmlDoc.SelectNodes("/NewDataSet/ThongKeThuocTheoNhom");

                // Kiểm tra nếu có dữ liệu
                if (nodes.Count > 0)
                {
                    // Sử dụng StringBuilder để tạo nội dung HTML
                    StringBuilder htmlContent = new StringBuilder();

                    // Bắt đầu HTML
                    htmlContent.Append("<html><body>");
                    htmlContent.Append("<h1>Danh sách thống kê thuốc theo nhóm</h1>");
                    htmlContent.Append("<table border='1'><tr><th>Tên Nhóm Thuốc</th><th>Số Lượng</th></tr>");

                    // Duyệt qua các node ThongKeThuocTheoNhom và thêm vào nội dung HTML
                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("tenNhomThuoc")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("<td>" + node.SelectSingleNode("SoLuong")?.InnerText.Trim() + "</td>");
                        htmlContent.Append("</tr>");
                    }

                    // Kết thúc bảng và HTML
                    htmlContent.Append("</table>");
                    htmlContent.Append("</body></html>");

                    // Ghi nội dung HTML ra một file tạm thời
                    string tempHtmlFile = Path.Combine(Application.StartupPath, "ThongKeThuocTheoNhom.html");
                    File.WriteAllText(tempHtmlFile, htmlContent.ToString());

                    // Mở file HTML bằng trình duyệt mặc định
                    System.Diagnostics.Process.Start(tempHtmlFile);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu thuốc theo nhóm trong file XML.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất thống kê thuốc theo nhóm: {ex.Message}");
            }
        }

    }
}
