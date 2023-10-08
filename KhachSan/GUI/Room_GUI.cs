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

            define_Column_Table();
            load_data_table();
        }
        private void define_Column_Table()
        {
            table_room.ColumnCount = 6;
            table_room.Columns[0].HeaderText = "Mã phòng";
            table_room.Columns[1].HeaderText = "Tên phòng";
            table_room.Columns[2].HeaderText = "Giá thuê";
            table_room.Columns[3].HeaderText = "Phí đặt phòng";
            table_room.Columns[4].HeaderText = "Loại phòng";
            table_room.Columns[5].HeaderText = "Số người tối đa";
        }
        
        private void load_data_table()
        {
            List<DTO.Room> rooms = DAO.Source.Room.findAll();
            rooms.ForEach(item =>
            {
                table_room.Rows.Add(new String[]
                {
                    item.ID.ToString(),
                    item.Name,
                    item.Price.ToString(),
                    item.Preset_money.ToString(),
                    item.Status,
                    item.Type_Room,
                    item.Max_People.ToString()
                });
            });
        }

        private void reset_all()
        {
            List<TextBox> txts = textBoxes();
            txts.ForEach(item => item.Text = "");
            load_data_table();
        }

        private List<TextBox> textBoxes()
        {
            return new List<TextBox>()
            {
                txt_tenphong,
                txt_giathue,
                txt_loaiphong,
                txt_phidatphong,
                txt_songuoitoida
            };
        }

        private void btn_themPhong_Click(object sender, EventArgs e)
        {
            List<TextBox> txts = textBoxes();
            foreach(TextBox t in txts)
            {
                if (t.Text.Equals(""))
                {
                    MessageBox.Show("Nhập đầy đủ dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult ch = MessageBox.Show("Thêm mới ?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (ch == DialogResult.Cancel) { return; }

            Boolean Done = DAO.Source.Room.insertOne(new DTO.Room()
            {
                Name = txt_tenphong.Text,
                Price = double.Parse(txt_giathue.Text),
                Preset_money = double.Parse(txt_phidatphong.Text),
                Status = "Trống",
                Type_Room = txt_loaiphong.Text,
                Max_People = int.Parse(txt_songuoitoida.Text)
            });

            if (Done)
            {
                MessageBox.Show("Thêm mới thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset_all();
                return;
            }

            MessageBox.Show("Thêm mới thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_xoaPhong_Click(object sender, EventArgs e)
        {
            DialogResult ch = MessageBox.Show("Xóa phòng này ?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (ch == DialogResult.Cancel) { return; }

            int row_index = table_room.CurrentCell.RowIndex;
            DataGridViewRow row = table_room.Rows[row_index];
            String id = row.Cells[0].Value.ToString();
            Boolean Done = DAO.Source.Room.deleteOne(id);

            if (Done)
            {
                MessageBox.Show("Xóa phòng thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset_all();
                return;
            }

            MessageBox.Show("Xóa phòng thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btn_updatePhong_Click(object sender, EventArgs e)
        {

            List<TextBox> txts = textBoxes();
            foreach (TextBox t in txts)
            {
                if (t.Text.Equals(""))
                {
                    MessageBox.Show("Nhập đầy đủ dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult ch = MessageBox.Show("Cập nhật ?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (ch == DialogResult.Cancel) { return; }

            int row_index = table_room.CurrentCell.RowIndex;
            DataGridViewRow row = table_room.Rows[row_index];
            long id = long.Parse(row.Cells[0].Value.ToString());

            Boolean Done = DAO.Source.Room.updateOne(new DTO.Room()
            {
                ID = id,
                Name = txt_tenphong.Text,
                Price = double.Parse(txt_giathue.Text),
                Preset_money = double.Parse(txt_phidatphong.Text),
                Status = "Trống",
                Type_Room = txt_loaiphong.Text,
                Max_People = int.Parse(txt_songuoitoida.Text)
            });

            if (Done)
            {
                MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset_all();
                return;
            }

            MessageBox.Show("Cập nhật thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void table_room_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row_index = table_room.CurrentCell.RowIndex;
            if (row_index == table_room.Rows.Count) return;

            DataGridViewRow row = table_room.Rows[row_index];
            txt_tenphong.Text = row.Cells[1].Value.ToString();
            txt_giathue.Text = row.Cells[2].Value.ToString();
            txt_phidatphong.Text = row.Cells[3].Value.ToString();
            txt_loaiphong.Text = row.Cells[4].Value.ToString();
            txt_songuoitoida.Text = row.Cells[5].Value.ToString();
        }
    }
}
