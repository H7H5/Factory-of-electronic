using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int needScience;
    public ItemElement[] needElements;
    public int[] countNeedElements;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetNeedScience()
    {
        return needScience;
    }
    public int GetId()
    {
        return id;
    }
}
