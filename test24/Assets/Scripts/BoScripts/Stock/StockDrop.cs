using UnityEngine;
using UnityEngine.EventSystems;

public class StockDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject drag = eventData.pointerDrag;
        if (drag != null)
        {  
            if(drag.GetComponent<ProductDrag>())
            {
                DBase.Instance.addElementOnId(drag.GetComponent<ProductDrag>().id);
                drag.GetComponent<ProductDrag>().oldParentDrop.isFull = false;
                drag.GetComponent<ProductDrag>().UpdateBasket();
                Destroy(drag);
                Stock.Instance.NewUpdate();
            }
        }
    }
}
