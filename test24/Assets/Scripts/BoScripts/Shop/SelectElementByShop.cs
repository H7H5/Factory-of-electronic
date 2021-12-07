using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectElementByShop : MonoBehaviour
{
    [SerializeField] private List<Image> images = new List<Image>();
    public void ShowImage(int n)
    {
        for (int i = 0; i < n; i++)
        {
            images[i].GetComponent<Image>().enabled = true;
        }
    }
    public void ShowImageElement(int n, Sprite sprite)
    {
        images[n].GetComponent<Image>().sprite = sprite;
    }
}
