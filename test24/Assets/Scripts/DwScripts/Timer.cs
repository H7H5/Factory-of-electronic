using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text progressBarText;
    private Slider progressBarMachine;
    private MachineOld machineHelper;
    private float productionTime;
    public bool isTimerProgress = false;
    private float time = 0;

    void Awake()
    {
        machineHelper = GetComponentInParent<MachineOld>();
        progressBarMachine = gameObject.GetComponent<Slider>();
    }

    void Update()
    {
        if (isTimerProgress == true)
        {
            if (time <= 0)
            {   
                isTimerProgress = false;
                machineHelper.FinishProduceDetail();
                progressBarMachine.gameObject.SetActive(false);
            }
            RunTimer();
        }
    }

    private void RunTimer()
    {
        time -= Time.deltaTime;
        float inverseTime = productionTime - time;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        progressBarText.text = textTime;
        progressBarMachine.value = inverseTime;
    }

    public void LoadTimer()
    {
        isTimerProgress = true;
        gameObject.SetActive(true);
        machineHelper = GetComponentInParent<MachineOld>();
        TimeSpan res = DateTime.UtcNow - DateTime.Parse(machineHelper.startTimeDetailProduce);
        int resInSeconds = res.Hours * 3600 + res.Minutes * 60 + res.Seconds;
        productionTime = machineHelper.timeProduceDetail * machineHelper.amountDetails;
        progressBarMachine.maxValue = productionTime;
        time = productionTime - resInSeconds;
    }

    public void StartTimer(float productionTime)
    {
        this.productionTime = productionTime;
        isTimerProgress = true;
        progressBarMachine.maxValue = productionTime;
        time = productionTime;
    }

    public bool IsTimerProgress()
    {
        return isTimerProgress;
    }
}
