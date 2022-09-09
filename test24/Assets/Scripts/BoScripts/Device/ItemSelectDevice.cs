using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectDevice : MonoBehaviour
{
    public int id;
    [SerializeField] private Button button;
    public void SetImage(Sprite sprite)
    {
            button.GetComponent<Image>().sprite = sprite;
    }
    public void OnClick()
    {
        DBase.Instance.SetSaverDevice(DBase.Instance.getDeviceObj(id));
        DBase.Instance.DeviceSelect.SetActive(false);
        ButtonController.Instance.UnBlockButtons();
        DBase.Instance.OpenSchema.GetComponent<SchemButtonHelper>().OpenSchema();
        DBase.Instance.OpenSchema.GetComponent<SchemButtonHelper>().OpenSchema();
        ButtonController.Instance.changeBlocked();
    }
}
