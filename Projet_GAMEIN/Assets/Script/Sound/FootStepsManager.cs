using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootStepsManager : MonoBehaviour
{
    #region Fields

    Scene scene;
    private AudioSource audioSource;

    #endregion

    #region UnityInspector

    [SerializeField]

    public  AudioClip[] clipsInside; 

    public AudioClip[] clipsOutside;

    #endregion

    #region Behaviour

    void Awake(){
        
        audioSource =  GetComponent<AudioSource>();

        
    }
     void Update() {
         scene = SceneManager.GetActiveScene();
//         Debug.Log(scene.name);
    }

    void Step(){

        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    AudioClip GetRandomClip (){
        if (scene.name == "Main")
        {
            
            return clipsOutside [UnityEngine.Random.Range(0, clipsOutside.Length)];
        }
        else 
        {
             return clipsInside [UnityEngine.Random.Range(0, clipsInside.Length)];

        }
        
    }

    #endregion


}
