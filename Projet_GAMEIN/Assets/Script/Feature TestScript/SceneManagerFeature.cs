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

    private void Awake() 
    {
        if(GameObject.Find("Player") != null)
        {
            PM =  GameObject.Find("Player").GetComponent<PlayerMovement>();
            CMVirtualCam.Follow = PM.transform ;
            SetPositionOnLoad();
            FadeImage = PM.GetComponent<PlayerScript>().CanvasIndestrucitble.gameObject.transform.Find("Fade").gameObject ;
            PM.GetComponent<PlayerScript>().CanvasIndestrucitble.GetComponent<Canvas>().worldCamera = Camera.main;
        
     
            PM.enabled = true ;
            PM.PlayerArrivedInNewScene = true ;
            PM.MakePlayerInGoodSens = true ;     
            PM.ChangePlayerSpeed(false);
        } 
    }

    void SetPositionOnLoad()
    {
        if(PM.GetComponentInChildren<PlayerProvenance>().ProviensCharacterCustomer || PM.GetComponentInChildren<PlayerProvenance>().ProviensMain) PM.transform.position = new Vector2 (-4f,-2f);        

        if(PM.GetComponentInChildren<PlayerProvenance>().ProviensCouchGameCrafter) PM.transform.position = new Vector2 (18.3f,9.25f);   


        // Reset bool
        PM.GetComponentInChildren<PlayerProvenance>().SetAllBoolToFalse();
        PM.GetComponentInChildren<PlayerProvenance>().ProviensMain = true ;

    }

    private void Start() 
    {
        FadeImage.GetComponent<AnimationTransitionScene>().OpenningScene();
    }

    IEnumerator WaitOppeningScene()
    {
        yield return new WaitForSeconds(0.05f);
        FadeImage.GetComponent<AnimationTransitionScene>().OpenningScene();
    }
}
