using UnityEngine;

public class MachineDeviceHelper : MachineOld
{
    [Header("Properties of Machine")]
    public int supper_idJSON;
    public int idDevice;
    
    public Sprite sprt;                                           
    public GameObject deviceObj;
  

    void Start()
    {
        spriteDetail = device.sprite;
    }
  
    public override void InitMachine(int x)
    {
        id = x;
        gameObject.GetComponent<SpriteRenderer>().sprite = DBase.Instance.spriteMachinesForDevice[x];
        sprt = DBase.Instance.spriteMachinesForDevice[x];
        deviceObj = DBase.Instance.getDeviceObj(idDevice);
    }
   
    public override void CollectProduct()
    {
        base.detailProduced.SetActive(false);
        base.MoveDetailToStock();
        DBase.Instance.AddDeviceByCount(device, amountDetails);
        StopProduce();
    }
    public override Sprite GetImageDetail()
    {
        return spriteDetail;
    }
  
}
