using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using DG.Tweening;

public class GameInManager : MonoBehaviour
{
    [SerializeField] private GameObject Player ;
    [SerializeField] private Image Fade ;

    [SerializeField] private GameObject EtageHaut ;
    [SerializeField] private GameObject EtageBas ;

    [SerializeField] private GameObject CustomButton ;
    private bool PlayerQuitGameIn ;
    private bool PlayerIsInTop ;


    
    void Start()
    {
        Player = GameObject.Find("Player");
        SetPositionOnLoadScene();
        StartCoroutine(FadeAnimtion(false, ""));
    }

    void SetPositionOnLoadScene()
    {
        if(Player.GetComponent<PlayerProvenance>().ProviensCharacterCustomer == true)
        {
            Player.transform.position = new Vector3(-10,10f, 0) ;
            EtageHaut.SetActive(true);
            EtageBas.SetActive(false);
        }        

        if(Player.GetComponent<PlayerProvenance>().ProviensMain == true)
        {
            Player.transform.position = new Vector3(-22,-12.5f, 0) ;
            EtageHaut.SetActive(false);
            EtageBas.SetActive(true);
        }        
    }


    void Update()
    {
        if(Player.transform.position.x < -4.5f)
        {
            CustomButton.SetActive(true);
        }

        if(Player.transform.position.x > -4.5f)
        {
            CustomButton.SetActive(false);
        }

        if(PlayerQuitGameIn == false && Player.transform.position.y < -13.5f)
        {
            PlayerQuitGameIn = true ;
            StartCoroutine(FadeAnimtion(true, "Main"));
        }

        if(Player.transform.position.y > 10f && (Player.transform.position.x > 20f && Player.transform.position.x < 25f))
        {
            EtageHaut.SetActive(true);
            EtageBas.SetActive(false);            

        }
        if(Player.transform.position.y < 10f && (Player.transform.position.x > 20f && Player.transform.position.x < 25f))
        {
            EtageHaut.SetActive(false);
            EtageBas.SetActive(true);
        }

    }


    public void GoToCustomisation()
    {
        //Player.GetComponent<GridDeplacement>().enabled = false ;
        StartCoroutine(FadeAnimtion(true, "Character Customer"));
    }

    IEnumerator FadeAnimtion(bool QuitScene, string NextScene)
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
            Player.GetComponent<PlayerProvenance>().SetAllBoolToFalse() ;             Player.GetComponent<PlayerProvenance>().ProviensGameIn = true ; 
            SceneManager.LoadScene(NextScene) ;
        }
    }
}
