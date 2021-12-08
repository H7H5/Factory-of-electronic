using UnityEngine;

public class ItemElementMachineD : MonoBehaviour
{
    [SerializeField] private GameObject device;
    [SerializeField] private Sprite img;
    [SerializeField] private Sprite imgDevice;
    [SerializeField] private int price;
    [Header("Properties Machine")]
    [SerializeField] private int timeProduceDetail;                                            //Dw
    [SerializeField] private int levelMinTimeProduceDetail;
    [SerializeField] private float levelMaxAmountDetail;

    public GameObject GetDevice()
    {
        return device;
    }
    public Sprite GetImg()
    {
        return img;
    }
    public Sprite GetImgDevice()
    {
        return imgDevice;
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
