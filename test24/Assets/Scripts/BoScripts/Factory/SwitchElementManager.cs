using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchElementManager : MonoBehaviour
{
    public List<GameObject> switchElementsObj = new List<GameObject>();
    public int countElements;
    public Sprite unknow;
    public DBase dBase;
    public GameObject PanelModernizationDetail;
    public Image imageElementModernizatiom;
    public Text textPriceModernization;
    public Button buttonModernization;
    private int idModernEL;
    private int CostModern;
    public int numberSelectEl = 0;
    public void ShowSwitchElementsObj(Element[] detailsToModern, bool[] boolDetailsToModern)
    {
        countElements = detailsToModern.Length;
        for (int i = 0; i < 4; i++)
        {
            switchElementsObj[i].SetActive(false);
        }
        for (int i = 0; i < detailsToModern.Length; i++)
        {
            if (boolDetailsToModern[i])
            {
                switchElementsObj[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                switchElementsObj[i].SetActive(true);
                switchElementsObj[i].transform.GetChild(0).GetComponent<Image>().sprite = detailsToModern[i].sprite;
                switchElementsObj[i].GetComponent<SwitchElement>().isActiv = true;
                switchElementsObj[i].GetComponent<SwitchElement>().idElement = detailsToModern[i].id;
                switchElementsObj[i].GetComponent<SwitchElement>().lockImg(false);
            }
            else
            {
                if (dBase.IsUpgradeElement(detailsToModern[i].id))
                {
                    switchElementsObj[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                    switchElementsObj[i].SetActive(true);
                    switchElementsObj[i].GetComponent<SwitchElement>().lockImg(true);
                    switchElementsObj[i].transform.GetChild(0).GetComponent<Image>().sprite = detailsToModern[i].sprite;
                    switchElementsObj[i].GetComponent<SwitchElement>().isActiv = false;
                    switchElementsObj[i].GetComponent<SwitchElement>().idElement = detailsToModern[i].id;
                }
                else
                {
                    switchElementsObj[i].SetActive(true);
                    switchElementsObj[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
                    switchElementsObj[i].transform.GetChild(0).GetComponent<Image>().sprite = unknow;
                    switchElementsObj[i].GetComponent<SwitchElement>().isActiv = false;
                }  
            }
        }
    }
    public void SwitchElementOn(int num)
    {
        if (switchElementsObj[num].GetComponent<SwitchElement>().isActiv == false)
        {
            numberSelectEl = num;
            idModernEL = switchElementsObj[num].GetComponent<SwitchElement>().idElement;
            CostModern = dBase.getDetailID(idModernEL).GetCostMachine();
            imageElementModernizatiom.sprite = dBase.getDetailID(idModernEL).sprite;
            textPriceModernization.text = CostModern.ToString();
            if (dBase.getDetailID(switchElementsObj[num].GetComponent<SwitchElement>().idElement).GetCostMachine() >= Purse.Instance.money)
            {
                buttonModernization.interactable = false;
               
            }
            else
            {
                buttonModernization.interactable = true;
            }
            PanelModernizationDetail.SetActive(true);
            return;
        }
        for (int i = 0; i < 4; i++)
        {
            if (switchElementsObj[i].activeSelf)
            {
                if (i == num)
                {
                    switchElementsObj[i].GetComponent<SwitchElement>().SetAtivate(true);
                }
                else
                {
                    switchElementsObj[i].GetComponent<SwitchElement>().SetAtivate(false);
                }
            }
        }
        transform.parent.GetComponent<ManagerMachineHelper>().selectElement(num);
    }
    public void pickModernszation()
    {
        Purse.Instance.SetMoney(Purse.Instance.money -= CostModern);
        switchElementsObj[numberSelectEl].transform.GetChild(0).GetComponent<Button>().interactable = true;
        switchElementsObj[numberSelectEl].SetActive(true);
        switchElementsObj[numberSelectEl].GetComponent<SwitchElement>().isActiv = true;
        switchElementsObj[numberSelectEl].GetComponent<SwitchElement>().lockImg(false);
        GameObject MachineMenu = GameObject.Find("MachineMenu");
        MachineMenu.GetComponent<ManagerMachineHelper>().ModernNewElement(idModernEL);
    }
}
