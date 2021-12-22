using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineHelper : MachineOld
{
    public List<Sprite> characters = new List<Sprite>();

    [Header ("Properties of Machine")]
    public int supper_id;
    public int supper_idJSON;
    public int id;
    public int idDetail;
    public int price = 0;
    public Sprite sprt;
    public float timeProduceDetail = 1;                                                      //Dw
    public int amountDetails = 1;                                                      //Dw
    public float maxAmountDetails = 1;
    public float levelMaxAmountDetail;
    public float levelMinTimeProduceDetail;
    public bool isProduce = false;          //Внимание эта переменная Теоретически отвечает за то производит ли станок на данный момент деталь!!!
    public DateTime startTimeProduceDetail; //Время Начала производства детали
    public String startTimeDetailProduce;
    public Sprite spriteDetail;                                                          //Dw
    public GameObject detail;
    public ItemElement itemElementDetail;
    public GameObject[] detailsToModern;
    public int[] idDetailsToModern;
    public bool[] boolDetailsToModern;
    public int selectedDetail = 0;
    public int levelDetailsToModern;
    public string x;
    public string y;
    public int selectNumberElement = 0;
    
    void Start()
    {
        if (detail)
        {
            itemElementDetail = detail.GetComponent<ItemElement>();
            spriteDetail = itemElementDetail.imgStock;
        }
        
    }
    public void onLoadMachine ()
    {
        GameObject timeBar = transform.GetChild(6).gameObject.transform.GetChild(0).gameObject;
        Timer timer = timeBar.GetComponent<Timer>();
        timer.loadTimer();
    }
    public void startProduce ()
    {
        isProduce = true;
    }
    public void stopProduce ()
    {
        isProduce = false;
        transform.parent.GetComponent<MachineParentScript>().UpDateOneMachineByid(gameObject); 
    }
    public void InitMachine(int x)
    {
        id = x;
        GameObject dataBase = GameObject.Find("DBase");
        DBase dBase = dataBase.GetComponent<DBase>();
        gameObject.GetComponent<SpriteRenderer>().sprite = dBase.spritesMachinForElements[x];
        sprt = dBase.spritesMachinForElements[x];
        detail = dBase.getDetailID(idDetail);
        for (int i = 0; i < idDetailsToModern.Length; i++)
        {
            detailsToModern[i] = dBase.getDetailID(idDetailsToModern[i]);
        }
    }
    public string GetData()
    {
        string temp = JsonUtility.ToJson(this, true);
        return temp;
    }
    public void Save()
    {
        Vector3 positionMachine = transform.position;
        x = positionMachine.x.ToString();
        y = positionMachine.y.ToString();
    }
    public void Load(string data)
    {
        JsonUtility.FromJsonOverwrite(data, this);
        float x1 = System.Convert.ToSingle(x);
        float y1 = System.Convert.ToSingle(y);
        Vector3 positionMachine = new Vector3(x1, y1, 1);
        transform.position = positionMachine;
        InitMachine(id);
        if (isProduce)
        {
            onLoadMachine();
        }
    }
    public int GetIdSelectedDetail()
    {
        return detailsToModern[selectedDetail].GetComponent<ItemElement>().GetId();
    }

    public override void CollectProduct()
    {
        base.detailProduced.SetActive(false);
        base.MoveDetailToStock();
        DBase.Instance.AddElementOnIdByCount(GetIdSelectedDetail(), amountDetails);
        stopProduce();
    }


}
