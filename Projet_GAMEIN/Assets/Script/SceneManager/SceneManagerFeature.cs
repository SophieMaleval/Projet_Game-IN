using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class SceneManagerFeature : MonoBehaviour
{
    #region Fields

    private PlayerMovement PM;
    //private GameObject FadeImage ;

    #endregion

    #region UnityInspector

    public CinemachineVirtualCamera CMVirtualCam ;

    #endregion

    #region Behaviour

    private void Awake() 
    {
        if (GameManager.Instance.player != null)
        {
            PM =  GameManager.Instance.player.GetComponent<PlayerMovement>();
            CMVirtualCam.Follow = PM.transform ;
            SetPositionOnLoad();
            //FadeImage = GameManager.Instance.gameCanvasManager.Fade ;
            PM.GetComponent<PlayerScript>().CanvasIndestrucitble.GetComponent<Canvas>().worldCamera = Camera.main;
        
     
            PM.enabled = true ;
            PM.PlayerNeedInitialePosition = true ;
            PM.MakePlayerInGoodSens = true ;     
            PM.ChangePlayerSpeed(false);
            PM.InExterior = true ;
        } 
    }

    void SetPositionOnLoad()
    {
        PM.transform.position = PM.GetComponent<PlayerScript>().MainSceneLoadPos ;
    }

    private void Start() 
    {
        //FadeImage.GetComponent<AnimationTransitionScene>().OpenningScene();
        if (GameManager.Instance.gameCanvasManager.CutoutMask != null)
            GameManager.Instance.gameCanvasManager.CutoutMask.FadeIn();
    }

    IEnumerator WaitOppeningScene()
    {
        yield return new WaitForSeconds(0.05f);
        //FadeImage.GetComponent<AnimationTransitionScene>().OpenningScene();
        if (GameManager.Instance.gameCanvasManager.CutoutMask != null)
            GameManager.Instance.gameCanvasManager.CutoutMask.FadeIn();
    }

    #endregion
}
