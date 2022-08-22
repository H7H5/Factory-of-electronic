using UnityEngine;
using UnityEngine.UI;

public class BasketBoardScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button button;
    [SerializeField] private Sprite button1;
    [SerializeField] private Sprite button2;
    private Image image;
    private bool isOpen = false;
    private string isOpenAnimator = "isOpen";
    private void Awake()
    {
        image = button.GetComponent<Image>();
    }
    public void Move()
    {
        isOpen = !isOpen;
        animator.SetBool(isOpenAnimator, isOpen);
        //image.sprite = isOpen ? button2: button1;
    }
    public void Hide()
    {
        isOpen = false;
        animator.SetBool(isOpenAnimator, isOpen);
        //image.sprite = button1;
    }
}
