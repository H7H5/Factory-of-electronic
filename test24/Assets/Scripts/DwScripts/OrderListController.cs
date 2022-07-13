using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OrderListController : MonoBehaviour
{
    [Serializable]
    public class Order
    {
        public string nameCompany;
        public int countLines;
        public int incomeOrder;
        public int incomeOrderBonus;
        public int timeForOrder;
        public string timeStartOrder;
        public List<int> typeDevice = new List<int>();
        public List<int> idDevice = new List<int>();
        public List<int> amountDevice = new List<int>();  
    }

    string[] nameCompany = new[] { "Electronics ART",
                                         "Design Tools",
                                         "Engineering Micro",
                                         "New Capacity",
                                         "Resistor Lab",
                                         "Micro Elements",
                                         "Creative Schemes" };

    private int minOrders = 2;
    private int maxOrders = 5; //На одно меньше

    private int minLinesOrders = 2;
    private int maxLinesOrders = 5; //На одно меньше

    private int ratingSuccessBy = 1;
    private int ratingFailureBy = 3;

    private int generatePercentIncomeMin = 10;
    private int generatePercentIncomeMax = 30;

    public Text timeToUpdate;
    public Text textRating;

    public GameObject orderListSelected;
    public GameObject orderList;
    public OrderListHelper orderListHelper;

    public GameObject openOrderList;

    public GameObject attentionRaingDecrease;
    public int failureOrderNumber;

    public Device[] devices;

    private int timeUpdateOrderlist = 3600; //Значение в секундах

    private float timeDelay = 1f;
    public DateTime currentTime;

    public GameObject dBase;
    public DBase dataBase;

    public GameObject machineParent;
    private MachineHelper machineHelper;


    [SerializeField] private List<GameObject> bookmarks = new List<GameObject>();
    public int bookMarkSelected = 0;

    public bool newOrdersInList = false;


    [Header("To Save")]
    [NonSerialized]
    
    public string checkPointTime;

    public static int rating = 1;
    public List<Order> orders;
    public List<Order> ordersSelected;
    public int countOrders;
    public int countSelectedOrders;
    public string data;

    void Start()
    {
        orderListHelper = orderList.GetComponent<OrderListHelper>();

        dataBase = dBase.GetComponent<DBase>();
        devices = dataBase.devicesParameters;

        checkPointTime = DateTime.UtcNow.ToString();;
        StartCoroutine(TimeDelay());
        ShowTextRating();
        SetColorOrdersIcon();

        Load();
    }

    public void SetColorOrdersIcon()
    {
        if (newOrdersInList == true)
        {
            openOrderList.GetComponent<Image>().color = Color.green;
        } 
        else
        {
            openOrderList.GetComponent<Image>().color = Color.white;
        }
    }

    public void OpenOrderList()
    {
        bookmarks[bookMarkSelected].SetActive(true);
        orderListHelper.OpenOrderList();
    }
    
    public void SwitchBookMark(int number)
    {
        for (int i = 0; i < bookmarks.Count; i++)
        {
            bookmarks[i].SetActive(false);
        }
        bookMarkSelected = number;
        OpenOrderList();
    }

    private void Save()
    {
        dataBase.SaveOrderList(rating, 2);
        dataBase.SaveOrderList(countOrders, 3);
        dataBase.SaveOrderList(countSelectedOrders, 4);

        //Нужно сохранить!!! Это строковая переменная.
        //checkPointTime
        dataBase.UpdateString(checkPointTime, 1);   // BO

        for (int i = 0; i < countOrders; i++)
        {
            data = JsonUtility.ToJson(orders[i], true);
            dataBase.SaveOrders(data, i);
        }
        for (int i = 6; i < countSelectedOrders+6; i++)
        {
            data = JsonUtility.ToJson(ordersSelected[i-6], true);
            dataBase.SaveOrders(data, i);
        }
    }
    private void Load()
    {
        rating = dataBase.ReadOrderList(2);
        countOrders = dataBase.ReadOrderList(3);
        countSelectedOrders = dataBase.ReadOrderList(4);

        //Нужно присвоить переменной значение из базы данных!!!
        //checkPointTime
        checkPointTime = dataBase.ReadString(1);    // BO

        ShowTextRating();
        orders = new List<Order>();
        for (int i = 0; i < countOrders; i++)
        {
            data = dataBase.Reader_Orders(i);
            orders.Add(new Order());
            JsonUtility.FromJsonOverwrite(data, orders[i]);
        }
        ordersSelected = new List<Order>();
        for (int i = 6; i < countSelectedOrders + 6; i++)
        {
            data = dataBase.Reader_Orders(i);
            ordersSelected.Add(new Order());
            JsonUtility.FromJsonOverwrite(data, ordersSelected[i-6]);
        }
    }

    IEnumerator TimeDelay()
    {
        while(true)
        {
            CheckPassedTime();
            yield return new WaitForSeconds(timeDelay);
        }
    }

    public void SetActiveAttention()
    {
        attentionRaingDecrease.SetActive(true);
    }

    public void RemoveOrderFailure()
    {
        rating -= ratingFailureBy;
        if (rating < 0)
        {
            rating = 1;
        }
        ordersSelected.RemoveAt(failureOrderNumber);
        ShowTextRating();
        countSelectedOrders = ordersSelected.Count;
        orderListHelper.OpenOrderList();

        Save();
    }

    public void ShowTextRating()
    {
        textRating.text = rating.ToString();
    }

    public void CheckPassedTime()
    {
        currentTime = DateTime.UtcNow;
        TimeSpan difTime = currentTime - DateTime.Parse(checkPointTime);
        float difTimeInSeconds = difTime.Seconds;

        if (currentTime.Hour != DateTime.Parse(checkPointTime).Hour || difTimeInSeconds > timeUpdateOrderlist)
        {
            checkPointTime = DateTime.UtcNow.ToString();

            GenerateOrders();

            orderListHelper.OpenOrderList();

            if(orderList.activeInHierarchy == false)
            {
                newOrdersInList = true;
                SetColorOrdersIcon();
            }
        }
    }

    public void GenerateCountOrders()
    {
        countOrders = UnityEngine.Random.Range(minOrders, maxOrders);
    }

    public void Reclame()
    {
        GenerateOrders();
        newOrdersInList = true;
        SetColorOrdersIcon();

        orderListHelper.OpenOrderList();

        Save();
    }

    public void SelectOrder(int key)
    {
        orders[key].timeStartOrder = DateTime.UtcNow.ToString();
        ordersSelected.Add(orders[key]);
        orders.RemoveAt(key);
        countOrders = orders.Count;
        countSelectedOrders = ordersSelected.Count;
        orderListHelper.OpenOrderList();

        Save();
    }

    public void RemoveOrder(int key)
    {
        for (int i = 0; i < ordersSelected[key].idDevice.Count; i++)
        {
            if (ordersSelected[key].typeDevice[i] == 0)
            {
                int count = dataBase.elementsParameters[ordersSelected[key].idDevice[i]].GetCount() - ordersSelected[key].amountDevice[i];
                dataBase.elementsParameters[ordersSelected[key].idDevice[i]].SetCount(count);
                dataBase.SaveOneElement(ordersSelected[key].idDevice[i]);
            }
            else
            {
                int count = dataBase.devicesParameters[ordersSelected[key].idDevice[i]].GetCount() - ordersSelected[key].amountDevice[i];
                dataBase.devicesParameters[ordersSelected[key].idDevice[i]].SetCount(count);
                dataBase.SaveOneElement(ordersSelected[key].idDevice[i]);
            }
        }
        Purse.Instance.SetMoney(Purse.Instance.money += ordersSelected[key].incomeOrder);
        rating += ratingSuccessBy;
        ShowTextRating();
        ordersSelected.RemoveAt(key);
        countSelectedOrders = ordersSelected.Count;
        orderListHelper.OpenOrderList();

        Save();
    }

    private void GenerateIncome()
    {
        for (int i = 0; i < orders.Count; i++)
        {
            //Генерируется дополнительная прибыль в процентном диапозоне от общей прибыли
            int percentIncome = UnityEngine.Random.Range(generatePercentIncomeMin, generatePercentIncomeMax);
            int income = 0;
            for (int j = 0; j < orders[i].idDevice.Count; j++)
            {
                if (orders[i].typeDevice[j] == 0)
                {  
                    income += dataBase.elementsParameters[orders[i].idDevice[j]].GetPrice() * orders[i].amountDevice[j];
                }
                else
                {
                    income += dataBase.devicesParameters[orders[i].idDevice[j]].GetPrice() * orders[i].amountDevice[j];
                }
            }
            //orders[i].incomeOrder = income + (income * percentIncome) / 100;
            orders[i].incomeOrder = income;
            orders[i].incomeOrderBonus = (income * percentIncome) / 100;
        }
    }    

    private List<int> FillListElements()
    {
        List<int> listElements = new List<int>();
        foreach (Transform machine in machineParent.transform)
        {
            if (machine.GetComponent<MachineHelper>() != null)
            {
                foreach (int idElement in machine.GetComponent<MachineHelper>().idDetailsToModern)
                {
                    listElements.Add(idElement);
                }
            }
        }
        foreach (int idElementDB in dataBase.listIdElementsUpGrade)
        {
            listElements.Add(idElementDB);
        }
        return listElements;
    }

    private List<int> FillListDevices()
    {
        List<int> listDevices = new List<int>();
        foreach (Transform machine in machineParent.transform)
        {
            if (machine.GetComponent<MachineDeviceHelper>() != null)
            {
                listDevices.Add(machine.GetComponent<MachineDeviceHelper>().idDevice);
            }
        }
        foreach (int idElementDB in dataBase.listIdDevicesUpGrade)
        {
            listDevices.Add(idElementDB);
        }
        return listDevices;
    }

    private int GenerateRandomType(List<int> listElements, List<int> listDevices)
    {
        int typeDevRange = 2;
        int typeDev;
        if (listElements.Count > 0 && listDevices.Count == 0)
        {
            typeDev = 0;
        }
        else if (listElements.Count == 0 && listDevices.Count > 0)
        {
            typeDev = 1;
        }
        else
        {
            typeDev = UnityEngine.Random.Range(0, typeDevRange);
        }
        return typeDev;
    }

    private int GenerateRandomAmount()
    {
        int amountDev = UnityEngine.Random.Range(2, 10);
        amountDev = amountDev + ((amountDev * rating) / 100);
        return amountDev;
    }

    private int GenerateProductID(List<int> listElements, List<int> listDevices, int typeProduct)
    {
        int productID = 0;
        if (typeProduct == 0)
        {
            int idListElements = UnityEngine.Random.Range(0, listElements.Count);
            for (int n = 0; n < dataBase.elementsParameters.Length; n++)
            {
                if (dataBase.elementsParameters[n].id == listElements[idListElements])
                {
                    productID = n;
                }
            }
        }
        else
        {
            int idListDevices = UnityEngine.Random.Range(0, listDevices.Count);
            for (int m = 0; m < dataBase.devicesParameters.Length; m++)
            {
                if (dataBase.devicesParameters[m].id == listDevices[idListDevices])
                {
                    productID = m;
                }
            }
        }
        return productID;
    }

    private void FillOrders()
    {
        List<int> listElements = new List<int>(FillListElements());
        List<int> listDevices = new List<int>(FillListDevices());
        orders = new List<Order>();
        for (int i = 0; i < countOrders; i++)
        {
            orders.Add(new Order());

            orders[i].nameCompany = nameCompany[UnityEngine.Random.Range(0, nameCompany.Length)];
            orders[i].countLines = UnityEngine.Random.Range(minLinesOrders, maxLinesOrders);
            orders[i].timeForOrder = UnityEngine.Random.Range(1, 7) * 3600;

            FillLinesWithoutRepetition(listElements, listDevices, i);
        }
    }

    private void FillLinesWithoutRepetition(List<int> listElements, List<int> listDevices, int i)
    {
        for (int j = 0; j < orders[i].countLines; j++)
        {
            int productType = GenerateRandomType(listElements, listDevices);
            int productID = GenerateProductID(listElements, listDevices, productType);
            int amountDev = GenerateRandomAmount();
            if (j == 0)
            {
                FillOrderLines(productType, productID, amountDev, i);
                continue;
            }
            if (orders[i].typeDevice.Count > 0)
            {
                bool isNew = true;
                for (int k = 0; k < orders[i].typeDevice.Count; k++)
                {
                    if (orders[i].typeDevice[k] == productType && orders[i].idDevice[k] == productID)
                    {
                        orders[i].amountDevice[k] += amountDev;
                        isNew = false;
                    }
                }
                if (isNew == true)
                {
                    FillOrderLines(productType, productID, amountDev, i);
                }
            }            
        }       
    }

    private void FillOrderLines(int productType, int productID, int amountDev, int i)
    {
        orders[i].typeDevice.Add(productType);
        orders[i].idDevice.Add(productID);
        orders[i].amountDevice.Add(amountDev);
    }
    
    public void GenerateOrders()
    {
        GenerateCountOrders();
        FillOrders();
        GenerateIncome();
        Save();
    }
}
