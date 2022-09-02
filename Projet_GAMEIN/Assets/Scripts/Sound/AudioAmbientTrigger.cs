using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmbientTrigger : MonoBehaviour
{
    #region Fields

    private AudioSource triggerAmbientSource;

    #endregion

    #region UnityInspector

    [SerializeField] private AudioData triggerAmbientToPlay;

    #endregion

    #region Behaviour

    private void Start()
    {
        if(triggerAmbientToPlay != null)
        {
            triggerAmbientSource = AudioController.Instance.PlayAudio(triggerAmbientToPlay);
            triggerAmbientSource.mute = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript player = collision.GetComponent<PlayerScript>();

        if(player != null && triggerAmbientToPlay != null)
        {
            triggerAmbientSource.mute = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerScript player = collision.GetComponent<PlayerScript>();

        if (player != null && triggerAmbientToPlay != null)
        {
            triggerAmbientSource.mute = true;
        }
    }

    #endregion
}
