using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    private int[] arrayDiamondsForUSD = { 55, 150, 325, 650, 1800, 5000 };
    private float[] USD = { 1.99f, 4.99f, 9.99f, 19.99f, 99.99f, 1.99f };
   
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "55diamonds":
                BuyDiamondsForUSD(0);
                break;
            case "150diamonds":
                BuyDiamondsForUSD(1);
                break;
            case "325diamonds":
                BuyDiamondsForUSD(2);
                break;
            case "650diamonds":
                BuyDiamondsForUSD(3);
                break;
            case "1800diamonds":
                BuyDiamondsForUSD(4);
                break;
            case "5000diamonds":
                BuyDiamondsForUSD(5);
                break;
        }
    }

    public void BuyDiamondsForUSD(int numberButton)
    {
        Purse.Instance.SetDiamonds(Purse.Instance.diamonds += arrayDiamondsForUSD[numberButton]);
    }
}
