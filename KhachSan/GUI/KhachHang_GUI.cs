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
    public partial class KhachHang_GUI : Form
    {
        public KhachHang_GUI()
        {
            InitializeComponent();
            
            cbb_gender.DataSource = new string[]
            {
                "Nam", "Nữ"
            };
            cbb_gender.SelectedIndex = 0;
            define_column_table();
            load_data_table();
        }

        private void define_column_table()
        {
            table_customer.ColumnCount = 5;
            table_customer.Columns[0].HeaderText = "Mã khách hàng";
            table_customer.Columns[1].HeaderText = "Tên khách hàng";
            table_customer.Columns[2].HeaderText = "SDT";
            table_customer.Columns[3].HeaderText = "CMND";
            table_customer.Columns[4].HeaderText = "Giới tính";
        }

        private void load_data_table()
        {
            table_customer.Rows.Clear();
            List<DTO.Customer> customers = DAO.Source.Customer.findAll();
            if(customers != null)
            {
                customers.ForEach(item =>
                {
                    table_customer.Rows.Add(new String[]
                    {
                        item.ID.ToString(),
                        item.Name,
                        item.CMND,
                        item.Phone,
                        item.Gender
                    });
                });
            }
        }

        private List<TextBox> textBoxes()
        {
            return new List<TextBox>()
            {
                txt_cmnd,
                txt_name,
                txt_numberPhone
            };
        }

        private void btn_add_Click(object sender, EventArgs e)
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

            DialogResult ch = MessageBox.Show("Thêm mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(ch == DialogResult.Cancel) { return; }

            Boolean Done = DAO.Source.Customer.insertOne(new DTO.Customer()
            {
                Name = txt_name.Text,
                CMND = txt_cmnd.Text,
                Phone = txt_numberPhone.Text,
                Gender = cbb_gender.Text
            });

            if(Done)
            {
                MessageBox.Show("Thêm mới thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset_all();
                return;
            }

            MessageBox.Show("Thêm mới thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void reset_all()
        {
            List<TextBox> txts = textBoxes();
            txts.ForEach(item => { item.Text = ""; });
            cbb_gender.SelectedIndex = 0;
            load_data_table();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            reset_all();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult ch = MessageBox.Show("Xóa khách hàng này ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ch == DialogResult.Cancel) { return; }

            int row_index = table_customer.CurrentCell.RowIndex;
            DataGridViewRow row = table_customer.Rows[row_index];
            String id = row.Cells[0].Value.ToString();
            Boolean Done = DAO.Source.Customer.deleteOne(id);

            if (Done)
            {
                MessageBox.Show("Xóa thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset_all();
                return;
            }

            MessageBox.Show("Xóa thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_edit_Click(object sender, EventArgs e)
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

            DialogResult ch = MessageBox.Show("Cập nhật ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ch == DialogResult.Cancel) { return; }

            int row_index = table_customer.CurrentCell.RowIndex;
            DataGridViewRow row = table_customer.Rows[row_index];

            Boolean Done = DAO.Source.Customer.updateOne(new DTO.Customer()
            {
                ID = long.Parse(row.Cells[0].Value.ToString()),
                Name = txt_name.Text,
                CMND = txt_cmnd.Text,
                Phone = txt_numberPhone.Text,
                Gender = cbb_gender.Text
            });

            if (Done)
            {
                MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset_all();
                return;
            }

            MessageBox.Show("Cập nhật thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void table_customer_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int row_index = table_customer.CurrentCell.RowIndex;
            if (row_index == table_customer.Rows.Count) return;

            DataGridViewRow row = table_customer.Rows[row_index];
            txt_name.Text = row.Cells[1].Value.ToString();
            txt_cmnd.Text = row.Cells[2].Value.ToString();
            txt_numberPhone.Text = row.Cells[3].Value.ToString();
            cbb_gender.Text = row.Cells[4].Value.ToString();
        }
    }
}
