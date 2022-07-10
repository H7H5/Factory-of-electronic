using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MachineDevice", menuName = "MachineDevice")]
public class MachineDevice : ScriptableObject
{
    [SerializeField] private Device device;
    [SerializeField] private GameObject deviceObj;
    [SerializeField] private Sprite img;
    [SerializeField] private int price;
    [Header("Properties Machine")]
    [SerializeField] private int timeProduceDetail;                                            //Dw
    [SerializeField] private int levelMinTimeProduceDetail;
    [SerializeField] private float levelMaxAmountDetail;

    public Device GetDevice()
    {
        return device;
    }
    public GameObject GetDeviceObj()
    {
        return deviceObj;
    }
    public Sprite GetImg()
    {
        return img;
    }
    public Sprite GetImgDevice()
    {
        return device.sprite;
    }
    public int GetPrice()
    {
        return price;
    }
    public int GetTimeProduceDetail()
    {
        return timeProduceDetail;
    }
    public int GetLevelMinTimeProduceDetail()
    {
        return levelMinTimeProduceDetail;
    }
    public float GetLevelMaxAmountDetail()
    {
        return levelMaxAmountDetail;
    }
}
