using UnityEngine;

public class ItemDevice : MonoBehaviour
{
    public GameObject container;
    public Sprite imgStock;
    public new string name;
    public string descriptionDevice = "default device";
    [SerializeField] private int id;
    [SerializeField] private int price;
    private int count = 0;
       
    public int GetId()
    {
        return id;
    }
    public int GetPrice()
    {
        return price;
    }
    public int GetCount()
    {
        return count;
    }
    public void SetCount(int d)
    {
        count = d;
    }
    public void Sell()
    {
        if (count > 0)
        {
            count--;
            Purse.Instance.SetMoney(Purse.Instance.money += price);
        }
    }
}
