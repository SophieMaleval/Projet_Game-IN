using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class SceneEntManager : MonoBehaviour
{
    public CinemachineVirtualCamera CMVirtualCam ;
    private PlayerMovement PM;
    private GameObject FadeImage ;
    
    [SerializeField] private Vector2 SetPosition ;
    


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
        }
    }

    void SetPositionOnLoad()
    {
        PM.transform.position = SetPosition;   

        // Reset bool
        PM.GetComponentInChildren<PlayerProvenance>().SetAllBoolToFalse();
        PM.GetComponentInChildren<PlayerProvenance>().ProviensCouchGameCrafter = true ;

    }

    private void Start() 
    {
        StartCoroutine(WaitTransitionAnim());
    }
    
    IEnumerator WaitTransitionAnim()
    {
        yield return new WaitForSeconds(0.25f);
        FadeImage.GetComponent<AnimationTransitionScene>().ShouldReveal = false ;
        yield return new WaitForSeconds(2f) ;
        FadeImage.SetActive(false);
    }
}
