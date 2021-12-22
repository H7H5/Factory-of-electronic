using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanels : MonoBehaviour
{
    public static UIPanels Instance;
    [SerializeField] private GameObject machineMenu;
    [SerializeField] private GameObject machineDeviceMenu;
    [SerializeField] private GameObject moveMachinePanel;
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
            machineMenu.GetComponent<ManagerMachineHelper>().select(machine); ;
        }
        if (machine.GetComponent<MachineDeviceHelper>())
        {
            machineDeviceMenu.SetActive(true);
            machineDeviceMenu.GetComponent<ManagerMachineDeviceHelper>().select(machine);
        } 
    }
    public void OpenMoveMachinePanel(GameObject machine)
    {
        moveMachinePanel.SetActive(true);
        moveMachinePanel.GetComponent<MoveButton>().select(machine, 0);
    }
}
