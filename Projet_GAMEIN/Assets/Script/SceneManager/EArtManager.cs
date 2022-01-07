using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using DG.Tweening;

public class EArtManager : MonoBehaviour
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
        SetPositionOnLoadScene();
        
        StartCoroutine(FadeAnimtion(false));
    }

    void SetPositionOnLoadScene()
    {
        Player.transform.position = new Vector3(17.5f,-11.5f, 0) ;
    }


    void Update()
    {
        if(PlayerQuitOrdiRetro == false && Player.transform.position.y < -12.5f)
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
            Player.GetComponent<PlayerProvenance>().SetAllBoolToFalse() ;       //      Player.GetComponent<PlayerProvenance>().ProviensEArtsup = true ; 
            SceneManager.LoadScene("Main") ;
        }
    }
}
