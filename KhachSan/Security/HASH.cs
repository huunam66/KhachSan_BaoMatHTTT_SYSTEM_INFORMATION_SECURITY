using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace KhachSan.Security
{
    class HASH
    {
        public String SH256(String text)
        {
            try
            {
                OracleConnection connection = DAO.Access.getUser() != null ? DAO.Access.Connect() : DAO.Access.Connect_To("nhom10", "123");
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = "nhom10.HASH_VALUE";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_str", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = text;
                cm.Parameters.Add("out_hash", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                return cm.Parameters["out_hash"].Value.ToString();
            }
            catch(Exception e) { return null; }
        }
    }
}
