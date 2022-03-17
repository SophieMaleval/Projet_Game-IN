using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [Header ("Lever Gestion")]
    [SerializeField] private List<SpriteRenderer> LeverRender = new List<SpriteRenderer>();
    private bool LeverIsActivate = false ;
    [SerializeField] private Sprite LeverDisable ;
    [SerializeField] private Sprite LeverDisableHighlight ;
    [SerializeField] private Sprite LeverEnable ;
    [SerializeField] private Sprite LeverEnableHighlight ;
    [Space]
    [SerializeField] private List<WorldObstacle> ObstacleInitiallyDisable = new List<WorldObstacle>() ;
    [SerializeField] private List<WorldObstacle> ObstacleInitiallyEnable = new List<WorldObstacle>() ;


    private PlayerScript Player ;

    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    Player = GameObject.Find("Player").GetComponent<PlayerScript>() ; }
    }

    void ActivationLever()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            
            Player.SwitchInputSprite();
        }      
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {

            Player.SwitchInputSprite();
        }
    }

    void ShowHighlightLever(bool StateHighlight)
    {
        for (int Sp = 0; Sp < LeverRender.Count; Sp++)
        {
            if(!LeverIsActivate)
            {
                
            }
        }
    }
}
