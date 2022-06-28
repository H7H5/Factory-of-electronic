using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "Element")]
public class Element : ScriptableObject
{
    public GameObject container;
    public new string name;
    public string description;
    public Sprite sprite;
    public Sprite spriteForShop;
    public int id;
    public int needScience;
    public int price;
    public int sellPrice;
    public int sellPriceScience;
    public int costMachine;
    public int timeProduce;

    private int count = 0;

    public int GetPrice()
    {
        return price;
    }
    //public int GetTimeProduceDetail()
    //{
    //    return timeProduceDetail;
    //}
    public int GetCostMachine()
    {
        return costMachine;
    }

    public void SetCount(int c)
    {
        count = c;
    }
    public int GetCount()
    {
        return count;
    }
    public void Sell()
    {
        if (count > 0)
        {
            count--;
            Purse.Instance.SetMoney(Purse.Instance.money += price);
        }
    }
    public void Substract()
    {
        if (count > 0)
        {
            count--;
        }
    }
}