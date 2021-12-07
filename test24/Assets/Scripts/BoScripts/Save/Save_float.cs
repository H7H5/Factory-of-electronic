using Mono.Data.Sqlite;
using System.Data;

public class Save_float 
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public void Insert_save_float(unitySQLite unitySQLite, float value, string name)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into save_float (value, name) " + "values (\"{0}\",\"{1}\")", value, name);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void StartInsertFloat(unitySQLite unitySQLite)
    {
        Insert_save_float(unitySQLite, 2, "x");
        Insert_save_float(unitySQLite, 1, "y");
        Insert_save_float(unitySQLite, 5, "s");
    }
    public void UpdateFloat(unitySQLite unitySQLite, float value, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE save_float set value = @value where id = @id ");
            SqliteParameter P_value = new SqliteParameter("@value", value);
            SqliteParameter P_id = new SqliteParameter("@id", id);
            dbcmd.Parameters.Add(P_id);
            dbcmd.Parameters.Add(P_value);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public float ReadFloat(unitySQLite unitySQLite, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        float value = 1f;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT value " + "FROM save_float where id =" + id;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                value = reader.GetFloat(0);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            return value;
        }
    }
}
