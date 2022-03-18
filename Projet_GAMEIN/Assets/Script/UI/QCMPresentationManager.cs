using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening ;
public class QCMPresentationManager : MonoBehaviour
{
    public Slider AudienceBar ;

    private void OnEnable() 
    {
        AudienceBar.value = 0.5f ;
        AudienceBar.gameObject.SetActive(true) ;
    }

    private void OnDisable() {
        AudienceBar.gameObject.SetActive(false);
    }

    public void AddValueSlider(bool IsPositive)
    {
      
        float ValueAdd = 0 ;
        if(!IsPositive)
        {
            ValueAdd = -0.1f ;
        } else {
            ValueAdd = 0.05f ;
        }

            AudienceBar.DOValue((AudienceBar.value + ValueAdd), 0.1f) ;        
    }

}
