using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailToInventory : MonoBehaviour
{
    public bool isMove = false;
    public GameObject stock;

    private void Awake()
    {
        stock = GameObject.Find("OpenStock");
    }

    void Start()
    {

    }

    void Update()
    {
        if (isMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, stock.transform.position, 0.1f);
            if (Vector2.Distance(transform.position, stock.transform.position) < 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void moveToInventory ()
    {
        isMove = true;
    }
}
