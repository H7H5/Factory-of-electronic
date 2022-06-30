using System.Collections.Generic;
using UnityEngine;

public class UpGradeBilder : MonoBehaviour
{
    public static UpGradeBilder Instance;
    [SerializeField] private List<GameObject> ups = new List<GameObject>();
    [SerializeField] private GameObject panelNeedResourse;
    [SerializeField] private GameObject content1;
    [SerializeField] private GameObject prefUP;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Bildding(List<int> arrayOnElements)
    {
        if (ups.Count == 0)
        {
            for (int j = 0; j < DBase.Instance.elementsParameters.Length; j++)
            {
                GameObject up = Instantiate(prefUP, content1.transform);
                up.GetComponent<ItemUpgrade>().Init(DBase.Instance.elementsParameters[j]);
                ups.Add(up);
            }
        }
        for (int i = 0; i < ups.Count; i++)
        {
            for (int j = 0; j < arrayOnElements.Count; j++)
            {
                if(ups[i].GetComponent<ItemUpgrade>().element.id == arrayOnElements[j])
                {
                    ups[i].GetComponent<ItemUpgrade>().SetAtivateProgram(true);
                }
            }
        }
    } 
    public void OnBild()
    {
        Bildding(DBase.Instance.listIdElementsUpGrade);
    }
    public void ActivatePanelNeedResourse(Element element)
    {
        panelNeedResourse.gameObject.SetActive(true);
        panelNeedResourse.GetComponent<PanelNeedResourse>().BildNeedResource(element);
    }
}
