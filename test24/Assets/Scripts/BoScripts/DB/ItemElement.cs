using UnityEngine;

public class ItemElement : ItemParent
{
    public GameObject container;
    public Sprite imgStock;
    public Sprite img;
    public new string name;
    public string descriptionElement = "default element";
    
    [SerializeField] private int price;
    [SerializeField] private int costMachine;
    [SerializeField] private int timeProduceDetail;                                            //Dw
    private int count = 0;
    
    public int GetPrice()
    {
        return price;
    }
    public int GetTimeProduceDetail()
    {
        return timeProduceDetail;
    }  
    public int GetCostMachine()
    {
        return costMachine;
    }
    
    public void SetCount(int c)
    {
        count = c;
    }
    public int GetCount()
    {
        return count;
    }
    public void Sell()
    {  
        if (count > 0)
        {
            count--;
            Purse.Instance.SetMoney(Purse.Instance.money += price);
        }
    }
    public void Substract()
    {
        if (count > 0)
        {
            count--;
        }
    }
}
