using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StockManager : MonoBehaviour
{
    public static StockManager Instance;
    public Element currentElement;
    public ItemDevice itemDevice;

    public int idDevice;
    private Device device;
    private Element element;

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
            DBase.Instance.sell(device);
            UpdateManagerOfDevice();
        }else
        {
            DBase.Instance.sell(currentElement);
            UpdateManagerOfElement(); 
        }
        Stock.Instance.NewUpdate();
    }

    public void ShowMoneyChanges(int sliderAmount)
    {
        int price;
        if (typeItem)
        {
            price = device.sellPrice;
        }
        else
        {
            price = element.price;
        }
        MoneyChangesDisplay.Instance.ShowMoneyChanges(sliderAmount * price);
        textPriceMoney.text = (sliderAmount * price).ToString();
    }

    public void ShowScienceChanges(int sliderAmount)
    {
        int sellPriceScience;
        if (typeItem)
        {
            sellPriceScience = device.sellPriceScience;
        }
        else
        {
            sellPriceScience = element.sellPriceScience;
        }
        ScienceChangesDisplay.Instance.ShowScienceChanges(sliderAmount * sellPriceScience);
        textPriceScience.text = (sliderAmount * sellPriceScience).ToString();
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
        id = currentElement == null? 1: currentElement.id;
        currentElement = DBase.Instance.getElement(id);
        image.sprite = currentElement.sprite;
        text.text = currentElement.GetCount().ToString();
        description.SetElement(currentElement);
      
        int idElementsParameters = DBase.Instance.IdElementParameters(currentElement.id);
        element = DBase.Instance.elementsParameters[idElementsParameters];
        textPriceScience.text = DBase.Instance.elementsParameters[idElementsParameters].sellPriceScience.ToString();

        //textPriceMoney.text = DBase.Instance.elementsParameters[idElementsParameters].sellPrice.ToString();
        textPriceMoney.text = DBase.Instance.elementsParameters[idElementsParameters].price.ToString();
        slider.ChangeSelect(currentElement.GetCount());
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
