﻿using Mono.Data.Sqlite;
using System.Data;

public class Save_string 
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public void Insert_saveString(unitySQLite unitySQLite, int id, string value, string name)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into save_string (id, value, name) " + "values (\"{0}\",\"{1}\",\"{2}\")", id, value, name);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public void StartInsertString(unitySQLite unitySQLite)
    {
        Insert_saveString(unitySQLite, 1, "07.05.2021 20:07:57", "checkPointTime");
        Insert_saveString(unitySQLite, 2, "{ \"\" listIdElementsUpGrade\"\": [ 8] }", "listidUpGradeElements");
    }
    public void UpdateString(unitySQLite unitySQLite, string value, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE save_string set value = @value where id = @id ");
            SqliteParameter P_value = new SqliteParameter("@value", value);
            SqliteParameter P_id = new SqliteParameter("@id", id);
            dbcmd.Parameters.Add(P_id);
            dbcmd.Parameters.Add(P_value);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public string ReadString(unitySQLite unitySQLite, int id)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        string value ="";
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT value " + "FROM save_string where id =" + id;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                value = reader.GetString(0);
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
