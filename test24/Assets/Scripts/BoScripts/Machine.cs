using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Machine", menuName = "Machine")]
public class Machine : ScriptableObject
{
    [SerializeField] private Element detail;
    [SerializeField] private Element[] detailsToModern;
    public Sprite sprite;
    [SerializeField] private int timeProduceDetail;                                            //Dw
    [SerializeField] private int levelMinTimeProduceDetail;
    [SerializeField] private float levelMaxAmountDetail;

    public Element GetDetail()
    {
        return detail;
    }
    public Element[] GetDetailsToModern()
    {
        return detailsToModern;
    }
    public Element GetOneDetailsToModern(int n)
    {
        return detailsToModern[n];
    }
    public Sprite GetImg()
    {
        return sprite;
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
