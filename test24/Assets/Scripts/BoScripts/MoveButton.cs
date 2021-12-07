using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveButton : MonoBehaviour
{
    public Sprite but1;
    public Sprite but2;
    private Sprite sprriteR;
    private GameObject moveObj;
    public Button button;
    private Image im;
    public bool canBeInstalled = false;
    private bool start = false;
    Vector3 StartPosition;

    void Start()
    {
        im = button.GetComponent<Image>();
    }

    void Update()
    {
        canBeInstalled = moveObj.GetComponent<MoveObjects>().canBeInstalled;
        if (canBeInstalled)
        {
            button.GetComponent<Button>().interactable = true;
            im.sprite = but1;
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
            im.sprite = but2;
        }
    }

    public void select(GameObject gameObject)
    {
        moveObj = gameObject;
        StartPosition = gameObject.transform.position;
        start = false;
    }

    public void select(GameObject gameObject, int x)
    {
        moveObj = gameObject;
        start = true;
    }

    public void move()
    {
        moveObj.transform.parent.GetComponent<MachineParentScript>().upDateOneMachineByid(moveObj);
        moveObj.gameObject.GetComponent<MoveObjects>().stopMove();
    }

    public void undo()
    {
        undoMove();
        if (start)
        {
            if (moveObj.GetComponent<MachineHelper>())
            {
                Purse.Instance.SetMoney(Purse.Instance.money += moveObj.GetComponent<MachineHelper>().price);
            }
            else if (moveObj.GetComponent<MachineDeviceHelper>())
            {
                Purse.Instance.SetMoney(Purse.Instance.money += moveObj.GetComponent<MachineDeviceHelper>().price);
                
            }
           
            //Purse.money += moveObj.GetComponent<MachineHelper>().price;
            Destroy(moveObj);
        }
    }

    public void undoMove()
    {
        moveObj.transform.position = StartPosition;
        moveObj.gameObject.GetComponent<MoveObjects>().stopMove();
    }
}
