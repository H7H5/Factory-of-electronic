using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Device", menuName = "Device")]
public class Device : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public Sprite spriteForShop;
    public int id;
    public int needScience;
    public int price;
    public int sellPrice;
    public int sellPriceScience;

    private int count = 0;

    public void SetCount(int c)
    {
        count = c;
    }
    public int GetCount()
    {
        return count;
    }
}