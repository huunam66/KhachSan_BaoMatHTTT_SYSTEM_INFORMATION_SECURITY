using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.DTO
{
    class Booking
    {
        public long ID { get; set; }
        public String Phone_Client { get; set; }
        public String Name_Client { get; set; }
        public long ID_Staff { get; set; }
        public long ID_Room { get; set; }
        public double Preset_Money { get; set; }
        public DateTime Day_Booking { get; set; }
        public DateTime Day_Check_In { get; set; }
        public DateTime Day_Check_Out { get; set; }
    }
}
