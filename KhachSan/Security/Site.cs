using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.Security
{
    class Site
    {
        public static MD5 MD5 = new MD5();
        public static RSA RSA = new RSA();
        public static OLS OLS = new OLS();
        public static GRANT GRANT = new GRANT();
        public static REVOKE REVOKE = new REVOKE();
    }
}
