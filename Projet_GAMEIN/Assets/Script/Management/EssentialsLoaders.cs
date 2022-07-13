using AllosiusDev.Audio;
using Core;
using Core.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EssentialsLoaders : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private AudioController audioController;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private SessionController sessionController;

    [SerializeField] private BaseGameController gameController;

    #endregion

    #region Behaviour

    private void Awake()
    {
        if (AudioController.Instance == null)
        {
            Instantiate(audioController);
        }

        if(GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }

        if(SceneLoader.Instance == null)
        {
            Instantiate(sceneLoader);
        }

        if(SessionController.Instance == null)
        {
            Instantiate(sessionController);
        }

        BaseGameController baseGameController = FindObjectOfType<BaseGameController>();
        if(baseGameController == null)
        {
            Instantiate(gameController);
        }
    }

    #endregion
}
