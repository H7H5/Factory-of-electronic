using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAmountToSell : MonoBehaviour
{
    private Slider sliderAmount;
    public Text textValue;

    private void Awake()
    {
        sliderAmount = GetComponent<Slider>();
        textValue.text = sliderAmount.value.ToString();
    }

    public void ShowSliderValue()
    {
        textValue.text = sliderAmount.value.ToString();
    }

    public void ChangeSelect(int maxValue)
    {
        sliderAmount.minValue = 1;
        sliderAmount.maxValue = maxValue;
        sliderAmount.value = 1;
    }

    public int GetSliderValue()
    {
        return (int)sliderAmount.value;
    }
}
