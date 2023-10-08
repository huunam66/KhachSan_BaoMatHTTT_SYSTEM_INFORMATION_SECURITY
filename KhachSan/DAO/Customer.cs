using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace KhachSan.DAO
{
    class Customer
    {
        private String symmetric_key = "nhom10-key-symmetric";

        public List<DTO.Customer> findAll()
        {
            try
            {
                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "SELECT * FROM nhom10.CLIENT";
                OracleDataReader reader = cm.ExecuteReader();
                List<DTO.Customer> clients = new List<DTO.Customer>();

                while (reader.Read())
                {
                    clients.Add(new DTO.Customer()
                    {
                        ID = long.Parse(reader.GetString(0)),
                        Name = reader.GetString(1),
                        CMND = reader.GetString(2),
                        Phone = reader.GetString(3),
                        Gender = reader.GetString(4)
                    });
                }

                return clients;
            }
            catch (Exception e) { return null; }
        }

        public DTO.Customer findOne(String id)
        {
            try
            {
                String s_key = Security.Site.HASH.SH256(symmetric_key);
                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "SELECT * FROM nhom10.CLIENT WHERE ID = " + id;
                OracleDataReader reader = cm.ExecuteReader();
                if (!reader.Read()) return null;

                return new DTO.Customer() { 
                    ID = long.Parse(reader.GetString(0)),
                    Name = reader.GetString(1),
                    CMND = Security.Site.SYMMETRIC.DECRYPT(reader.GetString(2), s_key),
                    Phone = Security.Site.SYMMETRIC.DECRYPT(reader.GetString(3), s_key),
                    Gender = reader.GetString(4)
                };
            }
            catch(Exception e) { return null; }
        }

        public Boolean insertOne(DTO.Customer customer)
        {
            try
            {
                String s_key = Security.Site.HASH.SH256(symmetric_key);

                customer.Phone = Security.Site.SYMMETRIC.ENCRYPT(customer.Phone, s_key);
                customer.CMND = Security.Site.SYMMETRIC.ENCRYPT(customer.CMND, s_key);

                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "INSERT_CLIENT";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_NAME", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = customer.Name;
                cm.Parameters.Add("inp_CMND", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = customer.CMND;
                cm.Parameters.Add("inp_PHONE", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = customer.Phone;
                cm.Parameters.Add("inp_GENDER", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = customer.Gender;
                cm.Parameters.Add("RESPONE", OracleDbType.Boolean).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                Boolean Done = Boolean.Parse(cm.Parameters["RESPONE"].Value.ToString());

                return Done;
            }
            catch (Exception e) { return false; }
        }


        public Boolean updateOne(DTO.Customer customer)
        {
            try
            {
                String s_key = Security.Site.HASH.SH256(symmetric_key);

                customer.Phone = Security.Site.SYMMETRIC.ENCRYPT(customer.Phone, s_key);
                customer.CMND = Security.Site.SYMMETRIC.ENCRYPT(customer.CMND, s_key);

                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "UPDATE_CLIENT";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_ID", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = customer.ID;
                cm.Parameters.Add("inp_NAME", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = customer.Name;
                cm.Parameters.Add("inp_CMND", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = customer.CMND;
                cm.Parameters.Add("inp_PHONE", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = customer.Phone;
                cm.Parameters.Add("inp_GENDER", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = customer.Gender;
                cm.Parameters.Add("RESPONE", OracleDbType.Boolean).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                Boolean Done = Boolean.Parse(cm.Parameters["RESPONE"].Value.ToString());

                return Done;
            }
            catch (Exception e) { return false; }
        }

        public Boolean deleteOne(String id)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "DELETE FROM nhom10.CLIENT WHERE ID = " + id;
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();
                return cm.ExecuteNonQuery() != 0 ? true : false;
            }
            catch(Exception e) { return false; }
        }
    }
}
