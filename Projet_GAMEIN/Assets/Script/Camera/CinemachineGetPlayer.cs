using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineGetPlayer : MonoBehaviour
{
    #region Behaviour

    private void Awake() 
    {
        if(GameManager.Instance.player != null)   // Récupère le player au lancement de la scène
        {    GetComponent<CinemachineVirtualCamera>().Follow = GameManager.Instance.player.transform ; }    
    }

    #endregion
}
