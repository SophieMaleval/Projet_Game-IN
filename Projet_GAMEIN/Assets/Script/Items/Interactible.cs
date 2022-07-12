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
    #region Fields

    private SpriteRenderer SpriteRend;

    private bool PlayerAround = false;
    //private bool gathered = false;

    #endregion

    #region UnityInspector

    public Quantité qté;
    public GameObject DisplayerInventory;
    public InteractibleObject Object ;
    public QuestSys questSys;
    //public ActiveAsProg AaP;

    [SerializeField] private PlayerScript PlayerScript;

    //public int code;
    //public int stepCode;

    #endregion

    #region Behaviour

    private void Awake() {
        if(GameManager.Instance.player != null)   // Récupère le player au lancement de la scène
        {    PlayerScript = GameManager.Instance.player ; }
        SpriteRend = GetComponent<SpriteRenderer>();
        questSys = GameManager.Instance.gameCanvasManager.questManager ;
        //AaP = this.gameObject.GetComponent<ActiveAsProg>();
    }

    private void Start()
    {
        PlayerCanCollectThisObject(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript != null)
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
                if(GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(Object, true);
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
            if (!PlayerScript.ItemChecker(Object))   //Si l'item n'existe pas dans l'inventaire
            {
                PlayerScript.AjoutInventaire(Object);
                Object.AddEntry();
                PlayerScript.SwitchInputSprite();
                Destroy(this.gameObject, 0.05f);
            }
            else                                    //Si l'item existe déjà
            {
                if(Object.unité < Object.valeurMax)
                {
                    PlayerScript.SwitchInputSprite();
                    Object.AddEntry();
                    Debug.Log("encore un effort " + "U: " + Object.unité + "VM:" + Object.valeurMax);
                    Destroy(this.gameObject, 0.05f);
                }
                else if (Object.unité == Object.valeurMax)        //mdr ça marche mais ça atteint d'abord la valeur max, puis ça progresse
                {
                    PlayerScript.SwitchInputSprite();
                    Object.AddEntry();
                    questSys.Progression();
                    Debug.Log("trop cool t'as tout");
                    Destroy(this.gameObject, 0.05f);      
                }         
            }
            
        }
                  
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript != null)
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

    #endregion
}
