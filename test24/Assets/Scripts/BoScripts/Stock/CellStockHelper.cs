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
            //StockManager.Instance.itemDevice = itemDevice;

            //New Scriptable Object system
            StockManager.Instance.idDevice = itemDevice.GetComponent<ItemDevice>().GetId();

            StockManager.Instance.UpdateManagerOfDevice();
        }
    }
}
