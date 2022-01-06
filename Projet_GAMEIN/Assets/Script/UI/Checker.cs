using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    // vérifie si l'object est actif ou non
    [SerializeField] GameObject objectToCheck;
    //[HideInInspector]
    public bool isOn = false;
    // Start is called before the first frame update
    private void Update()
    {
        if (objectToCheck.activeSelf)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }
    }
}
