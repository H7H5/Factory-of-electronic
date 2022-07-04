using UnityEngine;

public class ScrollSelectDevice : MonoBehaviour
{
    [SerializeField] private GameObject panelPrefab;
    private GameObject[] instPans;
    private int panOffset = 20;
    private int panCount;
    public void Init()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        panCount = DBase.Instance.listIdDevicesUpGrade.Count;
        instPans = new GameObject[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panelPrefab, transform, false);
            instPans[i].GetComponent<ItemSelectDevice>().SetImage(DBase.Instance.GetDevice(DBase.Instance.listIdDevicesUpGrade[i]).sprite);
            instPans[i].GetComponent<ItemSelectDevice>().id = DBase.Instance.listIdDevicesUpGrade[i];
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x +
                panelPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset, instPans[i].transform.localPosition.y);
        }
    }
}
