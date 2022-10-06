using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObjParent : ScriptableObject
{
    public GameObject container;
    public new string name;
    public string description;
    public Sprite sprite;
    public int id;
    public int needScience;
    public int price;
    public int sellPriceScience;
    protected int count = 0;
    public Element[] needElements;
    public int[] countNeedElements;

    public int GetPrice()
    {
        return price;
    }
    public int GetCount()
    {
        return count;
    }
    public void SetCount(int d)
    {
        count = d;
    }
    public void Sell()
    {
        if (count > 0)
        {
            count--;
            Purse.Instance.SetMoney(Purse.Instance.money += price);
        }
    }
}
