using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public static ButtonController Instance;
    public GameObject[] buttons;

    //personal settings test
    public GameObject bascet;
    public GameObject blockBascet;
    public GameObject bascetBoaard;
    public GameObject openStock;

    private Camera mainCamera;
    private CameraController cameraController;

    public bool isBlocked = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        isBlocked = false;
        mainCamera = Camera.main;
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    void Update()
    {
        
    }

    public void BlockButtons ()
    {
        isBlocked = true;

        cameraController.StopMove();

        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = false;
        }

        //personal settings test
        if (blockBascet.GetComponent<Button>().interactable == false)
        {
            bascet.GetComponent<Button>().interactable = false;
            bascetBoaard.GetComponent<BasketBoardScript>().Hide();
        }

        if (openStock.GetComponent<Button>().interactable == false)
        {
            bascet.GetComponent<Button>().interactable = true;
        }
    }

    public void UnBlockButtons()
    {
        isBlocked = false;

        cameraController.startMove();

        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }

        //personal settings test
        bascet.GetComponent<Button>().interactable = true;
    }

    public void setIsBlockedFalse ()
    {
        isBlocked = false;
    }

    public void setIsBlockedTrue ()
    {
        isBlocked = true;
    }

    public void changeBlocked ()
    {
        if (isBlocked == true)
        {
            isBlocked = false;
        } else
        {
            isBlocked = true;
        }

        if (cameraController.isMove == true)
        {
            cameraController.isMove = false;
        } else
        {
            cameraController.isMove = true;
        }
    }
}
