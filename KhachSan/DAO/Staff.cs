using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using System.IO;
using System.Diagnostics;

namespace KhachSan.DAO
{
    class Staff
    {
        public List<DTO.Staff> findAll()
        {
            try
            {
                String query = "SELECT * FROM nhom10.STAFF";
                OracleConnection connection = Access.Connect();
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                List<DTO.Staff> staffs = new List<DTO.Staff>();
                while (reader.Read())
                {
                    staffs.Add(new DTO.Staff()
                    {
                        ID = long.Parse(reader.GetString(0)),
                        Name = reader.GetString(1).ToString(),
                        Email = reader.GetString(2).ToString(),
                        CMND = reader.GetString(3).ToString(),
                        Phone = reader.GetString(4).ToString(),
                        BirthDay = DateTime.Parse(reader.GetString(5).ToString()),
                        Gender = reader.GetString(6).ToString(),
                        Position = reader.GetString(7).ToString()
                    });
                }
                return staffs;
            }
            catch(Exception e) { return null; }
        }

        public DTO.Staff findOne(String id)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                String query = "SELECT * FROM nhom10.STAFF WHERE ID = " + id;
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                OracleDataReader reader = cm.ExecuteReader();
                if (!reader.Read()) return null;

                DTO.Staff staff = new DTO.Staff()
                {
                    ID = long.Parse(reader.GetString(0)),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    CMND = reader.GetString(3),
                    Phone = reader.GetString(4),
                    BirthDay = DateTime.Parse(reader.GetString(5)),
                    Gender = reader.GetString(6),
                    Position = reader.GetString(7)
                };

                String private_key_file_name = Security.Site.HASH.SH256(staff.CMND + "private");
                String symmetric_key_file_name = Security.Site.HASH.SH256(staff.CMND + "symmetric");

                String private_key = Security.Site.KEYS.Read_file_key(private_key_file_name);
                String symmetric_key = Security.Site.KEYS.Read_file_key(symmetric_key_file_name);

                symmetric_key = Security.Site.RSA.DECRYPT(symmetric_key, private_key);

                staff.CMND = Security.Site.SYMMETRIC.DECRYPT(staff.CMND, symmetric_key);
                staff.Email = Security.Site.SYMMETRIC.DECRYPT(staff.Email, symmetric_key);
                staff.Phone = Security.Site.SYMMETRIC.DECRYPT(staff.Phone, symmetric_key);

                return staff;
            }
            catch(Exception e) {}
            return null;
        }

        public DTO.Staff findOne(long id)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                String query = "SELECT * FROM nhom10.STAFF WHERE ID = " + id;
                OracleCommand cm = new OracleCommand();
                cm.Connection = connection;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                OracleDataReader reader = cm.ExecuteReader();
                if (!reader.Read()) return null;

                DTO.Staff staff = new DTO.Staff()
                {
                    ID = long.Parse(reader.GetString(0)),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    CMND = reader.GetString(3),
                    Phone = reader.GetString(4),
                    BirthDay = DateTime.Parse(reader.GetString(5)),
                    Gender = reader.GetString(6),
                    Position = reader.GetString(7)
                };

                return staff;
            }
            catch (Exception e) { }
            return null;
        }



        public String insertOne(DTO.Staff staff)
        {
            try
            {
                Security.Site.KEYS.OpenSSL_Generate_Private_Key();
                Security.Site.KEYS.OpenSSL_Generate_Public_Key();

                String private_key = Security.Site.KEYS.Read_Private_Key_PEM();
                String public_key = Security.Site.KEYS.Read_Public_Key_PEM();

                String symmetric_key = Security.Site.HASH.SH256(staff.CMND);
                staff.CMND = Security.Site.SYMMETRIC.ENCRYPT(staff.CMND, symmetric_key);
                staff.Email = Security.Site.SYMMETRIC.ENCRYPT(staff.Email, symmetric_key);
                staff.Phone = Security.Site.SYMMETRIC.ENCRYPT(staff.Phone, symmetric_key);

                symmetric_key = Security.Site.RSA.ENCRYPT(symmetric_key, public_key);

                String private_key_save_file = Security.Site.HASH.SH256(staff.CMND + "private");
                String public_key_save_file = Security.Site.HASH.SH256(staff.CMND + "public");
                String symmetric_key_save_file = Security.Site.HASH.SH256(staff.CMND + "symmetric");

                Security.Site.KEYS.Save_Keys(private_key, private_key_save_file);
                Security.Site.KEYS.Save_Keys(public_key, public_key_save_file);
                Security.Site.KEYS.Save_Keys(symmetric_key, symmetric_key_save_file);

                int label_tag = 15;
                if (staff.Position.ToUpper().Equals("LỄ TÂN")) label_tag = 45;
                if (staff.Position.ToUpper().Equals("KẾ TOÁN")) label_tag = 65;
                if (staff.Position.ToUpper().Equals("QUẢN LÝ")) label_tag = 95;

                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "nhom10.INSERT_STAFF";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_NAME", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = staff.Name;
                cm.Parameters.Add("inp_EMAIL", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = staff.Email;
                cm.Parameters.Add("inp_CMND", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = staff.CMND;
                cm.Parameters.Add("inp_PHONE", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = staff.Phone;
                cm.Parameters.Add("inp_BIRTHDAY", OracleDbType.Date, ParameterDirection.Input).Value = staff.BirthDay;
                cm.Parameters.Add("inp_GENDER", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = staff.Gender;
                cm.Parameters.Add("inp_POSITION", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = staff.Position;
                cm.Parameters.Add("inp_LABEL", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = label_tag;

                return cm.ExecuteNonQuery() != 0 ? "Thêm mới thành công !" : "Thêm mới thất bại !";
            }
            catch (Exception e)
            { return "Thêm mới thất bại !"; }
        }

        public String deleteOne(long id)
        {
            try
            {
                String Drop_User = Source.Account.Drop_User(id);
                if (Drop_User.Equals("Bạn không thể xóa chính bạn !")) 
                    return Drop_User;

                String query = "SELECT CMND FROM nhom10.STAFF WHERE ID = " + id;
                OracleConnection connection = Access.Connect();
                OracleCommand cm = new OracleCommand();

                cm.Connection = connection;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                reader.Read();

                String CMND = reader.GetString(0);

                String private_key_file_name = Security.Site.HASH.SH256(CMND + "private");
                String public_key_file_name = Security.Site.HASH.SH256(CMND + "public");
                String symmetric_key_file_name = Security.Site.HASH.SH256(CMND + "symmetric");

                Security.Site.KEYS.Delete_File(private_key_file_name);
                Security.Site.KEYS.Delete_File(public_key_file_name);
                Security.Site.KEYS.Delete_File(symmetric_key_file_name);

                query = "DELETE FROM nhom10.STAFF WHERE ID = " + id;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                return cm.ExecuteNonQuery() != 0 ? "Xóa thành công !" : "Lỗi phiên xóa !";
            }
            catch (Exception e) { return "Xóa thất bại !"; }
        }

        public String updateOne(DTO.Staff st)
        {
            try
            {
                String query = "SELECT CMND, POSITION FROM nhom10.STAFF WHERE ID = " + st.ID;
                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();

                cm.Connection = connection;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                reader.Read();

                String CMND = reader.GetString(0);
                String old_Position = reader.GetString(1);

                String private_key_file_name = Security.Site.HASH.SH256(CMND + "private");
                String public_key_file_name = Security.Site.HASH.SH256(CMND + "public");
                String symmetric_key_file_name = Security.Site.HASH.SH256(CMND + "symmetric");

                String private_key = Security.Site.KEYS.Read_file_key(private_key_file_name);
                String public_key = Security.Site.KEYS.Read_file_key(public_key_file_name);
                String symmetric_key = Security.Site.HASH.SH256(st.CMND);

                Security.Site.KEYS.Delete_File(private_key_file_name);
                Security.Site.KEYS.Delete_File(public_key_file_name);
                Security.Site.KEYS.Delete_File(symmetric_key_file_name);

                st.CMND = Security.Site.SYMMETRIC.ENCRYPT(st.CMND, symmetric_key);
                st.Email = Security.Site.SYMMETRIC.ENCRYPT(st.Email, symmetric_key);
                st.Phone = Security.Site.SYMMETRIC.ENCRYPT(st.Phone, symmetric_key);

                symmetric_key = Security.Site.RSA.ENCRYPT(symmetric_key, public_key);

                String private_key_save_file = Security.Site.HASH.SH256(st.CMND + "private");
                String public_key_save_file = Security.Site.HASH.SH256(st.CMND + "public");
                String symmetric_key_save_file = Security.Site.HASH.SH256(st.CMND + "symmetric");

                Security.Site.KEYS.Save_Keys(private_key, private_key_save_file);
                Security.Site.KEYS.Save_Keys(public_key, public_key_save_file);
                Security.Site.KEYS.Save_Keys(symmetric_key, symmetric_key_save_file);

                cm.CommandText = "nhom10.UPDATE_STAFF";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_ID", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = st.ID;
                cm.Parameters.Add("inp_NAME", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = st.Name;
                cm.Parameters.Add("inp_EMAIL", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = st.Email;
                cm.Parameters.Add("inp_CMND", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = st.CMND;
                cm.Parameters.Add("inp_PHONE", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = st.Phone;
                cm.Parameters.Add("inp_BIRTHDAY", OracleDbType.Date, ParameterDirection.Input).Value = st.BirthDay;
                cm.Parameters.Add("inp_GENDER", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = st.Gender;
                cm.Parameters.Add("inp_POSITION", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = st.Position;

                if (cm.ExecuteNonQuery() == 0) return "Cập nhật thất bại !";

                Boolean Done = Security.Site.OLS.Set_Label_Tag_FOR_STAFF(st.ID.ToString(), st.Position);

                if(!Done) return "Cập nhật thất bại !";

                return Source.Account.updateROLE(st.ID, st.Position, old_Position);
            }
            catch (Exception e) {}
            return "Cập nhật thất bại !";
        }

    }
}
//cm.CommandType = CommandType.StoredProcedure;
////String g = Convert.ToBase64String((byte[])Result_Symmetric_Encrypt);
//cm.Parameters.Add("inp_public_key", OracleDbType.Varchar2, ParameterDirection.Input).Value = public_Key;
//cm.Parameters.Add("inp_private_key", OracleDbType.Varchar2, ParameterDirection.Input).Value = Convert.ToBase64String((byte[])Result_Symmetric_Encrypt);
//cm.Parameters.Add("key_symmetric", OracleDbType.Varchar2, 1024, ParameterDirection.Input).Value = key_Symmetric;
//cm.Parameters.Add("id", OracleDbType.Varchar2, 1024, ParameterDirection.Input).Value = id;
//cm.Parameters.Add("RESPONE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
//Respone = cm.Parameters["RESPONE"].Value.ToString();
