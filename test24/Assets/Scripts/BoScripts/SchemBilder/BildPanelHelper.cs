using UnityEngine;
using UnityEngine.UI;

public class BildPanelHelper : MonoBehaviour
{
    public static BildPanelHelper Instance;
    public GameObject basket;
    [SerializeField] private Button buttonCollect;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            OnVisible();
        }
    }
    public void OnVisible()
    {
        if (DBase.Instance.listIdDevicesUpGrade.Count > 0)
        {
            TempSave(DBase.Instance.GetSaverDevice());
        }
       
    }
    public void SetDeviceSever(GameObject partDevice)
    {
        DBase.Instance.SetSaverDevice(partDevice);
    }
    public void OnVisiblePart(GameObject partDevice)
    {
        TempSavePart(partDevice);
        if (transform.GetChild(0).gameObject.GetComponent<SaverBase>())
        {
            transform.GetChild(0).gameObject.GetComponent<SaverBase>().Load();
        }
    }
    public void Save()
    {
        if(transform.childCount>0)
        {
            if (transform.GetChild(0).gameObject.GetComponent<SaverBase>())
            {
                transform.GetChild(0).gameObject.GetComponent<SaverBase>().Save();
            }  
        }
    }
    public void TempSavePart(GameObject saveDevice)
    {
        if (gameObject.transform.childCount > 0)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        TempGenerate(saveDevice);
    }
    public void TempSave(GameObject saveDevice)
    {
        if (gameObject.transform.childCount > 0)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        TempGenerate(saveDevice);
    }
    public void TempGenerate(GameObject savObj)
    {
        GameObject device = Instantiate(Resources.Load<GameObject>(savObj.name), transform);
        if (device.GetComponent<SaverBase>())
        {
            device.GetComponent<SaverBase>().Load();
        }
    }
    public void OnButtonCollect(bool interactable)
    {
        buttonCollect.interactable = interactable;
    }
    public void Collect()
    {
        if (transform.GetChild(0).gameObject.GetComponent<CreatePartDevice>())
        {
            transform.GetChild(0).gameObject.GetComponent<CreatePartDevice>().Collect();
        }
        else
        {
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().Collect();
        }
    }
}
