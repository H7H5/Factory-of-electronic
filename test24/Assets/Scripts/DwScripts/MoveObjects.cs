using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    public bool MouseDown = false;
    private new Transform transform;
    public bool isMove = false; //Dw переменная указывающая двигается ли объект
    public bool canBeInstalled; //Dw переменная определяющая "можно установить станок"
    private SpriteRenderer spriteRenderer;
    public MachineOld machineOld;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        machineOld = gameObject.GetComponent<MachineOld>();
    }

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    public void StartMove()
    {
        isMove = true;
        CameraController.Instance.startMoveToObject();
        machineOld.HideTimerObject();
        spriteRenderer.sortingOrder = 100;
    }

    public void StopMove()
    {
        isMove = false;
        CameraController.Instance.stopMoveToObject();
        machineOld.ShowTimerObject();
        spriteRenderer.sortingOrder = 2;
    }

    private void OnMouseDown()
    {
        CameraController.Instance.StartPositionCursor();
        if (machineOld.IsFinishDetail() == false && isMove == true)
        {
            MouseDown = true;
        }
    }

    private void OnMouseUp()
    {
        MouseDown = false;
        if (CameraController.Instance.IsSelectMachine())
        {
            if (machineOld.IsFinishDetail()) 
            {
                machineOld.CollectProduct();
            }
            else
            {
                SelectMachine();
            }
        }
    } 
 
    private void SelectMachine()
    {
        if (isMove == false)                                                            
        {                                                                                  
            UIPanels.Instance.OpenMachineMenu(gameObject);                                                               
        }                                                                                   
    }

    private void CheckCanBeInstalled ()
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

    private void MoveMachine ()
    {
        transform.position = TileMapHelper.Instance.getTilePosition();
    }

    void Update()
    {
        if (isMove)
        {
            CheckCanBeInstalled();
            CameraController.Instance.SetPositionWhwenMoveMachine(transform);
        }
        if (MouseDown)
        {
            MoveMachine();
        }
    }

    public void SetupWhenBuying()                                                                                     
    {                                                                                                       
        UIPanels.Instance.OpenMoveMachinePanel(gameObject,true);
        StartMove(); 
    } 
}