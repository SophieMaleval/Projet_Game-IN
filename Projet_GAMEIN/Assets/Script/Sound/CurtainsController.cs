using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainsController : MonoBehaviour
{
    #region UnityInspector

    public AudioData Curtains;
    public AudioData CurtainsClosing;

    #endregion

    #region Behaviour

    public void PlayCurtainsSoundsOpening()
    {

        AllosiusDev.Audio.AudioController.Instance.PlayAudio(Curtains);
    }
    public void PlayCurtainsSoundsClosing()
    {

        AllosiusDev.Audio.AudioController.Instance.PlayAudio(CurtainsClosing);
    }

    #endregion
}
