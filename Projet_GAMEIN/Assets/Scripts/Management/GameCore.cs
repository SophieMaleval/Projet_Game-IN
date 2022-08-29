using AllosiusDev;
using AllosiusDev.Audio;
using AllosiusDev.DialogSystem;
using Cinemachine;
using Core.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : Singleton<GameCore>
{
    #region Fields

    #endregion

    #region Properties

    public SceneData CurrentScene => currentScene;

    public AudioData MainMusic => mainMusic;


    public Vector2 OldLastMovePlayer { get; set; }

    public RythmeGameRank currentRythmeGameRank { get; set; }


    public CinemachineVirtualCamera Vcam => vcam;

    public CinemachineVirtualCamera VCamDezoom => vCamDezoom;
    public bool dezoomTouchActive { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private SceneData currentScene;

    [SerializeField] private AudioData mainMusic;


    [SerializeField] private GameObject MiniGame;

    [Space]

    [SerializeField] private CinemachineVirtualCamera vcam;

    [SerializeField] private CinemachineVirtualCamera vCamDezoom;

    [SerializeField] private LayerMask mainCameraCullingMask;


    #endregion

    #region Events

    public event Action onDezoomActive;
    public event Action onDezoomDeactive;

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        if (GameManager.Instance.player != null && GameManager.Instance.player.PreviousSceneName == null)
        {
            if (currentScene == SessionController.Instance.Game.StartLevelScene)
            {
                GameManager.Instance.player.PreviousSceneName = SessionController.Instance.Game.CharacterCustomerScene;
            }
            else if(currentScene.isGameScene && currentScene.sceneOutside)
            {
                GameManager.Instance.player.MainSceneLoadPos = GameManager.Instance.InitExteriorMapPlayerSpawnPos;
            }
        }

        GameManager.Instance.zoomActive = false;

        if(currentScene.isGameScene)
        {
            Debug.Log("Init Camera Properties");
            Camera.main.cullingMask = mainCameraCullingMask;
        }
        

        //GameManager.Instance.gameCanvasManager.CutoutMask.ResetMask();
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioController.Instance.PlayAudio(mainMusic);

        if(vCamDezoom != null)
            vCamDezoom.gameObject.SetActive(false);

        GameManager.Instance.gameCanvasManager.inventory.ScooterButton.SetLockedState(!currentScene.sceneOutside);
        GameManager.Instance.gameCanvasManager.inventory.DezoomButton.SetLockedState(!currentScene.sceneOutside);

        if(currentScene.sceneLocation != null)
        {
            GameManager.Instance.locationsList.CompleteLocationExploration(currentScene.sceneLocation);
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Dezoom") && GameManager.Instance.zoomActive == false)
        {
            Zoom(dezoomTouchActive);
        }
    }

    [ContextMenu("OpenMinigame")]
    public void OpenMinigame()
    {
        if(MiniGame == null)
        {
            return;
        }

        OldLastMovePlayer = GameManager.Instance.player.GetComponent<PlayerMovement>().GetLastMovePlayer();

        if(GameManager.Instance.player.GetComponent<PlayerConversant>().CurrentConversant != null)
        {
            MiniGame.GetComponent<InstantiationPrefabGame>().npcScript = GameManager.Instance.player.GetComponent<PlayerConversant>().CurrentConversant;
        }
        
        MiniGame.SetActive(true);
    }

    public bool CheckRythmeGameRank(RythmeGameRank rythmeGameRank)
    {
        if(currentRythmeGameRank == rythmeGameRank)
        {
            return true;
        }

        return false;
    }

    public void Zoom(bool value)
    {
        if(currentScene.isGameScene && currentScene.sceneOutside)
        {
            VCamDezoom.gameObject.SetActive(!value);
            dezoomTouchActive = !value;

            if(value)
            {
                onDezoomDeactive.Invoke();
            }
            else
            {
                onDezoomActive.Invoke();
            }
        }
    }

    #endregion
}


