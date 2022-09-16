using Mono.Data.Sqlite;
using System.Data;

public class Devices 
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public int Reader_Device(unitySQLite unitySQLite, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        int count = 0;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT count " + "FROM devices where id =" + id;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            return count;
        }
    }
    public void UpdateDevice(unitySQLite unitySQLite, int id, int count, string name)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE devices set count = @count ,name = @name where id = @id ");
            SqliteParameter P_id = new SqliteParameter("@id", id);
            SqliteParameter P_name = new SqliteParameter("@name", name);
            SqliteParameter P_count = new SqliteParameter("@count", count);
            dbcmd.Parameters.Add(P_id);
            dbcmd.Parameters.Add(P_name);
            dbcmd.Parameters.Add(P_count);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void Insert_device(unitySQLite unitySQLite, int id, int count, string name)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into devices (id, count, name) " + "values (\"{0}\",\"{1}\",\"{2}\")", id, count, name);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void StartInsertDevices(unitySQLite unitySQLite)
    {
        Insert_device(unitySQLite,1, 0, "flashlight");
        Insert_device(unitySQLite,2, 0, "lamp");
        Insert_device(unitySQLite,3, 0, "chandelier");
        Insert_device(unitySQLite,4, 0, "garland");
        Insert_device(unitySQLite, 5, 0, "solder");
        Insert_device(unitySQLite, 6, 0, "curling iron");
        Insert_device(unitySQLite, 7, 0, "sampler");
        Insert_device(unitySQLite, 8, 0, "blender");
        Insert_device(unitySQLite, 9, 0, "meat grinder");
        Insert_device(unitySQLite, 10, 0, "teapot");
        Insert_device(unitySQLite, 11, 0, "fan heater");
        Insert_device(unitySQLite, 12, 0, "electric grill");
        Insert_device(unitySQLite, 13, 0, "fan");
        Insert_device(unitySQLite, 14, 0, "electric shaver");
        Insert_device(unitySQLite, 15, 0, "kettle");
        
    }
}
