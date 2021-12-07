using UnityEngine;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
    public GameObject element;
    [SerializeField] private Sprite act;
    [SerializeField] private Sprite noact;
    [SerializeField] private Button button;
    private Image imageElement;
    private void Awake()
    {
        imageElement = gameObject.GetComponent<Image>();
    }
    public void SetAtivate(bool activ)
    {
        if (activ)
        {
            UpGradeBilder.Instance.ActivatePanelNeedResourse(element);
        }
        else
        {
            imageElement.sprite = noact;
        }
    }
    public void SetAtivateProgram(bool activ)
    {
        if (activ)
        {
            imageElement.sprite = act;
            button.interactable = false;
        }
        else
        {
            imageElement.sprite = noact;
        }
    }
    public void PostSetAtivate(bool activ)
    {
        if (activ)
        {
            imageElement.sprite = act;
            button.interactable = false;
            UpGradeHelper.Instance.AddOnElement(element.GetComponent<ItemElement>().GetId());
        }
        else
        {
            imageElement.sprite = noact;
        }
    }
}
