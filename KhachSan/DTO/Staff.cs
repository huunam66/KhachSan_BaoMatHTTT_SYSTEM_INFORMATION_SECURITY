using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.DTO
{
    class Staff
    {
        public long ID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String CMND { get; set; }
        public String Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public String Gender { get; set; }
        public String Position { get; set; }
        public int Label { get; set; }
    }
}
