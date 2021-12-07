using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageController : MonoBehaviour
{
    public GameObject garbageToDelete;

    public GameObject attentionGarbageNotEnoughtMoney;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClearGarbage()
    {
        if (Purse.Instance.money >= 7000)
        {
            Purse.Instance.money -= 7000;
            Destroy(garbageToDelete);
            //garbageToDelete.SetActive(false);
        } else
        {
            attentionGarbageNotEnoughtMoney.SetActive(true);
        }
    }
}
