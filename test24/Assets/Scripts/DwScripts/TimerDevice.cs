using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDevice : MonoBehaviour
{
    public Slider progressBarMachine;
    public Text progressBarText;
    public float productionTime;
    public bool isTimerProgress;

    private float time = 0;

    private int resInSeconds = 0;

    private TimeSpan res;

    public MachineDeviceHelper machineHelper;
    

    public Image imageDetail;

    public Text textAmountDetails;

    void Start()
    {
        machineHelper = GetComponentInParent<MachineDeviceHelper>();
    }

    void Update()
    {
        if (isTimerProgress == true)
        {
            if (time <= 0)
            {
                isTimerProgress = false;
                machineHelper.isProduce = false;
                imageDetail.sprite = machineHelper.spriteDetail;
                imageDetail.gameObject.SetActive(true);
                ShowAmountDetails();
                progressBarMachine.gameObject.SetActive(false);
                machineHelper.startTimeDetailProduce = "";
            }

            time -= Time.deltaTime;
            float inverseTime = productionTime - time;

            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time - minutes * 60);

            string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            progressBarText.text = textTime;
            progressBarMachine.value = inverseTime;
        }
    }

    public void loadTimer()
    {
        isTimerProgress = true;
        progressBarMachine.gameObject.SetActive(true);

        machineHelper = GetComponentInParent<MachineDeviceHelper>();
        res = DateTime.UtcNow - DateTime.Parse(machineHelper.startTimeDetailProduce);
        resInSeconds = res.Hours * 3600 + res.Minutes * 60 + res.Seconds;
        productionTime = machineHelper.timeProduceDetail * machineHelper.amountDetails;
        progressBarMachine.maxValue = productionTime;
        time = productionTime - resInSeconds;
    }

    public void startTimer()
    {
        isTimerProgress = true;
        progressBarMachine.maxValue = productionTime;
        time = productionTime;
    }

    public void ShowAmountDetails()
    {
        textAmountDetails.text = machineHelper.amountDetails.ToString();
    }
}
