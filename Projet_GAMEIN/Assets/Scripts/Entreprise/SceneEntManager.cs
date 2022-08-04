﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using Core.Session;
using AllosiusDev.DialogSystem;

public class SceneEntManager : MonoBehaviour
{

    #region Fields

    private PNJDialogue[] pnjs;
    private NpcConversant[] npcs;

    private PlayerMovement PM;
    //private GameObject FadeImage ;

    //private GameObject GlobalLightPlayer ;

    #endregion

    #region UnityInspector

    [SerializeField] private List<GameObject> PartScene ;
    [SerializeField] private CameraTriggerVolume VolumeFirstCam ;
    
    [SerializeField] private Vector2 SetPosition ;
    
    //[SerializeField] private bool LightAutoGenerateInThisScene = false ;

    #endregion

    #region Behaviour

    private void Awake() 
    {
        if(GameManager.Instance.player != null)
        {
            PM =  GameManager.Instance.player.GetComponent<PlayerMovement>();

            pnjs = FindObjectsOfType<PNJDialogue>();
            npcs = FindObjectsOfType<NpcConversant>();

            if (VolumeFirstCam == null)
            {
                //if(PM.GetComponent<PlayerScript>().PreviousSceneName == "Character Customer") VolumeFirstCam = GameObject.Find("Camera Trigger Zone Etage").GetComponent<CameraTriggerVolume>() ;
                if(PM.GetComponent<PlayerScript>().PreviousSceneName.isGameScene == false) VolumeFirstCam = GameObject.Find("Camera Trigger Zone Etage").GetComponent<CameraTriggerVolume>();
                //if(PM.GetComponent<PlayerScript>().PreviousSceneName == "Main" || PM.GetComponent<PlayerScript>().PreviousSceneName == "Tilemaps test") VolumeFirstCam = GameObject.Find("Camera Trigger Zone RDC").GetComponent<CameraTriggerVolume>() ;
                if (PM.GetComponent<PlayerScript>().PreviousSceneName.sceneOutside) VolumeFirstCam = GameObject.Find("Camera Trigger Zone RDC").GetComponent<CameraTriggerVolume>();
            }


            SetPositionOnLoad();
            //FadeImage = GameManager.Instance.gameCanvasManager.Fade;
            GameManager.Instance.gameCanvasManager.GetComponent<Canvas>().worldCamera = Camera.main;
        
            PM.enabled = true ;
            PM.PlayerChangeScene = false ;
            PM.ChangePlayerSpeed(true);
            PM.InExterior = false ;
        }
    }

    void SetPositionOnLoad()
    {
        //if(SceneManager.GetActiveScene().name != "Game In")
        if(GameCore.Instance.CurrentScene != SessionController.Instance.Game.StartLevelScene)
        {
            PM.transform.position = SetPosition;  
        } 
        else 
        {
            //if(PM.GetComponent<PlayerScript>().PreviousSceneName == "Character Customer")
            if (PM.GetComponent<PlayerScript>().PreviousSceneName.isGameScene == false)
            {
                PM.transform.position = new Vector2(-5.5f, 2.65f);  
                PM.GiveGoodAnimation(new Vector2(0f, -1f));
                ChangePartScene(1);
            } 
            //if(PM.GetComponent<PlayerScript>().PreviousSceneName == "Main" || PM.GetComponent<PlayerScript>().PreviousSceneName == "Tilemaps test")
            if (PM.GetComponent<PlayerScript>().PreviousSceneName.isGameScene && PM.GetComponent<PlayerScript>().PreviousSceneName.sceneOutside)
            {   
                PM.transform.position = SetPosition;  
                PM.GiveGoodAnimation(new Vector2(0f, 1f));              
            } 
        }
 
    }

    private void Start() 
    {
        //if(FadeImage != null)    FadeImage.GetComponent<AnimationTransitionScene>().OpenningScene();
        if (GameManager.Instance.gameCanvasManager.CutoutMask != null)
            GameManager.Instance.gameCanvasManager.CutoutMask.FadeIn();

        if (VolumeFirstCam != null) VolumeFirstCam.SetFirstCamera();

        //GlobalLightPlayer = GameManager.Instance.player.globalLightPlayer;
        //if(LightAutoGenerateInThisScene) GlobalLightPlayer.SetActive(false);

        
    }
    
    private void Update() 
    {
        //if(GetComponent<ChangeScene>().PlayerChangeScene) GlobalLightPlayer.SetActive(true);
    }


    public void ChangePartScene(int PartDisplay)
    {
        for (int P = 0; P < PartScene.Count; P++)
        {
            PartScene[P].SetActive(false) ;
        }

        PartScene[PartDisplay].SetActive(true) ;

        for (int i = 0; i < pnjs.Length; i++)
        {
            pnjs[i].InitAnimator();
        }

        /*for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i].InitAnimator();
        }*/
    }

    #endregion
}
