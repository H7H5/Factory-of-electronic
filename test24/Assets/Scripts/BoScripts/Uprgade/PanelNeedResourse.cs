using UnityEngine;
using UnityEngine.UI;

public class PanelNeedResourse : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject prefabNeedElement;
    [SerializeField] private Button button;
    [SerializeField] private UpGradeBilder upGradeBilder;
    [SerializeField] private Text textSciense;
    private bool isfull;
    private ItemElement curentItemElement;
    
    public void BildNeedResource(GameObject element)
    {
        curentItemElement = element.GetComponent<ItemElement>();
        isfull = true;
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        int countNeedElements = curentItemElement.needElements.Length;
        for (int i = 0; i < countNeedElements; i++)
        {
            GameObject needCell = Instantiate(prefabNeedElement, content.transform);
            int countNeed = curentItemElement.countNeedElements[i];
            needCell.GetComponent<NeedItemElement>().Activate(curentItemElement.needElements[i],countNeed);
            if(needCell.GetComponent<NeedItemElement>().IsFull()==false)
            {
                isfull = false;
            }
        }
        textSciense.text = curentItemElement.GetNeedScience().ToString();
        if (curentItemElement.GetNeedScience() > Purse.Instance.science)
        {
            isfull = false;
        }
        button.interactable = isfull;
    }
    public void UpgradeElement()
    {
        int countNeedElements = curentItemElement.needElements.Length;
        for (int i = 0; i < countNeedElements; i++)
        {
            DBase.Instance.sellMuch(curentItemElement.needElements[i], curentItemElement.countNeedElements[i]);
        }
        UpGradeHelper.Instance.AddOnElement(curentItemElement.GetId());
        Purse.Instance.SetSciense(Purse.Instance.science - curentItemElement.GetNeedScience());
        gameObject.SetActive(false);
        upGradeBilder.OnBild();
    }
}
