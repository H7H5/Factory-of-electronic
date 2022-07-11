using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopElement : MonoBehaviour
{
    public List<Element> characters = new List<Element>();
    [SerializeField] private Sprite button1;
    [SerializeField] private Sprite button2;
    [SerializeField] private Text textPrice;
    [SerializeField] private GameObject button;
    private Image imageButton;
    [SerializeField] private Image image;
    private int currentNumberElement = 0;
    private int price;

    public void BuildContent(int number) // вызываем в начале когда строим палитру магазина
    {
        image.sprite = characters[number].sprite;
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




















