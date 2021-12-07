using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectDevice : MonoBehaviour
{
    private int id;
    [SerializeField] private Button button;
    public List<Sprite> characters = new List<Sprite>();
    [SerializeField] private List<GameObject> devices = new List<GameObject>();
    private Sprite sprsteDevice;
    public void SetImage(int x)
    {
        if (x < characters.Count)
        {
            sprsteDevice = characters[x];
            button.GetComponent<Image>().sprite = sprsteDevice;
            id = x;
        }
    }
    public void OnClick()
    {
        DBase.Instance.SetSaverDevice(devices[id]);
        DBase.Instance.DeviceSelect.SetActive(false);
        ButtonController.Instance.UnBlockButtons();
        DBase.Instance.OpenSchema.GetComponent<SchemButtonHelper>().OpenSchema();
        DBase.Instance.OpenSchema.GetComponent<SchemButtonHelper>().OpenSchema();
        ButtonController.Instance.changeBlocked();
    }
}
