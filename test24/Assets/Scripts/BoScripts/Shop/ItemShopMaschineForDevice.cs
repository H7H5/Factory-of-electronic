using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemShopMaschineForDevice : MonoBehaviour
{
    public List<MachineDevice> machinesDevice = new List<MachineDevice>();
    [SerializeField] private Sprite button1;
    [SerializeField] private Sprite button2;
    [SerializeField] private Image ImageDevice;
    [SerializeField] private GameObject machine;
    [SerializeField] private Text myText;
    [SerializeField] private GameObject button;
    [SerializeField] private Image imageButton;
    private GameObject deviceObj;
    private Device device;
    private TileMapHelper tileMapHelper;
    private int currentNumberMachine = 0;
    private int cost;
    private int timeProduceDetail;                                       //Dw
    private int levelMinTimeProduceDetail;                                //Dw
    private float levelMaxAmountDetail;                                   //Dw
    private int idDevice;
    [SerializeField] private Image image;

    void Start()
    {
        GameObject grid = GameObject.Find("Grid");                       //Dw
        tileMapHelper = grid.GetComponentInChildren<TileMapHelper>();    //Dw
    }
    public void BuildContent(int idElement)             // вызываем в начале когда строим палитру магазина
    {
        int number = GetNumberMachine(idElement);
        ImageDevice.sprite = machinesDevice[number].GetImgDevice();
        image.sprite = machinesDevice[number].GetImg();
        currentNumberMachine = number;
        cost = machinesDevice[number].GetPrice();
        timeProduceDetail = machinesDevice[number].GetTimeProduceDetail();                  //Dw
        levelMinTimeProduceDetail = machinesDevice[number].GetLevelMinTimeProduceDetail();  //Dw
        levelMaxAmountDetail = machinesDevice[number].GetLevelMaxAmountDetail();            //Dw
        device = machinesDevice[number].GetDevice();
        idDevice = device.id;
        deviceObj = machinesDevice[number].GetDeviceObj();
        myText.text = cost.ToString();
        imageButton.sprite = Purse.Instance.money >= cost ? button1 : button2;
    }
    public void BuyThisMachine()  // вызываем когда покупаем станок
    {
        transform.parent.GetComponent<ContentScrolling>().Clear();
        if (Purse.Instance.money >= cost)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= cost);
            Vector3 tilePositionCamera = tileMapHelper.getTilePositionCamera();                               //Dw
            GameObject machine1 = Instantiate(machine, tilePositionCamera, gameObject.transform.rotation);    //Dw
            machine1.transform.SetParent(MachineParentScript.Instance.transform);
            MachineDeviceHelper machineDeviceHelper = machine1.GetComponent<MachineDeviceHelper>();
            machineDeviceHelper.machineDevice = machinesDevice[currentNumberMachine];
            machineDeviceHelper.InitMachine(currentNumberMachine);
            machineDeviceHelper.upgradeAmountDetailsCost = Convert.ToInt32(cost * 1.5);
            machineDeviceHelper.price = cost/2;
            machineDeviceHelper.timeProduceDetail = timeProduceDetail;                     //Dw
            machineDeviceHelper.levelMinTimeProduceDetail = levelMinTimeProduceDetail;     //Dw
            machineDeviceHelper.levelMaxAmountDetail = levelMaxAmountDetail;               //Dw
            machineDeviceHelper.deviceObj = deviceObj;
            machineDeviceHelper.device = device;
            machineDeviceHelper.idDevice = idDevice;
            machine1.GetComponent<MoveObjects>().SetupWhenBuying();
            GameObject.Find("Shop").SetActive(false);
            ButtonController.Instance.isBlocked = false; //Dw
        }
    }

    private int GetNumberMachine(int idElement)
    {
        for (int i = 0; i < machinesDevice.Count; i++)
        {

            if (machinesDevice[i].GetDevice().id == idElement)
            {
                return i;
            }
        }
        return 0;
    }
}
