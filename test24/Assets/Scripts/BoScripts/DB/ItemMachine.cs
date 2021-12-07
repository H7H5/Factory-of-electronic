using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMachine : MonoBehaviour
{
    public int id;
    public Sprite img;
    public new string name = "MachineElement";
    public int cost;
    public GameObject detail;
    public GameObject[] detailsToModern;         
    public int timeProduceDetail;                                            //Dw
    public int levelMinTimeProduceDetail;
    public float levelMaxAmountDetail;
}
