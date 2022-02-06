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
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        SwitchBookMark(0);
        StockManager.Instance.UpdateManagerOfElement();
    }
    public void NewUpdate()
    {
        SetElements();
        SetDevices();
    }
    private void SetElements()
    {
        foreach (Transform child in placeInStockForElements.transform) Destroy(child.gameObject);
        int countElement = DBase.Instance.elementsScripts.Count;
        for (int i = 0; i < countElement; i++)
        {
            if (DBase.Instance.elementsScripts[i].GetCount() > 0)
            {
                GameObject imgContainer = Instantiate(prefabContainerElement, placeInStockForElements.transform);
                imgContainer.GetComponent<CellStockHelper>().itemElement = DBase.Instance.elementsScripts[i];
                imgContainer.GetComponent<ItemDrag>().Canvas = canvas;
                DBase.Instance.elementsScripts[i].container = imgContainer;
                GameObject panel = imgContainer.transform.Find("Panel").gameObject;
                panel.GetComponent<Image>().sprite = DBase.Instance.elementsScripts[i].imgStock;
                GameObject textCount = panel.transform.Find("Text").gameObject;
                textCount.GetComponent<Text>().text = DBase.Instance.elementsScripts[i].GetCount().ToString();
            }
        }
    }
    private void SetDevices()
    {
        foreach (Transform child in placeInStockForDevices.transform) Destroy(child.gameObject);
        int countDevice = DBase.Instance.devicesScripts.Count;
        for (int i = 0; i < countDevice; i++)
        {
            if (DBase.Instance.devicesScripts[i].GetCount() > 0)
            {
                GameObject imgContainer = Instantiate(prefabContainerDevice, placeInStockForDevices.transform);
                imgContainer.GetComponent<CellStockHelper>().itemDevice = DBase.Instance.devicesScripts[i]; 
                DBase.Instance.devicesScripts[i].container = imgContainer;
                GameObject panel = imgContainer.transform.Find("Panel").gameObject;
                panel.GetComponent<Image>().sprite = DBase.Instance.devicesScripts[i].imgStock;
                GameObject textCount = panel.transform.Find("Text").gameObject;
                textCount.GetComponent<Text>().text = DBase.Instance.devicesScripts[i].GetCount().ToString();
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
}
                                             