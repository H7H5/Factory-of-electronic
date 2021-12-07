using System.Collections.Generic;
using UnityEngine;

public class BasketHelper : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    [SerializeField] private List<GameObject> cels = new List<GameObject>();
    private int countCell = 8;
    private int countActivCels = 6;
    private int currentActivCells = 6;
    private int activCell = 0;
    void Start()
    {
        for (int i = 0; i < countCell; i++)
        {
            GameObject newCell = Instantiate(cell, transform);
            newCell.GetComponent<Drop>().numberCell = i;
            cels.Add(newCell);
        }
        if(countActivCels>cels.Count)
        {
            countActivCels = cels.Count;
        }
        ScrollLeft();
        Generate();  
    }
    public void ScrollLeft()
    {
        activCell -= countActivCels;
        if (activCell < 0)
        {
            activCell = 0;
        }
        currentActivCells = activCell+ countActivCels;
        ScrollCells();
    }
    public void ScrollRight()
    {
        activCell += countActivCels;
        int temp = cels.Count % countActivCels;
        int temp2;
        if (temp > 0)
        {
            temp2 = cels.Count - temp;
        }
        else
        {
            temp2 = cels.Count - countActivCels; 
        }
        if (activCell > temp2)
        {
            activCell = temp2;
        }
        currentActivCells = activCell + countActivCels;
        if (currentActivCells >= cels.Count)
        {
            currentActivCells = cels.Count;
        }
        ScrollCells();
    }
    private void ScrollCells()
    {
        for (int i = 0; i < cels.Count; i++)
        {
            cels[i].gameObject.SetActive(false);
        }
        for (int i = activCell; i < currentActivCells; i++)
        {
            cels[i].gameObject.SetActive(true);
        }
    }
    public void Save(int numberCell)
    {
        int childCount = transform.childCount;
        int numberChield = numberCell;
        int count = transform.GetChild(numberCell).transform.childCount;
        int idDetail = 0;
        if (transform.GetChild(numberCell).transform.childCount == 1)
        {
            idDetail = transform.GetChild(numberCell).transform.GetChild(0).GetComponent<ProductDrag>().id;
        }
        DBase.Instance.Update_basketCell(numberChield, count, idDetail);
    }
    public void Generate()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if(DBase.Instance.Reader_basketCount(i) == 1)
            {
                int XidDetail = DBase.Instance.Reader_basketCellbyNumberChield(i);
                transform.GetChild(i).GetComponent<Drop>().Load(XidDetail);
            }    
        }
    }
}
