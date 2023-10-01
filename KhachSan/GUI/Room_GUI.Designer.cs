
namespace KhachSan.GUI
{
    partial class Room_GUI
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
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2GroupBox3 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.TableKhachSan = new System.Windows.Forms.DataGridView();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_themPhong = new System.Windows.Forms.Button();
            this.btn_xoaPhong = new System.Windows.Forms.Button();
            this.btn_updatePhong = new System.Windows.Forms.Button();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.text_tinhTrang = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.text_phiDatPhong = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.text_giaPhong = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_maPhong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_tenPhong = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableKhachSan)).BeginInit();
            this.guna2GroupBox2.SuspendLayout();
            this.guna2GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2GroupBox3
            // 
            this.guna2GroupBox3.Controls.Add(this.TableKhachSan);
            this.guna2GroupBox3.CustomBorderColor = System.Drawing.Color.LightBlue;
            this.guna2GroupBox3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox3.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox3.Location = new System.Drawing.Point(7, 335);
            this.guna2GroupBox3.Name = "guna2GroupBox3";
            this.guna2GroupBox3.Size = new System.Drawing.Size(1046, 405);
            this.guna2GroupBox3.TabIndex = 5;
            this.guna2GroupBox3.Text = "Bảng danh sách";
            // 
            // TableKhachSan
            // 
            this.TableKhachSan.AllowUserToAddRows = false;
            this.TableKhachSan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TableKhachSan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableKhachSan.Location = new System.Drawing.Point(5, 43);
            this.TableKhachSan.Name = "TableKhachSan";
            this.TableKhachSan.ReadOnly = true;
            this.TableKhachSan.RowHeadersWidth = 51;
            this.TableKhachSan.RowTemplate.Height = 24;
            this.TableKhachSan.Size = new System.Drawing.Size(1042, 356);
            this.TableKhachSan.TabIndex = 1;
            this.TableKhachSan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableKhachSan_CellClick);
            // 
            // guna2GroupBox2
            // 
            this.guna2GroupBox2.Controls.Add(this.btn_reset);
            this.guna2GroupBox2.Controls.Add(this.btn_themPhong);
            this.guna2GroupBox2.Controls.Add(this.btn_xoaPhong);
            this.guna2GroupBox2.Controls.Add(this.btn_updatePhong);
            this.guna2GroupBox2.CustomBorderColor = System.Drawing.Color.LightBlue;
            this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox2.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox2.Location = new System.Drawing.Point(753, 12);
            this.guna2GroupBox2.Name = "guna2GroupBox2";
            this.guna2GroupBox2.Size = new System.Drawing.Size(300, 317);
            this.guna2GroupBox2.TabIndex = 4;
            this.guna2GroupBox2.Text = "Tác vụ";
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_reset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reset.Location = new System.Drawing.Point(82, 217);
            this.btn_reset.Margin = new System.Windows.Forms.Padding(5);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(125, 40);
            this.btn_reset.TabIndex = 30;
            this.btn_reset.Text = "Làm mới";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_reset_MouseUp);
            // 
            // btn_themPhong
            // 
            this.btn_themPhong.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_themPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_themPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_themPhong.Location = new System.Drawing.Point(82, 66);
            this.btn_themPhong.Margin = new System.Windows.Forms.Padding(5);
            this.btn_themPhong.Name = "btn_themPhong";
            this.btn_themPhong.Size = new System.Drawing.Size(125, 40);
            this.btn_themPhong.TabIndex = 25;
            this.btn_themPhong.Text = "Thêm";
            this.btn_themPhong.UseVisualStyleBackColor = false;
            this.btn_themPhong.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_themPhong_MouseUp);
            // 
            // btn_xoaPhong
            // 
            this.btn_xoaPhong.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_xoaPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_xoaPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoaPhong.Location = new System.Drawing.Point(82, 118);
            this.btn_xoaPhong.Margin = new System.Windows.Forms.Padding(5);
            this.btn_xoaPhong.Name = "btn_xoaPhong";
            this.btn_xoaPhong.Size = new System.Drawing.Size(125, 40);
            this.btn_xoaPhong.TabIndex = 27;
            this.btn_xoaPhong.Text = "Xoá";
            this.btn_xoaPhong.UseVisualStyleBackColor = false;
            this.btn_xoaPhong.Click += new System.EventHandler(this.btn_xoaPhong_Click);
            this.btn_xoaPhong.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_xoaPhong_MouseUp);
            // 
            // btn_updatePhong
            // 
            this.btn_updatePhong.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_updatePhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_updatePhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_updatePhong.Location = new System.Drawing.Point(82, 168);
            this.btn_updatePhong.Margin = new System.Windows.Forms.Padding(5);
            this.btn_updatePhong.Name = "btn_updatePhong";
            this.btn_updatePhong.Size = new System.Drawing.Size(125, 40);
            this.btn_updatePhong.TabIndex = 28;
            this.btn_updatePhong.Text = "Cập nhật";
            this.btn_updatePhong.UseVisualStyleBackColor = false;
            this.btn_updatePhong.Click += new System.EventHandler(this.btn_updatePhong_Click);
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Controls.Add(this.txtTimKiem);
            this.guna2GroupBox1.Controls.Add(this.comboBox1);
            this.guna2GroupBox1.Controls.Add(this.text_tinhTrang);
            this.guna2GroupBox1.Controls.Add(this.label7);
            this.guna2GroupBox1.Controls.Add(this.text_phiDatPhong);
            this.guna2GroupBox1.Controls.Add(this.label5);
            this.guna2GroupBox1.Controls.Add(this.text_giaPhong);
            this.guna2GroupBox1.Controls.Add(this.label4);
            this.guna2GroupBox1.Controls.Add(this.text_maPhong);
            this.guna2GroupBox1.Controls.Add(this.label3);
            this.guna2GroupBox1.Controls.Add(this.text_tenPhong);
            this.guna2GroupBox1.Controls.Add(this.label2);
            this.guna2GroupBox1.Controls.Add(this.label1);
            this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.LightBlue;
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox1.Location = new System.Drawing.Point(7, 12);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(739, 317);
            this.guna2GroupBox1.TabIndex = 3;
            this.guna2GroupBox1.Text = "Thông tin khách hàng";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(206, 272);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(258, 30);
            this.txtTimKiem.TabIndex = 20;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(469, 271);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(247, 33);
            this.comboBox1.TabIndex = 33;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // text_tinhTrang
            // 
            this.text_tinhTrang.BackColor = System.Drawing.Color.WhiteSmoke;
            this.text_tinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_tinhTrang.Location = new System.Drawing.Point(206, 227);
            this.text_tinhTrang.Name = "text_tinhTrang";
            this.text_tinhTrang.ReadOnly = true;
            this.text_tinhTrang.Size = new System.Drawing.Size(320, 30);
            this.text_tinhTrang.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(23, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "Tình trạng:";
            // 
            // text_phiDatPhong
            // 
            this.text_phiDatPhong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.text_phiDatPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_phiDatPhong.Location = new System.Drawing.Point(206, 183);
            this.text_phiDatPhong.Name = "text_phiDatPhong";
            this.text_phiDatPhong.Size = new System.Drawing.Size(320, 30);
            this.text_phiDatPhong.TabIndex = 22;
            this.text_phiDatPhong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_phiDatPhong_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(23, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 20);
            this.label5.TabIndex = 31;
            this.label5.Text = "Phí đặt phòng:";
            // 
            // text_giaPhong
            // 
            this.text_giaPhong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.text_giaPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_giaPhong.Location = new System.Drawing.Point(206, 138);
            this.text_giaPhong.Name = "text_giaPhong";
            this.text_giaPhong.Size = new System.Drawing.Size(320, 30);
            this.text_giaPhong.TabIndex = 21;
            this.text_giaPhong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_giaPhong_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(23, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 20);
            this.label4.TabIndex = 29;
            this.label4.Text = "Giá:";
            // 
            // text_maPhong
            // 
            this.text_maPhong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.text_maPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_maPhong.Location = new System.Drawing.Point(206, 53);
            this.text_maPhong.Name = "text_maPhong";
            this.text_maPhong.Size = new System.Drawing.Size(320, 30);
            this.text_maPhong.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(23, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 26;
            this.label3.Text = "Mã phòng:";
            // 
            // text_tenPhong
            // 
            this.text_tenPhong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.text_tenPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_tenPhong.Location = new System.Drawing.Point(206, 95);
            this.text_tenPhong.Name = "text_tenPhong";
            this.text_tenPhong.Size = new System.Drawing.Size(320, 30);
            this.text_tenPhong.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(23, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 24;
            this.label2.Text = "Tên phòng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(23, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Tìm kiếm:";
            // 
            // Room_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 753);
            this.Controls.Add(this.guna2GroupBox3);
            this.Controls.Add(this.guna2GroupBox2);
            this.Controls.Add(this.guna2GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Room_GUI";
            this.Text = "ROOM_GUI";
            this.Load += new System.EventHandler(this.ROOM_GUI_Load);
            this.guna2GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableKhachSan)).EndInit();
            this.guna2GroupBox2.ResumeLayout(false);
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox3;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private System.Windows.Forms.DataGridView TableKhachSan;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox text_tinhTrang;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox text_phiDatPhong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_giaPhong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_maPhong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_tenPhong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_updatePhong;
        private System.Windows.Forms.Button btn_xoaPhong;
        private System.Windows.Forms.Button btn_themPhong;
        private System.Windows.Forms.Label label1;
    }
}