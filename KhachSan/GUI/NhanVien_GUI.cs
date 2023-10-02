using KhachSan.DAO;
using KhachSan.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSan.GUI
{
    
    public partial class NhanVien_GUI : Form
    {
        
        public NhanVien_GUI()
        {
            InitializeComponent();
            cbb_chucvu.Items.Add("Nhân viên");
            cbb_chucvu.Items.Add("Kế toán");
            cbb_chucvu.Items.Add("Quản lý");
            cbb_chucvu.SelectedIndex = 0;
            cbb_GioiTinh.Items.Add("Nam");
            cbb_GioiTinh.Items.Add("Nữ");
            cbb_GioiTinh.SelectedIndex = 0;

            define_Column_TableEmployee();
            define_Column_TableAccount();


            load_data_table_employee();
            load_data_table_account();
        }

         

        private void define_Column_TableEmployee()
        {
            table_employee.ColumnCount = 8;
            table_employee.Columns[0].HeaderText = "Mã";
            table_employee.Columns[1].HeaderText = "Họ tên";
            table_employee.Columns[2].HeaderText = "Email";
            table_employee.Columns[3].HeaderText = "Số điện thoại";
            table_employee.Columns[4].HeaderText = "Chứng minh thư";
            table_employee.Columns[5].HeaderText = "Giới tính";
            table_employee.Columns[6].HeaderText = "Ngày sinh";
            table_employee.Columns[7].HeaderText = "Chức vụ";
        }


        private void define_Column_TableAccount()
        {
            table_account.ColumnCount = 3;
            table_account.Columns[0].HeaderText = "Tên tài khoản";
            table_account.Columns[1].HeaderText = "Mật khẩu";
            table_account.Columns[2].HeaderText = "Quyền hạn";
        }




        private void button2_Click(object sender, EventArgs e)
        {
            if (table_employee.Rows.Count == 1 || table_employee.SelectedCells.Count <= 0)
            {
                MessageBox.Show("Chọn nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            DialogResult d = MessageBox.Show("Modify ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (d == DialogResult.Cancel) return;

            if (String.IsNullOrWhiteSpace(txt_username.Text) || String.IsNullOrWhiteSpace(txt_password.Text))
            {
                MessageBox.Show("Dữ liệu trống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int row_index = table_employee.CurrentCell.RowIndex;
            DataGridViewRow row = table_employee.Rows[row_index];
            long id = long.Parse(row.Cells[0].Value.ToString());

            DTO.Account ac = DAO.Source.Account.findOne(id);
            if(ac == null)
            {
                String Respone = DAO.Source.Account.Create_Account(new DTO.Account()
                {
                    Staff_ID = id,
                    Username = txt_username.Text,
                    Password = txt_password.Text,
                    Role = row.Cells[row.Cells.Count - 1].Value.ToString()
                });
                MessageBox.Show(Respone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ac.Password = txt_password.Text;
            String res = DAO.Source.Account.Change_Password(ac);
            MessageBox.Show(res, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txt_password.Text = "";
        }

        private void load_data_table_employee()
        {
            table_employee.Rows.Clear();
            List<DTO.Staff> staffs = Source.Staff.findAll();
            if(staffs != null)
            {
                staffs.ForEach(item =>
                {
                    table_employee.Rows.Add(new String[]
                    {
                        item.ID.ToString(),
                        item.Name,
                        item.Email,
                        item.Phone,
                        item.CMND,
                        item.Gender,
                        item.BirthDay.ToString("dd/MM/yyyy"),
                        item.Position
                    });
                });
            }
        }

        private void load_data_table_account()
        {
            table_account.Rows.Clear();
            List<DTO.Account> accounts = DAO.Source.Account.find();
            if(accounts != null)
            {
                accounts.ForEach(item =>
                {
                    table_account.Rows.Add(new String[]
                    {
                        item.Username,
                        item.Password,
                        item.Role
                    });
                });
            } 
        }

        private List<TextBox> textBoxes()
        {
            return new List<TextBox>()
            {
                txt_CMND,
                txt_Email,
                txt_Name,
                txt_Phone
            };
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            List<TextBox> texts = textBoxes();
            foreach (TextBox t in texts)
            {
                if (t.Text.Equals(""))
                {
                    MessageBox.Show("Nhập đầy đủ dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            String Respone = DAO.Source.Staff.insertOne(new DTO.Staff()
            {
                Name = txt_Name.Text,
                Email = txt_Email.Text,
                CMND = txt_CMND.Text,
                Phone = txt_Phone.Text,
                BirthDay = picker_NgaySinh.Value,
                Gender = cbb_GioiTinh.Text,
                Position = cbb_chucvu.Text
            });

            MessageBox.Show(Respone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            load_data_table_employee();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void NhanVien_GUI_Load(object sender, EventArgs e)
        {

        }

        private void table_employee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row_index = table_employee.CurrentCell.RowIndex;
            if (row_index == table_employee.Rows.Count - 1) return;
            DataGridViewRow row = table_employee.Rows[row_index];
            DTO.Staff st = new DAO.Staff().findOne(row.Cells[0].Value.ToString());

            if(st != null)
            {
                txt_Name.Text = st.Name;
                txt_Email.Text = st.Email;
                txt_Phone.Text = st.Phone;
                txt_CMND.Text = st.CMND;
                picker_NgaySinh.Value = st.BirthDay;
                cbb_chucvu.SelectedItem = st.Gender;
                cbb_chucvu.SelectedItem = st.Position;

                DTO.Account ac = DAO.Source.Account.findOne(st.ID);
                if(ac != null) { 
                    txt_username.Text = ac.Username;
                    table_account.Rows.Clear();
                    table_account.Rows.Add(new String[]
                    {
                        ac.Username,
                        ac.Password,
                        ac.Role
                    });
                }
                else { txt_username.Text = ""; }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (table_employee.Rows.Count == 1 || table_employee.SelectedCells.Count <= 0) {
                MessageBox.Show("Không thể xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            DialogResult d = MessageBox.Show("Xóa ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.Cancel) return;

            int row_index = table_employee.CurrentCell.RowIndex;

            DataGridViewRow row = table_employee.Rows[row_index];
            long id = long.Parse(row.Cells[0].Value.ToString());
            String Respone = DAO.Source.Staff.deleteOne(id);
            MessageBox.Show(Respone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(Respone.Contains("thành công !"))
            {
                List<TextBox> texts = textBoxes();
                texts.ForEach(item => { item.Text = ""; });
                picker_NgaySinh.Value = DateTime.Now;
                cbb_chucvu.SelectedIndex = 0;
                cbb_GioiTinh.SelectedIndex = 0;
                txt_password.Text = "";
                txt_username.Text = "";
            }

            load_data_table_employee();
            load_data_table_account();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if(table_employee.Rows.Count == 1 || table_employee.SelectedCells.Count <= 0)
            {
                MessageBox.Show("Không thể cập nhật !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<TextBox> texts = textBoxes();
            foreach (TextBox t in texts)
            {
                if (t.Text.Equals(""))
                {
                    MessageBox.Show("Nhập đầy đủ dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult d = MessageBox.Show("Cập nhật ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.Cancel) return;

            String Respone = DAO.Source.Staff.updateOne(new DTO.Staff()
            {
                ID = long.Parse(table_employee.Rows[table_employee.CurrentCell.RowIndex].Cells[0].Value.ToString()),
                Name = txt_Name.Text,
                Email = txt_Email.Text,
                CMND = txt_CMND.Text,
                Phone = txt_Phone.Text,
                BirthDay = picker_NgaySinh.Value,
                Gender = cbb_GioiTinh.Text,
                Position = cbb_chucvu.Text
            });
            MessageBox.Show(Respone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            load_data_table_employee();
            load_data_table_account();
            txt_username.Text = "";
            txt_password.Text = "";
        }

        private void btnDrop_ac_Click(object sender, EventArgs e)
        {
            if (table_employee.Rows.Count == 1 || table_employee.SelectedCells.Count <= 0)
            {
                MessageBox.Show("Chọn nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            DialogResult d = MessageBox.Show("Drop ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (d == DialogResult.Cancel) return;

            int row_index = table_employee.CurrentCell.RowIndex;
            DataGridViewRow row = table_employee.Rows[row_index];
            long id = long.Parse(row.Cells[0].Value.ToString());

            String Respone = DAO.Source.Account.Drop_User(id);
            MessageBox.Show(Respone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(Respone.Contains("thành công"))
            {
                txt_username.Text = "";
                txt_password.Text = "";
                load_data_table_account();
                return;
            }

            txt_password.Text = "";

            load_data_table_account();
        }
    }
}
