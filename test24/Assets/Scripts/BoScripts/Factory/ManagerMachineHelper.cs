using System;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMachineHelper : PanelOld
{
    private float priceOneDetail;
    private float priceProduce;
    public Text textPriceMaterial;
    public Text textPriceOneDetail;
    public Text textPriceProduce;
    public Element[] detailsToModern;
    public int upgradeDetailToModernCost = 100000;
    public MachineHelper machineHelper;
    public SwitchElementManager switchElementManager;
    private void Awake()
    {
        sliderAmountProduce = GetComponentInChildren<Slider>();
    }

    public override void select(GameObject Object)   
    {
        InitDataInMachine(Object);
        ShowParameters();
        CalculateSliderValue();
        ButtonsON_OFF();
        InitSwitchElementManager();
    }
    private void InitDataInMachine(GameObject Object)
    {
        moveObj = Object;
        machineHelper = moveObj.GetComponent<MachineHelper>();
        img.sprite = machineHelper.sprt;
        amountDetails = machineHelper.amountDetails;
        detailsToModern = machineHelper.detailsToModern;
        imageDetail.sprite = detailsToModern[machineHelper.selectedDetail].sprite;
        priceOneDetail = detailsToModern[machineHelper.selectedDetail].GetPrice();
        upgradeAmountDetailsCost = Convert.ToInt32(machineHelper.detail.costMachine * 1.5);
    }

    private void ShowParameters()
    {
        textPriceOneDetail.text = priceOneDetail.ToString();
        textPriceMaterial.text = (priceOneDetail / 2).ToString();
        ShowProductionTimeText();
        ShowPriceProduceText();
        ShowLevelMaxDetails();
        ShowLevelTimeDetails();
        ShowUpgradePrices();
    }
    public void ShowProductionTimeText()
    {
        productionTimeText.text = GetProductionTime().ToString();
    }
    public void ShowPriceProduceText()
    {
        CalculatePriceProduce();
        textPriceProduce.text = priceProduce.ToString();
        MoneyChangesDisplay.Instance.ShowMoneyChanges(-(int)priceProduce);
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
        //timeProduceDetailText.text = machineHelper.timeProduceDetail.ToString();
    }
    public void ShowUpgradePrices()
    {
        textUpgradeAmountDetailsCost.text = upgradeAmountDetailsCost.ToString();
        textUpgradeTimeProduceDetailCost.text = upgradeTimeProduceDetailCost.ToString();
    }

    private float GetProductionTime()
    {
        return machineHelper.timeProduceDetail * sliderAmountProduce.value;
    }
    public void CalculatePriceProduce()
    {
        priceProduce = (int)((sliderAmountProduce.value * detailsToModern[machineHelper.selectedDetail].GetPrice()) / 2);
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
            buttonUpgradeTime.interactable = false;
            //buttonMax.interactable = false;
            buttonStop.interactable = true;
            buttonMoveMachine.interactable = false;
            buttonSellMachine.interactable = false;
            buttonSendStock.interactable = false;
        }
        else
        {
            sliderAmountProduce.interactable = true;
            buttonStart.interactable = priceProduce < Purse.Instance.money ? true : false;
            buttonUpgradeMaxDetail.interactable = Purse.Instance.money >= upgradeAmountDetailsCost ? true : false;
            buttonUpgradeTime.interactable = Purse.Instance.money >= upgradeTimeProduceDetailCost ? true : false;
            //buttonMax.interactable = sliderAmountProduce.value < sliderAmountProduce.maxValue ? true : false;
            buttonStop.interactable = false;
            buttonMoveMachine.interactable = true;
            buttonSellMachine.interactable = true;
            buttonSendStock.interactable = true;
        }
    }

    private void InitSwitchElementManager()
    {
        switchElementManager.ShowSwitchElementsObj(detailsToModern, machineHelper.boolDetailsToModern);
        switchElementManager.SwitchElementOn(machineHelper.selectNumberElement);
    }

    public void move()                                                                           
    {
        moveObj.gameObject.GetComponent<MoveObjects>().StartMove();
        UIPanels.Instance.OpenMoveMachinePanel(moveObj,false);
    }
    public void sell()                                                                       
    {
        Purse.Instance.SetMoney(Purse.Instance.money += moveObj.GetComponent<MachineHelper>().price);
        sendStock();
    }
    public void sendStock ()                                                                       
    {
        moveObj.transform.parent.GetComponent<MachineParentScript>().DeleteOneMachineByIdJSON(moveObj.GetComponent<MachineHelper>().supper_idJSON); 
        Destroy(moveObj);                                                                          
    }

    public override void startTimer()
    {
        CalculatePriceProduce();
        if (priceProduce < Purse.Instance.money)
        {
            machineHelper.startTimeDetailProduce = DateTime.UtcNow.ToString();
            Purse.Instance.SetMoney(Purse.Instance.money -= (int)priceProduce);
            activateTimer();
            moveObj.GetComponentInChildren<Timer>().StartTimer(GetProductionTime());
            ButtonController.Instance.UnBlockButtons();
            machineHelper.StartProduce();
            buttonExit.onClick.Invoke();
        }
        upDateDataBase();                                                                    
    }

    public override void stopTimer()
    {
        activateTimer();
        timerInPrigress = moveObj.GetComponentInChildren<Timer>();
        //timerInPrigress.StopTimer();
        machineHelper.OnLoadMachine();
        buttonExit.onClick.Invoke();
        upDateDataBase();                                                                    
    }

    public void activateTimer()
    {
        moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SetMaxValue ()
    {
        sliderAmountProduce.maxValue = machineHelper.maxAmountDetails;
        sliderAmountProduce.value = machineHelper.maxAmountDetails;
        upDateDataBase();                                                                    //Bo
    }

    public void getAmountDetails ()
    {
        machineHelper.amountDetails = (int)sliderAmountProduce.value;
        CheckButtonStartActiv();
        upDateDataBase();
    }

    public void CheckButtonStartActiv()
    {
        CalculatePriceProduce();
        if (priceProduce < Purse.Instance.money)
        {
            buttonStart.interactable = true;
        }
        else
        {
            buttonStart.interactable = false;
        }
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
        upDateDataBase();                                                                    
    }

    public void upDateDataBase()                                                                     
    {                                                                                               
        moveObj.transform.parent.GetComponent<MachineParentScript>().UpDateOneMachineByid(moveObj); 
    }
    
    public void selectElement(int num)                                //Bo
    {                                                                 //Bo
        machineHelper.selectNumberElement = num;                      //Bo
        machineHelper.selectedDetail =num;                                                                        //BO
        imageDetail.sprite = detailsToModern[machineHelper.selectedDetail].sprite;  //BO
        priceOneDetail = detailsToModern[machineHelper.selectedDetail].GetPrice();    //BO
        textPriceOneDetail.text = priceOneDetail.ToString();                                                      //BO
        textPriceMaterial.text = (priceOneDetail / 2).ToString();                                                 //BO
        ShowPriceProduceText();                                                                                   //BO
        ShowProductionTimeText();                                                                                 //BO
        ShowLevelTimeDetails();                                                                                   //BO
        upDateDataBase();                                             //Bo
    }                                                                 //Bo
    public void ModernNewElement(int id)
    {
        for (int i = 0; i < machineHelper.idDetailsToModern.Length; i++)
        {
            if (machineHelper.idDetailsToModern[i] == id)
            {
                machineHelper.boolDetailsToModern[i] = true;
                upDateDataBase();
            }
        }
    }
}
