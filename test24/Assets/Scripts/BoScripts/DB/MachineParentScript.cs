using System.Collections;
using UnityEngine;

public class MachineParentScript : MonoBehaviour
{
    public static MachineParentScript Instance;
    [SerializeField] private GameObject machine;
    [SerializeField] private GameObject machineDevice;
    private int max_id_JSON = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StartCoroutine(StartCoroutine());
    }
    public void UpDateOneMachineByid(GameObject machine)
    {  
        if (machine.GetComponent<MachineHelper>())
        {
            if (machine.GetComponent<MachineHelper>().supper_idJSON == 0)
            {
                DBase.Instance.Insert_functionJSON(" ");
                DBase.Instance.Reader_functionAllidJSON();
                machine.GetComponent<MachineHelper>().supper_idJSON = max_id_JSON;
                machine.GetComponent<MachineHelper>().Save();
                DBase.Instance.Update_functionJSON(max_id_JSON,machine.GetComponent<MachineHelper>().GetData());   
            }
            else
            {
                machine.GetComponent<MachineHelper>().Save();
                DBase.Instance.Update_functionJSON(machine.GetComponent<MachineHelper>().supper_idJSON, machine.GetComponent<MachineHelper>().GetData());
            }
        }
        if (machine.GetComponent<MachineDeviceHelper>())
        {
            if (machine.GetComponent<MachineDeviceHelper>().supper_idJSON == 0)
            {
                DBase.Instance.Insert_functionMachineDevice(" ");
                DBase.Instance.Reader_functionAllidMachineDevice();
                machine.GetComponent<MachineDeviceHelper>().supper_idJSON = max_id_JSON;
                machine.GetComponent<MachineDeviceHelper>().Save();
                DBase.Instance.Update_functionMachineDevice(max_id_JSON, machine.GetComponent<MachineDeviceHelper>().GetData());
            }
            else
            {
                machine.GetComponent<MachineDeviceHelper>().Save();
                DBase.Instance.Update_functionMachineDevice(machine.GetComponent<MachineDeviceHelper>().supper_idJSON, machine.GetComponent<MachineDeviceHelper>().GetData());
                
            }
        }
        if (machine.GetComponent<MachineHelper>() != null)
        {

        }
    }
    public void BuldMachines()
    {
        DBase.Instance.Reader_functionJSON();
        DBase.Instance.Reader_functionMachineDevice();
    }
    public void CreateOneMachineJSON(string data)
    {
        GameObject machine1 = Instantiate(machine, transform.position, gameObject.transform.rotation);
        machine1.transform.SetParent(transform);
        machine1.GetComponent<MachineOld>().Load(data);
    }
    public void CreateOneMachineDevice(string data)
    {
        GameObject machine1 = Instantiate(machineDevice, transform.position, gameObject.transform.rotation);
        machine1.transform.SetParent(transform);
        machine1.GetComponent<MachineOld>().Load(data);
    }
    public void DeleteOneMachineByIdJSON(int id)
    {
        DBase.Instance.Delete_functionJSON(id);
    }
    public void DeleteOneMachineByIdJSONDevice(int id)
    {
        DBase.Instance.Delete_functionMachineDevice(id);
    }
    IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        BuldMachines();
    }
    public void maxId_JSON(int id)
    {
        max_id_JSON = id;
    }
}

