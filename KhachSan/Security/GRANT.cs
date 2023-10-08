using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace KhachSan.Security
{
    class GRANT
    {
        public Boolean GRANT_PRIVILEGES_ROLES(DTO.Account ac)
        {
            OracleConnection connection = DAO.Access.Connect_To("SYS", "123");
            OracleCommand cm = new OracleCommand();
            cm.Connection = connection;
            cm.CommandText = "SYS.GRANT_PRIVILEGES_ROLES_SCHEMA_NHOM10";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("USERNAME", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = ac.Username;
            cm.Parameters.Add("NEW_POSITION", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = ac.Role;
            cm.Parameters.Add("RESPONE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
            cm.ExecuteNonQuery();

            String Respone = cm.Parameters["RESPONE"].Value.ToString();
            return Respone.Equals("TRUE") ? true : false;
        }
    }
}
