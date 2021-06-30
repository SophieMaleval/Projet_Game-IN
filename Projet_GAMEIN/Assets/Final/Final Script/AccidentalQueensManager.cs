using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using DG.Tweening;

public class AccidentalQueensManager : MonoBehaviour
{
    [SerializeField] private GameObject Player ;
    [SerializeField] private Image Fade ;

    [SerializeField] private GameObject EtageHaut ;
    [SerializeField] private GameObject EtageBas ;

    private bool PlayerQuitAccidental ;
    private bool PlayerIsInTop ;

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
            Player.transform.position = new Vector3(7.5f,-10.5f, 0) ;
            EtageHaut.SetActive(false);
            EtageBas.SetActive(true);     
    }


    void Update()
    {
        if(PlayerQuitAccidental == false && Player.transform.position.y < -12.75f)
        {
            PlayerQuitAccidental = true ;
            StartCoroutine(FadeAnimtion(true));
        }

        if(Player.transform.position.y > 1.5f && (Player.transform.position.x > -2f && Player.transform.position.x < 1.25f))
        {
            EtageHaut.SetActive(true);
            EtageBas.SetActive(false);            

        }
        if(Player.transform.position.y < 1.5f && (Player.transform.position.x > -2f && Player.transform.position.x < 1.25f))
        {
            EtageHaut.SetActive(false);
            EtageBas.SetActive(true);
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
            Player.GetComponent<PlayerProvenance>().SetAllBoolToFalse() ;             Player.GetComponent<PlayerProvenance>().ProviensAccidentalQueen = true ; 
            SceneManager.LoadScene("Main") ;
        }
    }
}
