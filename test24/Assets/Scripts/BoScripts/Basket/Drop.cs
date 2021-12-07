using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public bool isFull = false;
    public int numberCell = 0;
    [SerializeField] private Sprite imgBackground;
    [SerializeField] private GameObject product;
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject drag = eventData.pointerDrag;
        if(drag!= null && isFull == false)
        {
            if (drag.GetComponent<ItemDrag>())
            {
                OnDropItemDrag(drag);
            }
            else if (drag.GetComponent<ProductDrag>())
            {
                OnDropProductDrag(drag);
            }
            UpdateBasket();
        }
    }
    private void OnDropItemDrag(GameObject drag)
    {
        GameObject prod = Instantiate(product, transform);
        prod.transform.position = transform.position;
        prod.GetComponent<ProductDrag>().oldParentDrop = this;
        prod.GetComponent<Image>().sprite = drag.GetComponent<Image>().sprite;
        prod.transform.GetChild(0).GetComponent<Image>().sprite =
               drag.transform.GetChild(0).GetComponent<Image>().sprite;
        prod.GetComponent<ProductDrag>().id = drag.GetComponent<CellStockHelper>().itemElement.GetId();
        DBase.Instance.SubstractID(prod.GetComponent<ProductDrag>().id);
        StockManager.Instance.UpdateManagerOfElement();
        isFull = true;
    }
    private void OnDropProductDrag(GameObject drag)
    {
        drag.GetComponent<ProductDrag>().oldParentDrop.SetFullFalse();
        drag.GetComponent<ProductDrag>().oldParentDrop = this;
        drag.transform.SetParent(transform);
        isFull = true;
    }
    public void UpdateBasket()
    {
        transform.parent.GetComponent<BasketHelper>().Save(numberCell);
    }
    public void Load(int idDetail)
    {
        GameObject prod = Instantiate(product, transform);
        prod.transform.position = transform.position;
        prod.GetComponent<ProductDrag>().oldParentDrop = this;
        ItemElement tempItemElement = DBase.Instance.getElement(idDetail);
        prod.GetComponent<Image>().sprite = imgBackground;
        prod.transform.GetChild(0).GetComponent<Image>().sprite = tempItemElement.imgStock;
        prod.GetComponent<ProductDrag>().id = idDetail;
        isFull = true;
    }
    public void SetFullFalse()
    {
        if (transform.childCount==0)
        {
            isFull = false;
        }
    }
}
