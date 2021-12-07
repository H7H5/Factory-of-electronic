using UnityEngine;
using UnityEngine.UI;

public class MarkSelect : MonoBehaviour
{
    public Sprite pic1;
    public Sprite pic2;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Activate(bool activ)
    {
        image.sprite = activ ? pic1 : pic2;
    }
}
