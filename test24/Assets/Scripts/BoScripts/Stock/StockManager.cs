using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StockManager : MonoBehaviour
{
    public static StockManager Instance;
    public Element currentElement;

    public int idDevice;
    private Device device;
    private Element element;

    public SliderAmountToSell slider;
    [SerializeField] private Image image;
    [SerializeField] private Sprite background;
    [SerializeField] private Text text;
    [SerializeField] private Text textPriceScience;
    [SerializeField] private Text textPriceMoney;
    [SerializeField] private DescriptionHelper description;

    private int id = 1;
    private bool typeItem = false;//false = itemElement: true = itemDevice
    private bool item = true;
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
        if (item)
        {
            int count = slider.GetSliderValue();
            for (int i = 0; i < count; i++)
            {
                Sell();
            }
        }
    }
    
    public void UpdateManagerOfElement()
    {
        typeItem = false;
        if(currentElement == null)
        {
            ClearInformation();
            return;
        }
        id = currentElement.id;
        if (currentElement.GetCount()==0)
        {
            ClearInformation();
            return;
        }
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
        item = true;
    }
  

    public void UpdateManagerOfDevice()
    {
        typeItem = true;
        if(idDevice == 0)
        {
            ClearInformation();
            return;
        }
        
        device = DBase.Instance.GetDevice(idDevice);
        if (device.GetCount() == 0)
        {
            ClearInformation();
            return;
        }
        image.sprite = device.sprite;
        text.text = device.GetCount().ToString();
        description.SetDevice(device);

        textPriceScience.text = device.sellPriceScience.ToString();
        textPriceMoney.text = device.sellPrice.ToString();
        slider.ChangeSelect(device.GetCount());
        item = true;
    }
    private void ClearInformation()
    {
        item = false;
        text.text = "0";
        textPriceScience.text = "0";
        textPriceMoney.text = "0";
        image.sprite = background;
    }
}
