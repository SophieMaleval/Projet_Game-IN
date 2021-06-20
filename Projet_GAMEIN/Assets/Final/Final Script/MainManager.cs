using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using DG.Tweening;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject Player ;
    [SerializeField] private Image Fade ;

    private bool PlayerQuitGameIn ;


    
    void Start()
    {
        Player = GameObject.Find("Player");
        SetPositionOnLoadScene();
        StartCoroutine(FadeAnimtion(false, ""));
    }



    void SetPositionOnLoadScene()
    {
        if(Player.GetComponent<PlayerProvenance>().ProviensGameIn == true)
            Player.transform.position = new Vector3(0, -14.5f, 0) ;        

        if(Player.GetComponent<PlayerProvenance>().ProviensEArtsup == true)
            Player.transform.position = new Vector3(-32, 83f, 0) ;  

        if(Player.GetComponent<PlayerProvenance>().ProviensAccidentalQueen == true)
            Player.transform.position = new Vector3(185, 177.5f, 0) ;        

        if(Player.GetComponent<PlayerProvenance>().ProviensCouchGameCrafter == true)
            Player.transform.position = new Vector3(38.5f, 261.5f, 0) ;        

        if(Player.GetComponent<PlayerProvenance>().ProviensBPF == true)
            Player.transform.position = new Vector3(-156.5f, 282f, 0) ;  

        if(Player.GetComponent<PlayerProvenance>().ProviensNacon == true)
            Player.transform.position = new Vector3(-2.75f, 132.5f, 0) ;        

        if(Player.GetComponent<PlayerProvenance>().ProviensOrdiRetro == true)
            Player.transform.position = new Vector3(7.25f, 109f, 0) ;        
    }

    private void Update() 
    {
        if(PlayerQuitGameIn == false)
        {
            PlayerQuitGameIn = true ;
            //StartCoroutine(FadeAnimtion(true, "Main"));
        }
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
