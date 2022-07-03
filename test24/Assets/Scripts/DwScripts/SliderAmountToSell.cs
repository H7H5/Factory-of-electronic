using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAmountToSell : MonoBehaviour
{
    public Slider sliderAmount;
    [SerializeField] private Text textAmount;

    private void Awake()
    {
        sliderAmount = GetComponent<Slider>();
    }

    public void ShowSliderValue()
    {
        textAmount.text = sliderAmount.value.ToString();       
    }

    public void ShowMoneyChanges()
    {
        StockManager.Instance.ShowMoneyChanges((int)sliderAmount.value);
    }

    public void ShowScienceChanges()
    {
        StockManager.Instance.ShowScienceChanges((int)sliderAmount.value);
    }

    private void ShowInfoDisplay()
    {
        ShowSliderValue();
        ShowMoneyChanges();
        ShowScienceChanges();
    }

    public void ChangeSelect(int maxValue)
    {
        sliderAmount.maxValue = maxValue;
        sliderAmount.value = sliderAmount.minValue;
        ShowInfoDisplay();
    }

    public void SetSliderValueMax()
    {
        sliderAmount.value = sliderAmount.maxValue;
        ShowInfoDisplay();
    }

    public int GetSliderValue()
    {
        return (int)sliderAmount.value;
    }

    public void SliderOneStepLeft()
    {
        sliderAmount.value -= 1;
    }

    public void SliderOneStepRight()
    {
        sliderAmount.value += 1;
    }
}
