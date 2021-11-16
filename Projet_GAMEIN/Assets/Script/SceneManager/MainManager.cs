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

    private bool ZoomMax = true ;

    QuestSystem qs;
    Radio radio;

    
    void Start()
    {
        Player = GameObject.Find("Player");
        qs = Player.GetComponent<QuestSystem>();
        radio = Player.GetComponent<Radio>();
        radio.GetTutoRadio();
        qs.findtestQuest();
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
        // Zoom
        /* Zoom Game In */
        if((Player.transform.position.x > -35f && Player.transform.position.x < 58f) && (Player.transform.position.y > -33.5f && Player.transform.position.y < 25.5f) && ZoomMax == true)
        {
            ZoomMax = false ;
            Zooming(false);
        }
        /* Zoom Ville */
        if((Player.transform.position.x > -65f && Player.transform.position.x < 47.5f) && (Player.transform.position.y > 67.5f && Player.transform.position.y < 170f) && ZoomMax == true)
        {
            ZoomMax = false ;
            Zooming(false);
        }
        /* Zoom AQ */
        if((Player.transform.position.x > 147.5f && Player.transform.position.x < 210f) && (Player.transform.position.y > 140f && Player.transform.position.y < 197.5f) && ZoomMax == true)
        {
            ZoomMax = false ;
            Zooming(false);
        }
        /* Zoom CGC */
        if((Player.transform.position.x > -20.5f && Player.transform.position.x < 51f) && (Player.transform.position.y > 250f && Player.transform.position.y < 280f) && ZoomMax == true)
        {
            ZoomMax = false ;
            Zooming(false);
        }
        /* Zoom BPF */
        if((Player.transform.position.x > -200f && Player.transform.position.x < -137.75f) && (Player.transform.position.y > 307.5f && Player.transform.position.y < 272.5f) && ZoomMax == true)
        {
            ZoomMax = false ;
            Zooming(false);
        } 
        
        else {
            ZoomMax = true ;
            Zooming(true);
        }



    }





    public void GoToNewScene(string NameScene)
    {
        StartCoroutine(FadeAnimtion(true, NameScene));
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


    void Zooming(bool Zoom)
    {
        float ZoomMax = 7.5f ;
        float ZoomMin = 15f ;

        if(Zoom == true)
        {
            DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, ZoomMax, 2.5f);
        }

        if(Zoom == false)
        {
            DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, ZoomMin, 2.5f);
        }
    }
}
