using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public AudioData ClickingSlider;
    // Start is called before the first frame update
    
    public void ClickingOnSlider(){

        AudioController.Instance.PlayAudio(ClickingSlider);
    }
}
