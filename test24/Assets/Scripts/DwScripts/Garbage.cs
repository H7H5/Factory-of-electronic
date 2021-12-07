using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private GameObject attentionGarbage;
    private GarbageController garbageControllerScript;
    private ButtonController buttonController;

    private void Awake()
    {
        GameObject dwObjects = GameObject.Find("DwObjects");
        buttonController = dwObjects.GetComponentInChildren<ButtonController>();
        garbageControllerScript = dwObjects.GetComponentInChildren<GarbageController>();

        GameObject Canvas = GameObject.Find("Canvas");
        Transform attentionGarbageTransform = Canvas.GetComponent<Transform>().Find("AttentionGarbageClear");
        attentionGarbage = attentionGarbageTransform.gameObject;
    }

    private void OnMouseDown()
    {
        //Debug.Log(buttonController.isBlocked);
        if (buttonController.isBlocked == false)
        {
            attentionGarbage.SetActive(true);
            garbageControllerScript.garbageToDelete = transform.gameObject;
            buttonController.BlockButtons();
        }
    }
}
