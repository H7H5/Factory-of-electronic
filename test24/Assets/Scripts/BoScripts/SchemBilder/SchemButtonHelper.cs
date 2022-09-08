using UnityEngine;

public class SchemButtonHelper : MonoBehaviour
{
    [SerializeField] private GameObject schema;
    [SerializeField] private GameObject bildPanel;
    private bool isOpen = false;
    public void OpenSchema()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            schema.SetActive(false);
            schema.SetActive(true);
            bildPanel.GetComponent<BildPanelHelper>().OnVisible();
        }
   
    }
}
