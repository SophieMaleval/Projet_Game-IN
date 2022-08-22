using System.Collections;
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

        ChangementSceneEffects();

        SceneManager.LoadScene((int)(object)sceneData.sceneToLoad);
    }

    public void LoadSceneAsync(SceneData sceneData)
    {
        OnSceneChanged?.Invoke();

        SceneManager.LoadSceneAsync((int)(object)sceneData.sceneToLoad, LoadSceneMode.Additive);
    }

    public void UnloadSceneAsync(SceneData sceneData)
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null)
            GameManager.Instance.player.ResetInterractInputSprite();

        SceneManager.UnloadSceneAsync((int)(object)sceneData.sceneToLoad);
    }

    private void ChangementSceneEffects()
    {
        AllosiusDev.Audio.AudioController.Instance.StopAllAmbients();
        AllosiusDev.Audio.AudioController.Instance.StopAllMusics();

        if (GameManager.Instance != null && GameManager.Instance.player != null)
            GameManager.Instance.player.ResetInterractInputSprite();

        if (GameCore.Instance != null)
            GameCore.ResetInstance();
    }

    #endregion
}
