using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovePanel : MonoBehaviour
{
    public Sprite but1;
    public Sprite but2;
    private Sprite sprriteR;
    private GameObject moveObj;
    public Button button;
    private Image im;
    public bool canBeInstalled = false;
    public bool start = false;
    private Vector3 StartPosition;

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

    public void select(GameObject gameObject, bool start)
    {
        gameObject.SetActive(true);
        moveObj = gameObject;
        StartPosition = gameObject.transform.position;
        this.start = start;
    }

    public void move()
    {
        moveObj.transform.parent.GetComponent<MachineParentScript>().UpDateOneMachineByid(moveObj);
        moveObj.gameObject.GetComponent<MoveObjects>().StopMove();
    }

    public void undo()
    {
        undoMove();
        if (start)
        {
            Purse.Instance.SetMoney(Purse.Instance.money += moveObj.GetComponent<MachineOld>().price);
            Destroy(moveObj);
        }
    }

    public void undoMove()
    {
        moveObj.transform.position = StartPosition;
        moveObj.gameObject.GetComponent<MoveObjects>().StopMove();
    }
}
