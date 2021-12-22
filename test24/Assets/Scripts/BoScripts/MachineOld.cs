using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineOld : MonoBehaviour
{
    protected GameObject detailProduced;
    protected GameObject timerObject;
    private Timer timer;
    private TimerDevice timerDevice;
    private void Awake()
    {
        detailProduced = gameObject.transform.GetChild(6).gameObject.transform.GetChild(1).gameObject;

        timerObject = gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject;
        if (timerObject.GetComponent<Timer>())
        {
            timer = timerObject.GetComponent<Timer>();
        }
        if (timerObject.GetComponent<TimerDevice>())
        {
            timerDevice = timerObject.GetComponent<TimerDevice>();
        }
    }
    public virtual void CollectProduct() {

    }
    protected void MoveDetailToStock()
    {
        GameObject test = Instantiate(detailProduced, detailProduced.transform.parent);
        test.SetActive(true);
        test.GetComponent<DetailToInventory>().moveToInventory();
    }
    public bool IsFinishDetail()
    {
        return detailProduced.activeInHierarchy ? true : false;
    }
    public void ShowTimerObject() {
        if (timer)
        {
            if (timer.isTimerProgress == true)
            {
                timerObject.SetActive(true);
            }
        }
        if (timerDevice)
        {
            if (timerDevice.isTimerProgress == true)
            {
                timerObject.SetActive(true);
            }
        }
    }

    public void HideTimerObject()
    {
        timerObject.SetActive(false);
    }
}
