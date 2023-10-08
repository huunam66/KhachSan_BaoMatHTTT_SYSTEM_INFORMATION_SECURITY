using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace KhachSan.Security
{
    class SYMMETRIC
    {
        public String ENCRYPT(String text, String key)
        {
            try
            {
                OracleConnection connection = DAO.Access.Connect();
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = "nhom10.SYMMETRIC_ENCRYPT";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("p_input", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = text;
                cm.Parameters.Add("p_key", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = key;
                cm.Parameters.Add("enc_res", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                return cm.Parameters["enc_res"].Value.ToString();
            }
            catch(Exception e) { return null; }
        }

        public String DECRYPT(String text, String key)
        {
            try
            {
                OracleConnection connection = DAO.Access.Connect();
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = "nhom10.SYMMETRIC_DECRYPT";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("p_input", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = text;
                cm.Parameters.Add("p_key", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = key;
                cm.Parameters.Add("dec_res", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                return cm.Parameters["dec_res"].Value.ToString();
            }
            catch(Exception e) { return null; }
        }
    }
}
