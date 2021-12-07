using UnityEngine;
using UnityEngine.UI;

public class SchemButtonHelper : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Sprite button1;
    [SerializeField] private Sprite button2;
    [SerializeField] private GameObject schema;
    [SerializeField] private GameObject bildPanel;
    private bool isOpen = false;
    public void OpenSchema()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            schema.SetActive(true);
            bildPanel.GetComponent<BildPanelHelper>().OnVisible();
            button.GetComponent<Image>().sprite = button2;
        }
        else
        {
            schema.SetActive(false);
            button.GetComponent<Image>().sprite = button1;
        }
    }
}
