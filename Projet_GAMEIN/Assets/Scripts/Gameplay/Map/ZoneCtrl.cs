using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCtrl : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private LocationZone zoneAssociated;

    [SerializeField] private AudioData zoneAmbient;

    [SerializeField] private float transitionAnimDuration;

    #endregion

    #region Behaviour

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript player = collision.GetComponent<PlayerScript>();

        if(player != null && GameCore.Instance != null && GameCore.Instance.currentZone != zoneAssociated)
        {
            //GameCore.Instance.SetCurrentZone(zoneAssociated, zoneAmbient);

            //StartCoroutine(GameManager.Instance.gameCanvasManager.SetTitleBannerActivation(true, zoneAssociated.zoneName, transitionAnimDuration));
        }
    }

    #endregion
}
