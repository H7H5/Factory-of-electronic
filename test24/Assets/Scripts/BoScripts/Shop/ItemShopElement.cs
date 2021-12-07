using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopElement : MonoBehaviour
{
    public List<ItemElement> characters = new List<ItemElement>();
    [SerializeField] private Sprite button1;
    [SerializeField] private Sprite button2;
    [SerializeField] private Text textPrice;
    [SerializeField] private GameObject button;
    private Image imageButton;
    private int currentNumberElement = 0;
    private int price;

    public void BuildContent(int number) // вызываем в начале когда строим палитру магазина
    {
        gameObject.GetComponent<Image>().sprite = characters[number].img;
        currentNumberElement = number;
        price = characters[number].GetPrice();
        textPrice.text = price.ToString();
        imageButton = button.GetComponent<Image>();
        imageButton.sprite = Purse.Instance.money >= price ? button1 : button2;
    }
    public void BuyThisElement()       // вызываем когда покупаем елемент
    {
        if (Purse.Instance.money >= price)
        {
            Purse.Instance.SetMoney(Purse.Instance.money -= price);
            DBase.Instance.addElement(characters[currentNumberElement]);
        }
    }
}




















