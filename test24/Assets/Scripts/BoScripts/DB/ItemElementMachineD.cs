using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemElementMachineD : MonoBehaviour
{
    public int id;
    public Sprite img;
    public Sprite imgDevice;
    public new string name = "MachineDevice";
    public int cost;
    public GameObject device;
    [Header("Properties Machine")]
    public int timeProduceDetail;                                            //Dw
    public int levelMinTimeProduceDetail;
    public float levelMaxAmountDetail;
}
