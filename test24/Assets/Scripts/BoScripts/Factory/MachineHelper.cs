using UnityEngine;

public class MachineHelper : MachineOld
{
    [Header ("Properties of Machine")]
    public int supper_idJSON;
    public int idDetail;
    
    public Sprite sprt;
    public Element detail;
    //public ItemElement itemElementDetail;
    public int[] idDetailsToModern;
    public bool[] boolDetailsToModern;
    public int selectNumberElement = 0;

    void Start()
    {
        if (detail)
        {
            //itemElementDetail = detail.GetComponent<ItemElement>();
            spriteDetail = detail.sprite;
        } 
    }

    public override void InitMachine(int x)
    {
        id = x;
        gameObject.GetComponent<SpriteRenderer>().sprite = machine.sprite;
        sprt = machine.sprite;
        detail = DBase.Instance.getDetailID(idDetail);
        for (int i = 0; i < idDetailsToModern.Length; i++)
        {
            detailsToModern[i] = DBase.Instance.getDetailID(idDetailsToModern[i]);
        }
    }
    
    public override void CollectProduct()
    {
        base.detailProduced.SetActive(false);
        base.MoveDetailToStock();
        DBase.Instance.AddElementOnIdByCount(GetIdSelectedDetail(), amountDetails);
        StopProduce();
    }
    public override Sprite GetImageDetail()
    {
        return detailsToModern[selectedDetail].sprite;
    }

    public int GetIdSelectedDetail()
    {
        return detailsToModern[selectedDetail].id;
    }
   

}
