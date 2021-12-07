using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMachineHelper : MonoBehaviour
{
    private  GameObject moveObj;                                                                    //Bo
    public GameObject moveMachinePanel;                                                             //Bo
    public Image img;                                                                               //Bo
    private GameObject imgMac;                                                                      //Bo
    Vector3 StartPosition;                                                                          //Bo
    public BasketBoardScript basket;                                                                //Bo

    public Timer timerInPrigress;

    private Slider sliderAmountProduce;
    
    public float timeProduceDetail;
    public Text timeProduceDetailText;
    private float productionTime;
    public Text productionTimeText;
    public Text textLevelMaxDetails;
    public Text textLevelTimeDetails;
    public Text textPercentLevelMaxDetails;
    public Text textUpgradeAmountDetailsCost;
    public Text textUpgradeTimeProduceDetailCost;

    private float priceMaterial;
    private float priceOneDetail;
    private float priceProduce;
    public Text textPriceMaterial;
    public Text textPriceOneDetail;
    public Text textPriceProduce;

    public GameObject[] detailsToModern;
    public bool[] boolDetailsToModern;

    public int upgradeDetailToModernCost = 100000;
    public int upgradeAmountDetailsCost = 15000;
    public int upgradeTimeProduceDetailCost = 30000;

    public float amountDetails;

    public Image imageDetail;

    public MachineHelper machineHelper;

    public Transform barLevelMaxDetail;

    public ButtonController buttonController;

    public GameObject orderListControllerObject;
    private OrderListController orderListController;

    [Header("Buttons")]
    public GameObject buttonStartObj;
    public GameObject buttonUpgradeMaxDetailObj;
    public GameObject buttonUpgradeTimeObj;
    public GameObject buttonMaxObj;
    public GameObject buttonStopObj;
    public GameObject buttonModernizationObj;
    public GameObject buttonMoveMachineObj;
    public GameObject buttonSellMachineObj;
    public GameObject buttonSendStockObj;
    public GameObject buttonExitObj;
    public GameObject buttonLeftSelectDetailObj;
    public GameObject buttonRightSelectDetailObj;

    private Button buttonStart;
    private Button buttonUpgradeMaxDetail;
    private Button buttonUpgradeTime;
    private Button buttonMax;
    private Button buttonStop;
    private Button buttonModernization;
    private Button buttonMoveMachine;
    private Button buttonSellMachine;
    private Button buttonSendStock;
    private Button buttonExit;

    public SwitchElementManager switchElementManager;    //BO
    private void Awake()
    {
        sliderAmountProduce = GetComponentInChildren<Slider>();
        orderListController = orderListControllerObject.GetComponent<OrderListController>();

        buttonStart = buttonStartObj.GetComponent<Button>();
        buttonUpgradeMaxDetail = buttonUpgradeMaxDetailObj.GetComponent<Button>();
        buttonUpgradeTime = buttonUpgradeTimeObj.GetComponent<Button>();
        buttonMax = buttonMaxObj.GetComponent<Button>();
        buttonStop = buttonStopObj.GetComponent<Button>();
        buttonModernization = buttonModernizationObj.GetComponent<Button>();
        buttonMoveMachine = buttonMoveMachineObj.GetComponent<Button>();
        buttonSellMachine = buttonSellMachineObj.GetComponent<Button>();
        buttonSendStock = buttonSendStockObj.GetComponent<Button>();
        buttonExit = buttonExitObj.GetComponent<Button>();
    }

    void Start()
    {
        GameObject dwObjects = GameObject.Find("DwObjects");
        buttonController = dwObjects.GetComponentInChildren<ButtonController>();
    }

    void Update()
    {

    }

    public void ShowProductionTimeText()
    {
        calculateProductionTime();
        productionTimeText.text = productionTime.ToString();
    }

    public void ShowPriceProduceText ()
    {
        calculatePriceProduce();
        textPriceProduce.text = priceProduce.ToString();
    }

    public void ShowLevelMaxDetails ()
    {
        textLevelMaxDetails.text = machineHelper.maxAmountDetails.ToString() + "/" + machineHelper.levelMaxAmountDetail;
        barLevelMaxDetail.GetComponent<Image>().fillAmount = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail));
        float test = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail));
        test = (int)(test * 100);
        textPercentLevelMaxDetails.text = test.ToString() + "%";

        //if (detailsToModern.Length > 0)
        //{
        //    //textLevelMaxDetails.text = machineHelper.maxAmountDetails.ToString() + "/" + detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().levelMaxAmountDetail;
        //    textLevelMaxDetails.text = machineHelper.maxAmountDetails.ToString() + "/" + machineHelper.levelMaxAmountDetail;
        //    barLevelMaxDetail.GetComponent<Image>().fillAmount = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail);
        //    float test = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail);
        //    test = (int)(test * 100);
        //    textPercentLevelMaxDetails.text = test.ToString() + "%";
        //} 
        //else
        //{
        //    textLevelMaxDetails.text = machineHelper.maxAmountDetails.ToString() + "/" + machineHelper.levelMaxAmountDetail;
        //    barLevelMaxDetail.GetComponent<Image>().fillAmount = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail));
        //    float test = (float)(machineHelper.maxAmountDetails * (1 / machineHelper.levelMaxAmountDetail));
        //    test = (int)(test * 100);
        //    textPercentLevelMaxDetails.text = test.ToString() + "%";
        //}  
    }

    public void ShowLevelTimeDetails()
    {
        textLevelTimeDetails.text = machineHelper.timeProduceDetail.ToString() + "/" + machineHelper.levelMinTimeProduceDetail;
        timeProduceDetailText.text = machineHelper.timeProduceDetail.ToString();
    }

    public void ShowUpgradePrices ()
    {
        textUpgradeAmountDetailsCost.text = upgradeAmountDetailsCost.ToString();
        textUpgradeTimeProduceDetailCost.text = upgradeTimeProduceDetailCost.ToString();
    }

    public void select(GameObject gameObject)                                                      //Bo
    {
        moveObj = gameObject;                                                                       //Bo
        StartPosition = gameObject.transform.position;                                              //Bo

        machineHelper = moveObj.GetComponent<MachineHelper>();

        img.sprite = machineHelper.sprt;                                                            //Bo
          
        amountDetails = machineHelper.amountDetails;

        detailsToModern = machineHelper.detailsToModern;
        boolDetailsToModern = machineHelper.boolDetailsToModern;
        timeProduceDetail = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().GetTimeProduceDetail();
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

            calculatePriceProduce();
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
            
            if(sliderAmountProduce.value < sliderAmountProduce.maxValue)
            {
                buttonMax.interactable = true;
            }
            else
            {
                buttonMax.interactable = false;
            }
            
            buttonStop.interactable = false;
            buttonMoveMachine.interactable = true;
            buttonSellMachine.interactable = true;
            buttonSendStock.interactable = true;
        }

        if (orderListController.rating > 99 && detailsToModern.Length > 1 && machineHelper.levelDetailsToModern == 0)
        {
            buttonModernizationObj.SetActive(true);
            if (Purse.Instance.money >= upgradeDetailToModernCost)
            {
                //buttonModernization.interactable = true;
            } 
            else
            {
                buttonModernization.interactable = false;
            }      
        }
        else
        {
            buttonModernizationObj.SetActive(false);
        }

        if (machineHelper.levelDetailsToModern > 0)
        {
            buttonLeftSelectDetailObj.SetActive(true);
            buttonRightSelectDetailObj.SetActive(true);
        } else
        {
            buttonLeftSelectDetailObj.SetActive(false);
            buttonRightSelectDetailObj.SetActive(false);
        }

        switchElementManager.ShowSwitchElementsObj(detailsToModern, boolDetailsToModern);                          //BO
        switchElementManager.SwitchElementOn(machineHelper.selectNumberElement);              //BO
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

    public void SelectDetailRight ()
    {
        machineHelper.selectedDetail += 1;
        if (machineHelper.selectedDetail >= detailsToModern.Length)
        {
            machineHelper.selectedDetail = detailsToModern.Length - 1;          
        }
        imageDetail.sprite = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().imgStock;
        priceOneDetail = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().GetPrice();
        textPriceOneDetail.text = priceOneDetail.ToString();
        textPriceMaterial.text = (priceOneDetail / 2).ToString();
        ShowPriceProduceText();
        ShowProductionTimeText();
        ShowLevelTimeDetails();

        upDateDataBase();
    }

    public void SelectDetailLeft ()
    {
        machineHelper.selectedDetail -= 1;
        if (machineHelper.selectedDetail < 0)
        {
            machineHelper.selectedDetail = 0;           
        }
        imageDetail.sprite = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().imgStock;
        priceOneDetail = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().GetPrice();
        textPriceOneDetail.text = priceOneDetail.ToString();
        textPriceMaterial.text = (priceOneDetail / 2).ToString();
        ShowPriceProduceText();
        ShowProductionTimeText();
        ShowLevelTimeDetails();

        upDateDataBase();
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
        Purse.Instance.SetMoney(Purse.Instance.money += moveObj.GetComponent<MachineHelper>().price);
        //Purse.money += moveObj.GetComponent<MachineHelper>().price;                                 //Bo
       // moveObj.transform.parent.GetComponent<MachineParentScript>().deleteOneMachineById(moveObj.GetComponent<MachineHelper>().supper_id); //Bo
        moveObj.transform.parent.GetComponent<MachineParentScript>().deleteOneMachineByIdJSON(moveObj.GetComponent<MachineHelper>().supper_idJSON); //Bo
        Destroy(moveObj);                                                                           //Bo
    }

    public void sendStock ()                                                                        //Bo
    {
       // moveObj.transform.parent.GetComponent<MachineParentScript>().deleteOneMachineById(moveObj.GetComponent<MachineHelper>().supper_id); //Bo
        moveObj.transform.parent.GetComponent<MachineParentScript>().deleteOneMachineByIdJSON(moveObj.GetComponent<MachineHelper>().supper_idJSON); //Bo
        Destroy(moveObj);                                                                           //Bo
    }

    public void calculateProductionTime()
    {
        productionTime = machineHelper.timeProduceDetail * sliderAmountProduce.value;

        //if (detailsToModern.Length > 0)
        //{
        //    productionTime = detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().timeProduceDetail * sliderAmountProduce.value;
        //} 
        //else
        //{
        //    productionTime = machineHelper.timeProduceDetail * sliderAmountProduce.value;
        //}  
    }

    public void calculatePriceProduce ()
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

    public void startTimer()
    {
        calculatePriceProduce();
        if (priceProduce < Purse.Instance.money)
        {
            machineHelper.startTimeDetailProduce = DateTime.UtcNow.ToString();
            Purse.Instance.SetMoney(Purse.Instance.money -= (int)priceProduce);
            //Purse.money -= (int)priceProduce;

            moveObj.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            timerInPrigress = moveObj.GetComponentInChildren<Timer>();
            calculateProductionTime();

            ShowProductionTimeText();
            timerInPrigress.productionTime = productionTime;
            timerInPrigress.startTimer();

            sliderAmountProduce.interactable = false;

            buttonController.UnBlockButtons();
            machineHelper.startProduce();
            buttonExit.onClick.Invoke();
        }
        else
        {
            Debug.Log("Not enought money");
        }
        upDateDataBase();                                                                    //Bo
    }

    public void stopTimer()
    {
        activateTimer();
        //timerInPrigress = moveObj.GetComponentInChildren<Timer>();
        //timerInPrigress.stopTimer();
        machineHelper.onLoadMachine();
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
        machineHelper.amountDetails = sliderAmountProduce.value;
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
        moveObj.transform.parent.GetComponent<MachineParentScript>().upDateOneMachineByid(moveObj); //Bo
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
