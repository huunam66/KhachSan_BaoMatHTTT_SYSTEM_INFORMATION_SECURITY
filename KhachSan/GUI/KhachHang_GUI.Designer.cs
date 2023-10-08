
namespace KhachSan.GUI
{
    partial class KhachHang_GUI
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
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.cbb_gender = new System.Windows.Forms.ComboBox();
            this.txt_cmnd = new System.Windows.Forms.TextBox();
            this.txt_numberPhone = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.guna2GroupBox3 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.table_customer = new System.Windows.Forms.DataGridView();
            this.guna2GroupBox1.SuspendLayout();
            this.guna2GroupBox2.SuspendLayout();
            this.guna2GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table_customer)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Controls.Add(this.cbb_gender);
            this.guna2GroupBox1.Controls.Add(this.txt_cmnd);
            this.guna2GroupBox1.Controls.Add(this.txt_numberPhone);
            this.guna2GroupBox1.Controls.Add(this.txt_name);
            this.guna2GroupBox1.Controls.Add(this.label5);
            this.guna2GroupBox1.Controls.Add(this.label4);
            this.guna2GroupBox1.Controls.Add(this.label3);
            this.guna2GroupBox1.Controls.Add(this.label2);
            this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.LightBlue;
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox1.Location = new System.Drawing.Point(12, 13);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(572, 285);
            this.guna2GroupBox1.TabIndex = 0;
            this.guna2GroupBox1.Text = "Thông tin khách hàng";
            // 
            // cbb_gender
            // 
            this.cbb_gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_gender.FormattingEnabled = true;
            this.cbb_gender.Location = new System.Drawing.Point(120, 209);
            this.cbb_gender.Name = "cbb_gender";
            this.cbb_gender.Size = new System.Drawing.Size(421, 31);
            this.cbb_gender.TabIndex = 9;
            // 
            // txt_cmnd
            // 
            this.txt_cmnd.Location = new System.Drawing.Point(119, 167);
            this.txt_cmnd.Name = "txt_cmnd";
            this.txt_cmnd.Size = new System.Drawing.Size(423, 30);
            this.txt_cmnd.TabIndex = 8;
            // 
            // txt_numberPhone
            // 
            this.txt_numberPhone.Location = new System.Drawing.Point(118, 124);
            this.txt_numberPhone.Name = "txt_numberPhone";
            this.txt_numberPhone.Size = new System.Drawing.Size(423, 30);
            this.txt_numberPhone.TabIndex = 7;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(117, 81);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(423, 30);
            this.txt_name.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(27, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Giới tính:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(29, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "CMND:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(28, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "SĐT:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(27, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Họ tên:";
            // 
            // guna2GroupBox2
            // 
            this.guna2GroupBox2.Controls.Add(this.button1);
            this.guna2GroupBox2.Controls.Add(this.btn_edit);
            this.guna2GroupBox2.Controls.Add(this.btn_delete);
            this.guna2GroupBox2.Controls.Add(this.btn_add);
            this.guna2GroupBox2.CustomBorderColor = System.Drawing.Color.LightBlue;
            this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox2.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox2.Location = new System.Drawing.Point(590, 12);
            this.guna2GroupBox2.Name = "guna2GroupBox2";
            this.guna2GroupBox2.Size = new System.Drawing.Size(300, 285);
            this.guna2GroupBox2.TabIndex = 1;
            this.guna2GroupBox2.Text = "Tác vụ";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SkyBlue;
            this.button1.Location = new System.Drawing.Point(87, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 38);
            this.button1.TabIndex = 14;
            this.button1.Text = "Làm mới";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_edit.Location = new System.Drawing.Point(87, 160);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(119, 38);
            this.btn_edit.TabIndex = 13;
            this.btn_edit.Text = "Sửa";
            this.btn_edit.UseVisualStyleBackColor = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_delete.Location = new System.Drawing.Point(87, 108);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(119, 38);
            this.btn_delete.TabIndex = 12;
            this.btn_delete.Text = "Xóa";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_add.Location = new System.Drawing.Point(87, 54);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(119, 38);
            this.btn_add.TabIndex = 11;
            this.btn_add.Text = "Thêm";
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // guna2GroupBox3
            // 
            this.guna2GroupBox3.Controls.Add(this.table_customer);
            this.guna2GroupBox3.CustomBorderColor = System.Drawing.Color.LightBlue;
            this.guna2GroupBox3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox3.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox3.Location = new System.Drawing.Point(12, 304);
            this.guna2GroupBox3.Name = "guna2GroupBox3";
            this.guna2GroupBox3.Size = new System.Drawing.Size(878, 396);
            this.guna2GroupBox3.TabIndex = 2;
            this.guna2GroupBox3.Text = "Bảng danh sách";
            // 
            // table_customer
            // 
            this.table_customer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.table_customer.BackgroundColor = System.Drawing.Color.Silver;
            this.table_customer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table_customer.Location = new System.Drawing.Point(4, 44);
            this.table_customer.Name = "table_customer";
            this.table_customer.RowHeadersWidth = 51;
            this.table_customer.RowTemplate.Height = 24;
            this.table_customer.Size = new System.Drawing.Size(871, 349);
            this.table_customer.TabIndex = 0;
            this.table_customer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.table_customer_CellClick);
            // 
            // KhachHang_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 712);
            this.Controls.Add(this.guna2GroupBox3);
            this.Controls.Add(this.guna2GroupBox2);
            this.Controls.Add(this.guna2GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "KhachHang_GUI";
            this.Text = "customer";
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            this.guna2GroupBox2.ResumeLayout(false);
            this.guna2GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table_customer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox3;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private System.Windows.Forms.DataGridView table_customer;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.ComboBox cbb_gender;
        private System.Windows.Forms.TextBox txt_cmnd;
        private System.Windows.Forms.TextBox txt_numberPhone;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}