using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Learning : MonoBehaviour
{
    [SerializeField] private Text text;
    private int step = -1;
    [SerializeField] private GameObject [] images;
    [SerializeField] private GameObject Learning_panel;
    string[] learning_text = {"Привіт! у цій грі в тебе є власний завод по виготовленню електроніки. В головному цеху потрібно встановлювати станки які виготовляють радіоелементи або готові пристрої.",
    "Тут можна заключати економічні угоди і отримувати над прибуток а також підвищувати свій авторитет серед продавців аналогічної продукції і отримувати кращі умови для торгівлі ",
    "Це  магазин в ньому можна купити станки для виробництва радіоелементів, або електронних пристроїв. також тут можна придбати деякі радіоелементи",
    "Тут знаходиться наша лабораторія де можливо відкривати технології для виготовлення радіоелементів і електронних пристроїв. Після відкриття нового елемента в магазині з'являється можливість придбати станок який його виробляє ",
    "Це майстерня з виготовлення пристроїв власними руками. Після відкриття нового пристрою тут з'являється можливість його зібрати з потрібних елементів",
    "Тут знаходиться ваш склад в ньому зберігаються виготовленні елементи та пристрої. Зі складу можна продати елементи будь який момент часу і з бонусом для науки яка потрібна для відкриття нових технологій  але по дешевій ціні",
    "Це корзина в яку можна покласти радіоелементи зі складу і викласти з неї в майстерні для виготовлення пристроїв."};
    void Start()
    {
        NextStep();
    }

    public void NextStep()
    {
        step++;
        if (step >= learning_text.Length)
        {
            DBase.Instance.SaveLearning(step);
            Destroy(Learning_panel);
            return;
        }
        text.text = learning_text[step];
        showArrow(step);
    }

    public void showArrow(int x)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
        switch (x)
        {
            case 1:
                images[0].SetActive(true);
                break;
            case 2:
                images[1].SetActive(true);
                break;
            case 3:
                images[2].SetActive(true);
                break;
            case 4:
                images[3].SetActive(true);
                break;
            case 5:
                images[4].SetActive(true);
                break;
            case 6:
                images[5].SetActive(true);
                break;

        }
    }
    
}
