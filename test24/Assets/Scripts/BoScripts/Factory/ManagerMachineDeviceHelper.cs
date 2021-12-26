﻿using System;
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
    public override void select(GameObject gameObject)                                                      //Bo
    {
        moveObj = gameObject;                                                                       //Bo
       // StartPosition = gameObject.transform.position;                                              //Bo

        machineHelper = moveObj.GetComponent<MachineDeviceHelper>();

        img.sprite = machineHelper.sprt;                                                            //Bo
     
        imageDetail.sprite = machineHelper.device.GetComponent<ItemDevice>().imgStock;
        buildScroll();

        //timeProduceDetail = machineHelper.timeProduceDetail;
        amountDetails = machineHelper.amountDetails;

        ShowProductionTimeText();
        ShowLevelMaxDetails();
        ShowLevelTimeDetails();
        ShowUpgradePrices();

        sliderAmountProduce.maxValue = machineHelper.maxAmountDetails;
        sliderAmountProduce.value = amountDetails;

        if (machineHelper.isProduce == true)
        {
            sliderAmountProduce.interactable = false;
            buttonStart.interactable = false;
            buttonUpgradeMaxDetail.interactable = false;
            buttonUpgradeTime.interactable = false;
            buttonMax.interactable = false;
            buttonStop.interactable = true;
            buttonMoveMachine.interactable = false;
            buttonSellMachine.interactable = false;
            buttonSendStock.interactable = false;
        }
        else
        {
            sliderAmountProduce.interactable = true;
            
            if (IsNeedElement)
            {
                buttonStart.interactable = true;
            }
            else
            {
                buttonStart.interactable = false;
            }

            if (Purse.Instance.money >= upgradeAmountDetailsCost)
            {
                buttonUpgradeMaxDetail.interactable = true;
            }
            else
            {
                buttonUpgradeMaxDetail.interactable = false;
            }

            if (Purse.Instance.money >= upgradeTimeProduceDetailCost)
            {
                buttonUpgradeTime.interactable = true;
            }
            else
            {
                buttonUpgradeTime.interactable = false;
            }
            buttonMax.interactable = sliderAmountProduce.value < sliderAmountProduce.maxValue ? true : false;
            buttonStop.interactable = false;
            buttonMoveMachine.interactable = true;
            buttonSellMachine.interactable = true;
            buttonSendStock.interactable = true;
        }
    }

    public void ShowProductionTimeText()
    {
        CalculateProductionTime();
        productionTimeText.text = productionTime.ToString();
    }

    public override void CalculateProductionTime()
    {
        productionTime = machineHelper.timeProduceDetail * sliderAmountProduce.value;
    }

    public void ShowLevelMaxDetails()
    {
        textLevelMaxDetails.text = machineHelper.maxAmountDetails.ToString() + "/" + machineHelper.levelMaxAmountDetail;
        barLevelMaxDetail.GetComponent<Image>().fillAmount = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail));
        float test = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail));
        test = (int)(test * 100);
        textPercentLevelMaxDetails.text = test.ToString() + "%";
    }

    public void ShowLevelTimeDetails()
    {
        textLevelTimeDetails.text = machineHelper.timeProduceDetail.ToString() + "/" + machineHelper.levelMinTimeProduceDetail;
        timeProduceDetailText.text = machineHelper.timeProduceDetail.ToString();
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
        if (machineHelper.device.GetComponent<CreatePartDevice>() )
        {
            temp = machineHelper.device.GetComponent<CreatePartDevice>().GetNeedElements();
            idNeedElement1 = machineHelper.device.GetComponent<CreatePartDevice>().GetNeedElements();
            
        }
        else if(machineHelper.device.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>())
        {
            temp = machineHelper.device.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().GetNeedElements();
            idNeedElement1 = machineHelper.device.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().GetNeedElements();
            
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
            TempNeedElement.GetComponent<ItemNeedElement>().img.sprite = DBase.Instance.GetComponent<DBase>().elementsScripts[(int)IdAndCount[i].x - 1].imgStock;
            TempNeedElement.GetComponent<ItemNeedElement>().SetTextNeedElements(DBase.Instance.GetComponent<DBase>().elementsScripts[(int)IdAndCount[i].x - 1].GetCount(), (int)IdAndCount[i].y * (int)sliderAmountProduce.value);
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

    public void getAmountDetails()
    {
        machineHelper.amountDetails = (int)sliderAmountProduce.value;
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
            if (DBase.Instance.GetComponent<DBase>().elementsScripts[TempIdNeedElement[i] - 1].GetCount() >= TempIdNeedElementCount[i] * (int)sliderAmountProduce.value)
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
        //basket.Hide();                                                                              //Bo
        moveObj.gameObject.GetComponent<MoveObjects>().StartMove();
        UIPanels.Instance.OpenMoveMachinePanel(moveObj,false);
        //moveMachinePanel.GetComponent<MoveButton>().select(moveObj);                                //Bo
        //timerInPrigress.stopTimerToMoveMachine();
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

    public void activateTimer()
    {
        moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void disactivateTimer()
    {
        moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void UpDateDataBase()                                                                     //Bo
    {                                                                                               //Bo
        moveObj.transform.parent.GetComponent<MachineParentScript>().UpDateOneMachineByid(moveObj); //Bo
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
            moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            timerInPrigress = moveObj.GetComponentInChildren<Timer>();         //
            CalculateProductionTime();
            ShowProductionTimeText();
            timerInPrigress.productionTime = productionTime;
            timerInPrigress.startTimer();
            sliderAmountProduce.interactable = false;
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
        //timerInPrigress = moveObj.GetComponentInChildren<Timer>();
        //timerInPrigress.stopTimer();
        machineHelper.OnLoadMachine();
        buttonExit.onClick.Invoke();
        UpDateDataBase();                                                                    //Bo
    }

    public void upgradeAmountDetails()
    {
        if (Purse.Instance.money > upgradeAmountDetailsCost && machineHelper.maxAmountDetails < machineHelper.levelMaxAmountDetail)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= upgradeAmountDetailsCost);
            machineHelper.maxAmountDetails += 1;
            if (machineHelper.maxAmountDetails > machineHelper.levelMaxAmountDetail)
            {
                machineHelper.maxAmountDetails = machineHelper.levelMaxAmountDetail;
            }
            sliderAmountProduce.maxValue = machineHelper.maxAmountDetails;
        }
        select(moveObj);
        UpDateDataBase();                                                                    //Bo
    }

    public void upgradeLevelTimeDetails()
    {
        if (Purse.Instance.money > upgradeTimeProduceDetailCost && machineHelper.timeProduceDetail > machineHelper.levelMinTimeProduceDetail)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= upgradeTimeProduceDetailCost);
            machineHelper.timeProduceDetail -= 1;
            if (machineHelper.timeProduceDetail < 1)
            {
                machineHelper.timeProduceDetail = 1;
            }
        }
        select(moveObj);
        UpDateDataBase();                                                                    //Bo
    }

    public void SetMaxValue()
    {
        sliderAmountProduce.value = machineHelper.maxAmountDetails;
        UpDateDataBase();                                                                    
    }
}
