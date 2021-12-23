using UnityEngine;

public class MachineDeviceHelper : MachineOld
{
    [Header("Properties of Machine")]
    public int supper_idJSON;
    public int idDevice;
    public int price = 0;
    public Sprite sprt;                                           
    public GameObject device;
    public ItemDevice itemDevice;

    void Start()
    {
        if (device)
        {
            itemDevice = device.GetComponent<ItemDevice>();
            spriteDetail = itemDevice.imgStock;
        }
    }
  
    public override void InitMachine(int x)
    {
        id = x;
        gameObject.GetComponent<SpriteRenderer>().sprite = DBase.Instance.spriteMachinesForDevice[x];
        sprt = DBase.Instance.spriteMachinesForDevice[x];
        device = DBase.Instance.getDeviceObj(idDevice);
    }
   
    public override void CollectProduct()
    {
        base.detailProduced.SetActive(false);
        base.MoveDetailToStock();
        DBase.Instance.AddDeviceByCount(itemDevice, amountDetails);
        StopProduce();
    }
    public override Sprite GetImageDetail()
    {
        return spriteDetail;
    }
}
