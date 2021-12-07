using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchElement : MonoBehaviour
{
    public Sprite act;
    public Sprite noact;
    public GameObject image;   
    public bool isActiv = false;
    public int idElement;
    public void SetAtivate(bool activ)
    {
        if (activ)
        {
            gameObject.GetComponent<Image>().sprite = act;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = noact;
        }
    }
    public void lockImg(bool b)
    {
        if (b == true)
        {
            image.SetActive(true);
        }
        else
        {
            image.SetActive(false);
        }
    }
}
