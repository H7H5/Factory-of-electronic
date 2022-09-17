using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineOld : MonoBehaviour
{
    public Machine machine;
    public MachineDevice machineDevice;
    public Image imageDetail;
    public Text textAmountDetails;
    public Text test;

    public int price = 0;
    protected GameObject detailProduced;
    protected GameObject timerObject;
    private Timer timer;
    public Sprite spriteDetail;
    public bool isProduce = false;
    public Element[] detailsToModern;
    public int selectedDetail = 0;
    public String startTimeDetailProduce;
    public float timeProduceDetail = 1;
    public int amountDetails = 1;
    public float maxAmountDetails = 1;//Dw
    public float levelMaxAmountDetail;
    public float levelMinTimeProduceDetail;
    public DateTime startTimeProduceDetail; //Время Начала производства детали
    public string x;
    public string y;
    public GameObject can;
    public Device device;

    private void Awake()
    {
        detailProduced = gameObject.transform.GetChild(6).gameObject.transform.GetChild(1).gameObject;
        timerObject = gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject;
        timer = timerObject.GetComponent<Timer>();
        FindImageDetail();
    }
    public virtual void CollectProduct() {

    }
    protected void MoveDetailToStock()
    {
        GameObject test = Instantiate(detailProduced, detailProduced.transform.parent);
        test.SetActive(true);
        test.GetComponent<DetailToInventory>().moveToInventory();
    }
    public bool IsFinishDetail()
    {
        return detailProduced.activeInHierarchy ? true : false;
    }
    public void ShowTimerObject() {
        if (timer.IsTimerProgress())
        {
            timerObject.SetActive(true);
        }
    }
    public int id;

    public void HideTimerObject()
    {
        timerObject.SetActive(false);
    }
    public virtual Sprite GetImageDetail()
    {
        return spriteDetail;
    }

    public void OnLoadMachine()
    {
        GameObject timeBar = transform.GetChild(6).gameObject.transform.GetChild(0).gameObject;
        Timer timer = timeBar.GetComponent<Timer>();
        timer.LoadTimer();
    }
    public void StartProduce()
    {
        isProduce = true;
    }
    public void StopProduce()
    {
        isProduce = false;
        transform.parent.GetComponent<MachineParentScript>().UpDateOneMachineByid(gameObject);
    }

    public virtual void InitMachine(int x){  }

    public string GetData()
    {
        string temp = JsonUtility.ToJson(this, true);
        return temp;
    }
    public void Save()
    {
        Vector3 positionMachine = transform.position;
        x = positionMachine.x.ToString();
        y = positionMachine.y.ToString();
    }
    private void FindImageDetail()
    {
        imageDetail = gameObject.transform.GetChild(6).GetChild(1).GetComponent<Image>();
        textAmountDetails = imageDetail.transform.GetChild(0).GetComponent<Text>();
    }
    public void Load(string data)
    {
        JsonUtility.FromJsonOverwrite(data, this);
        float x1 = System.Convert.ToSingle(x);
        float y1 = System.Convert.ToSingle(y);
        Vector3 positionMachine = new Vector3(x1, y1, 1);
        transform.position = positionMachine;
        InitMachine(id);
        if (isProduce)
        {
            OnLoadMachine();
        }
        FindImageDetail();
    }
    public virtual void FinishProduceDetail()
    {
        isProduce = false;
        imageDetail.sprite = GetImageDetail();
        imageDetail.gameObject.SetActive(true);
        textAmountDetails.text = amountDetails.ToString();
        startTimeDetailProduce = "";
    }

}
