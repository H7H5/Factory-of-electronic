using UnityEngine;

public class ScrollSelectDevice : MonoBehaviour
{
    [SerializeField] private GameObject panelPrefab;
    private GameObject[] instPans;
    private int panOffset = 20;
    private int panCount;
    void Start()
    {
        panCount = panelPrefab.GetComponent<ItemSelectDevice>().characters.Count;
        instPans = new GameObject[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panelPrefab, transform, false);
            instPans[i].GetComponent<ItemSelectDevice>().SetImage(i);
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x +
                panelPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset, instPans[i].transform.localPosition.y);
        }
    }
}
