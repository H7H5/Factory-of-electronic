using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StockManager : MonoBehaviour
{
    public static StockManager Instance;
    public ItemElement itemElement;
    public ItemDevice itemDevice;

    public int idDevice;
    private Device device;

    public SliderAmountToSell slider;
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    [SerializeField] private Text textPriceScience;
    [SerializeField] private Text textPriceMoney;
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

    public void SellByAmount()
    {
        int count = slider.GetSliderValue();
        for (int i = 0; i < count; i++)
        {
            Sell();
        }
    }
    
    public void UpdateManagerOfElement()
    {
        typeItem = false;
        id = itemElement == null? 1: itemElement.GetId();
        itemElement = DBase.Instance.getElement(id);
        image.sprite = itemElement.imgStock;
        text.text = itemElement.GetCount().ToString();
        description.SetElement(itemElement);
      
        int idElementsParameters = DBase.Instance.IdElementParameters(itemElement.GetId());
        textPriceScience.text = DBase.Instance.elementsParameters[idElementsParameters].sellPriceScience.ToString();
        textPriceMoney.text = DBase.Instance.elementsParameters[idElementsParameters].sellPrice.ToString();
        slider.ChangeSelect(itemElement.GetCount());
    }
    public void UpdateManagerOfDevice()
    {
        typeItem = true;

        //id = itemDevice == null ? 1 : itemDevice.GetId();
        //itemDevice = DBase.Instance.getDeviceElement(id);
        //image.sprite = itemDevice.imgStock;
        //text.text = itemDevice.GetCount().ToString();
        //description.SetDevice(itemDevice);

        if(idDevice == 0)
        {
            idDevice = 1;
        }
        device = DBase.Instance.GetDevice(idDevice);
        image.sprite = device.sprite;
        text.text = device.GetCount().ToString();
        description.SetDeviceParameters(device);

        textPriceScience.text = device.sellPriceScience.ToString();
        textPriceMoney.text = device.sellPrice.ToString();
        slider.ChangeSelect(device.GetCount());
    }
}
