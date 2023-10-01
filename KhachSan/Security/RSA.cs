using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.Security
{
    class RSA
    {
        private RSACryptoServiceProvider rsa;
        private RSAParameters private_key;
        private RSAParameters public_key;
        private byte[] Source;
        private byte[] Result;

        public void Create(RSAParameters pr, RSAParameters pb)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.KeySize = 2048;
            private_key = pr;
            public_key = pb;
            Source = new byte[2048];
            Result = new byte[2048];
        }

        public void Import(String pr_xml)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.KeySize = 2048;
            rsa.FromXmlString(pr_xml);
            private_key = rsa.ExportParameters(true);
            public_key = rsa.ExportParameters(false);
            Source = new byte[2048];
            Result = new byte[2048];
        }

        public String Encrypt(String txt)
        {
            try
            {
                this.rsa.ImportParameters(this.public_key);
                Source = Encoding.Unicode.GetBytes(txt);
                Result = this.rsa.Encrypt(Source, false);
                return Convert.ToBase64String(Result);
            }
            catch (Exception e) { return null; }
        }

        public String Decrypt(String txt)
        {
            try
            {
                this.rsa.ImportParameters(this.private_key);
                Source = Convert.FromBase64String(txt);
                Result = this.rsa.Decrypt(Source, false);
                return Encoding.Unicode.GetString(Result);
            }
            catch (Exception e) { return null; }
        }

        public RSAParameters getPublic_key()
        {
            return this.public_key;
        }
    }
}
