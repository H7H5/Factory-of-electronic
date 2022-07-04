using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectDevice : MonoBehaviour
{
    public int id;
    [SerializeField] private Button button;
    [SerializeField] private List<GameObject> devices = new List<GameObject>();
    public void SetImage(Sprite sprite)
    {
            button.GetComponent<Image>().sprite = sprite;
    }
    public void OnClick()
    {
        GameObject currentDevice = devices[0];
        for (int i = 0; i < devices.Count; i++)
        {
            if (devices[i].GetComponent<ItemDevice>().GetId()==id)
            {
                currentDevice = devices[i];
            }
        }
        DBase.Instance.SetSaverDevice(currentDevice);
        DBase.Instance.DeviceSelect.SetActive(false);
        ButtonController.Instance.UnBlockButtons();
        DBase.Instance.OpenSchema.GetComponent<SchemButtonHelper>().OpenSchema();
        DBase.Instance.OpenSchema.GetComponent<SchemButtonHelper>().OpenSchema();
        ButtonController.Instance.changeBlocked();
    }
}
