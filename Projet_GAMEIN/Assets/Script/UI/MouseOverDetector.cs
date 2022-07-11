using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverDetector : MonoBehaviour
{

    #region Behaviour

    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject()) // Regarde si la souris survol le champ de text
        {
            transform.GetComponentInParent<DialogueDisplayerController>().MouseIsHover = true ;
        } else {
            transform.GetComponentInParent<DialogueDisplayerController>().MouseIsHover = false ;
        }
    }

    #endregion
}
