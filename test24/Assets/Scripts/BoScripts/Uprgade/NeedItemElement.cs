using UnityEngine;
using UnityEngine.UI;

public class NeedItemElement : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text textNeedCount;
    private int needCount;
    private int countDbaseEl;

    public void Activate(ItemElement element, int countNeed)
    {
        needCount = countNeed;
        countDbaseEl = DBase.Instance.getCountOnId(element.GetId());
        textNeedCount.color = countDbaseEl >= countNeed ? Color.green: Color.red;
        textNeedCount.text = countDbaseEl.ToString() +"/"+ countNeed.ToString();
        image.sprite = element.imgStock;
    }
    public bool IsFull()
    {
        return countDbaseEl >= needCount ? true : false;  
    }
}
