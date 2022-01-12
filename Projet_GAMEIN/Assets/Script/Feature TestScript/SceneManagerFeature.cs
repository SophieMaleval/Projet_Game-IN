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
        StartCoroutine(WaitTransitionAnim());
        //PM.GetComponent<PlayerScript>().InventoryUIIndestructible.GetComponent<InventoryScript>().SwitchToggleInventoryDisplay();
    }


  /*  private void OnTriggerEnter2D(Collider2D other) 
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
        SceneManager.LoadScene(NameScene);
    }*/
    
    IEnumerator WaitTransitionAnim()
    {
        yield return new WaitForSeconds(0.25f);
        FadeImage.GetComponent<AnimationTransitionScene>().enabled = true ;
        yield return new WaitForSeconds(2f) ;
        FadeImage.SetActive(false);
    }
}
