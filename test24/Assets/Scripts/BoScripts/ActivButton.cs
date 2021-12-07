using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject openShop;
    public GameObject openStock;
    public static bool activMachine = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activ(bool b) {
        openShop.GetComponent<Button>().interactable = b;
        openStock.GetComponent<Button>().interactable = b;
    }
    public void setActivMachine(bool act)
    {
        activMachine = act;
    }
}
