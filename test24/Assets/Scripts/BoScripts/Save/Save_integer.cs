using Mono.Data.Sqlite;
using System.Data;

public class Save_integer
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public void Insert_save_integer(unitySQLite unitySQLite, int id, int value, string name)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into save_integer (id, value, name) " + "values (\"{0}\",\"{1}\",\"{2}\")", id, value, name);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void StartInsertInteger(unitySQLite unitySQLite)
    {
        Insert_save_integer(unitySQLite, 1, 390, "money");
        Insert_save_integer(unitySQLite, 2, 1, "raiting");
        Insert_save_integer(unitySQLite, 3, 0, "countOrders");
        Insert_save_integer(unitySQLite, 4, 0, "countSelectedOrders");
        Insert_save_integer(unitySQLite, 5, 2, "sciense");
        Insert_save_integer(unitySQLite, 5, 0, "learning");
    }
    public void UpdateInteger(unitySQLite unitySQLite, int count,int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE save_integer set value = @value where id = @id ");
            SqliteParameter P_count = new SqliteParameter("@value", count);
            SqliteParameter P_id = new SqliteParameter("@id", id);
            dbcmd.Parameters.Add(P_id);
            dbcmd.Parameters.Add(P_count);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public int ReadInteger(unitySQLite unitySQLite, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        int count = 0;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT value " + "FROM save_integer where id =" + id;
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
}
