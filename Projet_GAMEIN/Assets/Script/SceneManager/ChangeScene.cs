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

            if(SceneManager.GetActiveScene().name == "Tilemaps test" || SceneManager.GetActiveScene().name == "Main") PM.GetComponent<PlayerScript>().MainSceneLoadPos = GiveNewPos();
            if(SceneManager.GetActiveScene().name == "Game In") PM.GetComponent<PlayerScript>().MainSceneLoadPos = new Vector2(-4f, -2f);
            PM.GetComponent<PlayerScript>().PreviousSceneName = SceneManager.GetActiveScene().name;

            DoorOpeningSound.Play();
            GoNewScene();
        }
    }


    Vector2 GiveNewPos()
    {
        BoxCollider2D BCol2D = GetComponent<BoxCollider2D>() ;
        Vector2 PositionSpawn = new Vector2(transform.position.x + BCol2D.offset.x, transform.position.y + BCol2D.offset.y - 0.5f) ;
        return PositionSpawn ;
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
