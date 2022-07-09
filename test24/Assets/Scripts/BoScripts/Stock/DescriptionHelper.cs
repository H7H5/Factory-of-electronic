using UnityEngine;
using UnityEngine.UI;

public class DescriptionHelper : MonoBehaviour
{
    
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    private Element element;

    public void SetElement (Element element1)
    {
        element = element1;
        text.text = element.name;
        text2.text = element.description;
    }
    public void SetDevice(Device device)
    {
        text.text = device.name;
        text2.text = device.description;
    }
}
