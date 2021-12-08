using UnityEngine;

public class ItemMachine : MonoBehaviour
{
    [SerializeField] private GameObject detail;
    [SerializeField] private GameObject[] detailsToModern;
    [SerializeField] private Sprite img;
    [SerializeField] private int price;
    [SerializeField] private int timeProduceDetail;                                            //Dw
    [SerializeField] private int levelMinTimeProduceDetail;
    [SerializeField] private float levelMaxAmountDetail;

    public GameObject GetDetail()
    {
        return detail;
    }
    public GameObject[] GetDetailsToModern()
    {
        return detailsToModern;
    }
    public GameObject GetOneDetailsToModern(int n)
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
