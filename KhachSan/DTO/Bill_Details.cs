using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.DTO
{
    class Bill_Details
    {
        public long ID_Bill { get; set; }
        public long ID_Room { get; set; }
        public double Price_Room { get; set; }
        public double Preset_Money { get; set; }
        public double Discount { get; set; }
        public DateTime Day_Check_In { get; set; }
        public DateTime Day_Check_Out { get; set; }
    }
}
