using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Learning : MonoBehaviour
{
    [SerializeField] private Text text;
    private int step = -1;
    [SerializeField] private GameObject [] images;
    [SerializeField] private GameObject Learning_panel;
    string[] learning_text = {"Hello! In this game you have your own electronics factory. In the main workshop, it is necessary to install machines that manufacture radio elements or ready-made devices.",
    "Here you can conclude economic agreements and receive extra income, as well as increase your authority among sellers of similar products and receive better conditions for trade ",
    "This is a shop where you can buy machines for the production of radio elements or electronic devices. You can also buy some radio cells here",
    "Our laboratory is located here, where it is possible to discover technologies for the manufacture of radio elements and electronic devices. After opening a new element in the store, there is an opportunity to purchase the machine that produces it ",
    "This is a workshop for making devices with your own hands. After opening a new device, there is an opportunity to assemble it from the necessary elements",
    "This is where your warehouse is located, where manufactured items and devices are stored. You can sell elements from the warehouse at any time and with a bonus for science, which is needed to discover new technologies, but at a cheap price",
    "This is a basket in which you can put radio elements from the warehouse and put them in the workshop for the manufacture of devices."};
    void Start()
    {
        NextStep();
    }

    public void NextStep()
    {
        step++;
        if (step >= learning_text.Length)
        {
            DBase.Instance.SaveLearning(step);
            Destroy(Learning_panel);
            return;
        }
        text.text = learning_text[step];
        showArrow(step);
    }

    public void showArrow(int x)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
        switch (x)
        {
            case 1:
                images[0].SetActive(true);
                break;
            case 2:
                images[1].SetActive(true);
                break;
            case 3:
                images[2].SetActive(true);
                break;
            case 4:
                images[3].SetActive(true);
                break;
            case 5:
                images[4].SetActive(true);
                break;
            case 6:
                images[5].SetActive(true);
                break;

        }
    }
    
}
