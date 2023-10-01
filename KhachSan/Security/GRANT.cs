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
        public Boolean GRANT_PRIVILEGES_ROLES()
        {
            OracleConnection connection = DAO.Access.Connect_To("SYS", "123");

        }


    }
}
