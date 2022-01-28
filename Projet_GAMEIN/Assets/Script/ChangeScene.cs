using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private PlayerMovement PM;
    private GameObject FadeImage ;
    public string NameScene;

    public AudioSource DoorOpeningSound;

    private void Awake() 
    {
        if(GameObject.Find("Player") != null)
        {
            PM =  GameObject.Find("Player").GetComponent<PlayerMovement>();
            FadeImage = PM.GetComponent<PlayerScript>().CanvasIndestrucitble.gameObject.transform.Find("Fade").gameObject ;
        }

        if(GameObject.Find("DoorOpening") != null)
        {
            DoorOpeningSound = GameObject.Find("DoorOpening").GetComponent<AudioSource>() ;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player" && !PM.OnScooter)
        {
            PM.PlayerChangeScene = true ;
            PM.PlayerArrivedInNewScene = false ;
            if(other.gameObject.transform.position.y < (transform.position.y + GetComponent<BoxCollider2D>().offset.y)) PM.MakePlayerInGoodSens = true ;
            else PM.MakePlayerInGoodSens = false ;

            PM.ResetVelocity();

            DoorOpeningSound.Play();
            GoNewScene();
        }
    }

    public void GoNewScene()
    {
        StartCoroutine(WaitBeforeChangeScene());
    }

    IEnumerator WaitBeforeChangeScene()
    {
        FadeImage.SetActive(true);
        FadeImage.GetComponent<AnimationTransitionScene>().QuitScene();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(NameScene);

    }
}
