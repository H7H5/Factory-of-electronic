using System.Collections.Generic;
using UnityEngine;

public class UpGradeBilder : MonoBehaviour
{
    public static UpGradeBilder Instance;
    [SerializeField] private List<GameObject> ups = new List<GameObject>();
    [SerializeField] private GameObject panelNeedResourse;
    [SerializeField] private GameObject content1;
    [SerializeField] private GameObject prefUP;
    public int mode = 1;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SetMode(int m)
    {
        mode = m;
        Bildding();
    }
    public void Clear()
    {
        foreach (Transform child in content1.transform)
        {
            Destroy(child.gameObject);
        }
        ups = new List<GameObject>();
    }
    public void Bildding()
    {
        Clear();
        if(mode == 1)
        {
            for (int j = 0; j < DBase.Instance.elementsParameters.Length; j++)
            {
                GameObject up = Instantiate(prefUP, content1.transform);
                up.GetComponent<ItemUpgrade>().Init(DBase.Instance.elementsParameters[j]);
                ups.Add(up);
            }

            for (int i = 0; i < ups.Count; i++)
            {
                for (int j = 0; j < DBase.Instance.listIdElementsUpGrade.Count; j++)
                {
                    if (ups[i].GetComponent<ItemUpgrade>().scriptObjParent.id == DBase.Instance.listIdElementsUpGrade[j])
                    {
                        ups[i].GetComponent<ItemUpgrade>().SetAtivateProgram(true);
                    }
                }
            }
        }
        else if(mode== 2)
        {
            for (int j = 0; j < DBase.Instance.devicesParameters.Length; j++)
            {
                GameObject up = Instantiate(prefUP, content1.transform);
                up.GetComponent<ItemUpgrade>().Init(DBase.Instance.devicesParameters[j]);
                ups.Add(up);
            }

            for (int i = 0; i < ups.Count; i++)
            {
                for (int j = 0; j < DBase.Instance.listIdDevicesUpGrade.Count; j++)
                {
                    if (ups[i].GetComponent<ItemUpgrade>().scriptObjParent.id == DBase.Instance.listIdDevicesUpGrade[j])
                    {
                        ups[i].GetComponent<ItemUpgrade>().SetAtivateProgram(true);
                    }
                }
            }
        }

    } 
    public void OnBild()
    {
        Bildding();
    }
    public void ActivatePanelNeedResourse(ScriptObjParent scriptObjParent)
    {
        panelNeedResourse.gameObject.SetActive(true);
        panelNeedResourse.GetComponent<PanelNeedResourse>().BildNeedResource(scriptObjParent);
    }
}
