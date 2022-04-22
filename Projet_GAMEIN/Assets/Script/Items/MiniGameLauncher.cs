using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameLauncher : MonoBehaviour
{
    //public InteractibleObject Object;
    public QuestSys questSys;
    //public ActiveAsProg AaP;
    private SpriteRenderer SpriteRend;
    [SerializeField] private PlayerScript PlayerScript;
    public GameObject gamePrefab;
    private bool PlayerAround = false;
    //public GameObject roomCam, minigameCam;
    //private bool gathered = false;
    public int etapeDeQuete, numeroDeQuete;
    //public int stepCode;

    private void Awake()
    {
        if (GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        { PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>(); }
        SpriteRend = GetComponent<SpriteRenderer>();
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        //gamePrefab = GameObject.FindGameObjectWithTag("MiniGame");
    }

    private void Start()
    {
        //CanStartThisMiniGame(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            //CanStartThisMiniGame(true);
            PlayerScript.SwitchInputSprite();
        }
    }

    private void Update()
    {
        if (PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
        if (PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);
        if (numeroDeQuete == questSys.niveau && etapeDeQuete == questSys.etape)
        {
            ZoneInteractible();
        }
        else
        {
            ZoneNonInteractible();
        }


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
        this.gameObject.GetComponent<Collider>().enabled = true; //active collider, ajouter autres effets visuels ici si besoin
    }
    
    void ZoneNonInteractible()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    void StartMiniGame()
    {
        gamePrefab.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            //CanStartThisMiniGame(false);
            PlayerScript.SwitchInputSprite();
        }
    }

    public void CanStartThisMiniGame(bool Can)
    {
        if (!Can)
        {
            PlayerAround = false;
            //SpriteRend.sprite = Object.NormalSprite;
        }
        else
        {
            PlayerAround = true;
            //SpriteRend.sprite = Object.HighlightSprite;
        }
    }
}
