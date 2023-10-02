using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace KhachSan.Security
{
    class REVOKE
    {
        public Boolean REVOKE_PRIVILEGES_ROLES(String Username, String old_Position)
        {
            OracleConnection connection = DAO.Access.Connect_To("SYS", "123");
            OracleCommand cm = new OracleCommand();
            cm.Connection = connection;
            cm.CommandText = "SYS.REVOKE_PRIVILEGES_ROLES_SCHEMA_NHOM10";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("USERNAME", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = Username;
            cm.Parameters.Add("OLD_POSITION", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = old_Position;
            cm.Parameters.Add("RESPONE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
            cm.ExecuteNonQuery();

            String Respone = cm.Parameters["RESPONE"].Value.ToString();
            return Respone.Equals("TRUE") ? true : false;
        }
    }
}
