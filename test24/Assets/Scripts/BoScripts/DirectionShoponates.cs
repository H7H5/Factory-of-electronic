using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionShoponates : MonoBehaviour
{
    public MarkSelect markSelect1;
    public MarkSelect markSelect2;
    public MarkSelect markSelect3;
    public GameObject PanelDiamonds;
    public GameObject PanelMoney;
    public GameObject PanelSciense;
    public Text text;
    public GameObject PanelNeedDiamonds;
    private string[] names = { "Buy diamonds for USD", "Exchange diamonds for money", "Swap diamonds for science" };

    private int[] arrayDiamondsForMoney = {20, 150, 250, 800, 1500, 5000};
    private int[] arrayMoneys = { 500, 5000, 10000, 50000, 100000, 500000};

    private int[] arrayDiamondsForSciense = { 20, 50, 500, 800, 1500, 5000 };
    private int[] arraySciense = { 5, 15, 350, 700, 1450, 5000};

    private int[] arrayDiamondsForUSD = { 55, 150, 325, 650, 1800, 5000 };
    private float[] USD = { 1.99f, 4.99f, 9.99f, 19.99f, 99.99f, 1.99f };

    public void OpenDiamonds()
    {
        markSelect1.Activate(true);
        markSelect2.Activate(false);
        markSelect3.Activate(false);
        PanelDiamonds.SetActive(true);
        PanelMoney.SetActive(false);
        PanelSciense.SetActive(false);
        text.text = names[0];
    }
    public void OpenMoney()
    {
        markSelect1.Activate(false);
        markSelect2.Activate(true);
        markSelect3.Activate(false);
        PanelDiamonds.SetActive(false);
        PanelMoney.SetActive(true);
        PanelSciense.SetActive(false);
        text.text = names[1];
    }
    public void OpenSciense()
    {
        markSelect1.Activate(false);
        markSelect2.Activate(false);
        markSelect3.Activate(true);
        PanelDiamonds.SetActive(false);
        PanelMoney.SetActive(false);
        PanelSciense.SetActive(true);
        text.text = names[2];
    }

    public void BuyDiamondsForUSD(int numberButton)
    {
        Purse.Instance.SetDiamonds(Purse.Instance.diamonds += arrayDiamondsForUSD[numberButton]);
    }

    public void ChangeMoney(int numberButton)
    {
        if (Purse.Instance.diamonds >= arrayDiamondsForMoney[numberButton])
        {
            Purse.Instance.SetMoney(Purse.Instance.money += arrayMoneys[numberButton]);
            Purse.Instance.SetDiamonds(Purse.Instance.diamonds -= arrayDiamondsForMoney[numberButton]);
        }
        else
        {
            PanelNeedDiamonds.SetActive(true);
        }
    }
    public void ChangeSciense(int numberButton)
    {
        if (Purse.Instance.diamonds >= arrayDiamondsForSciense[numberButton])
        {
            Purse.Instance.SetSciense(Purse.Instance.science += arraySciense[numberButton]);
            Purse.Instance.SetDiamonds(Purse.Instance.diamonds -= arrayDiamondsForSciense[numberButton]);
        }
        else
        {
            PanelNeedDiamonds.SetActive(true);
        }
    }
}
