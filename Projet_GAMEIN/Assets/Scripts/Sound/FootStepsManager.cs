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

    private GroundType currentGroundType = GroundType.Street;

    #endregion

    #region UnityInspector

    //public AudioClip[] clipsInside;

    //public AudioClip[] clipsOutside;

    [SerializeField] AudioData[] sfxInsides;

    [SerializeField] AudioData[] sfxOutsidesGrass;
    [SerializeField] AudioData[] sfxOutsidesStreet;
    [SerializeField] AudioData[] sfxOutsidesSand;

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

        if (GameCore.Instance != null)
        {
            AudioData[] datas = GetRandomClip();
            AudioController.Instance.PlayRandomOneShotAudio(datas);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ground ground = collision.GetComponent<Ground>();

        Debug.Log(ground);

        if(ground != null)
        {
            currentGroundType = ground.GroundType;
        }
    }

    AudioData[] GetRandomClip ()
    {
        if (GameCore.Instance.CurrentScene.sceneOutside)
        {
            if(currentGroundType == GroundType.Grass)
            {
                return sfxOutsidesGrass;
            }
            else if (currentGroundType == GroundType.Street)
            {
                return sfxOutsidesStreet;
            }
            else if (currentGroundType == GroundType.Sand)
            {
                return sfxOutsidesSand;
            }

            return sfxOutsidesGrass;

        }
        else 
        {
             return sfxInsides;

        }
        
    }

    #endregion

}

public enum GroundType
{
    Grass,
    Street,
    Sand,
}
