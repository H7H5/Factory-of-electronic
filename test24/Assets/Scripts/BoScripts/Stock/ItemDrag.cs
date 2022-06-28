using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject Canvas;
    [SerializeField] private Text text;
    private Transform oldparent;
    private int count;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        count = DBase.Instance.getCountOnId(gameObject.GetComponent<CellStockHelper>().element.id);
        if(count==1)
        {
            text.text = "";
            gameObject.GetComponent<Image>().enabled = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            oldparent = transform.parent;
            transform.SetParent(Canvas.transform);
        }
        else if (count > 1)
        {
            Stock.Instance.dragPrefab.SetActive(true);
            Stock.Instance.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Stock.Instance.dragPrefab.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
            Stock.Instance.dragPrefab.transform.GetChild(0).GetComponent<Image>().sprite =
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
            Stock.Instance.dragPrefab.transform.SetParent(Canvas.transform);  
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        float positionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float positionY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        Vector2 position = new Vector2(positionX, positionY);
        if (count == 1)
        {
            transform.position = position;
        }
        else
        {
            Stock.Instance.dragPrefab.transform.position = position;
        }   
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (count == 1)
        {
            transform.SetParent(oldparent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            Stock.Instance.dragPrefab.SetActive(false);
            Stock.Instance.dragPrefab.transform.SetParent(Stock.Instance.transform);
            Stock.Instance.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        Stock.Instance.NewUpdate();
    }
}
