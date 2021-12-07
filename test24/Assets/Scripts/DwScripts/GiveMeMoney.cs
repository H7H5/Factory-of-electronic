using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GiveMeMoney : MonoBehaviour
{

    public void giveMeMoney()
    {
        Purse.Instance.SetMoney(Purse.Instance.money += 100000); 
    }
}
