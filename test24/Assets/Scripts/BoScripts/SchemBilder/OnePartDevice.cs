using UnityEngine;

public class OnePartDevice : MonoBehaviour   
{
    public GameObject donePanel;
    [SerializeField] private GameObject parentDevice;

    public void LoadParent()
    {
        transform.parent.GetComponent<BildPanelHelper>().SetDeviceSever(parentDevice);
        transform.parent.GetComponent<BildPanelHelper>().OnVisible();
    }
    public int ReadClear_partDevice(int id)
    {   
        return DBase.Instance.GetComponent<DBase>().ReadClear_partDevice(id);
    }
    public void UpdateClear_partDevice(int id, int clear)
    {
        DBase.Instance.GetComponent<DBase>().UpdateClear_partDevice(id, clear);
    }
}
