using UnityEngine;
using UnityEngine.UI;

public class DescriptionHelper : MonoBehaviour
{
    
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    private ItemElement itemElement;
    private ItemDevice itemDevice;

    public void SetElement (ItemElement element)
    {
        itemElement = element;
        text.text = element.name;
        text2.text = element.descriptionElement;
    }
    public void SetDevice(ItemDevice device)
    {
        itemDevice = device;
        text.text = device.name;
        text2.text = device.descriptionDevice;
    }
}
