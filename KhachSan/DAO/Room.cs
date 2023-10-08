using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace KhachSan.DAO
{
    class Room
    {
        public DTO.Room findOne(String id)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "SELECT * FROM nhom10.ROOM WHERE ID = " + id;
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                if (!reader.Read()) return null;

                return new DTO.Room()
                {
                    ID = long.Parse(reader.GetString(0)),
                    Name = reader.GetString(1),
                    Price = double.Parse(reader.GetString(2)),
                    Preset_money = double.Parse(reader.GetString(3)),
                    Status = reader.GetString(4),
                    Type_Room = reader.GetString(5),
                    Max_People = int.Parse(reader.GetString(6))
                };
            }
            catch(Exception e) { return null; }
        }

        public List<DTO.Room> findAll()
        {
            try
            {
                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "SELECT * FROM nhom10.ROOM";
                cm.CommandType = CommandType.Text;
                OracleDataReader reader = cm.ExecuteReader();
                List<DTO.Room> rooms = new List<DTO.Room>();

                while (reader.Read())
                {
                    rooms.Add(new DTO.Room()
                    {
                        ID = long.Parse(reader.GetString(0)),
                        Name = reader.GetString(1),
                        Price = double.Parse(reader.GetString(2)),
                        Preset_money = double.Parse(reader.GetString(3)),
                        Status = reader.GetString(4),
                        Type_Room = reader.GetString(5),
                        Max_People = int.Parse(reader.GetString(6))
                    });
                }
                return rooms;
            }
            catch (Exception e) { return null; }
        }


        public Boolean insertOne(DTO.Room room)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "INSERT_ROOM";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_NAME", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = room.Name;
                cm.Parameters.Add("inp_PRICE", OracleDbType.Varchar2, 12, ParameterDirection.Input).Value = room.Price;
                cm.Parameters.Add("inp_PRESET_MONEY", OracleDbType.Varchar2, 12, ParameterDirection.Input).Value = room.Preset_money;
                cm.Parameters.Add("inp_STATUS", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = room.Status;
                cm.Parameters.Add("inp_TYPE_ROOM", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = room.Type_Room;
                cm.Parameters.Add("inp_MAX_PEOPLE", OracleDbType.Varchar2, 3, ParameterDirection.Input).Value = room.Max_People;
                cm.Parameters.Add("RESPONE", OracleDbType.Boolean).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                Boolean Done = Boolean.Parse(cm.Parameters["RESPONE"].Value.ToString());

                return Done;
            }
            catch (Exception e) { return false; }
        }


        public Boolean updateOne(DTO.Room room)
        {
            try
            {
                OracleConnection connection = Access.Connect();
                OracleCommand cm = connection.CreateCommand();
                cm.CommandText = "UPDATE_ROOM";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("inp_ID", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = room.ID;
                cm.Parameters.Add("inp_NAME", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = room.Name;
                cm.Parameters.Add("inp_PRICE", OracleDbType.Varchar2, 12, ParameterDirection.Input).Value = room.Price;
                cm.Parameters.Add("inp_PRESET_MONEY", OracleDbType.Varchar2, 12, ParameterDirection.Input).Value = room.Preset_money;
                cm.Parameters.Add("inp_STATUS", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = room.Status;
                cm.Parameters.Add("inp_TYPE_ROOM", OracleDbType.NVarchar2, 50, ParameterDirection.Input).Value = room.Type_Room;
                cm.Parameters.Add("inp_MAX_PEOPLE", OracleDbType.Varchar2, 3, ParameterDirection.Input).Value = room.Max_People;
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
                cm.CommandText = "DELETE FROM nhom10.ROOM WHERE ID = " + id;
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();
                return cm.ExecuteNonQuery() != 0 ? true : false;
            }
            catch (Exception e) { return false; }
        }
    }
}
