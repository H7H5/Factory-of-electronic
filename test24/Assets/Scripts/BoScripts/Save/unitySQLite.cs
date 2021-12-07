using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
public class unitySQLite : MonoBehaviour
{
    public string conn;
    public IDbConnection dbconn;
    public IDbCommand dbcmd;
    public GameObject MachinParent;
    private IDataReader reader;
    private string DatabaseName = "mydb.s3db";
    private Basket basket = new Basket();
    private ConstruktorDevice construktorDevice = new ConstruktorDevice();
    private Devices devices = new Devices();
    private Elements elements = new Elements();
    private Save_integer save_Integer = new Save_integer();
    private Save_float save_Float = new Save_float();
    private PartDevice partDevice = new PartDevice();
    private Orders orders = new Orders();
    private Save_string save_String = new Save_string();
    private Machines_JSON machines_JSON = new Machines_JSON();
    private Machine_device machine_Device = new Machine_device();
    private void Awake()
    {
#if UNITY_EDITOR
        string filepath = Application.dataPath + "/Plugins/" + DatabaseName;
        conn = "URI=file:" + filepath;
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
#elif UNITY_ANDROID
        ///*
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        if (!File.Exists(filepath))
        {
            // If not found on android will create Tables and database
            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/mydb");
            // UNITY_ANDROID
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/mydb.s3db");
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }
        conn = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        string query = "CREATE TABLE machines_JSON (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, data varchar(2000))";
        string query1 = "CREATE TABLE elements (super_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, count integer, name varchar(20), temp_int integer, temp_string varchar(20))";
        string query2 = "CREATE TABLE devices (super_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, count integer, name varchar(20), temp_int integer, temp_string varchar(20))";
        string query3 = "CREATE TABLE save_integer (super_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, value integer, name varchar(20))";
        string query4 = "CREATE TABLE construktor_device (super_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, data varchar(500))";
        string query5 = "CREATE TABLE basket (super_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, numberChield integer, count integer, idDetail integer)";
        string query6 = "CREATE TABLE partDevice (superid INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, state integer, clear integer)";
        string query7 = "CREATE TABLE orders (superid INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, data varchar(1000))";
        string query8 = "CREATE TABLE save_string (super_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, value varchar(200), name varchar(100))";
        string query9 = "CREATE TABLE machines_device (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, data varchar(2000))";
        string query10 = "CREATE TABLE save_float (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name varchar(200), value float)";
        try
        {
            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query; 
            reader = dbcmd.ExecuteReader(); 

            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query1; 
            reader = dbcmd.ExecuteReader(); 

            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query2; 
            reader = dbcmd.ExecuteReader(); 

            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query3; 
            reader = dbcmd.ExecuteReader(); 

            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query4;
            reader = dbcmd.ExecuteReader(); 

            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query5; 
            reader = dbcmd.ExecuteReader(); 

            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query6; 
            reader = dbcmd.ExecuteReader();

            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query7;
            reader = dbcmd.ExecuteReader();

            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query8; 
            reader = dbcmd.ExecuteReader(); 

            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query9; 
            reader = dbcmd.ExecuteReader(); 

            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query10; 
            reader = dbcmd.ExecuteReader();

            StartInsertInteger();
            StartInsertElements();
            StartInsertDevices();
            StartInsertConstruktorDevice();
            StartInsertBasket();
            StartInsert_partDevice();
            Start_Orders();
            StartInsertString();
            StartInsertFloat();
            
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
#endif
    }
    public void Insert_functionJSON(string data)
    {
        machines_JSON.Insert_function(this, data);
    }
    public void Update_functionJSON(int id, string data)
    {
        machines_JSON.Update_function(this, id, data);
    }
    public void Reader_functionAllidJSON()
    {
        machines_JSON.Reader_functionAllid(this, MachinParent);
    }
    public void Delete_functionJSON(int id)
    {
        machines_JSON.Delete_function(this, id);
    }
    public void Reader_functionJSON()
    {
        machines_JSON.Reader_function(this, MachinParent);
    }
    ///////////////
    public void Insert_functionMachineDevice(string data)
    {
        machine_Device.Insert_function(this, data);
    }
    public void Update_functionMachineDevice(int id, string data)
    {
        machine_Device.Update_function(this, id, data);
    }
    public void Reader_functionAllidMachineDevice()
    {
        machine_Device.Reader_functionAllid(this, MachinParent);
    }
    public void Delete_functionMachineDevice(int id)
    {
        machine_Device.Delete_function(this, id);
    }
    public void Reader_functionMachineDevice()
    {
        machine_Device.Reader_function(this, MachinParent);
    }
    ///////////////
    public void UpdateElement(int id, int count, string name)
    {
        elements.UpdateElement(this, id, count, name);
    }
    public void StartInsertElements()
    {
        elements.StartInsertElements(this);
    }
    public void Insert_element(int id, int count, string name)
    {
        elements.Insert_element(this, id, count, name);
    }
    public int Reader_element(int id)
    {
        return elements.Reader_element(this, id);
    }
    //////////////////
    public void StartInsertDevices()
    {
        devices.StartInsertDevices(this);
    }
    public void Insert_device(int id, int count, string name)
    {
        devices.Insert_device(this, id,count,name);
    }
    public void UpdateDevice(int id, int count, string name)
    {
        devices.UpdateDevice(this, id, count, name);
    }
    public int Reader_Device(int id)
    {
        return devices.Reader_Device(this,id);
    }
    ////////////////////////////
    public void StartInsertInteger()
    {
        save_Integer.StartInsertInteger(this);
    }
    public void UpdateInteger(int count, int id)
    {
        save_Integer.UpdateInteger(this, count, id);
    }
    public int ReadInteger(int id)
    {
        return save_Integer.ReadInteger(this, id);
    }
    ////////////////////////////
    public void StartInsertFloat()
    {
        save_Float.StartInsertFloat(this);
    }
    public void UpdateFloat(float value, int id)
    {
        save_Float.UpdateFloat(this, value, id);
    }
    public float ReadFloat(int id)
    {
        return save_Float.ReadFloat(this, id);
    }
    //////////////////
    public void StartInsertString()
    {
        save_String.StartInsertString(this);
    }
    public void UpdateString(string value, int id)
    {
        save_String.UpdateString(this, value, id);
    }
    public string ReadString(int id)
    {
        return save_String.ReadString(this, id);
    }
    //////////////////
    private void StartInsertConstruktorDevice()
    {
        construktorDevice.StartInsertConstruktorDevice(this);
    }
    private void InsertConstruktor(int id, string data)
    {
        construktorDevice.InsertConstruktor(this, id,data);
    }
    public void Update_ConstrukrotDevice(int id, string data)
    {
        construktorDevice.Update_ConstrukrotDevice(this, id, data);
    }
    public string Reader_ConstruktorDevice(int id)
    {
        return construktorDevice.Reader_ConstruktorDevice(this,id);
    }
    //////////////////
    public void Update_basketCell(int numberChield, int count, int idDetail)
    {
        basket.Update_basketCell(this, numberChield, count, idDetail);
    }
    public int Reader_basketCellbyNumberChield(int numberChield)
    {
       return basket.Reader_basketCellbyNumberChield(this, numberChield);
    }
    public int Reader_basketCount(int numberChield)
    {
        return basket.Reader_basketCount(this, numberChield);
    }
    public void StartInsertBasket()
    {
        for (int i = 0; i < 8; i++)
        {
            InsertBasket(i, i, 0, 0);
        }
    }
    private void InsertBasket(int id, int numberChield, int count, int idDetail)
    {
        basket.InsertBasket(this, id, numberChield, count, idDetail);
    }
    //////////////////
    private void StartInsert_partDevice()
    {
        partDevice.StartInsert_partDevice(this);
    }
    public void Insert__partDevice(int id, int state, int clear)
    {
        partDevice.Insert__partDevice(this, id, state, clear);
    }
    public void Update_partDevice(int id, int state)
    {
        partDevice.Update_partDevice(this, id, state);
    }
    public int Reader_partDevice(int id)
    {
        return partDevice.Reader_partDevice(this, id);
    }
    public void UpdateClear_partDevice(int id, int clear)
    {
        partDevice.UpdateClear_partDevice(this, id, clear);
    }
    public int ReadClear_partDevice(int id)
    {
        return partDevice.ReadClear_partDevice(this, id);
    }
    //////////////////
    public void SaveOrders(string data, int number)
    {
        orders.UpdateOrders(this, data, number);
    }
    public string Reader_Orders(int id)
    {
        return orders.Reader_Orders(this, id);
    }
    public void Start_Orders()
    {
        orders.StartInsertOrders(this);
    }
}
