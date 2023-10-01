using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.DTO
{
    class Key_Secure
    {
        public long STAFF_ID { get; set; }
        public String public_key { get; set; }
        public String private_key { get; set; }
        public String symmetric_key { get; set; }
    }
}
