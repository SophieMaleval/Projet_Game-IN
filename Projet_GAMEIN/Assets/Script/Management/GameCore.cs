﻿using AllosiusDev;
using AllosiusDev.Audio;
using AllosiusDev.DialogSystem;
using Core.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : Singleton<GameCore>
{
    #region Properties

    public SceneData CurrentScene => currentScene;

    public AudioData MainMusic => mainMusic;


    public Vector2 OldLastMovePlayer { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private SceneData currentScene;

    [SerializeField] private AudioData mainMusic;


    [SerializeField] private GameObject MiniGame;


    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        if (GameManager.Instance.player != null && GameManager.Instance.player.PreviousSceneName == null)
        {
            GameManager.Instance.player.PreviousSceneName = SessionController.Instance.Game.CharacterCustomerScene;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioController.Instance.PlayAudio(mainMusic);

        
        
    }

    public void OpenMinigame()
    {
        if(MiniGame == null)
        {
            return;
        }
        OldLastMovePlayer = GameManager.Instance.player.GetComponent<PlayerMovement>().GetLastMovePlayer();
        MiniGame.GetComponent<InstantiationPrefabGame>().npcScript = GameManager.Instance.player.GetComponent<PlayerConversant>().CurrentConversant;
        MiniGame.SetActive(true);
    }

    #endregion
}
