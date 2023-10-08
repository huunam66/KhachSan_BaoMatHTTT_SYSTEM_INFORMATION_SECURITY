using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
using System.Data;
namespace KhachSan.DAO
{
    class Account
    {
        public DTO.Account findOne(long id)
        {
            try
            {
                OracleConnection connection = Access.getUser() != null ? Access.Connect() : Access.Connect_To("nhom10", "123");
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
                OracleConnection connection = Access.getUser() != null ? Access.Connect() : Access.Connect_To("nhom10", "123");
                String query = "SELECT * FROM nhom10.ACCOUNT WHERE UPPER(USERNAME) = '" + Username.ToUpper() + "'";
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
            try
            {
                if (!Username.Equals("nhom10"))
                {
                    DTO.Account ac = findOne(Username);
                    if (ac == null) return false;

                    OracleConnection connection = Access.Connect_To("nhom10", "123");
                    OracleCommand cm = new OracleCommand();

                    String query = "SELECT CMND FROM nhom10.STAFF WHERE ID = " + ac.Staff_ID;
                    cm.Connection = connection;
                    cm.CommandText = query;
                    cm.CommandType = CommandType.Text;
                    OracleDataReader reader = cm.ExecuteReader();
                    reader.Read();
                    String CMND = reader.GetString(0);
                    String public_key_file = Security.Site.HASH.SH256(CMND + "public");
                    String public_key = Security.Site.KEYS.Read_file_key(public_key_file);

                    Boolean verify = Security.Site.AUTHEN.VERIFY_DATA(Password, ac.Password, public_key);

                    if (verify)
                    {
                        Access.setUser(Username);
                        Access.setPassword(Password);
                        Access.Connect();
                    }

                    return verify;
                }
                Access.setUser(Username);
                Access.setPassword(Password);
                Access.Connect();
            }
            catch (Exception e) { return false; }
            return true;
        }

        public String Create_Account(DTO.Account ac)
        {
            try
            {
                if (findOne(ac.Username) != null) return "Tài khoản đã tồn tại !";

                OracleConnection connection = Access.Connect();
                String query = "CREATE USER " + ac.Username + " IDENTIFIED BY " + ac.Password;
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                String Respone = "Lỗi tạo tài khoản !";

                if (cm.ExecuteNonQuery() != 0)
                {
                    DTO.Staff staff = DAO.Source.Staff.findOne(ac.Staff_ID);
                    String private_key_file = Security.Site.HASH.SH256(staff.CMND + "private");
                    String private_key = Security.Site.KEYS.Read_file_key(private_key_file);

                    String Password_SIGN = Security.Site.AUTHEN.SIGN_DATA(ac.Password, private_key);

                    cm.CommandText = "nhom10.INSERT_ACCOUNT";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("inp_STAFF_ID", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = ac.Staff_ID;
                    cm.Parameters.Add("inp_USERNAME", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = ac.Username;
                    cm.Parameters.Add("inp_PASSWORD", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = Password_SIGN;
                    cm.Parameters.Add("inp_ROLE", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = ac.Role;
                    cm.Parameters.Add("RESPONE", OracleDbType.Boolean).Direction = ParameterDirection.Output;
                    cm.ExecuteNonQuery();

                    Boolean Done = Boolean.Parse(cm.Parameters["RESPONE"].Value.ToString());

                    if (!Done) return "Lỗi tạo tài khoản !";

                    Done = Security.Site.GRANT.GRANT_PRIVILEGES_ROLES(ac);
                    if (!Done) return "Gán quyền thất bại !";

                    // LBACSYS gán nhãn truy cập tùy quyền cho USER (Tài khoản nhân viên) vừa tạo

                    Done = Security.Site.OLS.Set_level_OLS_FOR_USER(ac.Username, ac.Role);

                    return Done ? "Tạo tài khoản thành công !" : "Gán nhãn bảo mật thất bại, Lỗi tạo tài khoản !";
                }
                query = "DROP USER " + ac.Username + " CASCADE";
                cm.CommandText = query;
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

                DTO.Staff staff = DAO.Source.Staff.findOne(ac.Staff_ID);
                String private_key_file = Security.Site.HASH.SH256(staff.CMND + "private");
                String private_key = Security.Site.KEYS.Read_file_key(private_key_file);

                String Password_SIGN = Security.Site.AUTHEN.SIGN_DATA(ac.Password, private_key);

                query = "UPDATE nhom10.ACCOUNT SET PASSWORD = '" + Password_SIGN
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

                DTO.Account ac_current = findOne(Access.getUser());
                if (ac_current != null
                    && ac_current.Username.ToUpper().Equals(ac.Username.ToUpper()))
                {
                    return "Bạn không thể xóa chính bạn !";
                }

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
            if (ac == null) return "Không tìm thấy tài khoản !, Cập nhật thông tin cá nhân thành công !";
            try
            {
                ac.Role = new_ROLE;
                OracleConnection connection = DAO.Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "nhom10.UPDATE_ROLE";
                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("inp_STAFF_ID", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = ac.Staff_ID;
                cm.Parameters.Add("inp_ROLE", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = ac.Role;
                cm.Parameters.Add("RESPONE", OracleDbType.Boolean).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                Boolean Done = Boolean.Parse(cm.Parameters["RESPONE"].Value.ToString());

                if (!Done) return "Lỗi cập nhật dữ liệu tài khoản !";

                Done = Security.Site.REVOKE.REVOKE_PRIVILEGES_ROLES(ac.Username, old_ROLE);
                if (!Done) return "Gỡ bỏ quyền cũ thất bại !";

                Done = Security.Site.GRANT.GRANT_PRIVILEGES_ROLES(ac);
                if (!Done) return "Gán quyền mới thất bại !";

                // LBACSYS gán nhãn truy cập tùy quyền cho USER (Tài khoản nhân viên) vừa cập nhật POSITION
                Done = Security.Site.OLS.Set_level_OLS_FOR_USER(ac.Username, ac.Role);

                return Done ? "Cập nhật thành công !" : "Cập nhật nhãn bảo mật thất bại, Cập nhật thất bại !";
            }
            catch(Exception e) { return "Cập nhật lại quyền User thất bại !"; }
        }
    }
}
