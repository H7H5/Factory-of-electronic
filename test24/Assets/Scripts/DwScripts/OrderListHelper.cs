using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderListHelper : MonoBehaviour
{
    public GameObject orderList;
    public GameObject orderListSelected;
    public GameObject orderListControllerObject;
    private OrderListController orderListController;
    public GameObject order;
    public GameObject test;

    public GameObject[] panelsOrder;
    public GameObject[] panelsOrderSelected;

    private GameObject orderOne;

    public GameObject dBase;
    public DBase dataBase;
    public Device[] devices;

    private float timeDelay = 1f;

    void Start()
    {
        dataBase = dBase.GetComponent<DBase>();
        devices = dataBase.devicesParameters;

        orderListController = orderListControllerObject.GetComponent<OrderListController>();
    }

    void Update()
    {

    }

    IEnumerator TimeDelayToUpdate()
    {
        while (true)
        {
            int tMinute = orderListController.currentTime.Minute;
            int tSecond = orderListController.currentTime.Second;
            int allTime = (tMinute * 60) + tSecond;
            int res = 3600 - allTime;
            int minute = res / 60;
            int second = res - (minute * 60);

            orderListController.timeToUpdate.text = minute.ToString() + " m - " + second.ToString() + " s";
            yield return new WaitForSeconds(timeDelay);
        }
    }

    public void OpenOrderList()
    {
        orderListController = orderListControllerObject.GetComponent<OrderListController>();
        UpdateOrderList();
        orderListController.SetOpenOrderListColor();
        if(orderListController.orderList.activeInHierarchy == true)
        {
            StartCoroutine(TimeDelayToUpdate());
        }
    }

    public void UpdateOrderList()
    {
        ClearOrderList();

        for (int i = 0; i < orderListController.orders.Count; i++)
        {
            orderOne = Instantiate(order, orderList.transform.GetChild(i));
            OrderHelper orderHelper = orderOne.GetComponent<OrderHelper>();

            orderHelper.numberOrder = i;
            orderHelper.textNameOrganization.text = orderListController.orders[i].nameCompany.ToString();
            orderHelper.typeDevice = orderListController.orders[i].typeDevice;
            orderHelper.idDevice = orderListController.orders[i].idDevice;
            orderHelper.amountDevice = orderListController.orders[i].amountDevice;
            orderHelper.incomeOrder = orderListController.orders[i].incomeOrder;
            orderHelper.timeForOrder = orderListController.orders[i].timeForOrder;
        }

        for (int i = 0; i < orderListController.ordersSelected.Count; i++)
        {
            orderOne = Instantiate(order, orderListSelected.transform.GetChild(i));
            OrderHelper orderHelper = orderOne.GetComponent<OrderHelper>();

            orderHelper.numberOrder = i;
            orderHelper.textNameOrganization.text = orderListController.ordersSelected[i].nameCompany.ToString();
            orderHelper.typeDevice = orderListController.ordersSelected[i].typeDevice;
            orderHelper.idDevice = orderListController.ordersSelected[i].idDevice;
            orderHelper.amountDevice = orderListController.ordersSelected[i].amountDevice;
            orderHelper.incomeOrder = orderListController.ordersSelected[i].incomeOrder;
            orderHelper.timeForOrder = orderListController.ordersSelected[i].timeForOrder;
            orderHelper.timeStartOrder = orderListController.ordersSelected[i].timeStartOrder;
        }
    }

    public void ClearOrderList()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (panelsOrder[i].transform.childCount >= 1)
            {
                Destroy(panelsOrder[i].transform.GetChild(0).gameObject);
            }
        }
        for (int i = 0; i <= 3; i++)
        {
            if (panelsOrderSelected[i].transform.childCount >= 1)
            {
                Destroy(panelsOrderSelected[i].transform.GetChild(0).gameObject);
            }
        }
    }
}
