using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class Machines_JSON 
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public void Insert_function(unitySQLite unitySQLite, string data)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into machines_JSON (data) " + "values (\"{0}\")", "dfdf");
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void Update_function(unitySQLite unitySQLite, int id, string data)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE machines_JSON set data = @data where id = @id ");
            SqliteParameter P_id = new SqliteParameter("@id", id);
            SqliteParameter P_data = new SqliteParameter("@data", data);
            dbcmd.Parameters.Add(P_id);
            dbcmd.Parameters.Add(P_data);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void Reader_functionAllid(unitySQLite unitySQLite, GameObject MachinParent)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        int supper_id;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT id " + "FROM machines_JSON";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            int temp_id = 0;
            while (reader.Read())
            {
                supper_id = reader.GetInt32(0);
                if (supper_id > temp_id)
                {
                    temp_id = supper_id;
                }

            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            MachinParent.GetComponent<MachineParentScript>().maxId_JSON(temp_id);
        }
    }
    public void Delete_function(unitySQLite unitySQLite, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        if (id != 0)
        {
            using (dbconn = new SqliteConnection(unitySQLite.conn))
            {
                dbconn.Open(); 
                IDbCommand dbcmd = dbconn.CreateCommand();
                string sqlQuery = "DELETE FROM machines_JSON where id =" + id;
                dbcmd.CommandText = sqlQuery;
                IDataReader reader = dbcmd.ExecuteReader();
                dbcmd.Dispose();
                dbcmd = null;
                dbconn.Close();
            }
        }
    }
    public void Reader_function(unitySQLite unitySQLite, GameObject MachinParent)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        string data;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT data " + "FROM machines_JSON";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                data = reader.GetString(0);
                MachinParent.GetComponent<MachineParentScript>().CreateOneMachineJSON(data);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }
    }
}
