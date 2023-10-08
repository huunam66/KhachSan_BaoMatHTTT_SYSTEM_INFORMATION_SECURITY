using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.Security
{
    class Site
    {
        public static HASH MD5 = new HASH();
        public static RSA RSA = new RSA();
        public static OLS OLS = new OLS();
        public static GRANT GRANT = new GRANT();
        public static REVOKE REVOKE = new REVOKE();
        public static KEYS KEYS = new KEYS();
        public static HASH HASH = new HASH();
        public static SYMMETRIC SYMMETRIC = new SYMMETRIC();
        public static AUTHEN AUTHEN = new AUTHEN();
     
    }
}
