using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.DTO
{
    class Room
    {
        public long ID { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public double Preset_money { get; set; }
        public String Status { get; set; }
        public String Type_Room { get; set; }
        public int Max_People { get; set; }
    }
}
