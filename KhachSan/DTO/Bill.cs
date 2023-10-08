using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.DTO
{
    class Bill
    {
        public long ID { get; set; }
        public long ID_Client { get; set; }
        public long ID_Staff { get; set; }
        public DateTime Day_For_Pay { get; set; }
        public int Count_Room { get; set; }
        public String Status { get; set; }
        public double Total { get; set; }
    }
}
