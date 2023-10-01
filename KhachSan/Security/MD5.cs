using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.Security
{
    class MD5
    {
        public String Encrypt(String txt)
        {
            byte[] Source = new UnicodeEncoding().GetBytes(txt);
            byte[] Hash = new MD5CryptoServiceProvider().ComputeHash(Source);
            return BitConverter.ToString(Hash).Replace("-", "").ToLower();
        }
    }
}
