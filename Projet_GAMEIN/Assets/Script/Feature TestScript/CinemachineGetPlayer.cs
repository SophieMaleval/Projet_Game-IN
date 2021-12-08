using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineGetPlayer : MonoBehaviour
{
    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform ; }    
    }
}
