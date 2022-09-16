using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stock : MonoBehaviour
{
    public static Stock Instance;
    public GameObject dragPrefab;
    [SerializeField] private GameObject prefabContainerElement;
    [SerializeField] private GameObject prefabContainerDevice;
    [SerializeField] private GameObject placeInStockForElements;
    [SerializeField] private GameObject placeInStockForDevices;
    [SerializeField] private GameObject canvas;
    [SerializeField] private List<GameObject> bookmarks = new List<GameObject>();

    [SerializeField] private Scrollbar scrollbar1;
    [SerializeField] private Scrollbar scrollbar2;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    void Start()
    {
        //SwitchBookMark(1);
        //StockManager.Instance.UpdateManagerOfDevice();
        //scrollbar2.value = 1;
        SwitchBookMark(0);
        StockManager.Instance.UpdateManagerOfElement();
        scrollbar1.value = 1;
        
    }
    public void NewUpdate()
    {
        SetElements();
        SetDevices();
        
    }
    private void SetElements()
    {
        foreach (Transform child in placeInStockForElements.transform) Destroy(child.gameObject);
        int countElement = DBase.Instance.elementsParameters.Length;
        for (int i = 0; i < countElement; i++)
        {
            if (DBase.Instance.elementsParameters[i].GetCount() > 0)
            {
                GameObject imgContainer = Instantiate(prefabContainerElement, placeInStockForElements.transform);
                imgContainer.GetComponent<CellStockHelper>().element = DBase.Instance.elementsParameters[i];
                imgContainer.GetComponent<ItemDrag>().Canvas = canvas;
                DBase.Instance.elementsParameters[i].container = imgContainer;
                GameObject panel = imgContainer.transform.Find("Panel").gameObject;
                panel.GetComponent<Image>().sprite = DBase.Instance.elementsParameters[i].sprite;
                GameObject textCount = panel.transform.Find("Text").gameObject;
                textCount.GetComponent<Text>().text = DBase.Instance.elementsParameters[i].GetCount().ToString();
            }
        }
    }
    private void SetDevices()
    {
        foreach (Transform child in placeInStockForDevices.transform) Destroy(child.gameObject);
        int countDevice = DBase.Instance.devicesParameters.Length;
        for (int i = 0; i < countDevice; i++)
        {
            if (DBase.Instance.devicesParameters[i].GetCount() > 0)
            {
                GameObject imgContainer = Instantiate(prefabContainerDevice, placeInStockForDevices.transform);
                imgContainer.GetComponent<CellStockHelper>().device = DBase.Instance.devicesParameters[i]; 
                DBase.Instance.devicesParameters[i].container = imgContainer;
                GameObject panel = imgContainer.transform.Find("Panel").gameObject;
                panel.GetComponent<Image>().sprite = DBase.Instance.devicesParameters[i].sprite;
                GameObject textCount = panel.transform.Find("Text").gameObject;
                //textCount.GetComponent<Text>().text = DBase.Instance.devicesScripts[i].GetCount().ToString();
                textCount.GetComponent<Text>().text = DBase.Instance.devicesParameters[i].GetCount().ToString();
            }
        }
    }
    public void SwitchBookMark( int number)
    {
       
        for (int i = 0; i < bookmarks.Count; i++)
        {
            bookmarks[i].SetActive(false);
        }
        bookmarks[number].SetActive(true);
       

    }
    public void StartScrolbar2()
    {
        scrollbar2.value = 1;

    }
}
                                             