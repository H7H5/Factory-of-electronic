using Mono.Data.Sqlite;
using System.Data;

public class Elements 
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public int Reader_element(unitySQLite unitySQLite,int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        int count = 0;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT count " + "FROM elements where id =" + id;
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
    public void Insert_element(unitySQLite unitySQLite, int id, int count, string name)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into elements (id, count, name) " + "values (\"{0}\",\"{1}\",\"{2}\")", id, count, name);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void StartInsertElements(unitySQLite unitySQLite)
    {
        Insert_element(unitySQLite, 1, 0, "rezistor");
        Insert_element(unitySQLite,  2, 0, "capacitor");
        Insert_element(unitySQLite,  3, 0, "tranzistor");
        Insert_element(unitySQLite,  4, 0, "battery");
        Insert_element(unitySQLite,  5, 0, "switch");
        Insert_element(unitySQLite,  6, 0, "light_bulb");
        Insert_element(unitySQLite,  7, 0, "variable capacitor");
        Insert_element(unitySQLite,  8, 0, "led");
        Insert_element(unitySQLite,  9, 0, "electric_plug");
        Insert_element(unitySQLite, 10, 0, "button");
        Insert_element(unitySQLite, 11, 0, "diode");
        Insert_element(unitySQLite, 12, 0, "heating element");
        Insert_element(unitySQLite, 13, 0, "temperature_regulator");
    }
    public void UpdateElement(unitySQLite unitySQLite, int id, int count, string name)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE elements set count = @count ,name = @name where id = @id ");
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
}
