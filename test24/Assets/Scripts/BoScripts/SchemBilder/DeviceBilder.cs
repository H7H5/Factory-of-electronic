using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeviceBilder : MonoBehaviour
{
    public int activation = 0;
    public int id;
    [SerializeField] private List<ElementListener> listeners = new List<ElementListener>();
    [SerializeField] private Sprite noActiv;
    [SerializeField] private Sprite imgActivation;
    [SerializeField] private GameObject deviceObj;
    public void ActivationDevice()
        {
        bool collectedDevice = IsCollected();
        if (transform.parent.GetComponent<OnePartDevice>())
        {
            UpdatePartDevice(collectedDevice);
        }
        else
        {
            BildPanelHelper.Instance.OnButtonCollect(collectedDevice);
        }  
        BildPanelHelper.Instance.Save();
    }
    private void UpdatePartDevice(bool collectedDevice)
    {
        OnePartDevice onePartDevice = transform.parent.GetComponent<OnePartDevice>();
        if (onePartDevice.ReadClear_partDevice(transform.parent.GetComponent<ItemDevice>().GetId()) == 1)
        {
            onePartDevice.UpdateClear_partDevice(transform.parent.GetComponent<ItemDevice>().GetId(), 0);
            Collect();
            transform.parent.GetComponent<SaverBase>().Save();
        }
        if (collectedDevice)
        {
            DBase.Instance.Update_partDevice(id, 1);
            onePartDevice.donePanel.SetActive(true);
        }
        else
        {
            DBase.Instance.Update_partDevice(id, 0);
            onePartDevice.donePanel.SetActive(false);
        }
    }
    private bool IsCollected()
    {
        bool collectedDevice = true;
        for (int i = 0; i < listeners.Count; i++)
        {
            if (listeners[i].IsActivation() == false)
            {
                collectedDevice = false;
            }
        }
        return collectedDevice;
    }
    public void SetActivationDevice()
    {
         gameObject.GetComponent<Image>().sprite = imgActivation;
         for (int i = 0; i < listeners.Count; i++)
         {
              listeners[i].SetImg();
         }
    }
    public void Collect()
    {
        gameObject.GetComponent<Image>().sprite = imgActivation;
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].SetImg();
            listeners[i].DestroyChild();
        }
        DBase.Instance.addDevice(transform.parent.gameObject.GetComponent<ItemDevice>());
        activation = 1;
        BildPanelHelper.Instance.OnButtonCollect(false);
        BildPanelHelper.Instance.Save();
    }
    public List<int> GetNeedElements()
    {
        List<int> idNeedElement = new List<int>();
        for (int i = 0; i < transform.childCount; i++)
        {
            idNeedElement.Add(transform.GetChild(i).gameObject.GetComponent<ElementListener>().needIdElement);
        }
        return idNeedElement;
    }
}
