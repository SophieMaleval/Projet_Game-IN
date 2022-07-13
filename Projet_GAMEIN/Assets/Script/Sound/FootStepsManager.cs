using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootStepsManager : MonoBehaviour
{
    #region Fields

    //Scene scene;
    //private AudioSource audioSource;

    #endregion

    #region UnityInspector

    //public AudioClip[] clipsInside;

    //public AudioClip[] clipsOutside;

    [SerializeField] AudioData[] sfxInsides;
    [SerializeField] AudioData[] sfxOutsides;

    #endregion

    #region Behaviour

    void Awake() {

        //audioSource =  GetComponent<AudioSource>();


    }
    void Update() {
        //scene = SceneManager.GetActiveScene();
        //         Debug.Log(scene.name);
    }

    void Step() 
    {

        /*AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);*/

        AudioData[] datas = GetRandomClip();
        AudioController.Instance.PlayRandomOneShotAudio(datas);
    }

    AudioData[] GetRandomClip ()
    {
        if (GameCore.Instance.CurrentScene.sceneOutside)
        {
            return sfxOutsides;
        }
        else 
        {
             return sfxInsides;

        }
        
    }

    #endregion


}
