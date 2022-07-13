using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;
using DG.Tweening;
using Core;
using Core.Session;

public class LoadingScreenScript : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private Image Fade ;
    [SerializeField] private float TimeWait = 5f ;

    #endregion

    #region Behaviour

    void Start()
    {
        StartCoroutine(FadeAnimtion(false));
        StartCoroutine(WaitAndLoadMenu(TimeWait));
    }

    IEnumerator WaitAndLoadMenu(float TimeToWait)
    {
        yield return new WaitForSeconds(TimeToWait - 1.25f);
        StartCoroutine(FadeAnimtion(true));
        yield return new WaitForSeconds(1f);
        SceneLoader.Instance.ChangeScene(SessionController.Instance.Game.MenuScene);
        //SceneManager.LoadScene("Menu");
    }

    IEnumerator FadeAnimtion(bool GameAsLunch)
    {
        if(GameAsLunch == false)
        {
            yield return new WaitForSeconds(1f);
            Fade.DOFade(0, 1f) ;
            yield return new WaitForSeconds(0.5f);
            Fade.raycastTarget = false ;
        }

        if(GameAsLunch == true)
        {
            Fade.raycastTarget = true ;
            Fade.DOFade(1, 1f) ;
            yield return new WaitForSeconds(2f);
            SceneLoader.Instance.ChangeScene(SessionController.Instance.Game.CharacterCustomerScene);
        }
    }

    #endregion
}
