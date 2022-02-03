using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyChangesDisplay : MonoBehaviour
{
    public static MoneyChangesDisplay Instance;

    [SerializeField] private GameObject moneyChanges;
    [SerializeField] private Text moneyChangeValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ActivateMoneyChanges()
    {
        moneyChanges.SetActive(true);
    }

    public void DisActivateMoneyChanges()
    {
        moneyChanges.SetActive(false);
    }

    public void ShowMoneyChanges(int money)
    {
        if (money == 0)
        {
            DisActivateMoneyChanges();
        }
        else
        {
            ActivateMoneyChanges();
        }
    }
}
