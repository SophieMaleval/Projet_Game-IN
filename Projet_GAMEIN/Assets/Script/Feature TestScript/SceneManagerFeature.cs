using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class SceneManagerFeature : MonoBehaviour
{
    public CinemachineVirtualCamera CMVirtualCam ;
    private PlayerMovement PM;
    private GameObject FadeImage ;

    private void Awake() {
        if(GameObject.Find("Player") != null)
        {
            PM =  GameObject.Find("Player").GetComponent<PlayerMovement>();
            CMVirtualCam.Follow = PM.transform ;
            PM.transform.position = new Vector2 (-4f,-2f);
            FadeImage = PM.GetComponent<PlayerScript>().CanvasIndestrucitble.gameObject.transform.Find("Fade").gameObject ;
            PM.GetComponent<PlayerScript>().CanvasIndestrucitble.GetComponent<Canvas>().worldCamera = Camera.main;

        }
    }

    private void Start() 
    {
        StartCoroutine(WaitTransitionAnim());
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player" && !PM.OnScooter)
        {
            other.gameObject.GetComponent<PlayerMovement>().enabled = false ;
            other.gameObject.GetComponent<PlayerMovement>().ResetVelocity();
            GoCustom();
        }
    }

    public void GoCustom()
    {
        StartCoroutine(WaitBeforeChangeScene());
    }

    IEnumerator WaitBeforeChangeScene()
    {
        FadeImage.SetActive(true);
        FadeImage.GetComponent<AnimationTransitionScene>().ShouldReveal = false ;
        yield return new WaitForSeconds(1.75f);
        SceneManager.LoadScene("Character Customer");
    }
    
    IEnumerator WaitTransitionAnim()
    {
        yield return new WaitForSeconds(0.25f);
        FadeImage.GetComponent<AnimationTransitionScene>().enabled = true ;
        yield return new WaitForSeconds(2f) ;
        FadeImage.SetActive(false);
    }
}
