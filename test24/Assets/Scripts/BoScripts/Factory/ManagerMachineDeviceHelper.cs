using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMachineDeviceHelper : PanelOld
{
    public GameObject content;
    public GameObject scrollbox;
    public GameObject NeedElement;
    private bool IsNeedElement;
    List<int> temp;
    List<int> idNeedElement1 ;
    List<int> idNeedElementCount ;
    List<int> idNeedElement;
    private float curentSlider = 0;
    public MachineDeviceHelper machineHelper;
    public Text textPriceMachine;

    private void Awake()
    {
        sliderAmountProduce = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if(curentSlider != sliderAmountProduce.value)
        {
            buildScroll();
            curentSlider = sliderAmountProduce.value;
        }
    }
    public void ShowPriceMachine()
    {
        textPriceMachine.text = machineHelper.price.ToString();
    }
    public override void select(GameObject Object)                                                      
    {
        InitDataInMachine(Object);
        buildScroll();
        ShowParameters();
        CalculateSliderValue();
        ButtonsON_OFF();
    }

    private void InitDataInMachine(GameObject Object)
    {
        moveObj = Object;
        machineHelper = moveObj.GetComponent<MachineDeviceHelper>();
        img.sprite = machineHelper.sprt;
        amountDetails = machineHelper.amountDetails;
        imageDetail.sprite = machineHelper.device.sprite;
        upgradeAmountDetailsCost = machineHelper.upgradeAmountDetailsCost;
    }

    private void buildScroll()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        if (machineHelper.deviceObj.GetComponent<CreatePartDevice>())
        {
            temp = machineHelper.deviceObj.GetComponent<CreatePartDevice>().GetNeedElements();
            idNeedElement1 = machineHelper.deviceObj.GetComponent<CreatePartDevice>().GetNeedElements();

        }
        else if (machineHelper.deviceObj.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>())
        {
            temp = machineHelper.deviceObj.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().GetNeedElements();
            idNeedElement1 = machineHelper.deviceObj.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().GetNeedElements();

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
            TempNeedElement.GetComponent<ItemNeedElement>().img.sprite = DBase.Instance.GetComponent<DBase>().getElement(idNeedElement[i]).sprite;
            TempNeedElement.GetComponent<ItemNeedElement>().SetTextNeedElements(DBase.Instance.GetComponent<DBase>().getElement(idNeedElement[i]).GetCount(), (int)IdAndCount[i].y * (int)sliderAmountProduce.value);
        }
        if (!IsNeedElement)
        {
            buttonStart.interactable = false;
        }
        else
        {
            buttonStart.interactable = true;
        }
    }

    private void ShowParameters()
    {
        ShowProductionTimeText();
        ShowLevelMaxDetails();
        //ShowLevelTimeDetails();
        ShowUpgradePrices();
    }
    public void ShowProductionTimeText()
    {
        productionTimeText.text = GetProductionTime().ToString();
    }
    public void ShowLevelMaxDetails()
    {
        textLevelMaxDetails.text = machineHelper.maxAmountDetails.ToString() + "/" + machineHelper.levelMaxAmountDetail;
        barLevelMaxDetail.GetComponent<Image>().fillAmount = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail));
        float test = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail));
        test = (int)(test * 100);
        textPercentLevelMaxDetails.text = test.ToString() + "%";
    }
    //public void ShowLevelTimeDetails()
    //{
    //    textLevelTimeDetails.text = machineHelper.timeProduceDetail.ToString() + "/" + machineHelper.levelMinTimeProduceDetail;
    //    timeProduceDetailText.text = machineHelper.timeProduceDetail.ToString();
    //}
    public void ShowUpgradePrices()
    {
        textUpgradeAmountDetailsCost.text = upgradeAmountDetailsCost.ToString();
        //textUpgradeTimeProduceDetailCost.text = upgradeTimeProduceDetailCost.ToString();
    }

    private float GetProductionTime()
    {
        return machineHelper.timeProduceDetail * sliderAmountProduce.value;
    }
    private void CalculateSliderValue()
    {
        sliderAmountProduce.maxValue = machineHelper.maxAmountDetails;
        sliderAmountProduce.value = amountDetails;
    }

    private void ButtonsON_OFF()
    {
        if (machineHelper.isProduce == true)
        {
            sliderAmountProduce.interactable = false;
            buttonStart.interactable = false;
            buttonUpgradeMaxDetail.interactable = false;
            //buttonUpgradeTime.interactable = false;
            buttonMax.interactable = false;
            buttonStop.interactable = true;
            buttonMoveMachine.interactable = false;
            buttonSellMachine.interactable = false;
            //buttonSendStock.interactable = false;
        }
        else
        {
            sliderAmountProduce.interactable = true;
            buttonStart.interactable = IsNeedElement ? true : false;
            buttonUpgradeMaxDetail.interactable = Purse.Instance.money >= upgradeAmountDetailsCost ? true : false;
            //buttonUpgradeTime.interactable = Purse.Instance.money >= upgradeTimeProduceDetailCost ? true : false;
            buttonMax.interactable = sliderAmountProduce.value < sliderAmountProduce.maxValue ? true : false;
            buttonStop.interactable = false;
            buttonMoveMachine.interactable = true;
            buttonSellMachine.interactable = true;
            //buttonSendStock.interactable = true;
        }
    }

    public void getAmountDetails()
    {
        machineHelper.amountDetails = (int)sliderAmountProduce.value;
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
            if (DBase.Instance.GetComponent<DBase>().getElement(TempIdNeedElement[i]).GetCount() >= TempIdNeedElementCount[i] * (int)sliderAmountProduce.value)
            {
                result.Add(TempIdNeedElement[i]);
                resultIdAndCount.Add(new Vector2(TempIdNeedElement[i], TempIdNeedElementCount[i]));
            }
            else
            {
                result.Add(TempIdNeedElement[i]);
                resultIdAndCount.Add(new Vector2(TempIdNeedElement[i], TempIdNeedElementCount[i]));
                IsNeedElement = false;
            }
        }
        return resultIdAndCount;
    }

    public void move()                                                                             
    {                                                                            
        moveObj.gameObject.GetComponent<MoveObjects>().StartMove();
        UIPanels.Instance.OpenMoveMachinePanel(moveObj,false);
    }
    public void sell()                                                                              
    {
        Purse.Instance.SetMoney(Purse.Instance.money += moveObj.GetComponent<MachineDeviceHelper>().price);
        sendStock();
    }
    public void sendStock()                                                                        
    {
        moveObj.transform.parent.GetComponent<MachineParentScript>().DeleteOneMachineByIdJSONDevice(moveObj.GetComponent<MachineDeviceHelper>().supper_idJSON); 
        Destroy(moveObj);                                                                           
    }

    public void activateTimer()
    {
        moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UpDateDataBase()                                                                     
    {                                                                                               
        moveObj.transform.parent.GetComponent<MachineParentScript>().UpDateOneMachineByid(moveObj); 
    }
 
    public override void startTimer()
    {
        if (IsNeedElement)
        {
            for (int i = 0; i < idNeedElement1.Count; i++)
            {
                for (int j = 0; j < (int)sliderAmountProduce.value; j++)
                {
                    DBase.Instance.GetComponent<DBase>().SubstractID(idNeedElement1[i]);
                }
            }
            machineHelper.startTimeDetailProduce = DateTime.UtcNow.ToString();
            activateTimer();
            moveObj.GetComponentInChildren<Timer>().StartTimer(GetProductionTime());      
            ButtonController.Instance.UnBlockButtons();
            machineHelper.StartProduce();
            buttonExit.onClick.Invoke();
        }
        buildScroll();
        UpDateDataBase();
    }

    public override void stopTimer()
    {
        activateTimer();
        timerInPrigress = moveObj.GetComponentInChildren<Timer>();
        //timerInPrigress.StopTimer();
        machineHelper.OnLoadMachine();
        buttonExit.onClick.Invoke();
        UpDateDataBase();                                                                    
    }

    public void upgradeAmountDetails()
    {
        if (Purse.Instance.money > upgradeAmountDetailsCost && machineHelper.maxAmountDetails < machineHelper.levelMaxAmountDetail)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= upgradeAmountDetailsCost);
            machineHelper.price += upgradeAmountDetailsCost / 2;
            machineHelper.maxAmountDetails += 1;
            if (machineHelper.maxAmountDetails > machineHelper.levelMaxAmountDetail)
            {
                machineHelper.maxAmountDetails = machineHelper.levelMaxAmountDetail;
            }
            sliderAmountProduce.maxValue = machineHelper.maxAmountDetails;
        }
        select(moveObj);
        UpDateDataBase();                                                                    
    }



    public void SetMaxValue()
    {
        sliderAmountProduce.value = machineHelper.maxAmountDetails;
        UpDateDataBase();                                                                    
    }
}
