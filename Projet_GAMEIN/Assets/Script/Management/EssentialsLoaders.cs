using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EssentialsLoaders : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private AllosiusDev.Audio.AudioController audioController;
    [SerializeField] private GameManager gameManager;

    #endregion

    #region Behaviour

    private void Awake()
    {
        if (AllosiusDev.Audio.AudioController.Instance == null)
        {
            Instantiate(audioController);
        }

        if(GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }
    }

    #endregion
}
