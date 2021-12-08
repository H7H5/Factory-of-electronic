using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMachineDeviceHelper : MonoBehaviour
{
    private GameObject moveObj;                                                                    //Bo
    public GameObject content;
    public GameObject scrollbox;
    public GameObject NeedElement;
    public GameObject moveMachinePanel;                                                             //Bo
    public Image img;                                                                               //Bo
    private GameObject imgMac;                                                                      //Bo
    Vector3 StartPosition;                                                                          //Bo
    public BasketBoardScript basket;                                                                //Bo
    public GameObject buttonExit;
    public Image imageDetail;
    public Transform barLevelMaxDetail;
    public ButtonController buttonController;
    private Slider sliderAmountProduce;
    public float timeProduceDetail;
    public Text timeProduceDetailText;
    public Text textLevelMaxDetails;
    public Text textLevelTimeDetails;
    public Text textPercentLevelMaxDetails;
    public Text textUpgradeAmountDetailsCost;
    public Text textUpgradeTimeProduceDetailCost;
    private bool IsNeedElement;
    List<int> temp;
    List<int> idNeedElement1 ;
    List<int> idNeedElementCount ;
    List<int> idNeedElement;
    GameObject dataBase;
    private float curentSlider = 0;
    public MachineDeviceHelper machineDeviceHelper;
    public TimerDevice timerInPrigress;
    private float productionTime;
    public Text productionTimeText;
    public float amountDetails;
    public int upgradeDetailToModernCost = 100000;
    public int upgradeAmountDetailsCost = 15000;
    public int upgradeTimeProduceDetailCost = 30000;
    [Header("Buttons")]
    public GameObject buttonStart;
    public GameObject buttonUpgradeMaxDetail;
    public GameObject buttonUpgradeTime;
    public GameObject buttonMax;
    public GameObject buttonStop;
    public GameObject buttonModernization;
    public GameObject buttonMoveMachine;
    public GameObject buttonSellMachine;
    public GameObject buttonSendStock;

    private void Awake()
    {
        sliderAmountProduce = GetComponentInChildren<Slider>();
        dataBase = GameObject.Find("DBase");
    }
    void Start()
    {
        GameObject dwObjects = GameObject.Find("DwObjects");
        buttonController = dwObjects.GetComponentInChildren<ButtonController>();
    }
    void Update()
    {
        if(curentSlider != sliderAmountProduce.value)
        {
            buildScroll();
            curentSlider = sliderAmountProduce.value;
        }
    }
    public void select(GameObject gameObject)                                                      //Bo
    {
        moveObj = gameObject;                                                                       //Bo
        StartPosition = gameObject.transform.position;                                              //Bo

        machineDeviceHelper = moveObj.GetComponent<MachineDeviceHelper>();

        img.sprite = machineDeviceHelper.sprt;                                                            //Bo
     
        imageDetail.sprite = machineDeviceHelper.device.GetComponent<ItemDevice>().imgStock;
        buildScroll();

        timeProduceDetail = machineDeviceHelper.timeProduceDetail;
        amountDetails = machineDeviceHelper.amountDetails;

        ShowProductionTimeText();
        ShowLevelMaxDetails();
        ShowLevelTimeDetails();
        ShowUpgradePrices();

        sliderAmountProduce.maxValue = machineDeviceHelper.maxAmountDetails;
        sliderAmountProduce.value = amountDetails;

        if (machineDeviceHelper.isProduce == true)
        {
            sliderAmountProduce.interactable = false;
            buttonStart.GetComponent<Button>().interactable = false;
            buttonUpgradeMaxDetail.GetComponent<Button>().interactable = false;
            buttonUpgradeTime.GetComponent<Button>().interactable = false;
            buttonMax.GetComponent<Button>().interactable = false;
            buttonStop.GetComponent<Button>().interactable = true;
            buttonMoveMachine.GetComponent<Button>().interactable = false;
            buttonSellMachine.GetComponent<Button>().interactable = false;
            buttonSendStock.GetComponent<Button>().interactable = false;
        }
        else
        {
            sliderAmountProduce.interactable = true;
            
            if (IsNeedElement)
            {
                buttonStart.GetComponent<Button>().interactable = true;
            }
            else
            {
                buttonStart.GetComponent<Button>().interactable = false;
            }

            if (Purse.Instance.money >= upgradeAmountDetailsCost)
            {
                buttonUpgradeMaxDetail.GetComponent<Button>().interactable = true;
            }
            else
            {
                buttonUpgradeMaxDetail.GetComponent<Button>().interactable = false;
            }

            if (Purse.Instance.money >= upgradeTimeProduceDetailCost)
            {
                buttonUpgradeTime.GetComponent<Button>().interactable = true;
            }
            else
            {
                buttonUpgradeTime.GetComponent<Button>().interactable = false;
            }

            if (sliderAmountProduce.value < sliderAmountProduce.maxValue)
            {
                buttonMax.GetComponent<Button>().interactable = true;
            }
            else
            {
                buttonMax.GetComponent<Button>().interactable = false;
            }
            buttonStop.GetComponent<Button>().interactable = false;
            buttonMoveMachine.GetComponent<Button>().interactable = true;
            buttonSellMachine.GetComponent<Button>().interactable = true;
            buttonSendStock.GetComponent<Button>().interactable = true;
        }
    }

    public void ShowProductionTimeText()
    {
        calculateProductionTime();
        productionTimeText.text = productionTime.ToString();
    }
    

    public void ShowLevelMaxDetails()
    {
        textLevelMaxDetails.text = machineDeviceHelper.maxAmountDetails.ToString() + "/" + machineDeviceHelper.levelMaxAmountDetail;
        barLevelMaxDetail.GetComponent<Image>().fillAmount = (float)(machineDeviceHelper.maxAmountDetails * (1 / machineDeviceHelper.levelMaxAmountDetail));
        float test = (float)(machineDeviceHelper.maxAmountDetails * (1 / machineDeviceHelper.levelMaxAmountDetail));
        test = (int)(test * 100);
        textPercentLevelMaxDetails.text = test.ToString() + "%";
    }

    public void ShowLevelTimeDetails()
    {
        textLevelTimeDetails.text = machineDeviceHelper.timeProduceDetail.ToString() + "/" + machineDeviceHelper.levelMinTimeProduceDetail;
        timeProduceDetailText.text = machineDeviceHelper.timeProduceDetail.ToString();
    }

    public void ShowUpgradePrices()
    {
        textUpgradeAmountDetailsCost.text = upgradeAmountDetailsCost.ToString();
        textUpgradeTimeProduceDetailCost.text = upgradeTimeProduceDetailCost.ToString();
    }

    private void buildScroll()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        if (machineDeviceHelper.device.GetComponent<CreatePartDevice>() )
        {
            temp = machineDeviceHelper.device.GetComponent<CreatePartDevice>().GetNeedElements();
            idNeedElement1 = machineDeviceHelper.device.GetComponent<CreatePartDevice>().GetNeedElements();
            
        }
        else if(machineDeviceHelper.device.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>())
        {
            temp = machineDeviceHelper.device.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().GetNeedElements();
            idNeedElement1 = machineDeviceHelper.device.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().GetNeedElements();
            
        }
       
        idNeedElementCount = new List<int>();
        idNeedElement = temp.Distinct().ToList();
        
        for (int i = 0; i < idNeedElement.Count; i++)
        {
            int d = 0;
            for (int j = 0; j < idNeedElement1.Count; j++)
            {
                if (idNeedElement[i] == idNeedElement1[j])
                {
                    d++;
                }
            }
            idNeedElementCount.Add(d);
        }
        
        scrollbox.GetComponent<Scrollbar>().value = 1.0f;
        List<Vector2> IdAndCount = cortIdNeedElement(idNeedElement, idNeedElementCount);
        for (int i = 0; i < IdAndCount.Count; i++)
        {
            GameObject TempNeedElement = Instantiate(NeedElement, content.transform);
            TempNeedElement.GetComponent<ItemNeedElement>().img.sprite = dataBase.GetComponent<DBase>().elementsScripts[(int)IdAndCount[i].x - 1].imgStock;
            TempNeedElement.GetComponent<ItemNeedElement>().SetTextNeedElements(dataBase.GetComponent<DBase>().elementsScripts[(int)IdAndCount[i].x - 1].GetCount(), (int)IdAndCount[i].y * (int)sliderAmountProduce.value);
        }
        if (!IsNeedElement)
        {
            buttonStart.GetComponent<Button>().interactable = false;
        }
        else
        {
            buttonStart.GetComponent<Button>().interactable = true;
        }
    }
    public void getAmountDetails()
    {
        machineDeviceHelper.amountDetails = sliderAmountProduce.value;
        select(moveObj);
    }

    private List<Vector2> cortIdNeedElement(List<int> TempIdNeedElement, List<int> TempIdNeedElementCount)
    {
        IsNeedElement = true;
        List<int> min = new List<int>();
        List<int> max = new List<int>();
        List<int> result = new List<int>();
        List<Vector2> resultIdAndCount = new List<Vector2>();
        List<Vector2> minIdAndCount = new List<Vector2>();
        List<Vector2> maxIdAndCount = new List<Vector2>();
        for (int i = 0; i < TempIdNeedElement.Count; i++)
        {
            if (dataBase.GetComponent<DBase>().elementsScripts[TempIdNeedElement[i] - 1].GetCount() >= TempIdNeedElementCount[i] * (int)sliderAmountProduce.value)
            {
                max.Add(TempIdNeedElement[i]);
                maxIdAndCount.Add(new Vector2(TempIdNeedElement[i], TempIdNeedElementCount[i]));
            }
            else
            {
                min.Add(TempIdNeedElement[i]);
                minIdAndCount.Add(new Vector2(TempIdNeedElement[i], TempIdNeedElementCount[i]));
                IsNeedElement = false;
            }
        }
        for (int i = 0; i < min.Count; i++)
        {
            result.Add(min[i]);
            resultIdAndCount.Add(minIdAndCount[i]);
        }
        for (int i = 0; i < max.Count; i++)
        {
            result.Add(max[i]);
            resultIdAndCount.Add(maxIdAndCount[i]);
        }

        return resultIdAndCount;
    }
    public void move()                                                                             //Bo
    {
        basket.Hide();                                                                              //Bo
        moveObj.gameObject.GetComponent<MoveObjects>().startMove();                                 //Bo
        moveMachinePanel.GetComponent<MoveButton>().select(moveObj);                                //Bo
        //timerInPrigress.stopTimerToMoveMachine();
    }
    public void undoMove()                                                                          //Bo
    {
        moveObj.transform.position = StartPosition;                                                 //Bo
        moveObj.gameObject.GetComponent<MoveObjects>().startMove();                                 //Bo
    }
    public void sell()                                                                              //Bo
    {
        Purse.Instance.SetMoney(Purse.Instance.money += moveObj.GetComponent<MachineDeviceHelper>().price);
        moveObj.transform.parent.GetComponent<MachineParentScript>().DeleteOneMachineByIdJSONDevice(moveObj.GetComponent<MachineDeviceHelper>().supper_idJSON); //Bo
        Destroy(moveObj);                                                                           //Bo
    }

    public void sendStock()                                                                        //Bo
    {
        moveObj.transform.parent.GetComponent<MachineParentScript>().DeleteOneMachineByIdJSONDevice(moveObj.GetComponent<MachineDeviceHelper>().supper_idJSON); //Bo
        Destroy(moveObj);                                                                           //Bo
    }
    public void stopTimer()
    {
        activateTimer();
        //timerInPrigress = moveObj.GetComponentInChildren<Timer>();
        //timerInPrigress.stopTimer();
        machineDeviceHelper.onLoadMachine();
        buttonExit.GetComponent<Button>().onClick.Invoke();
        upDateDataBase();                                                                    //Bo
    }
    public void activateTimer()
    {
        moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void disactivateTimer()
    {
        moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void upDateDataBase()                                                                     //Bo
    {                                                                                               //Bo
        moveObj.transform.parent.GetComponent<MachineParentScript>().UpDateOneMachineByid(moveObj); //Bo
    }
 
    public void startTimer()
    {
        if (IsNeedElement)
        {
            for (int i = 0; i < idNeedElement1.Count; i++)
            {

                for (int j = 0; j < (int)sliderAmountProduce.value; j++)
                {

                    dataBase.GetComponent<DBase>().SubstractID(idNeedElement1[i]);
                }
            }
            machineDeviceHelper.startTimeDetailProduce = DateTime.UtcNow.ToString();
            moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            timerInPrigress = moveObj.GetComponentInChildren<TimerDevice>();         //
            calculateProductionTime();
            ShowProductionTimeText();
            timerInPrigress.productionTime = productionTime;
            timerInPrigress.startTimer();
            sliderAmountProduce.interactable = false;
            buttonController.UnBlockButtons();
            machineDeviceHelper.startProduce();
            buttonExit.GetComponent<Button>().onClick.Invoke();
        }
        buildScroll();
        upDateDataBase();
    }

    public void calculateProductionTime()
    {
        productionTime = machineDeviceHelper.timeProduceDetail * sliderAmountProduce.value;
    }

    public void upgradeAmountDetails()
    {
        if (Purse.Instance.money > upgradeAmountDetailsCost && machineDeviceHelper.maxAmountDetails < machineDeviceHelper.levelMaxAmountDetail)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= upgradeAmountDetailsCost);
            machineDeviceHelper.maxAmountDetails += 1;
            if (machineDeviceHelper.maxAmountDetails > machineDeviceHelper.levelMaxAmountDetail)
            {
                machineDeviceHelper.maxAmountDetails = machineDeviceHelper.levelMaxAmountDetail;
            }
            sliderAmountProduce.maxValue = machineDeviceHelper.maxAmountDetails;
        }
        select(moveObj);
        upDateDataBase();                                                                    //Bo
    }

    public void upgradeLevelTimeDetails()
    {
        if (Purse.Instance.money > upgradeTimeProduceDetailCost && machineDeviceHelper.timeProduceDetail > machineDeviceHelper.levelMinTimeProduceDetail)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= upgradeTimeProduceDetailCost);
            machineDeviceHelper.timeProduceDetail -= 1;
            if (machineDeviceHelper.timeProduceDetail < 1)
            {
                machineDeviceHelper.timeProduceDetail = 1;
            }
        }
        select(moveObj);
        upDateDataBase();                                                                    //Bo
    }
    public void SetMaxValue()
    {
        activateTimer();

        timerInPrigress = moveObj.GetComponentInChildren<TimerDevice>();

        sliderAmountProduce.maxValue = machineDeviceHelper.maxAmountDetails;
        sliderAmountProduce.value = machineDeviceHelper.maxAmountDetails;

        disactivateTimer();

        upDateDataBase();                                                                    //Bo
    }
}
