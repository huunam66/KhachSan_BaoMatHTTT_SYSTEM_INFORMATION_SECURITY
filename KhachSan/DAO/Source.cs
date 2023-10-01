using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.DAO
{
    class Source
    {
        public static Account Account = new Account();
        public static Bill Bill = new Bill();
        public static Bill_Details Bill_Details = new Bill_Details();
        public static Booking Booking = new Booking();
        public static Client Client = new Client();
        public static Room Room = new Room();
        public static Staff Staff = new Staff();
    }
}
