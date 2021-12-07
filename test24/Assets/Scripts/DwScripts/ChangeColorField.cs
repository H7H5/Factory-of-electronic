using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorField : MonoBehaviour
{

    public Sprite[] sprites = new Sprite[3];

    public bool isMove = false;
    private MoveObjects moveObject;

    private bool isIntersect = false;

    private void Start()
    {
        moveObject = transform.parent.GetComponent<MoveObjects>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColorField")
        {
            isIntersect = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColorField")
        {
            isIntersect = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
    }

    void Update()
    {
        isMove = moveObject.isMove;

        if (isMove)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (!isMove)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (isMove && !isIntersect)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }
}