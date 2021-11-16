using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using DG.Tweening;

public class CouchGameCrafterManager : MonoBehaviour
{
    [SerializeField] private GameObject Player ;
    [SerializeField] private Image Fade ;

    private bool PlayerQuitCGC ;

    [SerializeField] private GameObject EtageHaut ;
    [SerializeField] private GameObject EtageBas ;

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
        Player.transform.position = new Vector3(2.5f,-15f, 0) ;
    }


    void Update()
    {
        if(PlayerQuitCGC == false && Player.transform.position.y < -16.75f)
        {
            PlayerQuitCGC = true ;
            StartCoroutine(FadeAnimtion(true));
        }


        if(Player.transform.position.y > -2f && (Player.transform.position.x > 7.25f && Player.transform.position.x < 10.15f))
        {
            EtageHaut.SetActive(true);
            EtageBas.SetActive(false);            

        }
        if(Player.transform.position.y < -2f && (Player.transform.position.x > 7.25f && Player.transform.position.x < 10.15f))
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
            Player.GetComponent<PlayerProvenance>().SetAllBoolToFalse() ;             Player.GetComponent<PlayerProvenance>().ProviensCouchGameCrafter = true ; 
            SceneManager.LoadScene("Main") ;
        }
    }
}
