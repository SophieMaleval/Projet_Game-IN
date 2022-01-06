using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips; 
    private AudioSource audioSource;

    void Awake(){

        audioSource =  GetComponent<AudioSource>();

        
    }

    void Step(){

        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    AudioClip GetRandomClip (){

        return clips [UnityEngine.Random.Range(0, clips.Length)];
    }


    

}
