using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace KhachSan.DAO
{
    class Access
    {
        private static String User = null;
        private static String Password = null;
        private static String Container = "OLS";
        private static OracleConnection Connection = null;
        public static void setUser(String User)
        {
            Access.User = User.ToUpper();
        }
        public static void setPassword(String Password)
        {
            Access.Password = Password;
        }

        public static OracleConnection Connect()
        {
            String Source = "DATA SOURCE=localhost:1521/" + Access.Container +
                            ";USER ID=" + Access.User + "; PASSWORD=" + Access.Password + ";";
            Connection = new OracleConnection(Source);
            Connection.Open();
            return Connection;
        }

        public static OracleConnection Connect_To(String User1, String Password1)
        {
            String Source = "DATA SOURCE=localhost:1521/" + Access.Container +
                            ";USER ID=" + User1 + "; PASSWORD=" + Password1 + ";";
            OracleConnection oracleConnection = new OracleConnection(Source);
            oracleConnection.Open();
            return oracleConnection;
        }
    }
}
