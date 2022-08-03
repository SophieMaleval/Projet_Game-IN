using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsControls : MonoBehaviour
{
    public void PlaySound(AudioData audioData)
    {
        AllosiusDev.Audio.AudioController.Instance.PlayAudio(audioData);
    }
}
