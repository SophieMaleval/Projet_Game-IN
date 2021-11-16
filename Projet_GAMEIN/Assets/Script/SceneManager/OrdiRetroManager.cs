using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using DG.Tweening;

public class OrdiRetroManager : MonoBehaviour
{
    [SerializeField] private GameObject Player ;
    [SerializeField] private Image Fade ;

    private bool PlayerQuitOrdiRetro ;

    QuestSystem qs;
    
    void Start()
    {
        Player = GameObject.Find("Player");
        qs = Player.GetComponent<QuestSystem>();
        qs.findtestQuest();
        qs.findLesGens();
        /*if(qs.stepCount >= 17)
        {
            qs.findLesGens();
        }*/
        SetPositionOnLoadScene();
        StartCoroutine(FadeAnimtion(false));
    }

    void SetPositionOnLoadScene()
    {
        Player.transform.position = new Vector3(5.5f,-5f, 0) ;
    }


    void Update()
    {
        if(PlayerQuitOrdiRetro == false && Player.transform.position.y < -5.75f)
        {
            PlayerQuitOrdiRetro = true ;
            StartCoroutine(FadeAnimtion(true));
        }
    }

    IEnumerator FadeAnimtion(bool QuitScene)
    {
        if(QuitScene == false)
        {
            yield return new WaitForSeconds(1f);            
            Fade.DOFade(0, 1f) ;
        }

        if(QuitScene == true)
        {
            Fade.DOFade(1, 1f) ;
            yield return new WaitForSeconds(1.5f);
            Player.GetComponent<PlayerProvenance>().SetAllBoolToFalse() ;             Player.GetComponent<PlayerProvenance>().ProviensOrdiRetro = true ; 
            SceneManager.LoadScene("Main") ;
        }
    }
}
