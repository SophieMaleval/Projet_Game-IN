using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using DG.Tweening;

public class NaconManager : MonoBehaviour
{
    [SerializeField] private GameObject Player ;
    [SerializeField] private Image Fade ;

    [SerializeField] private GameObject EtageHaut ;
    [SerializeField] private GameObject EtageBas ;

    private bool PlayerQuitNacon ;
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
            Player.transform.position = new Vector3(14.75f,-10f, 0) ;
            EtageHaut.SetActive(false);
            EtageBas.SetActive(true);     
    }


    void Update()
    {
        if(PlayerQuitNacon == false && Player.transform.position.y < -11.75f)
        {
            PlayerQuitNacon = true ;
            StartCoroutine(FadeAnimtion(true));
        }

        if(Player.transform.position.y > 0f && (Player.transform.position.x > 9.5f && Player.transform.position.x < 12.75f))
        {
            EtageHaut.SetActive(true);
            EtageBas.SetActive(false);            

        }
        if(Player.transform.position.y < 0f && (Player.transform.position.x > 9.5f && Player.transform.position.x < 12.75f))
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
            Player.GetComponent<PlayerProvenance>().SetAllBoolToFalse() ;             Player.GetComponent<PlayerProvenance>().ProviensNacon = true ; 
            SceneManager.LoadScene("Main") ;
        }
    }
}
