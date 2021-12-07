using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopMachineForElement : MonoBehaviour
{
    public List<ItemMachine> characters = new List<ItemMachine>();
    public Sprite spriteMachine;
    public int currentNumberMachine = 0;
    [SerializeField] private Sprite button1;
    [SerializeField] private Sprite button2;
    [SerializeField] private GameObject machine;
    [SerializeField] private GameObject button;
    private GameObject detail;
    private GameObject[] detailsToModern;
    private TileMapHelper tileMapHelper;
    private int cost;
    private int timeProduceDetail;                                        //Dw
    private int levelMinTimeProduceDetail;                                //Dw
    private float levelMaxAmountDetail;                                   //Dw
    private int idDetail;
    public int[] idDetailsToModern;                                       //BO

    void Start()
    {
        GameObject grid = GameObject.Find("Grid");                       //Dw
        tileMapHelper = grid.GetComponentInChildren<TileMapHelper>();    //Dw
    }
    public void BuildContent(int idElement)    // вызываем в начале когда строим палитру магазина
    {
        int number = GetNumberMachine(idElement);
        spriteMachine = characters[number].img;
        gameObject.GetComponent<Image>().sprite = spriteMachine;
        currentNumberMachine = number;
        cost = characters[number].cost;
        timeProduceDetail = characters[number].timeProduceDetail;                  //Dw
        levelMinTimeProduceDetail = characters[number].levelMinTimeProduceDetail;  //Dw
        levelMaxAmountDetail = characters[number].levelMaxAmountDetail;            //Dw
        detailsToModern = characters[number].detailsToModern;                      //Dw
        idDetailsToModern = new int[characters[number].detailsToModern.Length];
        for (int i = 0; i < characters[number].detailsToModern.Length; i++)
        {
            idDetailsToModern[i] = characters[number].detailsToModern[i].GetComponent<ItemElement>().GetId();
        }
        detail = characters[number].detail;
        idDetail = detail.GetComponent<ItemElement>().GetId();
        BildElementByMachine(number);
    }
    private void BildElementByMachine(int number)
    {
        GetComponent<SelectElementByShop>().ShowImage(characters[number].detailsToModern.Length);
        for (int i = 0; i < characters[number].detailsToModern.Length; i++)
        {
            if (DBase.Instance.IsUpgradeElement(characters[number].detailsToModern[i].GetComponent<ItemElement>().GetId()))
            {
                GetComponent<SelectElementByShop>().ShowImageElement(i, characters[number].detailsToModern[i].GetComponent<ItemElement>().imgStock);
            }
        }
    }

    public void BuyThisMachine(int totalCost, bool []boolDetailsToModern)    // вызываем когда покупаем станок
    {
        transform.parent.GetComponent<ContentScrolling>().Clear();
        if (Purse.Instance.money >= totalCost)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= totalCost);
            Vector3 tilePositionCamera = tileMapHelper.getTilePositionCamera();                               //Dw
            GameObject machine1 = Instantiate(machine, tilePositionCamera, gameObject.transform.rotation);    //Dw
            machine1.transform.SetParent(MachineParentScript.Instance.gameObject.transform);
            MachineHelper machineHelper = machine1.GetComponent<MachineHelper>();
            machineHelper.InitMachine(currentNumberMachine);
            machineHelper.price = cost;
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
            machine1.GetComponent<MoveObjects>().setup();
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
        for (int i = 0; i < characters.Count; i++)
        {
            for (int j = 0; j < characters[i].detailsToModern.Length; j++)
            {
                if (characters[i].detailsToModern[j].GetComponent<ItemElement>().GetId() == idElement)
                {
                    return i;
                }
            }
        }
        return 0;
    }
    
}
