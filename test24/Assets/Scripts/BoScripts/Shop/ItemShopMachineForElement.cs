using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopMachineForElement : MonoBehaviour
{
    public List<Machine> machines = new List<Machine>();
    public Sprite spriteMachine;
    public int currentNumberMachine = 0;
    [SerializeField] private Sprite button1;
    [SerializeField] private Sprite button2;
    [SerializeField] private GameObject machine;
    [SerializeField] private GameObject button;
    private Element detail;
    private Element[] detailsToModern;
    private TileMapHelper tileMapHelper;
    private int cost;
    private int timeProduceDetail;                                        //Dw
    private int levelMinTimeProduceDetail;                                //Dw
    private float levelMaxAmountDetail;                                   //Dw
    private int idDetail;
    public int[] idDetailsToModern;                                       //BO
    [SerializeField] private Image image;

    void Start()
    {
        GameObject grid = GameObject.Find("Grid");                       //Dw
        tileMapHelper = grid.GetComponentInChildren<TileMapHelper>();    //Dw
    }
    public void BuildContent(int idElement)    // вызываем в начале когда строим палитру магазина
    {
        int number = GetNumberMachine(idElement);
        spriteMachine = machines[number].sprite;
        image.sprite = machines[number].sprite;
        currentNumberMachine = number;
        timeProduceDetail = machines[number].GetTimeProduceDetail();                  //Dw
        levelMinTimeProduceDetail = machines[number].GetLevelMinTimeProduceDetail();  //Dw
        levelMaxAmountDetail = machines[number].GetLevelMaxAmountDetail();            //Dw
        detailsToModern = machines[number].GetDetailsToModern();                      //Dw
        idDetailsToModern = new int[machines[number].GetDetailsToModern().Length];
        for (int i = 0; i < machines[number].GetDetailsToModern().Length; i++)
        {
            idDetailsToModern[i] = machines[number].GetOneDetailsToModern(i).id;
        }
        detail = machines[number].GetDetail();
        idDetail = detail.id;
        BildElementByMachine(number);
    }
    private void BildElementByMachine(int number)
    {
        GetComponent<SelectElementByShop>().ShowImage(machines[number].GetDetailsToModern().Length);
        for (int i = 0; i < machines[number].GetDetailsToModern().Length; i++)
        {
            if (DBase.Instance.IsUpgradeElement(machines[number].GetOneDetailsToModern(i).id))
            {
                GetComponent<SelectElementByShop>().ShowImageElement(i, machines[number].GetOneDetailsToModern(i).sprite);
            }
        }
    }

    public void BuyThisMachine(int totalCost, bool []boolDetailsToModern, int currentMachine)    // вызываем когда покупаем станок
    {
        transform.parent.GetComponent<ContentScrolling>().Clear();
        if (Purse.Instance.money >= totalCost)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= totalCost);
            Vector3 tilePositionCamera = tileMapHelper.getTilePositionCamera();                               //Dw
            GameObject machine1 = Instantiate(machine, tilePositionCamera, gameObject.transform.rotation);    //Dw
            machine1.transform.SetParent(MachineParentScript.Instance.gameObject.transform);
            MachineHelper machineHelper = machine1.GetComponent<MachineHelper>();
            machineHelper.machine = machines[currentMachine];
            machineHelper.InitMachine(currentNumberMachine);
            machineHelper.price = totalCost;
            machineHelper.timeProduceDetail = timeProduceDetail;                     //Dw
            machineHelper.levelMinTimeProduceDetail = levelMinTimeProduceDetail;     //Dw
            machineHelper.levelMaxAmountDetail = levelMaxAmountDetail;               //Dw
            machineHelper.detail = detail;
            machineHelper.idDetail = idDetail;
            machineHelper.detailsToModern = detailsToModern;
            machineHelper.idDetailsToModern = idDetailsToModern;
            machineHelper.boolDetailsToModern = boolDetailsToModern;
            for (int i = boolDetailsToModern.Length-1; i >-1 ; i--)
            {
                if (boolDetailsToModern[i])
                {
                    machineHelper.selectNumberElement = i;
                    machineHelper.selectedDetail = i;
                }
            }
            machine1.GetComponent<MoveObjects>().SetupWhenBuying();
            GameObject.Find("Shop").SetActive(false);
            ButtonController.Instance.isBlocked = false;        //Dw
        }
    }
    public void OpenComplect()
    {
        ContentScrolling.Instance.OnVisibleEquipmentPanel(this);
    }
    private int GetNumberMachine( int idElement)
    {
        for (int i = 0; i < machines.Count; i++)
        {
            for (int j = 0; j < machines[i].GetDetailsToModern().Length; j++)
            {
                if (machines[i].GetOneDetailsToModern(j).id == idElement)
                {
                    return i;
                }
            }
        }
        return 0;
    }
    
}
