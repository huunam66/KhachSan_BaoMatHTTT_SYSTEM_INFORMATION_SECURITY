using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
namespace KhachSan.DAO
{
    class Account
    {
        public DTO.Account findOne(long id)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                String query = "SELECT * FROM nhom10.ACCOUNT WHERE STAFF_ID = " + id;
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                if (!reader.Read()) return null;
                return new DTO.Account()
                {
                    Staff_ID = int.Parse(reader.GetString(0).ToString()),
                    Username = reader.GetString(1).ToString(),
                    Password = reader.GetString(2).ToString(),
                    Role = reader.GetString(3).ToString()
                };
            }
            catch(Exception e) { return null; }
        }
        public DTO.Account findOne(String Username)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                String query = "SELECT * FROM nhom10.ACCOUNT WHERE USERNAME = '" + Username + "'";
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                if (!reader.Read()) return null;
                return new DTO.Account()
                {
                    Staff_ID = int.Parse(reader.GetString(0).ToString()),
                    Username = reader.GetString(1).ToString(),
                    Password = reader.GetString(2).ToString(),
                    Role = reader.GetString(3).ToString()
                };
            }
            catch(Exception e) { return null; }
        }

        public List<DTO.Account> find()
        {
            try
            {
                OracleConnection connection = Access.Connect();
                String query = "SELECT * FROM nhom10.ACCOUNT";
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                List<DTO.Account> accounts = new List<DTO.Account>();
                while (reader.Read())
                {
                    accounts.Add(new DTO.Account()
                    {
                        Staff_ID = int.Parse(reader.GetString(0).ToString()),
                        Username = reader.GetString(1).ToString(),
                        Password = reader.GetString(2).ToString(),
                        Role = reader.GetString(3).ToString()
                    });
                }
                return accounts;
            }
            catch(Exception e) { return null; }
        }

        public Boolean Can_Login(String Username, String Password)
        {
            Access.setUser(Username);
            Access.setPassword(Password);
            try
            {
                Access.Connect();
                return true;
            }
            catch (Exception e) { return false; }
        }

        public String Create_Account(DTO.Account ac)
        {
            try
            {
                if (findOne(ac.Username) != null) return "Tài khoản đã tồn tại !";

                OracleConnection connection = Access.Connect();
                String query = "CREATE USER " + ac.Username + " IDENTIFIED BY " + ac.Password;
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;

                String Respone = "Lỗi tạo tài khoản !";

                if (cm.ExecuteNonQuery() != 0)
                {
                    Respone = Security.Site.GRANT.GRANT_PRIVILEGES_ROLES(ac) ? "TRUE" : "FALSE";
                    if (!Respone.Equals("TRUE")) return "Gán quyền thất bại !";

                    String Password_MD5_Encrypted = Security.Site.MD5.Encrypt(ac.Password);

                    query = "INSERT INTO nhom10.ACCOUNT(STAFF_ID, USERNAME, PASSWORD, ROLE) "
                                    + "VALUES(" + ac.Staff_ID + ", '" + ac.Username + "', '" + Password_MD5_Encrypted + "', '" + ac.Role + "')";
                    cm.CommandText = query;
                    cm.CommandType = CommandType.Text;

                    if (cm.ExecuteNonQuery() == 0) return "Lỗi tạo tài khoản !";

                    // LBACSYS gán nhãn truy cập tùy quyền cho USER (Tài khoản nhân viên) vừa tạo

                    Boolean Done_set_level = Security.Site.OLS.Set_level_OLS_FOR_USER(ac.Username, ac.Role);

                    return Done_set_level ? "Tạo tài khoản thành công !" : "Lỗi tạo tài khoản !";
                }
                String q = "DROP USER " + ac.Username + " CASCADE";
                cm.CommandText = q;
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();
                return Respone;
            }
            catch (Exception e) { }
            return "Tạo tài khoản thất bại !";
        }

        public String Change_Password(DTO.Account ac)
        {
            try
            {
                String query = "ALTER USER " + ac.Username + " IDENTIFIED BY " + ac.Password;
                OracleConnection connection = DAO.Access.Connect();
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                if (cm.ExecuteNonQuery() == 0) return "Đổi mật khẩu thất bại !";

                String Password_MD5_Encrypted = Security.Site.MD5.Encrypt(ac.Password);

                query = "UPDATE nhom10.ACCOUNT SET PASSWORD = '" + Password_MD5_Encrypted
                    + "' WHERE STAFF_ID = " + ac.Staff_ID;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                return cm.ExecuteNonQuery() != 0 ? "Đổi mật khẩu thành công !" : "Đổi mật khẩu thất bại !";
            }
            catch (Exception e) { return "Đổi mật khẩu thất bại !"; }
        }

        public String Drop_User(long id)
        {
            try
            {
                DTO.Account ac = findOne(id);
                if (ac == null) return "Không tồn tại tài khoản !";

                String query = "DROP USER " + ac.Username + " CASCADE";
                OracleConnection connection = DAO.Access.Connect();
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                if (cm.ExecuteNonQuery() == 0) return "Xóa tài khoản thất bại !";

                query = "DELETE FROM nhom10.ACCOUNT WHERE STAFF_ID = " + ac.Staff_ID;
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;
                return cm.ExecuteNonQuery() != 0 ? "Xóa dữ liệu tài khoản thành công !" : "Xóa dữ liệu tài khoản thất bại !";
            }
            catch (Exception e) { return "Xóa tài khoản thất bại !"; }
        }

        public String updateROLE(long id, String new_ROLE, String old_ROLE)
        {
            DTO.Account ac = findOne(id);
            if (ac == null) return "Không tìm thấy tài khoản !";
            try
            {
                ac.Role = new_ROLE;
                String query = "UPDATE nhom10.ACCOUNT SET ROLE = '" + ac.Role + "' WHERE STAFF_ID = " + ac.Staff_ID;
                OracleConnection connection = DAO.Access.Connect();
                OracleCommand cm = new OracleCommand(query, connection);
                cm.CommandType = CommandType.Text;
                if(cm.ExecuteNonQuery() == 0) return "Lỗi cập nhật dữ liệu tài khoản !";

                String Respone = Security.Site.REVOKE.REVOKE_PRIVILEGES_ROLES(ac.Username, old_ROLE) ? "TRUE" : "FALSE";
                if (!Respone.Equals("TRUE")) return "Gỡ bỏ quyền cũ thất bại !";

                Respone = Security.Site.GRANT.GRANT_PRIVILEGES_ROLES(ac) ? "TRUE" : "FALSE";
                if (!Respone.Equals("TRUE")) return "Gán quyền mới thất bại !";

                // LBACSYS gán nhãn truy cập tùy quyền cho USER (Tài khoản nhân viên) vừa cập nhật POSITION
                Boolean Done_set_level = Security.Site.OLS.Set_level_OLS_FOR_USER(ac.Username, ac.Role);

                return Done_set_level ? "Cập nhật lại quyền User thành công !" : "Cập nhật lại quyền User thất bại !";
            }
            catch(Exception e) { return "Cập nhật lại quyền User thất bại !"; }
        }
    }
}
