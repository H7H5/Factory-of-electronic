using UnityEngine;
using UnityEngine.UI;

public class Purse : MonoBehaviour
{
    public static Purse Instance;
    public int money = 10000;
    public int science = 121;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text scienceText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SetMoney(int NewMoney)
    {
        money = NewMoney;
        DBase.Instance.SaveMoney(money);
        moneyText.text = money.ToString();
    }
    public void SetSciense(int Newsciense)
    {
        science = Newsciense;
        DBase.Instance.SaveSciense(science);
        scienceText.text = science.ToString();
    }
}
