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

    public List<ItemDevice> devices;

    private int timeUpdateOrderlist = 3600; //Значение в секундах

    private float timeDelay = 1f;
    public DateTime currentTime;

    public GameObject dBase;
    public DBase dataBase;

    public GameObject machineParent;
    private MachineHelper machineHelper;

    [Header("To Save")]
    [NonSerialized]
    
    //public ItemDevice itemDevice;
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
        devices = dataBase.devicesScripts;

        checkPointTime = DateTime.UtcNow.ToString();;
        StartCoroutine(TimeDelay());
        //StartCoroutine(TimeDelayToUpdate());
        ShowTextRating();

        Load();
    }

    void Update()
    {

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
        orderListHelper.OpenOrderList();
        ShowTextRating();
        countSelectedOrders = ordersSelected.Count;

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

            GenerateCountOrders();

            GenerateOrders();

            orderListHelper.OpenOrderList();

            SetOpenOrderListColor();
        }
    }

    public void SetOpenOrderListColor()
    {
        if(orderList.activeInHierarchy == true || orderListSelected.activeInHierarchy == true)
        {
            openOrderList.GetComponent<Image>().color = Color.white;
        } else
        {
            openOrderList.GetComponent<Image>().color = Color.green;
        }
    }

    public void GenerateCountOrders()
    {
        countOrders = UnityEngine.Random.Range(minOrders, maxOrders);
    }

    public void Reclame()
    {
        GenerateCountOrders();
        GenerateOrders();
        SetOpenOrderListColor();

        orderListHelper.OpenOrderList();

        Save();
    }

    public void SelectOrder(int key)
    {
        orders[key].timeStartOrder = DateTime.UtcNow.ToString();
        ordersSelected.Add(orders[key]);
        orders.RemoveAt(key);
        orderListHelper.OpenOrderList();
        countOrders = orders.Count;
        countSelectedOrders = ordersSelected.Count;

        Save();
    }

    public void RemoveOrder(int key)
    {
        for (int i = 0; i < ordersSelected[key].idDevice.Count; i++)
        {
            if (ordersSelected[key].typeDevice[i] == 0)
            {
                int count = dataBase.elementsScripts[ordersSelected[key].idDevice[i]].GetCount() - ordersSelected[key].amountDevice[i];
                dataBase.elementsScripts[ordersSelected[key].idDevice[i]].SetCount(count);
                dataBase.SaveOneElement(ordersSelected[key].idDevice[i]);
            }
            else
            {
                int count = dataBase.devicesScripts[ordersSelected[key].idDevice[i]].GetCount() - ordersSelected[key].amountDevice[i];
                dataBase.devicesScripts[ordersSelected[key].idDevice[i]].SetCount(count);
                dataBase.SaveOneElement(ordersSelected[key].idDevice[i]);
            }
        }
        Purse.Instance.SetMoney(Purse.Instance.money += ordersSelected[key].incomeOrder);
        rating += ratingSuccessBy;
        ShowTextRating();
        ordersSelected.RemoveAt(key);
        orderListHelper.OpenOrderList();
        countSelectedOrders = ordersSelected.Count;

        Save();
    }

    public void GenerateIncome()
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
                    income += dataBase.elementsScripts[orders[i].idDevice[j]].GetPrice() * orders[i].amountDevice[j];
                }
                else
                {
                    income += dataBase.devicesScripts[orders[i].idDevice[j]].GetPrice() * orders[i].amountDevice[j];
                }
            }
            orders[i].incomeOrder = income + (income * percentIncome) / 100;
        }
    }

    public void GenerateOrders()
    {
        //Сбор с MachineParent данных о станках и формирование
        //на этой основе заказов Orders
        List<int> arrayElements = new List<int>();
        List<int> arrayDevices = new List<int>();
        foreach (Transform machine in machineParent.transform)
        {
            if (machine.GetComponent<MachineHelper>() != null)
            {
                foreach (int idElement in machine.GetComponent<MachineHelper>().idDetailsToModern)
                {
                    arrayElements.Add(idElement);
                }
            }
            else if (machine.GetComponent<MachineDeviceHelper>() != null)
            {
                arrayDevices.Add(machine.GetComponent<MachineDeviceHelper>().idDevice);
            }
        }

        //Добавляет к генерации открытые в науке элементы
        foreach (int idElementDB in dataBase.listIdElementsUpGrade)
        {
            arrayElements.Add(idElementDB);
        }

        //Условие если нет ни одного станка
        if (arrayElements.Count == 0 && arrayDevices.Count == 0)
        {
            countOrders = 0;
        }

        orders = new List<Order>();

        for (int i = 0; i < countOrders; i++)
        {            
            orders.Add(new Order());

            orders[i].nameCompany = nameCompany[UnityEngine.Random.Range(0, nameCompany.Length)];
            orders[i].countLines = UnityEngine.Random.Range(minLinesOrders, maxLinesOrders);
            orders[i].timeForOrder = UnityEngine.Random.Range(1, 7) * 3600;

            for (int j = 0; j < orders[i].countLines; j++)
            {
                int typeDevRange = 1;
                int typeDev;

                if(arrayElements.Count > 0 && arrayDevices.Count == 0)
                {
                    typeDev = 0;
                }
                else if (arrayElements.Count == 0 && arrayDevices.Count > 0)
                {
                    typeDev = 1;
                }
                else
                {
                    typeDev = UnityEngine.Random.Range(0, typeDevRange);
                }

                int idDev = 0;
                int amountDev = UnityEngine.Random.Range(2, 10);

                //Коррекция генерации количества в зависимости от Рейтинга
                amountDev = amountDev + ((amountDev * rating) / 100);

                if (typeDev == 0)
                {
                    int idArrayElements = UnityEngine.Random.Range(0, arrayElements.Count);
                    for (int n = 0; n < dataBase.elementsScripts.Count; n++)
                    {
                        if(dataBase.elementsScripts[n].GetId() == arrayElements[idArrayElements])
                        {
                            idDev = n;
                        }
                    }
                } else
                {
                    int idArrayDevices = UnityEngine.Random.Range(0, arrayDevices.Count);
                    for (int m = 0; m < dataBase.devicesScripts.Count; m++)
                    {
                        if (dataBase.devicesScripts[m].GetId() == arrayDevices[idArrayDevices])
                        {
                            idDev = m;
                        }
                    }
                }
                
                if(orders[i].typeDevice.Count > 0)
                {
                    bool isNew = true;
                    for (int k = 0; k < orders[i].typeDevice.Count; k++)
                    {
                        if (orders[i].typeDevice[k] == typeDev && orders[i].idDevice[k] == idDev)
                        {
                            orders[i].amountDevice[k] += amountDev;
                            isNew = false;
                        }
                    }
                    if (isNew == true)
                    {
                        orders[i].typeDevice.Add(typeDev);
                        orders[i].idDevice.Add(idDev);
                        orders[i].amountDevice.Add(amountDev);
                    }
                }

                if(j == 0)
                {
                    orders[i].typeDevice.Add(typeDev);
                    orders[i].idDevice.Add(idDev);
                    orders[i].amountDevice.Add(amountDev);
                }
            }
            
        }

        GenerateIncome();

        Save();
    }
}
