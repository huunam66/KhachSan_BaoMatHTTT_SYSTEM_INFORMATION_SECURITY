using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace KhachSan.Security
{
    class OLS
    {
        public Boolean Set_level_OLS_FOR_USER(String Username, String Position)
        {
            try
            {
                OracleConnection connection = DAO.Access.Connect_To("LBACSYS", "123");
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = "SET_LEVEL_OLS_SCHEMA_NHOM10_FOR_USER";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("USERNAME", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = Username;
                cm.Parameters.Add("POSITION", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = Position;
                cm.Parameters.Add("RESPONE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cm.ExecuteNonQuery();

                String Respone = cm.Parameters["RESPONE"].Value.ToString();
                return Respone.Equals("TRUE") ? true : false;
            }
            catch(Exception e) { return false; }
        }

        public void Set_Label_Tag_FOR_STAFF(String id, String Position)
        {
            int label_tag = 45;
            if (Position.ToUpper().Equals("KẾ TOÁN")) label_tag = 65;
            if (Position.ToUpper().Equals("QUẢN LÝ")) label_tag = 95;

            String query = "UPDATE nhom10.STAFF SET LABEL = " + label_tag + " WHERE ID = " + id;
            OracleConnection connection = DAO.Access.Connect();
            OracleCommand cm = new OracleCommand(query, connection);
            cm.CommandType = CommandType.Text;
            cm.ExecuteNonQuery();
        }
    }
}
