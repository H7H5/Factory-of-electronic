using UnityEngine;
using UnityEngine.UI;

public class ElementListener : MonoBehaviour
{
    public int idElement = 0;
    public int needIdElement = 1;
    [SerializeField] private GameObject backPanel;
    private DeviceBilder deviceBilder;
    private void Awake()
    {
        deviceBilder = backPanel.GetComponent<DeviceBilder>();
    }
    public void SetElement(int id)
    {  
        idElement = id;
        deviceBilder.ActivationDevice();  
    }
    public bool IsActivation()
    {
        return idElement == needIdElement ? true : false;
    }
    public void DestroyChild()
    {
        idElement = 0;
        if (gameObject.transform.childCount>0)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }
}
