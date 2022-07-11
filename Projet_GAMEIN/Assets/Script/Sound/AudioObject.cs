using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    #region UnityInspector

    public AudioData ClickingSlider;

    #endregion

    #region Behaviour

    public void ClickingOnSlider(){

        AudioController.Instance.PlayAudio(ClickingSlider);
    }

    #endregion
}
