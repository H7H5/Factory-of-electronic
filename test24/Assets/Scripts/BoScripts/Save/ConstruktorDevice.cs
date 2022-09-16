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
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 3, \"\"array\"\": [  0,  0, 0],\"\"id\"\": 1,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
        InsertConstruktor(unitySQLite,1, data);
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 3, \"\"array\"\": [  0,  0, 0],\"\"id\"\": 2,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
        InsertConstruktor(unitySQLite, 2, data);
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 5, \"\"array\"\": [  0,  0, 0, 0, 0],\"\"id\"\": 3,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
        InsertConstruktor(unitySQLite, 3, data);
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 9, \"\"array\"\": [0, 0, 0, 0, 0, 0, 0, 0, 0],\"\"id\"\": 4,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
        InsertConstruktor(unitySQLite, 4, data);
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 2, \"\"array\"\": [ 0, 0],\"\"id\"\": 5,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
        InsertConstruktor(unitySQLite, 5, data);
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 4, \"\"array\"\": [ 0, 0, 0, 0],\"\"id\"\": 6,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
        InsertConstruktor(unitySQLite, 6, data);
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 11, \"\"array\"\": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],\"\"id\"\": 101,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
        InsertConstruktor(unitySQLite, 101, data);
        //data = "{\"\"rectWidth\"\": 0, \"\"rectHeight\"\": 0,\"\"countDevice\"\": 10, \"\"array\"\": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],\"\"id\"\": 102,   \"\"activation\"\": 0,\"\"data\"\": \"\"\"\", \"\"unitySQLite\"\": {  \"\"instanceID\"\": 14990 }}";
        InsertConstruktor(unitySQLite, 102, data);

        InsertConstruktor(unitySQLite, 7, data);
        InsertConstruktor(unitySQLite, 8, data);
        InsertConstruktor(unitySQLite, 9, data);
        InsertConstruktor(unitySQLite, 10, data);
        InsertConstruktor(unitySQLite, 11, data);
        InsertConstruktor(unitySQLite, 12, data);
        InsertConstruktor(unitySQLite, 13, data);
        InsertConstruktor(unitySQLite, 14, data);
        InsertConstruktor(unitySQLite, 15, data);
    }
}
