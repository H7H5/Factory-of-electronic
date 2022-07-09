using UnityEngine;

public class SaverBase : MonoBehaviour
{
    public int countDevice;
    public int[] array;
    public int id;
    public int activation = 0;
    [Multiline(20)]
    public string data;
    private void Awake()
    {
        countDevice = transform.GetChild(0).transform.childCount;
        array = new int[countDevice];
        id = gameObject.GetComponent<ItemDevice>().device.id;
    }
    public void Save()
    {
       
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<ElementListener>().idElement;
        }
        activation = transform.GetChild(0).gameObject.GetComponent<DeviceBilder>().activation;
        data = JsonUtility.ToJson(this, true);
        DBase.Instance.Update_ConstrukrotDevice(id, data);
        data = null;  
    }
    public void Load()
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (transform.GetChild(0).transform.GetChild(i).childCount > 0)
            {
                Destroy(transform.GetChild(0).transform.GetChild(i).GetChild(0).gameObject);
            }
        }
        data = DBase.Instance.Reader_ConstruktorDevice(id);
        if (data==null)
        {
            Load();
        }
        else
        {
            JsonUtility.FromJsonOverwrite(data, this);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > 0)
                {
                    transform.GetChild(0).transform.GetChild(i).GetComponent<DropSchem>().Load(array[i]);
                }
            }
            DeviceBilder deviceBilder = transform.GetChild(0).gameObject.GetComponent<DeviceBilder>();
            if (activation == 1)
            {
                deviceBilder.activation = 1;
                deviceBilder.SetActivationDevice();
            }
            deviceBilder.ActivationDevice();
        }  
    }
}

