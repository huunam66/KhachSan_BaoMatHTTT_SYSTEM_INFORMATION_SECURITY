using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace KhachSan.Security
{
    class RSA
    {
        public String ENCRYPT(String text, String public_key)
        {
            try
            {
                OracleConnection connection = DAO.Access.Connect();
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = "nhom10.RSA_ENCRYPT";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_str", OracleDbType.Varchar2, 4096, ParameterDirection.Input).Value = text;
                cm.Parameters.Add("public_key", OracleDbType.Varchar2, 4096, ParameterDirection.Input).Value = public_key;
                cm.Parameters.Add("enc_res", OracleDbType.Varchar2, 4096).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                return cm.Parameters["enc_res"].Value.ToString();
            }
            catch(Exception e) { return null; }
                
        }

        public String DECRYPT(String text, String private_key)
        {
            try
            {
                OracleConnection connection = DAO.Access.Connect();
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = "nhom10.RSA_DECRYPT";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_str", OracleDbType.Varchar2, 4096, ParameterDirection.Input).Value = text;
                cm.Parameters.Add("private_key", OracleDbType.Varchar2, 4096, ParameterDirection.Input).Value = private_key;
                cm.Parameters.Add("dec_res", OracleDbType.Varchar2, 4096).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                return cm.Parameters["dec_res"].Value.ToString();
            }
            catch (Exception e) { return null; }

        }
    }
}
