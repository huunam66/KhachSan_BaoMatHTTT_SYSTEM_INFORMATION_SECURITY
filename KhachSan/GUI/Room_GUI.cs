using KhachSan.DAO;
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
    public partial class Room_GUI : Form
    {
        public Room_GUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Enabled = false;

        }
        private void define_Column_Table()
        {
            TableKhachSan.ColumnCount = 5;
            TableKhachSan.Columns[0].HeaderText = "Mã phòng";
            TableKhachSan.Columns[1].HeaderText = "Tên phòng";
            TableKhachSan.Columns[2].HeaderText = "Giá";
            TableKhachSan.Columns[3].HeaderText = "Phí đặt phòng";
            TableKhachSan.Columns[4].HeaderText = "Tình trạng";
        }
        
        private List<TextBox> l_TextBox()
        {
            List<TextBox> textBoxs = new List<TextBox>();
            textBoxs.Add(text_maPhong);
            textBoxs.Add(text_tenPhong);
            textBoxs.Add(text_giaPhong);
            textBoxs.Add(text_phiDatPhong);
            return textBoxs;
        }


        private bool check_Number(string str)
        {
            try
            {
                float.Parse(str);
                return true;
            }
            catch { return false; }
        }

        private void ROOM_GUI_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Tìm kiếm theo mã phòng");
            comboBox1.Items.Add("Tìm kiếm theo tên phòng");
            btn_updatePhong.Enabled = false;
            btn_xoaPhong.Enabled = false;
        }

        private void btn_themPhong_MouseUp(object sender, MouseEventArgs e)
        {
            List<TextBox> textBoxs = this.l_TextBox();
            foreach (TextBox textBox in textBoxs)
            {
                if (textBox.Text == string.Empty)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!this.check_Number(text_giaPhong.Text))
            {
                MessageBox.Show("Dữ liệu giá phòng nhập vào không hợp lệ !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!this.check_Number(text_phiDatPhong.Text))
            {
                MessageBox.Show("Dữ liệu phí đặt phòng nhập vào không hợp lệ !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhong = text_maPhong.Text;
            string tenPhong = text_tenPhong.Text;
            float giaPhong = float.Parse(text_giaPhong.Text);
            float phiDatPhong = float.Parse(text_phiDatPhong.Text);
            

            //MessageBox.Show(maPhong + tenPhong + giaPhong + phiDatPhong);

            //if (Room_DAO.insert_Room(maPhong, tenPhong, giaPhong, phiDatPhong))
            //{
            //    this.reset_All();
            //}
            //else
            //{
            //    MessageBox.Show("Lỗi hệ thống !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void reset_All()
        {
            List<TextBox> list = this.l_TextBox();
            foreach (TextBox text in this.l_TextBox())
            {
                text.Text = string.Empty;
            }
            text_maPhong.Focus();
        }

        private void TableKhachSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView table = sender as DataGridView;
            if (table.CurrentCell.Selected)
            {
                DataGridViewRow row = table.CurrentRow;
                text_maPhong.Text = row.Cells[0].Value.ToString();
                text_tenPhong.Text = row.Cells[1].Value.ToString();
                text_giaPhong.Text = row.Cells[2].Value.ToString();
                text_phiDatPhong.Text = row.Cells[3].Value.ToString();
                text_tinhTrang.Text = row.Cells[4].Value.ToString();
                btn_updatePhong.Enabled = true;
                btn_xoaPhong.Enabled = true;
            }
        }

        private void btn_xoaPhong_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView table = TableKhachSan;
            if (table.CurrentCell.Selected)
            {
                string maPhong = table.CurrentRow.Cells[0].Value.ToString();
                string tenPhong = table.CurrentRow.Cells[1].Value.ToString();
                if (MessageBox.Show("Đồng ý xoá " + maPhong + ": " + tenPhong + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //if (Room_DAO.delete_Room(maPhong))
                    //{
                    //    this.reset_All();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Lỗi hệ thống !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
        }

        private void btn_reset_MouseUp(object sender, MouseEventArgs e)
        {
            btn_updatePhong.Enabled = false;
            btn_xoaPhong.Enabled = false;
            this.reset_All();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox1.SelectedIndex == 0)
            //{
            //    load_DATA_ROOM(Room_DAO.timKiemTheoMa(txtTimKiem.Text.Trim()));
            //}
            //else
            //{
            //    load_DATA_ROOM(Room_DAO.timKiemTheoTen(txtTimKiem.Text.Trim()));
            //}
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "")
            {
                comboBox1.Enabled = true;
                comboBox1.Text = String.Empty;

            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.Text = String.Empty;

            }

        }

        private void btn_updatePhong_Click(object sender, EventArgs e)
        {
            if (text_maPhong.Text == "" || text_tenPhong.Text == "" || text_giaPhong.Text == "" || text_phiDatPhong.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ thông tin !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                string maPhong = text_maPhong.Text;
                string tenPhong = text_tenPhong.Text;
                float giaPhong = float.Parse(text_giaPhong.Text);
                float phiDatPhong = float.Parse(text_phiDatPhong.Text);

                //if (Room_DAO.edit_Room(maPhong, tenPhong, giaPhong, phiDatPhong))
                //{
                //    this.reset_All();
                //    MessageBox.Show("Thay đổi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
                //else
                //{
                //    MessageBox.Show("Lỗi hệ thống !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }

        private void text_giaPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void text_phiDatPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_xoaPhong_Click(object sender, EventArgs e)
        {

        }
    }
}
