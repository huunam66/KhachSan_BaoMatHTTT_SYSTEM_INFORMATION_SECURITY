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


        private String Symmetric_Encrypt(String p_input, String p_key)
        {
            OracleConnection connection = Access.Connect();
            OracleCommand cm = new OracleCommand("nhom10.ENCRYPT_DOIXUNG", connection);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("p_input", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = p_input;
            cm.Parameters.Add("p_key", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = p_key;
            cm.Parameters.Add("Result_ENCRYPT", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;
            return cm.ExecuteNonQuery() != 0 ? cm.Parameters["Result_ENCRYPT"].Value.ToString() : null;
        }


        public DTO.Key_Secure find_Keys(String id)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                String query = "SELECT * FROM nhom10.KEY_SECURE WHERE STAFF_ID = " + id;
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();

                if (!reader.Read()) return null;

                DTO.Key_Secure keys = new DTO.Key_Secure()
                {
                    STAFF_ID = int.Parse(reader.GetString(0)),
                    public_key = reader.GetString(1),
                    private_key = reader.GetString(2),
                    symmetric_key = reader.GetString(3)
                };

                cm.CommandText = "nhom10.DECRYPT_DOIXUNG";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("p_input", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = keys.private_key;
                cm.Parameters.Add("p_key", OracleDbType.Varchar2, 2000, ParameterDirection.Input).Value = keys.symmetric_key;
                cm.Parameters.Add("Result_DECRYPT", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                if (cm.ExecuteNonQuery() == 0) return null;

                keys.private_key = cm.Parameters["Result_DECRYPT"].Value.ToString();

                return keys;
            }
            catch(Exception e) { return null; }
        }

        public DTO.Staff findOne(String id)
        {
            try
            {
                DTO.Key_Secure keys = find_Keys(id);

                if (keys == null) return null;

                Security.Site.RSA.Import(keys.private_key);

                OracleConnection connection = Access.Connect();

                String query = "SELECT * FROM nhom10.STAFF WHERE ID = " + id;
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;

                OracleDataReader reader = cm.ExecuteReader();

                if (!reader.Read()) return null;

                return new DTO.Staff()
                {
                    ID = long.Parse(reader.GetString(0)),
                    Name = reader.GetString(1),

                    Email = Security.Site.RSA.Decrypt(reader.GetString(2)),
                    CMND = Security.Site.RSA.Decrypt(reader.GetString(3)),
                    Phone = Security.Site.RSA.Decrypt(reader.GetString(4)),

                    BirthDay = DateTime.Parse(reader.GetString(5)),
                    Gender = reader.GetString(6),
                    Position = reader.GetString(7)
                };
            }
            catch(Exception e) { return null; }
        }

        public String insertOne(DTO.Staff staff)
        {
            try
            {
                RSACryptoServiceProvider rsp = new RSACryptoServiceProvider();

                //Truyền vào tham số khóa riêng và công khai
                Security.Site.RSA.Create(
                    rsp.ExportParameters(true),
                    rsp.ExportParameters(false)
                );

                //staff.Name = rsa.Encrypt(staff.Name);
                staff.Email = Security.Site.RSA.Encrypt(staff.Email);
                staff.CMND = Security.Site.RSA.Encrypt(staff.CMND);
                staff.Phone = Security.Site.RSA.Encrypt(staff.Phone);
                //staff.Gender = rsa.Encrypt(staff.Gender);

                OracleConnection connection = Access.Connect();

                String query = "INSERT INTO nhom10.STAFF (NAME, EMAIL, CMND, PHONE, BIRTHDAY, GENDER, POSITION) "
                            + "VALUES('" + staff.Name + "', '" + staff.Email + "', '" + staff.CMND + "', '" + staff.Phone + "', " +
                            "TO_DATE('" + staff.BirthDay.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd'), '" + staff.Gender + "', '" + staff.Position + "')";

                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                int Respone = cm.ExecuteNonQuery();

                if (Respone == 0) return "Error insert_Staff !";

                query = "SELECT ID FROM nhom10.STAFF WHERE CMND = '" + staff.CMND + "'";
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                reader.Read();

                String id = reader.GetString(0);

                if (id == null) return "Error find_Staff !";

                // User quản lý gán nhãn cho trường dữ liệu nhân viên vừa thêm theo chức vụ
                // Nhãn 45 => Nhân viên
                // Nhãn 65 => Kế toán
                // Nhãn 95 => Quản lý (Cao nhất)
                Security.Site.OLS.Set_Label_Tag_FOR_STAFF(id, staff.Position);

                //=======================
                String symmetric_key = Security.Site.MD5.Encrypt(staff.CMND);

                //=======================
                String private_Key = rsp.ToXmlString(true); //Lấy ra khóa riêng tư
                String public_Key = rsp.ToXmlString(false); //Lấy ra khóa công khai
                //MessageBox.Show(private_Key, "", MessageBoxButtons.OK);
                String Result_Symmetric_Encrypt = Symmetric_Encrypt(private_Key, symmetric_key);

                if (Result_Symmetric_Encrypt == null) return "Error Result_Symmetric_Encrypt !";

                int len = Result_Symmetric_Encrypt.Length;
                query = "INSERT INTO KEY_SECURE(STAFF_ID, PUBLIC_KEY, PRIVATE_KEY, SYMMETRIC_KEY) " +
                        "VALUES(" + id + ", '" + public_Key + "', '" + Result_Symmetric_Encrypt + "', '" + symmetric_key + "')";

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                Respone = cm.ExecuteNonQuery();


                if (Respone == 0) return "Error insert_key !";

                return "Thêm mới thành công !";
            }
            catch (Exception e)
            { return "Thêm mới thất bại !"; }
        }

        public String deleteOne(String id)
        {
            try
            {
                String query = "SELECT USERNAME FROM nhom10.ACCOUNT WHERE STAFF_ID = " + id;
                OracleConnection connection = Access.Connect();
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                if (reader.Read())
                {
                    query = "DROP USER " + reader.GetString(0) + " CASCADE";
                    cm.CommandText = query;
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                }

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
                OracleConnection connection = Access.Connect();
                String query = "SELECT POSITION FROM nhom10.STAFF WHERE ID = " + st.ID;
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                reader.Read();
                String Old_Position = reader.GetString(0);

                DTO.Key_Secure keys = find_Keys(st.ID.ToString());
                if (keys == null) return "Không tìm thấy khóa !";

                Security.Site.RSA.Import(keys.private_key);
                st.CMND = Security.Site.RSA.Encrypt(st.CMND);
                st.Email = Security.Site.RSA.Encrypt(st.Email);
                st.Phone = Security.Site.RSA.Encrypt(st.Phone);

                query = "UPDATE nhom10.STAFF SET NAME = '" + st.Name + "', EMAIL = '" + st.Email + "', CMND = '" + st.CMND + "', "
                            + "PHONE = '" + st.Phone + "', BIRTHDAY = TO_DATE('" + st.BirthDay.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd'), "
                            + "GENDER = '" + st.Gender + "', POSITION = '" + st.Position + "' WHERE ID = " + st.ID;

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                if (cm.ExecuteNonQuery() == 0) return "Lỗi cập nhât !";

                String Symmetric_key = Security.Site.MD5.Encrypt(st.CMND);

                String private_key_Symmetrict_encrypted = Symmetric_Encrypt(keys.private_key, Symmetric_key);

                query = "UPDATE nhom10.KEY_SECURE SET PUBLIC_KEY = '" + keys.public_key + "', PRIVATE_KEY = '" + private_key_Symmetrict_encrypted + "', " +
                        "SYMMETRIC_KEY = '" + Symmetric_key + "' WHERE STAFF_ID = " + st.ID;

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                return cm.ExecuteNonQuery() != 0 ? "Cập nhật thành công !" : "Cập nhật không thành công !";
            }
            catch (Exception e) { return "Cập nhật thất bại !"; }
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
