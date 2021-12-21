using System;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    public bool MouseDown = false;

    private new Transform transform;

    public GameObject Canvas;                           //Bo
    private GameObject machineMenu;                     //Bo
    private GameObject machineDeviceMenu;                     //Bo
    private GameObject moveMachinePanel;                //Bo
    private GameObject detailProduced;
    private GameObject timerObject;
    private Timer timer;
    private TimerDevice timerDevice;

    public bool isMove = false; //Dw переменная указывающая двигается ли объект

    public bool canBeInstalled; //Dw переменная определяющая "можно установить станок"

    private TileMapHelper tileMapHelper;

    public Camera mainCamera;
    public CameraController cameraController;

    private SpriteRenderer spriteRenderer;

    public MachineHelper machineHelper;
    public MachineDeviceHelper machineDeviceHelper;

    public ButtonController buttonController;

    private DBase dBase;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraController = mainCamera.GetComponent<CameraController>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        detailProduced = gameObject.transform.GetChild(6).gameObject.transform.GetChild(1).gameObject;
        timerObject = gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject;
        if (timerObject.GetComponent<Timer>())
        {
            timer = timerObject.GetComponent<Timer>();
        }
        if (timerObject.GetComponent<TimerDevice>())
        {
            timerDevice = timerObject.GetComponent<TimerDevice>();
        }

        machineHelper = gameObject.GetComponent<MachineHelper>();
        machineDeviceHelper = gameObject.GetComponent<MachineDeviceHelper>();

        GameObject dwObjects = GameObject.Find("DwObjects");
        buttonController = dwObjects.GetComponentInChildren<ButtonController>();
    }

    void Start()
    {
        Canvas = GameObject.Find("Canvas");                                                                 //Bo
        Transform transformMachineMenu = Canvas.GetComponent<Transform>().Find("MachineMenu");              //Bo
        machineMenu = transformMachineMenu.gameObject;                                                      //Bo
        transformMachineMenu = Canvas.GetComponent<Transform>().Find("MachineDeviceMenu");                  //Bo
        machineDeviceMenu = transformMachineMenu.gameObject;                                                //Bo

        Transform transformMoveMachinePanel = Canvas.GetComponent<Transform>().Find("MoveMachinePanel");    //Bo
        moveMachinePanel = transformMoveMachinePanel.gameObject;                                            //Bo

        GameObject grid = GameObject.Find("Grid");
        tileMapHelper = grid.GetComponentInChildren<TileMapHelper>();
        transform = GetComponent<Transform>();

        GameObject dataBase = GameObject.Find("DBase");
        dBase = dataBase.GetComponent<DBase>();
    }

    public void startMove()
    {
        isMove = true;
        cameraController.startMoveToObject();
        timerObject.SetActive(false);
        spriteRenderer.sortingOrder = 100;
    }

    public void stopMove()
    {
        isMove = false;
        cameraController.stopMoveToObject();
        if (machineHelper)
        {
            if (timer.isTimerProgress == true)
            {
                timerObject.SetActive(true);
            }
        }
        if (machineDeviceHelper)
        {
            if (timerDevice.isTimerProgress == true)
            {
                timerObject.SetActive(true);
            }
        }
        spriteRenderer.sortingOrder = 2;
    }

    private void OnMouseDown()
    {
        cameraController.StartPositionCursor();
        if (detailProduced.activeInHierarchy == false && isMove == true)
        {
            MouseDown = true;
        }
    }

    private void OnMouseUp()
    {
        MouseDown = false;
        if (IsSelectMachine())
        {
            if (detailProduced.activeInHierarchy == true) 
            {
                CollectProduct();
            }
            else
            {
                SelectMachine();
            }
        }
    }
    private bool IsSelectMachine()
    {
        cameraController.EndPositionCursor();
        float distance = Vector2.Distance(cameraController.startPositionCursor, cameraController.endPositionCursor);
        return buttonController.isBlocked == false && distance < 0.1 ? true : false;
    } 
    private void CollectProduct()
    {
        detailProduced.SetActive(false);
        if (machineHelper)
        {
            dBase.AddElementOnIdByCount(machineHelper.GetIdSelectedDetail(), machineHelper.amountDetails);
            MoveDetailToStock();
            machineHelper.stopProduce();
        }
        if (machineDeviceHelper)
        {
            dBase.AddDeviceByCount(machineDeviceHelper.itemDevice, machineDeviceHelper.amountDetails);
            MoveDetailToStock();
            machineDeviceHelper.stopProduce();
        }
    }
    private void MoveDetailToStock()
    {
        GameObject test = Instantiate(detailProduced, detailProduced.transform.parent);
        test.SetActive(true);
        test.GetComponent<DetailToInventory>().moveToInventory(); 
    }
    private void SelectMachine()
    {
        if (isMove == false)                                                            
        {                                                                                  
            buttonController.BlockButtons();
            if (machineHelper)
            {
                machineMenu.SetActive(true);                                                
                machineMenu.GetComponent<ManagerMachineHelper>().select(gameObject);        
            }
            if (machineDeviceHelper)
            {
                machineDeviceMenu.SetActive(true);                                                
                machineDeviceMenu.GetComponent<ManagerMachineDeviceHelper>().select(gameObject);  
            }                                                            
        }                                                                                   
    }

    private void checkCanBeInstalled ()
    {
        canBeInstalled = true;

        foreach (Transform child in transform)
        {
            if (child.tag == "ColorField")
            {
                if (child.GetComponent<SpriteRenderer>().sprite.name == "ColorFieldRed")
                {
                    canBeInstalled = false;
                }
            }
        }
    }

    private void moveMachine ()
    {
        transform.position = tileMapHelper.getTilePosition();
    }

    void Update()
    {
        if (isMove)
        {
            checkCanBeInstalled();

            cameraController.MovableObjectPositionX = transform.position.x;
            cameraController.MovableObjectPositionY = transform.position.y;
        }

        if (MouseDown)
        {
            moveMachine();
        }
    }

    public void setup()                                                                                     //Bo
    {                                                                                                       //Bo
        GameObject Canvas = GameObject.Find("Canvas");                                                      //Bo
        Transform transformMoveMachinePanel = Canvas.GetComponent<Transform>().Find("MoveMachinePanel");    //Bo
        moveMachinePanel = transformMoveMachinePanel.gameObject;                                            //Bo
        moveMachinePanel.SetActive(true);                                                                   //Bo

        startMove();

        moveMachinePanel.GetComponent<MoveButton>().select(gameObject,0);                                   //Bo
        //GameObject.Find("BlockButton").GetComponent<ActivButton>().activ(false);                            //Bo
        //ActivButton.activMachine = true;                                                                    //Bo
    }                                                                                                       //Bo
}