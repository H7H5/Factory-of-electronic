using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderAmount : MonoBehaviour
{

    private Slider sliderAmount;
    private Text textValue;

    private void Awake()
    {
        sliderAmount = GetComponent<Slider>();
        textValue = GetComponentInChildren<Text>();
        textValue.text = sliderAmount.value.ToString();
    }

    public void ShowSliderValue ()
    {
        textValue.text = sliderAmount.value.ToString();
    }
}
