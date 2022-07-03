using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpGradeHelper : MonoBehaviour
{
    public static UpGradeHelper Instance;
    public List<int> listIdElementsUpGrade = new List<int>();
    public List<int> listIdDevicesUpGrade = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StartCoroutine(Starting());
    }
    
    public void Save()
    {
        string data = JsonUtility.ToJson(this, true);
        DBase.Instance.UpdateString(data, 2);
        data = null;
    }
    public void Load()
    {
        string data = DBase.Instance.ReadString(2);
        JsonUtility.FromJsonOverwrite(data, this);
        DBase.Instance.listIdElementsUpGrade = listIdElementsUpGrade;
        DBase.Instance.listIdDevicesUpGrade = listIdDevicesUpGrade;
    }
    public void AddOnElement(int id)
    {
        listIdElementsUpGrade.Add(id);
        DBase.Instance.SetListIdElementsUpGrade(listIdElementsUpGrade);
        Save();
    }
    public void AddOnDevice(int id)
    {
        listIdDevicesUpGrade.Add(id);
        DBase.Instance.SetListIdDevicesUpGrade(listIdDevicesUpGrade);
        Save();
    }
    IEnumerator Starting()
    {
        yield return new WaitForSeconds(0.3f);
        Load();
    }
   
}
