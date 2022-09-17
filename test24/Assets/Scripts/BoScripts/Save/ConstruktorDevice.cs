using Mono.Data.Sqlite;
using System.Data;

public class ConstruktorDevice 
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public string Reader_ConstruktorDevice(unitySQLite unitySQLite, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        string data = " ";
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT data " + "FROM construktor_device where id =" + id;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                data = reader.GetString(0);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            return data;
        }
    }
    public void Update_ConstrukrotDevice(unitySQLite unitySQLite, int id, string data)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE construktor_device set data = @data where id = @id ");
            SqliteParameter P_id = new SqliteParameter("@id", id);
            SqliteParameter P_data = new SqliteParameter("@data", data);
            dbcmd.Parameters.Add(P_id);
            dbcmd.Parameters.Add(P_data);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void InsertConstruktor(unitySQLite unitySQLite, int id, string data)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into construktor_device (id, data) " + "values (\"{0}\",\"{1}\")", id, data);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void StartInsertConstruktorDevice(unitySQLite unitySQLite)
    {
        string data = "";
        for (int i = 0; i < 15; i++)
        {
            InsertConstruktor(unitySQLite, i, data);
        }
        InsertConstruktor(unitySQLite, 101, data);
        InsertConstruktor(unitySQLite, 102, data);
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 3, \"\"array\"\": [  0,  0, 0],\"\"id\"\": 1,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
    }
}
