using UnityEngine;
using UnityEngine.EventSystems;

public class CellStockHelper : MonoBehaviour, IPointerClickHandler
{
    public ItemElement itemElement;
    public ItemDevice itemDevice;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.GetComponent<ItemDrag>())
        { 
            StockManager.Instance.itemElement = itemElement;
            StockManager.Instance.UpdateManagerOfElement();
        }else
        {
            StockManager.Instance.itemDevice = itemDevice;
            StockManager.Instance.UpdateManagerOfDevice();
        }
    }
}
