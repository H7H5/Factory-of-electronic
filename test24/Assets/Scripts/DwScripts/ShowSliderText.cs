using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderText : MonoBehaviour
{
    private Slider sliderAmount;
    private Text textValue;

    private void Awake()
    {
        sliderAmount = GetComponent<Slider>();
        textValue = GetComponentInChildren<Text>();
        textValue.text = sliderAmount.value.ToString() + "/100";
    }

    public void ShowSliderValue()
    {
        //textValue.text = sliderAmount.value.ToString() + "/100";
    }
}
