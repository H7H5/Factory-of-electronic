using UnityEngine;
using UnityEngine.UI;

public class ItemNeedElement : MonoBehaviour
{
    public Image img;
    [SerializeField] private Text textCountNeedElements;
    public void SetTextNeedElements(int there_is, int need)
    {
        textCountNeedElements.GetComponent<Text>().color = (there_is >= need) ? Color.green: Color.red;
        textCountNeedElements.text = (there_is).ToString() + "/" + (need).ToString(); ;
    }
}
