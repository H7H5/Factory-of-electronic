using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSchem : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject product;
    private GameObject cell;

    private void Awake()
    {
        cell = BildPanelHelper.Instance.basket.transform.GetChild(0).gameObject;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject drag = eventData.pointerDrag;
        if (drag != null && drag.GetComponent<ProductDrag>() && gameObject.transform.childCount == 0)
        {
            drag.GetComponent<ProductDrag>().oldParentDrop.SetFullFalse();
            drag.GetComponent<ProductDrag>().UpdateBasket();
            drag.transform.SetParent(transform);
            gameObject.GetComponent<ElementListener>().SetElement(drag.GetComponent<ProductDrag>().id); 
        }
    }
    public void Load(int idDetail)
    {
        GameObject prod = Instantiate(product, transform);
        prod.transform.position = transform.position;
        prod.GetComponent<ProductDrag>().oldParentDrop = cell.GetComponent<Drop>();
        Element tempElement = DBase.Instance.getElement(idDetail);
        prod.transform.GetChild(0).GetComponent<Image>().sprite = tempElement.sprite;
        prod.GetComponent<ProductDrag>().id = idDetail;
        gameObject.GetComponent<ElementListener>().SetElement(prod.GetComponent<ProductDrag>().id);
    }
}
