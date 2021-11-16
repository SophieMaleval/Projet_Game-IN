using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using DG.Tweening ;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image Fade ;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeAnimtion(false));
    }

    // Update is called once per frame
    public void LunchGame()
    {
        StartCoroutine(FadeAnimtion(true));
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
            SceneManager.LoadScene("Character Customer") ;
        }
    }
}
