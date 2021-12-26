using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMachineHelper : PanelOld
{
    private float priceOneDetail;
    private float priceProduce;
    public Text textPriceMaterial;
    public Text textPriceOneDetail;
    public Text textPriceProduce;
    public GameObject[] detailsToModern;
    public int upgradeDetailToModernCost = 100000;
    public MachineHelper machineHelper;
    public SwitchElementManager switchElementManager;    
    private void Awake()
    {
        sliderAmountProduce = GetComponentInChildren<Slider>();
    }

    public override void select(GameObject gameObject)   
        
        //Bo
    {
        moveObj = gameObject;                                                                       //Bo
        //StartPosition = gameObject.transform.position;                                              //Bo

        machineHelper = moveObj.GetComponent<MachineHelper>();

        img.sprite = machineHelper.sprt;                                                            //Bo
          
        amountDetails = machineHelper.amountDetails;

        detailsToModern = machineHelper.detailsToModern;
        //boolDetailsToModern = machineHelper.boolDetailsToModern;
        //timeProduceDetail = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().GetTimeProduceDetail();
        imageDetail.sprite = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().imgStock;
        priceOneDetail = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().GetPrice();

        textPriceOneDetail.text = priceOneDetail.ToString();
        textPriceMaterial.text = (priceOneDetail / 2).ToString();

        ShowProductionTimeText();
        ShowPriceProduceText();
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

            CalculatePriceProduce();
            if(priceProduce < Purse.Instance.money)
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
        switchElementManager.ShowSwitchElementsObj(detailsToModern, machineHelper.boolDetailsToModern);                          //BO
        switchElementManager.SwitchElementOn(machineHelper.selectNumberElement);              //BO
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

    public void CalculatePriceProduce()
    {
        if (detailsToModern.Length > 0)
        {
            priceProduce = (int)((sliderAmountProduce.value * detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().GetPrice()) / 2);
        }
        else
        {
            priceProduce = (int)((sliderAmountProduce.value * machineHelper.detail.GetComponent<ItemElement>().GetPrice()) / 2);
        }
    }

    public void ShowPriceProduceText()
    {
        CalculatePriceProduce();
        textPriceProduce.text = priceProduce.ToString();
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

    public void ModernMachine ()
    {
        if (Purse.Instance.money >= upgradeDetailToModernCost)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= upgradeDetailToModernCost);
            machineHelper.levelDetailsToModern += 1;
            upDateDataBase();
            select(moveObj);
        }
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
        Purse.Instance.SetMoney(Purse.Instance.money += moveObj.GetComponent<MachineHelper>().price);
        //Purse.money += moveObj.GetComponent<MachineHelper>().price;                                 //Bo
       // moveObj.transform.parent.GetComponent<MachineParentScript>().deleteOneMachineById(moveObj.GetComponent<MachineHelper>().supper_id); //Bo
        moveObj.transform.parent.GetComponent<MachineParentScript>().DeleteOneMachineByIdJSON(moveObj.GetComponent<MachineHelper>().supper_idJSON); //Bo
        Destroy(moveObj);                                                                           //Bo
    }

    public void sendStock ()                                                                        //Bo
    {
       // moveObj.transform.parent.GetComponent<MachineParentScript>().deleteOneMachineById(moveObj.GetComponent<MachineHelper>().supper_id); //Bo
        moveObj.transform.parent.GetComponent<MachineParentScript>().DeleteOneMachineByIdJSON(moveObj.GetComponent<MachineHelper>().supper_idJSON); //Bo
        Destroy(moveObj);                                                                           //Bo
    }

    public override void startTimer()
    {
        CalculatePriceProduce();
        if (priceProduce < Purse.Instance.money)
        {
            machineHelper.startTimeDetailProduce = DateTime.UtcNow.ToString();
            Purse.Instance.SetMoney(Purse.Instance.money -= (int)priceProduce);
            //Purse.money -= (int)priceProduce;

            moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            timerInPrigress = moveObj.GetComponentInChildren<Timer>();
            CalculateProductionTime();

            ShowProductionTimeText();
            timerInPrigress.productionTime = productionTime;
            timerInPrigress.startTimer();

            sliderAmountProduce.interactable = false;

            ButtonController.Instance.UnBlockButtons();
            machineHelper.StartProduce();
            buttonExit.onClick.Invoke();
        }
        else
        {
            Debug.Log("Not enought money");
        }
        upDateDataBase();                                                                    //Bo
    }

    public override void stopTimer()
    {
        activateTimer();
        //timerInPrigress = moveObj.GetComponentInChildren<Timer>();
        //timerInPrigress.stopTimer();
        machineHelper.OnLoadMachine();
        buttonExit.onClick.Invoke();
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

    public void SetMaxValue ()
    {
        activateTimer();

        timerInPrigress = moveObj.GetComponentInChildren<Timer>();

        sliderAmountProduce.maxValue = machineHelper.maxAmountDetails;
        sliderAmountProduce.value = machineHelper.maxAmountDetails;

        disactivateTimer();

        upDateDataBase();                                                                    //Bo
    }

    public void getAmountDetails ()
    {
        machineHelper.amountDetails = (int)sliderAmountProduce.value;
        select(moveObj);
    }

    public void upgradeAmountDetails ()
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
        upDateDataBase();                                                                    //Bo
    }

    public void upgradeLevelTimeDetails ()
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
        upDateDataBase();                                                                    //Bo
    }
    public void upDateDataBase()                                                                     //Bo
    {                                                                                               //Bo
        moveObj.transform.parent.GetComponent<MachineParentScript>().UpDateOneMachineByid(moveObj); //Bo
    }                                                                                                //Bo
    public void selectElement(int num)                                //Bo
    {                                                                 //Bo
        machineHelper.selectNumberElement = num;                      //Bo
        machineHelper.selectedDetail =num;                                                                        //BO
        imageDetail.sprite = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().imgStock;  //BO
        priceOneDetail = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().GetPrice();    //BO
        textPriceOneDetail.text = priceOneDetail.ToString();                                                      //BO
        textPriceMaterial.text = (priceOneDetail / 2).ToString();                                                 //BO
        ShowPriceProduceText();                                                                                   //BO
        ShowProductionTimeText();                                                                                 //BO
        ShowLevelTimeDetails();                                                                                   //BO
        upDateDataBase();                                             //Bo
    }                                                                 //Bo
    public void ModernNewElement(int id)
    {
        int j = 0;
        for (int i = 0; i < machineHelper.idDetailsToModern.Length; i++)
        {
            if (machineHelper.idDetailsToModern[i] == id)
            {
                j = i;
            }
        }
        machineHelper.boolDetailsToModern[j] = true;
        upDateDataBase();

    }
}
