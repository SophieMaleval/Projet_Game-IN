using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootStepsManager : MonoBehaviour
{
    [SerializeField]

    public  AudioClip[] clipsInside; 

    public AudioClip[] clipsOutside; 
    Scene scene;
    private AudioSource audioSource;

    void Awake(){
        
        audioSource =  GetComponent<AudioSource>();

        
    }
     void Update() {
         scene = SceneManager.GetActiveScene();
         Debug.Log(scene.name);
    }

    void Step(){

        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    AudioClip GetRandomClip (){
        if (scene.name == "Tilemaps test")
        {
            
            return clipsOutside [UnityEngine.Random.Range(0, clipsOutside.Length)];
        }
        else 
        {
             return clipsInside [UnityEngine.Random.Range(0, clipsInside.Length)];

        }
        
    }


    

}
