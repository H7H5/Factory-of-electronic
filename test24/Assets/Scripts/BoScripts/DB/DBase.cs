using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBase : MonoBehaviour
{
    public static DBase Instance;
    public GameObject Canvas;
    public GameObject saverDevice;
    public GameObject DeviceSelect;
    public GameObject OpenSchema;
    public List<int> listIdElementsUpGrade = new List<int>();
    public List<int> listIdDevicesUpGrade = new List<int>();
    public List<Sprite> spriteMachinesForDevice = new List<Sprite>();
    public Element[] elementsParameters;
    public Device[] devicesParameters;
    [SerializeField] private List<GameObject> devicesPrefabs = new List<GameObject>();
    private unitySQLite unitySQLite;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        unitySQLite = gameObject.GetComponent<unitySQLite>();
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.2f);
        Generate();
    }

    public void SaveLearning(int x)
    {
        unitySQLite.UpdateInteger(x, 6);
    }
    public bool GetLearning()
    {
        int d = unitySQLite.ReadInteger(6);
        if(d == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public GameObject GetSaverDevice()
    {
        if (saverDevice == null)
        {
            saverDevice = getDeviceObj(listIdDevicesUpGrade[0]);
        }
        return saverDevice;
    }
    public void SetListIdElementsUpGrade(List<int> list)
    {
        listIdElementsUpGrade = list;
    }
    public void SetListIdDevicesUpGrade(List<int> list)
    {
        listIdDevicesUpGrade = list;
    }
    public bool IsUpgradeElement(int id)
    {
        for (int i = 0; i < listIdElementsUpGrade.Count; i++)
        {
            if (listIdElementsUpGrade[i] == id)
            {
                return true;
            }
        }
        return false;
    }

    public void addElement(Element element)
    {
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].name == element.name)
            {
                elementsParameters[i].SetCount(elementsParameters[i].GetCount()+1);
                SaveOneElement(i);
            }
        } 
    }

    public int IdElementParameters(int idElement)
    {
        int toReturn = 1;
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].id == idElement)
            {
                toReturn = i;
            }
        }
        return toReturn;
    }

    public int IdDeviceParameters(int idDevice)
    {
        int toReturn = 1;
        for (int i = 0; i < devicesParameters.Length; i++)
        {
            if (devicesParameters[i].id == idDevice)
            {
                toReturn = i;
            }
        }
        return toReturn;
    }

    public void sell(Element element)
    {
        foreach (var elememt in elementsParameters)
        {
            if (elememt.id == element.id)
            {
                Purse.Instance.SetSciense(Purse.Instance.science += elememt.sellPriceScience);
            }
        }
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].id == element.id)
            {
                elementsParameters[i].Sell();
                SaveOneElement(i);
            }
        }
    }
    public void sell(Device device)
    {
        //int count = devicesScripts.Count;
        //for (int i = 0; i < count; i++)
        //{
        //    if (devicesScripts[i].GetId() == device.id)
        //    {
        //        devicesScripts[i].Sell();
        //        SaveOneDevicet(i);
        //    }
        //}
        for (int i = 0; i < devicesParameters.Length; i++)
        {
            if (device.id == devicesParameters[i].id)
            {
                Purse.Instance.SetMoney(Purse.Instance.money += device.sellPrice);
                Purse.Instance.SetSciense(Purse.Instance.science += device.sellPriceScience);
                devicesParameters[i].SetCount(devicesParameters[i].GetCount() - 1);
                SaveOneDevicet(i);
            }
        }
    }
    public void sellMuch(Element element, int sale)
    {
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].id == element.id)
            {
                if (elementsParameters[i].GetCount() >= sale)
                {
                    for (int j = 0; j < sale; j++)
                    {
                        elementsParameters[i].Substract();
                    }
                }
                SaveOneElement(i);
            }
        }
    }
   
    public string Reader_Orders(int id)
    {
        return unitySQLite.Reader_Orders(id);
    }
    public string ReadString(int id)
    {
        return unitySQLite.ReadString(id);
    }
    public void UpdateString(string value, int id)
    {
        unitySQLite.UpdateString(value, id);
    }
    public int ReadOrderList(int id)
    {
        return unitySQLite.ReadInteger(id);
    }
    public void SaveOrders(string data, int number)
    {
        unitySQLite.SaveOrders(data, number);
    }
    public void SaveOrderList(int rating, int id)
    {
        unitySQLite.UpdateInteger(rating, id);
    }
    public void SubstractID(int id)
    {
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].id == id)
            {
                elementsParameters[i].Substract();
                SaveOneElement(i);
            }
        }
    }
    public Element getElement(int id)
    {
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].id == id)
            {
                return elementsParameters[i];
            }
        }
        return null;
    }
    public int getCountOnId(int id)
    {
        int retCount = 0;
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].id == id)
            {
                retCount = elementsParameters[i].GetCount();
            }
        }
        return retCount;
    }
    public void AddElementOnIdByCount(int id,int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddElementOnId(id);
        }
    }

    public void AddElementOnId(int id)
    {
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].id == id)
            {
                elementsParameters[i].SetCount(elementsParameters[i].GetCount() + 1);
                SaveOneElement(i);
            }
        }
    }
    public void AddDeviceByCount(Device device, int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddDevice(device);
        }
    }
    public void AddDevice(Device device)
    {
        int count = devicesParameters.Length;
       
        for (int i = 0; i < count; i++)
        {
            if (devicesParameters[i].id == device.id)
            {
                devicesParameters[i].SetCount(devicesParameters[i].GetCount() + 1);
                SaveOneDevicet(i);
            }
        }
    }

    //Old Prefab system
    public Device getDeviceElement(int id)
    {
        Device tempElement;
        for (int i = 0; i < devicesParameters.Length; i++)
        {
            if (devicesParameters[i].id == id)
            {
                tempElement = devicesParameters[i];
                SaveOneDevicet(i);
                return tempElement;
                
            }
        }
        return null;
    }

    //New Scriptable Object system
    public Device GetDevice(int id)
    {
        Device device;
        for (int i = 0; i < devicesParameters.Length; i++)
        {
            if (devicesParameters[i].id == id)
            {
                device = devicesParameters[i];
                //SaveOneDevicet(i);
                return device;
            }
        }
        return null;
    }

    //New Scriptable Object system
    public Element GetElement(int id)
    {
        Element element;
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if (elementsParameters[i].id == id)
            {
                element = elementsParameters[i];
                //SaveOneElement(i);
                return element;
            }
        }
        return null;
    }

    ///
    public void SaveOneElement(int number)
    {
        unitySQLite.UpdateElement(elementsParameters[number].id, elementsParameters[number].GetCount(), elementsParameters[number].name);
    }
    public void SaveOneDevicet(int number)
    {
        //unitySQLite.UpdateDevice(devicesScripts[number].GetId(), devicesScripts[number].GetCount(), devicesScripts[number].name);
        unitySQLite.UpdateDevice(devicesParameters[number].id, devicesParameters[number].GetCount(), devicesParameters[number].name);
    
    }
    public void SaveMoney(int money)
    {
        unitySQLite.UpdateInteger(money, 1);
    }
    public void SaveSciense(int sciense)
    {
        unitySQLite.UpdateInteger(sciense, 5);
    }
    public void SaveDiamonds(int diamonds)
    {
        unitySQLite.UpdateInteger(diamonds, 7);
    }
    public void Generate()           
    {
        Purse.Instance.SetMoney(unitySQLite.ReadInteger(1));
        Purse.Instance.SetSciense(unitySQLite.ReadInteger(5));
        Purse.Instance.SetDiamonds(unitySQLite.ReadInteger(7));
        LoadElements();
        LoadDevices();
    }
    private void LoadElements()
    {
        //Old Prefab system
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            elementsParameters[i].SetCount(unitySQLite.Reader_element(elementsParameters[i].id));
        }
        //New Scriptable Object system
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            elementsParameters[i].SetCount(unitySQLite.Reader_element(elementsParameters[i].id));
        }
    }
    private void LoadDevices()
    {
        //Old Prefab system
        for (int i = 0; i < devicesParameters.Length; i++)
        {
            devicesParameters[i].SetCount(unitySQLite.Reader_Device(devicesParameters[i].id));
        }
        //New Scriptable Object system
        for (int i = 0; i < devicesParameters.Length; i++)
        {
            devicesParameters[i].SetCount(unitySQLite.Reader_Device(devicesParameters[i].id));
        }
    }
    public int Reader_partDevice(int id)
    {
       return unitySQLite.Reader_partDevice(id);
    }
    public void Update_partDevice(int id, int state)
    {
        unitySQLite.Update_partDevice(id, state);
    }
    public int ReadClear_partDevice(int id)
    {
        return unitySQLite.ReadClear_partDevice(id);
    }
    public void UpdateClear_partDevice(int id, int clear)
    {
        unitySQLite.UpdateClear_partDevice(id, clear);
    }
    public Element getDetailID(int id)
    {
        for (int i = 0; i < elementsParameters.Length; i++)
        {
            if(elementsParameters[i].id == id)
            {
                return elementsParameters[i];
            }
        }
        return elementsParameters[2];
    }
    public GameObject getDeviceObj(int id)
    {
        for (int i = 0; i < devicesPrefabs.Count; i++)
        {
            if (devicesPrefabs[i].GetComponent<ItemDevice>().device.id == id)
            {
                return devicesPrefabs[i];
            }
        }
        return devicesPrefabs[2];
    }
    public void SaveFloat(float value, int id)
    {
        unitySQLite.UpdateFloat(value, id);                           
    }
    public float ReadFloat(int id) 
    {
        return unitySQLite.ReadFloat(id);
    }
    public void SetSaverDevice(GameObject device)
    {
        saverDevice = device;
    }

    public void Update_basketCell(int numberChield, int count,int idDetail)
    {
        unitySQLite.Update_basketCell(numberChield, count, idDetail);
    }
    public int Reader_basketCount(int id)
    {
        return unitySQLite.Reader_basketCount(id);
    }
    public int Reader_basketCellbyNumberChield(int id)
    {
        return unitySQLite.Reader_basketCellbyNumberChield(id);
    }
    public void Update_ConstrukrotDevice(int id, string data)
    {
        unitySQLite.Update_ConstrukrotDevice(id, data);
    }
    public string Reader_ConstruktorDevice(int id)
    {
        return unitySQLite.Reader_ConstruktorDevice(id);
    }
    public void Insert_functionJSON(string s)
    {
        unitySQLite.Insert_functionJSON(s);
    }
    public void Reader_functionAllidJSON()
    {
        unitySQLite.Reader_functionAllidJSON();
    }
    public void Update_functionJSON(int id, string data)
    {
        unitySQLite.Update_functionJSON(id, data);
    }
    public void Insert_functionMachineDevice(string s)
    {
        unitySQLite.Insert_functionMachineDevice(s);
    }
    public void Reader_functionAllidMachineDevice()
    {
        unitySQLite.Reader_functionAllidMachineDevice();
    }
    public void Update_functionMachineDevice(int id, string data)
    {
        unitySQLite.Update_functionMachineDevice(id, data);
    }
    public void Reader_functionJSON()
    {
        unitySQLite.Reader_functionJSON();
    }
    public void Reader_functionMachineDevice()
    {
        unitySQLite.Reader_functionMachineDevice();
    }
    public void Delete_functionJSON(int id)
    {
        unitySQLite.Delete_functionJSON(id);
    }
    public void Delete_functionMachineDevice(int id)
    {
        unitySQLite.Delete_functionMachineDevice(id);
    }
}
