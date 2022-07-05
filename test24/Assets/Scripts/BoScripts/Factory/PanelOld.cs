using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOld : MonoBehaviour
{
    public GameObject moveObj;
    public GameObject moveMachinePanel;
    public Image img;
    protected Timer timerInPrigress;
    protected Slider sliderAmountProduce;
    public Text timeProduceDetailText;
    public Text productionTimeText;
    public Text textLevelMaxDetails;
    public Text textLevelTimeDetails;
    public Text textPercentLevelMaxDetails;
    public Text textUpgradeAmountDetailsCost;
    public Text textUpgradeTimeProduceDetailCost;
    public int upgradeAmountDetailsCost;
    public int upgradeTimeProduceDetailCost = 30000;
    public float amountDetails;
    public Image imageDetail;
    public Transform barLevelMaxDetail;
    public Button buttonUpgradeMaxDetail;
    public Button buttonUpgradeTime;
    public Button buttonStop;
    public Button buttonMoveMachine;
    public Button buttonSellMachine;
    public Button buttonSendStock;
    public Button buttonExit;

    public Button buttonStart;



    public Button buttonMax;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void select(GameObject gameObject) { 
            
    }
    public virtual void startTimer()
    {

    }
    public virtual void stopTimer()
    {

    }


}
