using UnityEngine;
using UnityEngine.EventSystems;

public class CellStockHelper : MonoBehaviour, IPointerClickHandler
{
    public Element element;
    public Device device;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.GetComponent<ItemDrag>())
        { 
            StockManager.Instance.currentElement = element;
            StockManager.Instance.UpdateManagerOfElement();
        }else
        {
            //StockManager.Instance.itemDevice = itemDevice;

            //New Scriptable Object system
            StockManager.Instance.idDevice = device.id;

            StockManager.Instance.UpdateManagerOfDevice();
        }
    }
}
