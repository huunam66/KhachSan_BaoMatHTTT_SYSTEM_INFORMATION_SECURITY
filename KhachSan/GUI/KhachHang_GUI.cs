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
            btn_delete.Enabled = false;
            btn_edit.Enabled = false;

            comboBox_sex.DataSource = new string[]
            {
                "Nam", "Nữ"
            };

            cbb_search.DataSource = new string[]
            {
                "Theo mã khách", "Theo họ tên", "Theo sdt", "Theo cmnd"
            };


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

        

    }
}
