using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScienceChangesDisplay : MonoBehaviour
{
    public static ScienceChangesDisplay Instance;

    [SerializeField] private GameObject scienceChanges;
    [SerializeField] private Text scienceChangeValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ActivateScienceChanges()
    {
        //scienceChanges.SetActive(true);
    }

    public void DisActivateScienceChanges()
    {
        //scienceChanges.SetActive(false);
    }

    public void ShowScienceChanges(int science)
    {
        if (science == 0)
        {
            DisActivateScienceChanges();
        }
        else
        {
            ActivateScienceChanges();
            string mathematicalSign = "";
            if (science > 0)
            {
                mathematicalSign = "+";
            }
            scienceChangeValue.text = mathematicalSign + science.ToString();
        }
    }
}
