using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "Element")]
public class Element : ScriptObjParent
{
    public int priceDiamonds;
    public int costMachine;
    public int GetCostMachine()
    {
        return costMachine;
    }
    public void Substract()
    {
        if (count > 0)
        {
            count--;
        }
    }
}