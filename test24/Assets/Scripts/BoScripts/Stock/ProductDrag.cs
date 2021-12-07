using UnityEngine;
using UnityEngine.EventSystems;

public class ProductDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int id = 0;
    public Drop oldParentDrop;
    private Transform oldParent;
    private Drop tempParentDrop;
    private GameObject Canvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(transform.parent.gameObject.GetComponent<ElementListener>())
        {
            transform.parent.gameObject.GetComponent<ElementListener>().SetElement(0);
        }
        if (transform.parent.gameObject.GetComponent<Drop>())
        {
            tempParentDrop = transform.parent.gameObject.GetComponent<Drop>();
        }
        else
        {
            tempParentDrop = null;
        }
        oldParent = transform.parent;
        Canvas = DBase.Instance.Canvas;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(Canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float positionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float positionY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        Vector2 position = new Vector2(positionX, positionY);
        gameObject.transform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == Canvas.transform)
        {
            gameObject.transform.SetParent(oldParent.transform);
        }
        if (transform.parent.gameObject.GetComponent<ElementListener>())
        {
            transform.parent.gameObject.GetComponent<ElementListener>().SetElement(id);
        }
        if (tempParentDrop != null)
        {
            tempParentDrop.UpdateBasket();
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void UpdateBasket()
    {
        if (tempParentDrop != null)
        {
            tempParentDrop.UpdateBasket();
            tempParentDrop = null;
        }
    }
}
