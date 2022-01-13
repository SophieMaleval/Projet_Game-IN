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
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player" && !PM.OnScooter)
        {
            other.gameObject.GetComponent<PlayerMovement>().enabled = false ;
            other.gameObject.GetComponent<PlayerMovement>().ResetVelocity();
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
        FadeImage.GetComponent<AnimationTransitionScene>().ShouldReveal = false ;
        yield return new WaitForSeconds(1.75f);
        SceneManager.LoadScene(NameScene);
    }
}
