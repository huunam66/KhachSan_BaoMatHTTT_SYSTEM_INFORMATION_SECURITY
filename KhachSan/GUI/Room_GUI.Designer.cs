
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
            this.table_room = new System.Windows.Forms.DataGridView();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_themPhong = new System.Windows.Forms.Button();
            this.btn_xoaPhong = new System.Windows.Forms.Button();
            this.btn_updatePhong = new System.Windows.Forms.Button();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.txt_songuoitoida = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_loaiphong = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_phidatphong = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_giathue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_tenphong = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table_room)).BeginInit();
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
            this.guna2GroupBox3.Controls.Add(this.table_room);
            this.guna2GroupBox3.CustomBorderColor = System.Drawing.Color.LightBlue;
            this.guna2GroupBox3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox3.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox3.Location = new System.Drawing.Point(7, 285);
            this.guna2GroupBox3.Name = "guna2GroupBox3";
            this.guna2GroupBox3.Size = new System.Drawing.Size(1046, 405);
            this.guna2GroupBox3.TabIndex = 5;
            this.guna2GroupBox3.Text = "Bảng danh sách";
            // 
            // table_room
            // 
            this.table_room.AllowUserToAddRows = false;
            this.table_room.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.table_room.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table_room.Location = new System.Drawing.Point(5, 43);
            this.table_room.Name = "table_room";
            this.table_room.ReadOnly = true;
            this.table_room.RowHeadersWidth = 51;
            this.table_room.RowTemplate.Height = 24;
            this.table_room.Size = new System.Drawing.Size(1038, 356);
            this.table_room.TabIndex = 1;
            this.table_room.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.table_room_CellClick);
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
            this.guna2GroupBox2.Size = new System.Drawing.Size(300, 267);
            this.guna2GroupBox2.TabIndex = 4;
            this.guna2GroupBox2.Text = "Tác vụ";
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_reset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reset.Location = new System.Drawing.Point(82, 200);
            this.btn_reset.Margin = new System.Windows.Forms.Padding(5);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(125, 40);
            this.btn_reset.TabIndex = 30;
            this.btn_reset.Text = "Làm mới";
            this.btn_reset.UseVisualStyleBackColor = false;
            // 
            // btn_themPhong
            // 
            this.btn_themPhong.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_themPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_themPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_themPhong.Location = new System.Drawing.Point(82, 54);
            this.btn_themPhong.Margin = new System.Windows.Forms.Padding(5);
            this.btn_themPhong.Name = "btn_themPhong";
            this.btn_themPhong.Size = new System.Drawing.Size(125, 40);
            this.btn_themPhong.TabIndex = 25;
            this.btn_themPhong.Text = "Thêm";
            this.btn_themPhong.UseVisualStyleBackColor = false;
            this.btn_themPhong.Click += new System.EventHandler(this.btn_themPhong_Click);
            // 
            // btn_xoaPhong
            // 
            this.btn_xoaPhong.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_xoaPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_xoaPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoaPhong.Location = new System.Drawing.Point(82, 102);
            this.btn_xoaPhong.Margin = new System.Windows.Forms.Padding(5);
            this.btn_xoaPhong.Name = "btn_xoaPhong";
            this.btn_xoaPhong.Size = new System.Drawing.Size(125, 40);
            this.btn_xoaPhong.TabIndex = 27;
            this.btn_xoaPhong.Text = "Xoá";
            this.btn_xoaPhong.UseVisualStyleBackColor = false;
            this.btn_xoaPhong.Click += new System.EventHandler(this.btn_xoaPhong_Click);
            // 
            // btn_updatePhong
            // 
            this.btn_updatePhong.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_updatePhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_updatePhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_updatePhong.Location = new System.Drawing.Point(82, 152);
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
            this.guna2GroupBox1.Controls.Add(this.txt_songuoitoida);
            this.guna2GroupBox1.Controls.Add(this.label3);
            this.guna2GroupBox1.Controls.Add(this.txt_loaiphong);
            this.guna2GroupBox1.Controls.Add(this.label1);
            this.guna2GroupBox1.Controls.Add(this.txt_phidatphong);
            this.guna2GroupBox1.Controls.Add(this.label5);
            this.guna2GroupBox1.Controls.Add(this.txt_giathue);
            this.guna2GroupBox1.Controls.Add(this.label4);
            this.guna2GroupBox1.Controls.Add(this.txt_tenphong);
            this.guna2GroupBox1.Controls.Add(this.label2);
            this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.LightBlue;
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox1.Location = new System.Drawing.Point(7, 12);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(739, 267);
            this.guna2GroupBox1.TabIndex = 3;
            this.guna2GroupBox1.Text = "Thông tin khách hàng";
            // 
            // txt_songuoitoida
            // 
            this.txt_songuoitoida.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_songuoitoida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_songuoitoida.Location = new System.Drawing.Point(206, 210);
            this.txt_songuoitoida.Name = "txt_songuoitoida";
            this.txt_songuoitoida.Size = new System.Drawing.Size(320, 30);
            this.txt_songuoitoida.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(23, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 20);
            this.label3.TabIndex = 35;
            this.label3.Text = "Số người tối đa:";
            // 
            // txt_loaiphong
            // 
            this.txt_loaiphong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_loaiphong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_loaiphong.Location = new System.Drawing.Point(206, 174);
            this.txt_loaiphong.Name = "txt_loaiphong";
            this.txt_loaiphong.Size = new System.Drawing.Size(320, 30);
            this.txt_loaiphong.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(23, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "Loại phòng:";
            // 
            // txt_phidatphong
            // 
            this.txt_phidatphong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_phidatphong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_phidatphong.Location = new System.Drawing.Point(206, 138);
            this.txt_phidatphong.Name = "txt_phidatphong";
            this.txt_phidatphong.Size = new System.Drawing.Size(320, 30);
            this.txt_phidatphong.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(23, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 20);
            this.label5.TabIndex = 31;
            this.label5.Text = "Phí đặt phòng:";
            // 
            // txt_giathue
            // 
            this.txt_giathue.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_giathue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_giathue.Location = new System.Drawing.Point(206, 102);
            this.txt_giathue.Name = "txt_giathue";
            this.txt_giathue.Size = new System.Drawing.Size(320, 30);
            this.txt_giathue.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(23, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 29;
            this.label4.Text = "Giá thuê:";
            // 
            // txt_tenphong
            // 
            this.txt_tenphong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_tenphong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tenphong.Location = new System.Drawing.Point(206, 66);
            this.txt_tenphong.Name = "txt_tenphong";
            this.txt_tenphong.Size = new System.Drawing.Size(320, 30);
            this.txt_tenphong.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(23, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 24;
            this.label2.Text = "Tên phòng:";
            // 
            // Room_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 697);
            this.Controls.Add(this.guna2GroupBox3);
            this.Controls.Add(this.guna2GroupBox2);
            this.Controls.Add(this.guna2GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Room_GUI";
            this.Text = "ROOM_GUI";
            this.guna2GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table_room)).EndInit();
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
        private System.Windows.Forms.DataGridView table_room;
        private System.Windows.Forms.TextBox txt_phidatphong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_giathue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_tenphong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_updatePhong;
        private System.Windows.Forms.Button btn_xoaPhong;
        private System.Windows.Forms.Button btn_themPhong;
        private System.Windows.Forms.TextBox txt_songuoitoida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_loaiphong;
        private System.Windows.Forms.Label label1;
    }
}