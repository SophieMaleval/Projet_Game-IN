﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AllosiusDev;

public class SceneLoader : Singleton<SceneLoader>
{
    #region Properties

    public event System.Action OnSceneChanged;

    #endregion

    #region Behaviour

    public void ChangeScene(SceneData sceneData)
    {
        OnSceneChanged?.Invoke();

        AllosiusDev.Audio.AudioController.Instance.StopAllAmbients();
        AllosiusDev.Audio.AudioController.Instance.StopAllMusics();

        SceneManager.LoadScene((int)(object)sceneData.sceneToLoad);
    }

    #endregion
}
