using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour{
    public AudioMixer MyAudioMixer;


    public void SetVolume(float sliderValue){

        MyAudioMixer.SetFloat("Musique",Mathf.Log10(sliderValue) * 20 );
        
    }
     public void SetVolumeSFX(float sliderValue){

        MyAudioMixer.SetFloat("SFX",Mathf.Log10(sliderValue) * 20 );
    }

    public void SetVolumeGlobal(float sliderValue){

        MyAudioMixer.SetFloat("SFX",Mathf.Log10(sliderValue) * 20 );
        MyAudioMixer.SetFloat("Ambient_Menu",Mathf.Log10(sliderValue) * 20 );
        MyAudioMixer.SetFloat("Musique",Mathf.Log10(sliderValue) * 20 );
    }
    
}
