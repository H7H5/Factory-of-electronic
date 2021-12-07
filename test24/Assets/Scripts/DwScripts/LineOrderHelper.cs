using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineOrderHelper : MonoBehaviour
{
    public Image imageDevice;
    public Text textAmountLine;

    public GameObject dBase;
    public DBase dataBase;
    public List<ItemDevice> devices;

    public int typeDevice;
    public int idDevice;
    public int amountDevice;

    void Start()
    {
        dBase = GameObject.Find("DBase");
        dataBase = dBase.GetComponent<DBase>();
        devices = dataBase.devicesScripts;
        UpdateLine();
    }

    void Update()
    {

    }

    public void UpdateLine()
    {
        if (typeDevice == 0)
        {
            imageDevice.sprite = dataBase.elementsScripts[idDevice].imgStock;
            textAmountLine.text = dataBase.elementsScripts[idDevice].GetCount() + "/" + amountDevice.ToString();
            textAmountLine.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
            if (dataBase.elementsScripts[idDevice].GetCount() >= amountDevice)
            {
                textAmountLine.GetComponent<Text>().color = Color.green;
            } 
            else
            {
                textAmountLine.GetComponent<Text>().color = Color.red;
            }
        }
        else
        {
            imageDevice.sprite = dataBase.devicesScripts[idDevice].imgStock;
            textAmountLine.text = dataBase.devicesScripts[idDevice].GetCount() + "/" + amountDevice.ToString();
            textAmountLine.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
            if (dataBase.devicesScripts[idDevice].GetCount() >= amountDevice)
            {
                textAmountLine.GetComponent<Text>().color = Color.green;
            }
            else
            {
                textAmountLine.GetComponent<Text>().color = Color.red;
            }
        } 
    }
}
