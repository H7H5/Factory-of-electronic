using Mono.Data.Sqlite;
using System.Data;
public class Basket
{
    private string sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    public void Update_basketCell(unitySQLite unitySQLite, int numberChield, int count, int idDetail)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE basket set count = @count ,idDetail = @idDetail where numberChield = @numberChield ");
            SqliteParameter P_numberChield = new SqliteParameter("@numberChield", numberChield);
            SqliteParameter P_idDetail = new SqliteParameter("@idDetail", idDetail);
            SqliteParameter P_count = new SqliteParameter("@count", count);
            dbcmd.Parameters.Add(P_numberChield);
            dbcmd.Parameters.Add(P_idDetail);
            dbcmd.Parameters.Add(P_count);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public int Reader_basketCellbyNumberChield(unitySQLite unitySQLite, int numberChield)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        int idDetail = 0;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT idDetail " + "FROM basket where numberChield =" + numberChield;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                idDetail = reader.GetInt32(0);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            return idDetail;
        }
    }
    public void InsertBasket(unitySQLite unitySQLite, int id, int numberChield, int count, int idDetail)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into basket (id, numberChield, count, idDetail) " +
                "values (\"{0}\",\"{1}\",\"{2}\",\"{3}\")", id, numberChield, count, idDetail);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }
    public int Reader_basketCount(unitySQLite unitySQLite, int numberChield)
    {
        dbconn = unitySQLite.dbconn;
        dbcmd = unitySQLite.dbcmd;
        int count = 0;
        using (dbconn = new SqliteConnection(unitySQLite.conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT count " + "FROM basket where numberChield =" + numberChield;
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
