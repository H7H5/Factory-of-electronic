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
    private string[] names = { "Buy diamonds for USD", "Exchange diamonds for money", "Swap diamonds for science" };

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

}
