using UnityEngine;

public class ItemMachine : MonoBehaviour
{
    [SerializeField] private Element detail;
    [SerializeField] private Element[] detailsToModern;
    [SerializeField] private Sprite img;
    [SerializeField] private int price;
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
        return img;
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
