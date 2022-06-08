using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderHelper : MonoBehaviour
{
    public GameObject order;
    public GameObject orderLine;

    public GameObject orderController;
    public OrderListController orderListController;

    public GameObject buttonSelect;

    public GameObject buttonClose;

    public GameObject dBase;
    public DBase dataBase;
    public List<ItemDevice> devices;

    public Text textNameOrganization;

    public List<int> typeDevice;
    public List<int> idDevice;
    public List<int> amountDevice;

    public Text textIncomeOrder;
    public int incomeOrder;

    public GameObject failedOrder;

    public Text textTimeForOrder;
    public int timeForOrder;
    public string stringTimeForOrder;

    public string timeStartOrder;

    private GameObject line;

    public int numberOrder;

    public int timeLeft;

    public bool isAllGreen = true;

    private float timeDelay = 1f;

    void Start()
    {
        orderController = GameObject.Find("OrderController");
        orderListController = orderController.GetComponent<OrderListController>();

        dBase = GameObject.Find("DBase");
        dataBase = dBase.GetComponent<DBase>();
        devices = dataBase.devicesScripts;

        if (orderListController.orderListSelected.activeInHierarchy == true)
        {
            StartCoroutine(TimeDelayOrderSelected());
        }

        UpdateOrder();
    }

    IEnumerator TimeDelayOrderSelected()
    {
        while (true)
        {
            if (orderListController.orderListSelected.activeInHierarchy == true)
            {
                TimeTickingClock();
            }
            yield return new WaitForSeconds(timeDelay);
        }
    }

    void Update()
    {
        
    }

    public void ConvertTimeToString()
    {
        stringTimeForOrder = (timeForOrder / 3600) + ":00:00";
    }

    public void TimeTickingClock ()
    {
        TimeSpan diffTime = DateTime.UtcNow - DateTime.Parse(timeStartOrder);
        int diffInSeconds = diffTime.Days * 86400 + diffTime.Hours * 3600 + diffTime.Minutes * 60 + diffTime.Seconds;
        timeLeft = timeForOrder - diffInSeconds;

        int hours = timeLeft / 3600;
        int minutes = (timeLeft - (hours * 3600)) / 60;
        int seconds = timeLeft - (hours * 3600) - (minutes * 60);

        if (timeLeft <= 0)
        {
            hours = 0;
            minutes = 0;
            seconds = 0;

            failedOrder.SetActive(true);
            //buttonSelect.GetComponent<Button>().interactable = false;
            buttonSelect.SetActive(false);
        }

        string resTime = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        textTimeForOrder.text = resTime;
    }

    public void UpdateOrder()
    {
        textIncomeOrder.text = incomeOrder.ToString();  

        for (int i = 0; i < idDevice.Count; i++)
        {
            line = Instantiate(orderLine, order.transform.GetChild(i));
            LineOrderHelper lineOrderHelper = line.GetComponent<LineOrderHelper>();
            lineOrderHelper.typeDevice = typeDevice[i];
            lineOrderHelper.idDevice = idDevice[i];
            lineOrderHelper.amountDevice = amountDevice[i];
            if(typeDevice[i] == 0)
            {
                if (dataBase.elementsScripts[idDevice[i]].GetCount() < amountDevice[i])
                {
                    isAllGreen = false;
                }
            }else
            {
                if (dataBase.devicesScripts[idDevice[i]].GetCount() < amountDevice[i])
                {
                    isAllGreen = false;
                }
            }
            
        }
        if (orderListController.orderList.activeInHierarchy == true)
        {
            ConvertTimeToString();
            textTimeForOrder.text = stringTimeForOrder;
            buttonClose.SetActive(false);
            if (orderListController.ordersSelected.Count < 4)
            {
                buttonSelect.GetComponent<Button>().interactable = true;
            }
            else
            {
                buttonSelect.GetComponent<Button>().interactable = false;
            }
        }
        if (orderListController.orderListSelected.activeInHierarchy == true)
        {
            TimeTickingClock();

            if (isAllGreen && timeLeft > 0)
            {
                buttonSelect.GetComponent<Button>().interactable = true;
            }
            else
            {
                buttonSelect.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void FailureOrder()
    {
        orderListController.failureOrderNumber = numberOrder;
        if(timeLeft > 0)
        {
            orderListController.SetActiveAttention();
        }
        else
        {
            orderListController.RemoveOrderFailure();
        }
    }

    public void SelectOrder ()
    {
        if(orderListController.orderList.activeInHierarchy == true)
        {
            orderListController.SelectOrder(numberOrder);
        }
        if(orderListController.orderListSelected.activeInHierarchy == true)
        {
            orderListController.RemoveOrder(numberOrder);
        }
    }
}
