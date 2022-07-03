using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Sprite locks;
    [SerializeField] private Image imageMachine;
    [SerializeField] private List<Image> images = new List<Image>();
    [SerializeField] private List<GameObject> toggles = new List<GameObject>();
    [SerializeField] private Text priceText;
    [SerializeField] private Button button;
    private ItemShopMachineForElement itemShopMachine;
    private int totalCost = 0;
    private bool[] boolDetailsToModern;                                  

    public void Activation(ItemShopMachineForElement item)
    {
        totalCost = 0;
        itemShopMachine = item;
        imageMachine.sprite = itemShopMachine.spriteMachine;
        int currentMachine = itemShopMachine.currentNumberMachine;
        ShowImage(itemShopMachine.characters[currentMachine].GetDetailsToModern().Length);
        for (int i = 0; i < itemShopMachine.characters[currentMachine].GetDetailsToModern().Length; i++)
        {
            if (DBase.Instance.IsUpgradeElement(itemShopMachine.characters[currentMachine].GetOneDetailsToModern(i).GetComponent<ItemElement>().GetId()))
            {
                ShowImageElement(i, itemShopMachine.characters[currentMachine].GetOneDetailsToModern(i).GetComponent<ItemElement>().imgStock,
                    itemShopMachine.characters[currentMachine].GetOneDetailsToModern(i).GetComponent<ItemElement>().GetCostMachine());
                totalCost += itemShopMachine.characters[currentMachine].GetOneDetailsToModern(i).GetComponent<ItemElement>().GetCostMachine();
            }
        }
        priceText.text = totalCost.ToString();  
        button.interactable = totalCost > 0 && Purse.Instance.money >= totalCost ? true : false;
    }
    public void ShowImage(int n)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].enabled = false;
            images[i].sprite = locks;
            toggles[i].SetActive(false); 
        }
        for (int i = 0; i < n; i++)
        {
            images[i].enabled = true;
        }
        boolDetailsToModern = new bool[n];
    }
    public void ShowImageElement(int n, Sprite sprite, int costMachine)
    {
        images[n].GetComponent<Image>().sprite = sprite;
        toggles[n].SetActive(true);
        toggles[n].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        toggles[n].transform.GetChild(0).GetComponent<Text>().text = costMachine.ToString();

    }

    public void CalculatePrice()
    {
        totalCost = 0;
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].gameObject.activeSelf == true)
            {
                if (toggles[i].GetComponent<Toggle>().isOn)
                {
                    int price = int.Parse(toggles[i].transform.GetChild(0).GetComponent<Text>().text);
                    totalCost += price;
                }
            }
        }
        priceText.text = totalCost.ToString();
        button.interactable = totalCost > 0 && Purse.Instance.money >= totalCost ? true : false;
    }
    public void BuyNow()
    {
        SetBoolArray();
        itemShopMachine.BuyThisMachine(totalCost, boolDetailsToModern);
        gameObject.SetActive(false);
    }
    private void SetBoolArray()
    {
        for (int i = 0; i < boolDetailsToModern.Length; i++)
        {
            boolDetailsToModern[i] = false;
            if (toggles[i].GetComponent<Toggle>().isOn)
            {
                boolDetailsToModern[i] = true;
            }
        }
    }
}
