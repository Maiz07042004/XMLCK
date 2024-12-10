using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace XMLCK
{
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
            SaveDataToXML();
            LoadDataFromXML();
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

                    string query = "SELECT * FROM NhanVien";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dt.TableName = "NhanVien"; // Đặt tên cho bảng

                            if (dt.Rows.Count > 0)
                            {
                                string filePath = "NhanVien.xml";
                                dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
                            }
                            else
                            {
                                MessageBox.Show("Không có dữ liệu từ bảng NhanVien.");
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

                dt.ReadXml("NhanVien.xml");

                TableNV.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void AddIntoSQL(string maNV, string tenNV, string gioiTinh, string tenCV)
        {
            string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = "INSERT INTO NhanVien (maNV, tenNV, gioiTinh, tenCV) VALUES (@maNV, @tenNV, @gioiTinh, @tenCV)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số với giá trị tương ứng
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    cmd.Parameters.AddWithValue("@tenNV", tenNV);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@tenCV", tenCV);

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

        private void AddNewRow(string maNV, string tenNV, string gioiTinh, string tenCV)
        {
            // Lấy DataTable từ DataSource của TableNV
            DataTable dt = (DataTable)TableNV.DataSource;

            if (dt != null)
            {
                // Tạo một hàng mới và gán giá trị
                DataRow newRow = dt.NewRow();
                newRow["maNV"] = maNV;
                newRow["tenNV"] = tenNV;
                newRow["gioiTinh"] = gioiTinh;
                newRow["tenCV"] = tenCV;

                // Thêm hàng mới vào DataTable
                dt.Rows.Add(newRow);

                // Cập nhật lại DataSource cho DataGridView
                TableNV.DataSource = dt;

                // Lưu DataTable vào XML
                dt.WriteXml("NhanVien.xml");

                
            }
            else
            {
                MessageBox.Show("Không thể thêm dữ liệu vì DataTable chưa được khởi tạo.");
            }
        }

        private void UpdateRowInSQL(string maNV, string tenNV, string gioiTinh, string tenCV)
        {
            try
            {
                string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    string query = "UPDATE NhanVien SET tenNV = @tenNV, gioiTinh = @gioiTinh, tenCV = @tenCV WHERE maNV = @maNV";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@maNV", maNV);
                        cmd.Parameters.AddWithValue("@tenNV", tenNV);
                        cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                        cmd.Parameters.AddWithValue("@tenCV", tenCV);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin nhân viên thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên để cập nhật.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }





        private void NhanVien_Load(object sender, EventArgs e)
        {

        }

        private void RdbuttonNam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TableNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void TableNV_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (TableNV.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = TableNV.SelectedRows[0];

                    MaNV.Text = selectedRow.Cells["maNV"].Value?.ToString() ?? "";
                    TenNV.Text = selectedRow.Cells["tenNV"].Value?.ToString() ?? "";
                    RadioButton1.Checked = selectedRow.Cells["gioiTinh"].Value?.ToString() == "Nam";
                    RadioButton2.Checked = selectedRow.Cells["gioiTinh"].Value?.ToString() == "Nữ";
                    RadioButton3.Checked = selectedRow.Cells["tenCV"].Value?.ToString() == "Nhân viên Dược";
                    RadioButton4.Checked = selectedRow.Cells["tenCV"].Value?.ToString() == "Nhân viên kho thuốc";
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
                string maNV = MaNV.Text;
                string tenNV = TenNV.Text;
                string gioiTinh = RadioButton1.Checked ? "Nam" : "Nữ";
                string tenCV = RadioButton3.Checked ? "Nhân viên Dược" : "Nhân viên kho thuốc";

                // Gọi hàm thêm vào DataGridView và SQL
                AddNewRow(maNV, tenNV, gioiTinh, tenCV);
                AddIntoSQL(maNV, tenNV, gioiTinh, tenCV);

                // Hiển thị thông báo thành công
                MessageBox.Show("Thêm nhân viên thành công!");
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (TableNV.SelectedRows.Count > 0)
                {
                    // Lấy chỉ số dòng được chọn
                    int selectedIndex = TableNV.SelectedRows[0].Index;

                    // Lấy mã nhân viên từ ô nhập
                    string maNV = MaNV.Text;

                    // Lấy DataTable từ DataSource
                    DataTable dt = (DataTable)TableNV.DataSource;

                    // Xóa dòng từ DataTable
                    dt.Rows[selectedIndex].Delete();

                    // Lưu lại DataTable vào file XML
                    dt.WriteXml("NhanVien.xml");

                    // Cập nhật DataGridView
                    TableNV.DataSource = dt;

                    // Xóa nhân viên khỏi cơ sở dữ liệu
                    string connString = "Data Source=LAPTOP-K1LPNPC4;Initial Catalog=qlThuocBenhVien;Integrated Security=True;";
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();

                        string query = "DELETE FROM NhanVien WHERE maNV = @maNV";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@maNV", maNV);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa nhân viên thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy nhân viên để xóa trong cơ sở dữ liệu.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
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
                if (TableNV.SelectedRows.Count > 0)
                {
                    int selectedIndex = TableNV.SelectedRows[0].Index;

                    // Lấy dữ liệu từ các TextBox
                    string maNV = MaNV.Text;
                    string tenNV = TenNV.Text;
                    string gioiTinh = RadioButton1.Checked ? "Nam" : "Nữ";
                    string tenCV = RadioButton3.Checked ? "Nhân viên Dược" : "Nhân viên kho thuốc";

                    // Lấy DataTable từ DataSource
                    DataTable dt = (DataTable)TableNV.DataSource;

                    // Cập nhật dữ liệu trong hàng được chọn
                    DataRow row = dt.Rows[selectedIndex];
                    row["maNV"] = maNV;
                    row["tenNV"] = tenNV;
                    row["gioiTinh"] = gioiTinh;
                    row["tenCV"] = tenCV;

                    // Ghi dữ liệu mới vào file XML
                    dt.WriteXml("NhanVien.xml");

                    // Cập nhật lại DataSource
                    TableNV.DataSource = dt;

                    // Gọi hàm cập nhật vào cơ sở dữ liệu
                    UpdateRowInSQL(maNV, tenNV, gioiTinh, tenCV);

                    MessageBox.Show("Sửa thông tin nhân viên thành công!");
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
