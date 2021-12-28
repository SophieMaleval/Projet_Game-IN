using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public InteractibleObject Object ;
    public QuestSys questSys;
    private SpriteRenderer SpriteRend;
    [SerializeField] private PlayerScript PlayerScript;
    private bool PlayerAround = false;
    //public int code;
    //public int stepCode;

    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; }
        SpriteRend = GetComponent<SpriteRenderer>();
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>() ;
    }

    private void Start()
    {
        PlayerCanCollectThisObject(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerCanCollectThisObject(true);
            PlayerScript.SwitchInputSprite();
        }      
    }

    private void Update()
    {
        if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
        if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);


        if (PlayerAround)
        {
            if(PlayerScript.CanCollectObject && PlayerScript.PlayerAsInterract)
            {
                PlayerScript.PlayerAsInterract = false ;
                Collected();
            }
            else 
            {              
                PlayerScript.PlayerAsInterract = false ;
            } 
        }
    }


    void Collected()
    {
        PlayerScript.AjoutInventaire(Object);
        PlayerScript.SwitchInputSprite();       
        questSys.Progression();        
        Destroy(this.gameObject, 0.025f);              
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {
            PlayerCanCollectThisObject(false);
            PlayerScript.SwitchInputSprite();
        }
    }

    public void PlayerCanCollectThisObject(bool Can)
    {
        if(!Can)
        {
            PlayerAround = false ;
            SpriteRend.sprite = Object.NormalSprite;             
        } 
        else 
        {
            PlayerAround = true ;
            SpriteRend.sprite = Object.HighlightSprite;            
        }      
    }
}
