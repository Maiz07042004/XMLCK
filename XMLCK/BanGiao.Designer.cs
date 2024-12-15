namespace XMLCK
{
    partial class BanGiao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu5 = new System.Windows.Forms.ToolStripMenuItem();
            this.TìmKiếmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableBGT = new System.Windows.Forms.DataGridView();
            this.Sua = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.Xoa = new System.Windows.Forms.Button();
            this.Them = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.MaThuoc = new System.Windows.Forms.ComboBox();
            this.MaNV = new System.Windows.Forms.ComboBox();
            this.NgayBanGiao = new System.Windows.Forms.DateTimePicker();
            this.Phong = new System.Windows.Forms.TextBox();
            this.MaBG = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.MenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableBGT)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu1,
            this.menu2,
            this.menu3,
            this.menu4,
            this.menu5,
            this.TìmKiếmToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MenuStrip1.Size = new System.Drawing.Size(1155, 31);
            this.MenuStrip1.TabIndex = 61;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // menu1
            // 
            this.menu1.BackColor = System.Drawing.SystemColors.Control;
            this.menu1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(184, 27);
            this.menu1.Text = "Thông tin nhân viên";
            // 
            // menu2
            // 
            this.menu2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu2.Name = "menu2";
            this.menu2.Size = new System.Drawing.Size(152, 27);
            this.menu2.Text = "Thông tin thuốc";
            // 
            // menu3
            // 
            this.menu3.BackColor = System.Drawing.Color.White;
            this.menu3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu3.Name = "menu3";
            this.menu3.Size = new System.Drawing.Size(185, 27);
            this.menu3.Text = "Nhập thuốc vào kho";
            // 
            // menu4
            // 
            this.menu4.BackColor = System.Drawing.Color.PaleTurquoise;
            this.menu4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu4.Name = "menu4";
            this.menu4.Size = new System.Drawing.Size(144, 27);
            this.menu4.Text = "Bàn giao thuốc";
            // 
            // menu5
            // 
            this.menu5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu5.Name = "menu5";
            this.menu5.Size = new System.Drawing.Size(99, 27);
            this.menu5.Text = "Thống kê";
            // 
            // TìmKiếmToolStripMenuItem
            // 
            this.TìmKiếmToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TìmKiếmToolStripMenuItem.Name = "TìmKiếmToolStripMenuItem";
            this.TìmKiếmToolStripMenuItem.Size = new System.Drawing.Size(100, 27);
            this.TìmKiếmToolStripMenuItem.Text = "Tìm kiếm";
            // 
            // TableBGT
            // 
            this.TableBGT.AllowUserToAddRows = false;
            this.TableBGT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TableBGT.BackgroundColor = System.Drawing.Color.LightCyan;
            this.TableBGT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableBGT.Location = new System.Drawing.Point(143, 444);
            this.TableBGT.Name = "TableBGT";
            this.TableBGT.RowHeadersWidth = 51;
            this.TableBGT.RowTemplate.Height = 24;
            this.TableBGT.Size = new System.Drawing.Size(699, 158);
            this.TableBGT.TabIndex = 60;
            this.TableBGT.SelectionChanged += new System.EventHandler(this.TableBGT_SelectionChanged);
            // 
            // Sua
            // 
            this.Sua.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Sua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sua.ForeColor = System.Drawing.Color.Snow;
            this.Sua.Location = new System.Drawing.Point(498, 374);
            this.Sua.Name = "Sua";
            this.Sua.Size = new System.Drawing.Size(118, 42);
            this.Sua.TabIndex = 59;
            this.Sua.Text = "Sửa";
            this.Sua.UseVisualStyleBackColor = false;
            this.Sua.Click += new System.EventHandler(this.Sua_Click);
            // 
            // Button5
            // 
            this.Button5.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button5.ForeColor = System.Drawing.Color.Snow;
            this.Button5.Location = new System.Drawing.Point(858, 374);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(114, 42);
            this.Button5.TabIndex = 56;
            this.Button5.Text = "Thoát";
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Xoa
            // 
            this.Xoa.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Xoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Xoa.ForeColor = System.Drawing.Color.Snow;
            this.Xoa.Location = new System.Drawing.Point(329, 374);
            this.Xoa.Name = "Xoa";
            this.Xoa.Size = new System.Drawing.Size(118, 42);
            this.Xoa.TabIndex = 57;
            this.Xoa.Text = "Xoá";
            this.Xoa.UseVisualStyleBackColor = false;
            this.Xoa.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Them
            // 
            this.Them.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Them.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Them.ForeColor = System.Drawing.Color.Snow;
            this.Them.Location = new System.Drawing.Point(180, 374);
            this.Them.Name = "Them";
            this.Them.Size = new System.Drawing.Size(107, 42);
            this.Them.TabIndex = 58;
            this.Them.Text = "Thêm";
            this.Them.UseVisualStyleBackColor = false;
            this.Them.Click += new System.EventHandler(this.Them_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.LightCyan;
            this.GroupBox1.Controls.Add(this.MaThuoc);
            this.GroupBox1.Controls.Add(this.MaNV);
            this.GroupBox1.Controls.Add(this.NgayBanGiao);
            this.GroupBox1.Controls.Add(this.Phong);
            this.GroupBox1.Controls.Add(this.MaBG);
            this.GroupBox1.Controls.Add(this.Label7);
            this.GroupBox1.Controls.Add(this.Label11);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupBox1.Location = new System.Drawing.Point(143, 155);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.GroupBox1.Size = new System.Drawing.Size(699, 178);
            this.GroupBox1.TabIndex = 55;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Thông tin thuốc";
            // 
            // MaThuoc
            // 
            this.MaThuoc.FormattingEnabled = true;
            this.MaThuoc.Location = new System.Drawing.Point(128, 136);
            this.MaThuoc.Name = "MaThuoc";
            this.MaThuoc.Size = new System.Drawing.Size(142, 24);
            this.MaThuoc.TabIndex = 55;
            // 
            // MaNV
            // 
            this.MaNV.FormattingEnabled = true;
            this.MaNV.Location = new System.Drawing.Point(128, 87);
            this.MaNV.Name = "MaNV";
            this.MaNV.Size = new System.Drawing.Size(142, 24);
            this.MaNV.TabIndex = 54;
            // 
            // NgayBanGiao
            // 
            this.NgayBanGiao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NgayBanGiao.Location = new System.Drawing.Point(415, 87);
            this.NgayBanGiao.Name = "NgayBanGiao";
            this.NgayBanGiao.Size = new System.Drawing.Size(147, 22);
            this.NgayBanGiao.TabIndex = 7;
            // 
            // Phong
            // 
            this.Phong.Location = new System.Drawing.Point(415, 46);
            this.Phong.Name = "Phong";
            this.Phong.Size = new System.Drawing.Size(147, 22);
            this.Phong.TabIndex = 4;
            // 
            // MaBG
            // 
            this.MaBG.Location = new System.Drawing.Point(128, 43);
            this.MaBG.Name = "MaBG";
            this.MaBG.Size = new System.Drawing.Size(142, 22);
            this.MaBG.TabIndex = 4;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(303, 90);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(111, 29);
            this.Label7.TabIndex = 2;
            this.Label7.Text = "Ngày bàn giao";
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(303, 43);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(93, 29);
            this.Label11.TabIndex = 2;
            this.Label11.Text = "Phòng";
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(19, 136);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(125, 29);
            this.Label6.TabIndex = 2;
            this.Label6.Text = "Mã thuốc";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(21, 46);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(125, 29);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Mã bàn giao";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(21, 90);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(125, 29);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Mã nhân viên";
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Label3.Location = new System.Drawing.Point(282, 96);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(462, 34);
            this.Label3.TabIndex = 54;
            this.Label3.Text = "BÀN GIAO THUỐC CHO CÁC PHÒNG";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Snow;
            this.button1.Location = new System.Drawing.Point(681, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 42);
            this.button1.TabIndex = 62;
            this.button1.Text = "Xuất XML";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BanGiao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1155, 613);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MenuStrip1);
            this.Controls.Add(this.TableBGT);
            this.Controls.Add(this.Sua);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Xoa);
            this.Controls.Add(this.Them);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label3);
            this.Name = "BanGiao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.BanGiao_Load);
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableBGT)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem menu1;
        internal System.Windows.Forms.ToolStripMenuItem menu2;
        internal System.Windows.Forms.ToolStripMenuItem menu3;
        internal System.Windows.Forms.ToolStripMenuItem menu4;
        internal System.Windows.Forms.ToolStripMenuItem menu5;
        internal System.Windows.Forms.ToolStripMenuItem TìmKiếmToolStripMenuItem;
        internal System.Windows.Forms.DataGridView TableBGT;
        internal System.Windows.Forms.Button Sua;
        internal System.Windows.Forms.Button Button5;
        internal System.Windows.Forms.Button Xoa;
        internal System.Windows.Forms.Button Them;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ComboBox MaThuoc;
        internal System.Windows.Forms.ComboBox MaNV;
        internal System.Windows.Forms.DateTimePicker NgayBanGiao;
        internal System.Windows.Forms.TextBox Phong;
        internal System.Windows.Forms.TextBox MaBG;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button button1;
    }
}