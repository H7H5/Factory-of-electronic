using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePartDevice : MonoBehaviour
{
    [SerializeField] private Sprite button1;
    [SerializeField] private Sprite button2;
    [SerializeField] private GameObject partDevice1;
    [SerializeField] private GameObject partDevice2;
    [SerializeField] private List<GameObject> listeners = new List<GameObject>();
    [SerializeField] private List<Button> buttons = new List<Button>();
    private void Awake()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            if (DBase.Instance.Reader_partDevice(listeners[i].transform.GetChild(0).GetComponent<DeviceBilder>().id) == 0)
            {
                buttons[i].GetComponent<Image>().sprite = button1;
                transform.parent.GetComponent<BildPanelHelper>().OnButtonCollect(false);
            }
            else
            {
                buttons[i].GetComponent<Image>().sprite = button2;
                transform.parent.GetComponent<BildPanelHelper>().OnButtonCollect(true);
            }
        }
    }
    public void LoadPart(int number)
    {
        BildPanelHelper.Instance.OnButtonCollect(false);
        switch (number)
        {
            case 1:
                BildPanelHelper.Instance.SetDeviceSever(partDevice1);
                break;
            case 2:
                BildPanelHelper.Instance.SetDeviceSever(partDevice2);
                break;
        }
        BildPanelHelper.Instance.OnVisible();
    }
    
    public void Collect()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            DBase.Instance.UpdateClear_partDevice(listeners[i].transform.GetComponent<ItemDevice>().GetId(), 1);
            DBase.Instance.Update_partDevice(listeners[i].transform.GetComponent<ItemDevice>().GetId(), 0);
        }
        for (int i = 0; i < listeners.Count; i++)
        {
            if (DBase.Instance.Reader_partDevice(listeners[i].transform.GetChild(0).GetComponent<DeviceBilder>().id) == 0)
            {
                buttons[i].GetComponent<Image>().sprite = button1;
            }
            else
            {
                buttons[i].GetComponent<Image>().sprite = button2;
            }
        }
        DBase.Instance.addDevice(transform.GetComponent<ItemDevice>());
        BildPanelHelper.Instance.OnButtonCollect(false);
        BildPanelHelper.Instance.Save();
    }
    public List<int> GetNeedElements()
    {
        List<int> idNeedElement = new List<int>();
        for (int i = 0; i < listeners.Count; i++)
        {
            List<int> temp = listeners[i].transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().GetNeedElements();
            for (int j = 0; j < temp.Count; j++)
            {
                idNeedElement.Add(temp[j]);
            }
            temp = null;
        }
        return idNeedElement;
    }
}
