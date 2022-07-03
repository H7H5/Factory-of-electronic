using UnityEngine;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
    public ScriptObjParent scriptObjParent;
    [SerializeField] private Sprite act;
    [SerializeField] private Sprite noact;
    [SerializeField] private Button button;
    private Image imageElement;
    private void Awake()
    {
        imageElement = gameObject.GetComponent<Image>();
    }
    public void Init(ScriptObjParent el)
    {
        scriptObjParent = el;
        button.image.sprite = scriptObjParent.sprite;

    }
    public void SetAtivate(bool activ)
    {
        if (activ)
        {
            UpGradeBilder.Instance.ActivatePanelNeedResourse(scriptObjParent);
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
            if(UpGradeBilder.Instance.mode == 1)
            {
                UpGradeHelper.Instance.AddOnElement(scriptObjParent.id);
            }
            else if (UpGradeBilder.Instance.mode == 2)
            {
                UpGradeHelper.Instance.AddOnDevice(scriptObjParent.id);
            }
           
        }
        else
        {
            imageElement.sprite = noact;
        }
    }
}
