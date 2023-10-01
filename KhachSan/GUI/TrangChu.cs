using KhachSan.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSan.GUI
{
    public partial class TrangChu : Form
    {
        private Login logForm = null;
        public TrangChu(Login log)
        {
            this.logForm = log;
            InitializeComponent();
            OpenChildForm(new TongQuan());
            this.StartPosition = FormStartPosition.CenterScreen;
          
        }
        private Form currentformchild;
        private void OpenChildForm(Form child)
        {
            if (currentformchild != null)
                currentformchild.Close();
            currentformchild = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            bunifuPanel_child.Controls.Add(child);
            bunifuPanel_child.BringToFront();
            this.Width = child.Width + bunifuPanel_menu.Width + 20;
            bunifuPanel_title.Width = child.Width;
            bunifuPanel_child.Width = child.Width;
            bunifuPanel_child.Height = child.Height;
            this.Height = bunifuPanel_child.Height + bunifuPanel_title.Height + 40;
            child.Show();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

            DialogResult h = MessageBox.Show
            ("Bạn có chắc muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (h == DialogResult.OK)
            {
                Application.Exit();
            }
        }

   

        private void btn_room_Click(object sender, EventArgs e)
        {
            btn_room.Top = ((Control)sender).Top;
            OpenChildForm(new Room_GUI());
            label1.Text = "ROOM";
        }

        private void btn_logout_Click_1(object sender, EventArgs e)
        {
           
          
            DialogResult h = MessageBox.Show
           ("Bạn có chắc muốn đăng xuất không?", "Thông báo", MessageBoxButtons.OKCancel);
           
            if (h == DialogResult.OK)
            {

                this.logForm.Show();
                this.Close();
              
             }
        }

        private void btn_khachhang_Click(object sender, EventArgs e)
        {
           btn_khachhang.Top = ((Control)sender).Top;
            OpenChildForm(new KhachHang_GUI());
            label1.Text = "Khách Hàng";
        }

        private void btn_nhanvien_Click(object sender, EventArgs e)
        {
            btn_nhanvien.Top = ((Control)sender).Top;
            OpenChildForm(new NhanVien_GUI());
            label1.Text = "Nhân Viên";
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }
    }
}
