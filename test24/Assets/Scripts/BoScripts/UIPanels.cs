using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanels : MonoBehaviour
{
    public static UIPanels Instance;
    [SerializeField] private GameObject machineMenu;
    [SerializeField] private GameObject machineDeviceMenu;
    [SerializeField] private GameObject moveMachinePanel;
    [SerializeField] private BasketBoardScript basket;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void OpenMachineMenu(GameObject machine)
    {
        ButtonController.Instance.BlockButtons();
        if (machine.GetComponent<MachineHelper>())
        {
            machineMenu.SetActive(true);
            machineMenu.GetComponent<PanelOld>().select(machine); ;
        }
        if (machine.GetComponent<MachineDeviceHelper>())
        {
            machineDeviceMenu.SetActive(true);
            machineDeviceMenu.GetComponent<PanelOld>().select(machine);
        } 
    }
    public void OpenMoveMachinePanel(GameObject machine, bool start)
    {
        moveMachinePanel.SetActive(true);
        moveMachinePanel.GetComponent<MovePanel>().select(machine, start);
    }
}
