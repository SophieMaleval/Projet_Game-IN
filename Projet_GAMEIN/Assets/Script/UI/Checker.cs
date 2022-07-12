using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    #region UnityInspector

    // vérifie si l'object est actif ou non
    [SerializeField] GameObject objectToCheck;
    public GameObject firstQuest, secondQuest, thirdQuest,fourthQuest;
    //[HideInInspector]
    public bool isOn = false;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    private void Update()
    {
        if (objectToCheck.activeSelf)
        {
            isOn = true;
            firstQuest.SetActive(true);
            secondQuest.SetActive(true);
          /*  thirdQuest.SetActive(true);
            fourthQuest.SetActive(true);*/
        }
        else
        {
            isOn = false;
            firstQuest.SetActive(false);
            secondQuest.SetActive(false);
        /*    thirdQuest.SetActive(false);
            fourthQuest.SetActive(false);*/
        }
    }

    #endregion
}
