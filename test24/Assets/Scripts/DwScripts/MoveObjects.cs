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
        cameraController.EndPositionCursor();
        float distance = Vector2.Distance(cameraController.startPositionCursor, cameraController.endPositionCursor);
        if (buttonController.isBlocked == false && distance < 0.1)
        {
            if (detailProduced.activeInHierarchy == true)
            {
                detailProduced.SetActive(false);
                
                if (machineHelper)
                {
                    for (int i = 0; i < machineHelper.amountDetails; i++)
                    {
                        if (machineHelper.selectedDetail > 0)
                        {
                            dBase.addElementOnId(machineHelper.detailsToModern[machineHelper.selectedDetail].GetComponent<ItemElement>().GetId());
                        }
                        else
                        {
                            dBase.addElementOnId(machineHelper.itemElementDetail.GetId());
                        }
                    }
                    GameObject test = Instantiate(detailProduced, detailProduced.transform.parent);
                    test.SetActive(true);
                    test.GetComponent<DetailToInventory>().moveToInventory();
                    machineHelper.stopProduce();
                }
                if (machineDeviceHelper)
                {
                    for (int i = 0; i < machineDeviceHelper.amountDetails; i++)
                    {
                        dBase.addDevice(machineDeviceHelper.itemDevice);
                    }
                    GameObject test = Instantiate(detailProduced, detailProduced.transform.parent);
                    test.SetActive(true);
                    test.GetComponent<DetailToInventory>().moveToInventory();
                    machineDeviceHelper.stopProduce();
                }


            }
            else
            {
                if (isMove == false)                                                                //Bo
                {                                                                                   //Bo
                    buttonController.BlockButtons();
                    if (ActivButton.activMachine == false)                                          //Bo
                    {                                                                               //Bo
                        if (machineHelper)
                        {
                            machineMenu.SetActive(true);                                                //Bo
                            machineMenu.GetComponent<ManagerMachineHelper>().select(gameObject);        //Bo
                        }
                        if (machineDeviceHelper)
                        {
                            machineDeviceMenu.SetActive(true);                                                //Bo
                            machineDeviceMenu.GetComponent<ManagerMachineDeviceHelper>().select(gameObject);  //Bo
                        }

                        cameraController.stopMove();

                        GameObject.Find("BlockButton").GetComponent<ActivButton>().activ(false);    //Bo
                        ActivButton.activMachine = true;                                            //Bo
                    }                                                                               //Bo
                }                                                                                   //Bo
            }
        }
        MouseDown = false;
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
        GameObject.Find("BlockButton").GetComponent<ActivButton>().activ(false);                            //Bo
        ActivButton.activMachine = true;                                                                    //Bo
    }                                                                                                       //Bo
}