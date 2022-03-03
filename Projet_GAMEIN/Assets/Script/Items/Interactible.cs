using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Quantité
{
    Single,
    Multiple
}
public class Interactible : MonoBehaviour
{
    public Quantité qté;
    public GameObject DisplayerInventory;
    public InteractibleObject Object ;
    public QuestSys questSys;
    //public ActiveAsProg AaP;
    private SpriteRenderer SpriteRend;
    [SerializeField] private PlayerScript PlayerScript;
    private bool PlayerAround = false;
    private bool gathered = false;
    //public int code;
    //public int stepCode;

    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; }
        SpriteRend = GetComponent<SpriteRenderer>();
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>() ;
        //AaP = this.gameObject.GetComponent<ActiveAsProg>();
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
        if(qté == Quantité.Single)
        {
            PlayerScript.AjoutInventaire(Object);
            PlayerScript.SwitchInputSprite();       
            questSys.Progression();
            //AaP.StrikeThrough();
            Destroy(this.gameObject, 0.05f);    
        }
        else if(qté == Quantité.Multiple)
        {
            if (!PlayerScript.ItemChecker(Object))
            {
                PlayerScript.AjoutInventaire(Object);
         
                PlayerScript.SwitchInputSprite();
                Destroy(this.gameObject, 0.05f);
            }
            else
            {
                PlayerScript.SwitchInputSprite();
                Object.AddEntry();
                Destroy(this.gameObject, 0.05f);               
            }
            
        }
                  
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
