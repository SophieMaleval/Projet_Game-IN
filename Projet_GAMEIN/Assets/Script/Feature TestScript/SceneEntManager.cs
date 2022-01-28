using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class SceneEntManager : MonoBehaviour
{

    private PlayerMovement PM;
    private GameObject FadeImage ;

    [SerializeField] private List<GameObject> PartScene ;
    [SerializeField] private CameraTriggerVolume VolumeFirstCam ;
    
    [SerializeField] private Vector2 SetPosition ;
    


    private void Awake() 
    {
        if(GameObject.Find("Player") != null)
        {
            PM =  GameObject.Find("Player").GetComponent<PlayerMovement>();

            SetPositionOnLoad();
            FadeImage = PM.GetComponent<PlayerScript>().CanvasIndestrucitble.gameObject.transform.Find("Fade").gameObject ;
            PM.GetComponent<PlayerScript>().CanvasIndestrucitble.GetComponent<Canvas>().worldCamera = Camera.main;
        
            PM.enabled = true ;
            PM.PlayerChangeScene = false ;
            PM.ChangePlayerSpeed(true);
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
        if(FadeImage != null)    FadeImage.GetComponent<AnimationTransitionScene>().OpenningScene();

        if(VolumeFirstCam != null) VolumeFirstCam.SetFirstCamera();        
    }


    public void ChangePartScene(int PartDisplay)
    {
        for (int P = 0; P < PartScene.Count; P++)
        {
            PartScene[P].SetActive(false) ;
        }

        PartScene[PartDisplay].SetActive(true) ;
    }
}
