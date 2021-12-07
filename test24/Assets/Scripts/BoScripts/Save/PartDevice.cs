using Mono.Data.Sqlite;
using System.Data;

public class PartDevice 
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public int ReadClear_partDevice(unitySQLite unitySQLite, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        int clear = 0;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT clear " + "FROM partDevice where id =" + id;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                clear = reader.GetInt32(0);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            return clear;
        }
    }
    public void UpdateClear_partDevice(unitySQLite unitySQLite, int id, int clear)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE partDevice set clear = @clear where id = @id ");
            SqliteParameter P_id = new SqliteParameter("@id", id);
            SqliteParameter P_clear = new SqliteParameter("@clear", clear);
            dbcmd.Parameters.Add(P_id);
            dbcmd.Parameters.Add(P_clear);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public int Reader_partDevice(unitySQLite unitySQLite, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        int state = 0;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT state " + "FROM partDevice where id =" + id;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                state = reader.GetInt32(0);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            return state;
        }
    }
    public void Update_partDevice(unitySQLite unitySQLite, int id, int state)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE partDevice set state = @state where id = @id ");
            SqliteParameter P_id = new SqliteParameter("@id", id);
            SqliteParameter P_state = new SqliteParameter("@state", state);
            dbcmd.Parameters.Add(P_id);
            dbcmd.Parameters.Add(P_state);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void Insert__partDevice(unitySQLite unitySQLite, int id, int state, int clear)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into partDevice (id, state, clear) " + "values (\"{0}\",\"{1}\",\"{2}\")", id, state, clear);// table name

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void StartInsert_partDevice(unitySQLite unitySQLite)
    {
        Insert__partDevice(unitySQLite, 101, 0, 0);
        Insert__partDevice(unitySQLite, 102, 0, 0);
    }
}
