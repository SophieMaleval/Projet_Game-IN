using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(InteractableElement))]
public class MiniGameLauncher : MonoBehaviour
{
    #region Fields


    private SpriteRenderer SpriteRend;

    private bool PlayerAround = false;

    //private bool gathered = false;

    #endregion

    #region UnityInspector

    //public InteractibleObject Object;
    //public QuestSys questSys;
    //public ActiveAsProg AaP;

    [SerializeField] private PlayerScript PlayerScript;
    public GameObject gamePrefab;

    //public GameObject roomCam, minigameCam;

    public int etapeDeQuete, numeroDeQuete;
    //public int stepCode;

    [SerializeField] private InteractableElement interactableElement;

    #endregion

    #region Behaviour

    private void Awake()
    {
        if (GameManager.Instance.player != null)   // Récupère le player au lancement de la scène
        { PlayerScript = GameManager.Instance.player; }
        SpriteRend = GetComponent<SpriteRenderer>();
        //questSys = GameManager.Instance.gameCanvasManager.questManager;
        //gamePrefab = GameObject.FindGameObjectWithTag("MiniGame");
    }

    private void Start()
    {
        CanStartThisMiniGame(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            CanStartThisMiniGame(true);
            PlayerScript.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
        }
    }

    private void Update()
    {
        //if (PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
        //if (PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);
        /*if (numeroDeQuete == questSys.niveau && etapeDeQuete == questSys.etape)
        {
            ZoneInteractible();
        }
        else
        {
            ZoneNonInteractible();
        }*/


        if (PlayerAround)
        {
            if (PlayerScript.CanCollectObject && PlayerScript.PlayerAsInterract)
            {
                PlayerScript.PlayerAsInterract = false;
                StartMiniGame();
            }
            else
            {
                PlayerScript.PlayerAsInterract = false;
            }
        }
    }

    void ZoneInteractible()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = true; //active collider, ajouter autres effets visuels ici si besoin
        SpriteRend.enabled = true;
    }
    
    void ZoneNonInteractible()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        SpriteRend.enabled = false;
    }

    void OnDestroy()
    {
        //questSys.Progression();
    }

    void StartMiniGame()
    {
        gamePrefab.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            CanStartThisMiniGame(false);
            PlayerScript.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
        }
    }

    public void CanStartThisMiniGame(bool Can)
    {
        if (!Can)
        {
            PlayerAround = false;
        }
        else
        {
            PlayerAround = true;
            //SpriteRend.sprite = Object.HighlightSprite;
        }
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + interactableElement.interactableSpritePosOffset, interactableElement.collisionRadius);
    }

    #endregion
}
