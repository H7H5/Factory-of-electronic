using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineParentScript : MonoBehaviour
{
    public static MachineParentScript Instance;
    public GameObject machine;
    public GameObject machineDevice;
    public GameObject DetailCreator;
    public unitySQLite unitySQLite;
    //public int max_id = 0;
    public int max_id_JSON = 0;
    public int max_id_JSON_Device = 0;
    public GameObject DBase;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        unitySQLite = DBase.GetComponent<unitySQLite>();
        StartCoroutine(StartCoroutine());

        //buldMachines();
    }
    public void upDateOneMachineByid(GameObject machine)
    {  
        if (machine.GetComponent<MachineHelper>())
        {
            if (machine.GetComponent<MachineHelper>().supper_idJSON == 0)
            {
                unitySQLite.Insert_functionJSON(" ");
                unitySQLite.Reader_functionAllidJSON();
                machine.GetComponent<MachineHelper>().supper_idJSON = max_id_JSON;
                machine.GetComponent<MachineHelper>().Save();
                unitySQLite.Update_functionJSON(max_id_JSON,machine.GetComponent<MachineHelper>().GetData());
                
            }
            else
            {
                machine.GetComponent<MachineHelper>().Save();
                unitySQLite.Update_functionJSON(machine.GetComponent<MachineHelper>().supper_idJSON, machine.GetComponent<MachineHelper>().GetData());
            }
        }
        if (machine.GetComponent<MachineDeviceHelper>())
        {
            if (machine.GetComponent<MachineDeviceHelper>().supper_idJSON == 0)
            {
                unitySQLite.Insert_functionMachineDevice(" ");
                unitySQLite.Reader_functionAllidMachineDevice();
                machine.GetComponent<MachineDeviceHelper>().supper_idJSON = max_id_JSON;
                machine.GetComponent<MachineDeviceHelper>().Save();
                unitySQLite.Update_functionMachineDevice(max_id_JSON, machine.GetComponent<MachineDeviceHelper>().GetData());
               

            }
            else
            {
                machine.GetComponent<MachineDeviceHelper>().Save();
                unitySQLite.Update_functionMachineDevice(machine.GetComponent<MachineDeviceHelper>().supper_idJSON, machine.GetComponent<MachineDeviceHelper>().GetData());
                
            }
        }
        if (machine.GetComponent<MachineHelper>() != null)
        {

        }
    }
    public void buldMachines()
    {
        unitySQLite.Reader_functionJSON();
        unitySQLite.Reader_functionMachineDevice();
    }
    public void CreateOneMachineJSON(string data)
    {
        GameObject machine1 = Instantiate(machine, transform.position, gameObject.transform.rotation);
        machine1.transform.SetParent(transform);
        machine1.GetComponent<MachineHelper>().Load(data);
    }
    public void CreateOneMachineDevice(string data)
    {
        GameObject machine1 = Instantiate(machineDevice, transform.position, gameObject.transform.rotation);
        machine1.transform.SetParent(transform);
        machine1.GetComponent<MachineDeviceHelper>().Load(data);
    }
    public void deleteOneMachineByIdJSON(int id)
    {
        unitySQLite.Delete_functionJSON(id);
    }
    public void deleteOneMachineByIdJSONDevice(int id)
    {
        unitySQLite.Delete_functionMachineDevice(id);
    }

    IEnumerator StartCoroutine()
    {
        // Вывести время первого вызова функции.
        // Debug.Log(" Запуск сопрограммы в метку времени:" +Time.time);

        // yield по новой инструкции YieldInstruction, которая ждет 5 секунд.
        yield return new WaitForSeconds(0.2f);

        buldMachines();
        // Через 5 секунд снова вывести время.
        //Debug.Log("Завершенная сопрограмма на отметке времени:" +Time.time);
    }
  
    public void maxId_JSON(int id)
    {
        max_id_JSON = id;
    }
    public void maxid_JSON_Device(int id)
    {
        max_id_JSON_Device = id;
    }
}

