using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace KhachSan.Security
{
    class AUTHEN
    {
        public String SIGN_DATA(String text, String private_key)
        {
            try
            {
                OracleConnection connection = DAO.Access.getUser() != null ? DAO.Access.Connect() : DAO.Access.Connect_To("nhom10", "123");
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = "nhom10.SIGN_DATA";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_str", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = text;
                cm.Parameters.Add("private_key", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = private_key;
                cm.Parameters.Add("sign_res", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;
                cm.ExecuteNonQuery();
                return cm.Parameters["sign_res"].Value.ToString();
            }
            catch(Exception e) { return null; }
        }

        public Boolean VERIFY_DATA(String text, String sign_value, String public_key)
        {
            try
            {
                OracleConnection connection = DAO.Access.getUser() != null ? DAO.Access.Connect() : DAO.Access.Connect_To("nhom10", "123");
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = "nhom10.VERIFY_DATA";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_str", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = text;
                cm.Parameters.Add("inp_sign_val", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = sign_value;
                cm.Parameters.Add("public_key", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = public_key;
                cm.Parameters.Add("verify_return", OracleDbType.Boolean).Direction = ParameterDirection.Output;
                cm.ExecuteNonQuery();
                return Boolean.Parse(cm.Parameters["verify_return"].Value.ToString());
            }
            catch (Exception e) { return false; }
        }
    }
}
