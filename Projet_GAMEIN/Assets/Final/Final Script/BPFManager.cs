using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using DG.Tweening;

public class BPFManager : MonoBehaviour
{
    [SerializeField] private GameObject Player ;
    [SerializeField] private Image Fade ;

    private bool PlayerQuitBPF ;


    
    void Start()
    {
        Player = GameObject.Find("Player");
        SetPositionOnLoadScene();
        StartCoroutine(FadeAnimtion(false));
    }

    void SetPositionOnLoadScene()
    {
        Player.transform.position = new Vector3(12.5f,-9.5f, 0) ;
    }


    void Update()
    {
        if(PlayerQuitBPF == false && Player.transform.position.y < -11.5f)
        {
            PlayerQuitBPF = true ;
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
            Player.GetComponent<PlayerProvenance>().SetAllBoolToFalse() ;             Player.GetComponent<PlayerProvenance>().ProviensBPF = true ; 
            SceneManager.LoadScene("Main") ;
        }
    }
}
