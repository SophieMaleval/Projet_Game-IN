﻿using AllosiusDev;
using AllosiusDev.Audio;
using Core.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : Singleton<GameCore>
{
    #region Properties

    public SceneData CurrentScene => currentScene;

    public AudioData MainMusic => mainMusic;

    #endregion

    #region UnityInspector

    [SerializeField] private SceneData currentScene;

    [SerializeField] private AudioData mainMusic;

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

    #endregion
}