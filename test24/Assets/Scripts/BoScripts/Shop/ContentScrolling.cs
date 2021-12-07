using UnityEngine;
using UnityEngine.UI;

public class ContentScrolling : MonoBehaviour
{
    public static ContentScrolling Instance;
    [SerializeField] private GameObject ScrollPrefab1;
    [SerializeField] private GameObject ScrollPrefab2;
    [SerializeField] private GameObject ScrollPrefab3;
    [SerializeField] private GameObject scrollbox;
    [SerializeField] private GameObject EquipmentPanel;
    private GameObject[] panels;
    private int mode = 0;
    private int currentPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void BildContent1()
    {
        if (mode == 1) return;
        if (transform.childCount > 0 ) Clear();
        scrollbox.GetComponent<Scrollbar>().value = 0.0f;
        currentPanel = DBase.Instance.listIdElementsUpGrade.Count;
        panels = new GameObject[currentPanel];
        for (int i = 0; i < currentPanel; i++)
        {
            bool flag = IsRepeatMachine(i);
            if (flag == true)
            {
                panels[i] = Instantiate(ScrollPrefab1, transform, false);
                panels[i].GetComponent<ItemShopMachineForElement>().BuildContent(DBase.Instance.listIdElementsUpGrade[i]);
                if (i == 0) continue;
            }
        }
        mode = 1;
    }
    private bool IsRepeatMachine(int i)
    {
        bool flag = true;
        for (int j = 0; j < transform.childCount; j++)
        {
            if (transform.GetChild(j).GetComponent<ItemShopMachineForElement>())
            {
                for (int q = 0; q < transform.GetChild(j).GetComponent<ItemShopMachineForElement>().idDetailsToModern.Length; q++)
                {
                    if (DBase.Instance.listIdElementsUpGrade[i] == transform.GetChild(j).GetComponent<ItemShopMachineForElement>().idDetailsToModern[q])
                    {
                        flag = false;
                    }
                }
            }
        }
        return flag;
    }
    public void BildContent2()
    {
        if (mode == 2) return;
        Clear();
        scrollbox.GetComponent<Scrollbar>().value = 0.0f;
        currentPanel = ScrollPrefab2.GetComponent<ItemShopElement>().characters.Count;
        panels = new GameObject[currentPanel];
        for (int i = 0; i < currentPanel; i++)
        {
            panels[i] = Instantiate(ScrollPrefab2, transform, false);
            panels[i].GetComponent<ItemShopElement>().BuildContent(i);
            if (i == 0) continue;
            panels[i].transform.localPosition = new Vector2(panels[i - 1].transform.localPosition.x +
                ScrollPrefab1.GetComponent<RectTransform>().sizeDelta.x , panels[i].transform.localPosition.y);
        }
        mode = 2;
    }
    public void BildContent3()
    {
        if (mode == 3) return;
        scrollbox.GetComponent<Scrollbar>().value = 0.0f;
        Clear();
        currentPanel = ScrollPrefab3.GetComponent<ItemShopMaschineForDevice>().characters.Count;
        panels = new GameObject[currentPanel];
        for (int i = 0; i < currentPanel; i++)
        {
            panels[i] = Instantiate(ScrollPrefab3, transform, false);
            panels[i].GetComponent<ItemShopMaschineForDevice>().BuildContent(i);
            if (i == 0) continue;
            panels[i].transform.localPosition = new Vector2(panels[i - 1].transform.localPosition.x +
                ScrollPrefab1.GetComponent<RectTransform>().sizeDelta.x , panels[i].transform.localPosition.y);
        }
        mode = 3;
    }
    public void Clear()
    {
        mode = 0;
        foreach (Transform child in transform)
        {  
            Destroy(child.gameObject);
        }
    }
    public void OnVisibleEquipmentPanel(ItemShopMachineForElement item)
    {
        EquipmentPanel.SetActive(true);
        EquipmentPanel.GetComponent<Equipment>().Activation(item);
    }
}