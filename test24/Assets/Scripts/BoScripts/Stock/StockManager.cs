using UnityEngine;
using UnityEngine.UI;

public class StockManager : MonoBehaviour
{
    public static StockManager Instance;
    public ItemElement itemElement;
    public ItemDevice itemDevice;
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    [SerializeField] private DescriptionHelper description;
    private int id = 1;
    private bool typeItem = false;//false = itemElement: true = itemDevice
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void Sell()
    {
        if (typeItem)
        {
            DBase.Instance.sell(itemDevice);
            UpdateManagerOfDevice();
        }else
        {
            DBase.Instance.sell(itemElement);
            UpdateManagerOfElement(); 
        }
        Stock.Instance.NewUpdate();
    }
    
    public void UpdateManagerOfElement()
    {
        typeItem = false;
        id = itemElement == null? 1: itemElement.GetId();
        itemElement = DBase.Instance.getElement(id);
        image.sprite = itemElement.imgStock;
        text.text = itemElement.GetCount().ToString();
        description.SetElement(itemElement);

    }
    public void UpdateManagerOfDevice()
    {
        typeItem = true;
        id = itemElement == null ? 1 : itemDevice.GetId();
        itemDevice = DBase.Instance.getDeviceElement(id);
        image.sprite = itemDevice.imgStock;
        text.text = itemDevice.GetCount().ToString();
        description.SetDevice(itemDevice);
    }
}
