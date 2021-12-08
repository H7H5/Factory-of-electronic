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
    public List<Sprite> spritesMachinForElements = new List<Sprite>();
    public List<Sprite> spriteMachinesForDevice = new List<Sprite>();
    public List<ItemElement> elementsScripts = new List<ItemElement>();
    [SerializeField] private List<GameObject> elementsPrefabs = new List<GameObject>();
    public List<ItemDevice> devicesScripts = new List<ItemDevice>();
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
    public void SetListIdElementsUpGrade(List<int> list)
    {
        listIdElementsUpGrade = list;
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

    public void addElement(ItemElement element)
    {
        int count = elementsScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (elementsScripts[i].name == element.name)
            {
                elementsScripts[i].SetCount(elementsScripts[i].GetCount()+1);
                SaveOneElement(i);
            }
        } 
    }
    public void sell(ItemElement itemElement)
    {
        int count = elementsScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (elementsScripts[i].GetId() == itemElement.GetId())
            {
                elementsScripts[i].Sell();
                SaveOneElement(i);
            }
        }
    }
    public void sell(ItemDevice itemDevice)
    {
        int count = devicesScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (devicesScripts[i].GetId() == itemDevice.GetId())
            {
                devicesScripts[i].Sell();
                SaveOneDevicet(i);
            }
        }
    }
    public void sellMuch(ItemElement itemElement, int sale)
    {
        int count = elementsScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (elementsScripts[i].GetId() == itemElement.GetId())
            {
                if (elementsScripts[i].GetCount() >= sale)
                {
                    for (int j = 0; j < sale; j++)
                    {
                        elementsScripts[i].Substract();
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
        int count = elementsScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (elementsScripts[i].GetId() == id)
            {
                elementsScripts[i].Substract();
                SaveOneElement(i);
            }
        }
    }
    public ItemElement getElement(int id)
    {
        ItemElement tempElement;
        int count = elementsScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (elementsScripts[i].GetId() == id)
            {
                tempElement = elementsScripts[i];
                return tempElement;
            }
        }
        return null;
    }
    public int getCountOnId(int id)
    {
        int count = elementsScripts.Count;
        int retCount = 0;
        for (int i = 0; i < count; i++)
        {
            if (elementsScripts[i].GetId() == id)
            {
                retCount = elementsScripts[i].GetCount();
            }
        }
        return retCount;
    }
    public void addElementOnId(int id)
    {
        int count = elementsScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (elementsScripts[i].GetId() == id)
            {
                elementsScripts[i].SetCount(elementsScripts[i].GetCount() + 1);
                SaveOneElement(i);
            }
        }
    }
    public void addDevice(ItemDevice device)
    {
        int count = devicesScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (devicesScripts[i].GetId() == device.GetId())
            {
                devicesScripts[i].SetCount(devicesScripts[i].GetCount()+1);
                SaveOneDevicet(i);
            }
        }
    }
    public ItemDevice getDeviceElement(int id)
    {
        ItemDevice tempElement;
        int count = devicesScripts.Count;
        for (int i = 0; i < count; i++)
        {
            if (devicesScripts[i].GetId() == id)
            {
                tempElement = devicesScripts[i];
                SaveOneDevicet(i);
                return tempElement;
                
            }
        }
        return null;
    }

    ///
    public void SaveOneElement(int number)
    {
        unitySQLite.UpdateElement(elementsScripts[number].GetId(), elementsScripts[number].GetCount(), elementsScripts[number].name);
    }
    public void SaveOneDevicet(int number)
    {
        unitySQLite.UpdateDevice(devicesScripts[number].GetId(), devicesScripts[number].GetCount(), devicesScripts[number].name);
    }
    public void SaveMoney(int money)
    {
        unitySQLite.UpdateInteger(money, 1);
    }
    public void SaveSciense(int sciense)
    {
        unitySQLite.UpdateInteger(sciense, 5);
    }
    public void Generate()           
    {
        Purse.Instance.SetMoney(unitySQLite.ReadInteger(1));
        Purse.Instance.SetSciense(unitySQLite.ReadInteger(5));
        LoadElements();
        LoadDevices();
    }
    private void LoadElements()
    {
        for (int i = 0; i < elementsScripts.Count; i++)
        {
            elementsScripts[i].SetCount(unitySQLite.Reader_element(elementsScripts[i].GetId()));  
        }
    }
    private void LoadDevices()
    {
        for (int i = 0; i < devicesScripts.Count; i++)
        {
            devicesScripts[i].SetCount(unitySQLite.Reader_Device(devicesScripts[i].GetId())); 
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
    public GameObject getDetailID(int id)
    {
        for (int i = 0; i < elementsPrefabs.Count; i++)
        {
            if(elementsPrefabs[i].GetComponent<ItemElement>().GetId() == id)
            {
                return elementsPrefabs[i];
            }
        }
        return elementsPrefabs[2];
    }
    public GameObject getDeviceObj(int id)
    {
        for (int i = 0; i < devicesPrefabs.Count; i++)
        {
            if (devicesPrefabs[i].GetComponent<ItemDevice>().GetId() == id)
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
