using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public Sprite RegularSprite; //sprite normal
    public Sprite HighlightSprite; //sprite si sélectionnable
    private SpriteRenderer render;
    [SerializeField] private PlayerScript PlayerScript;

    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; }
    }

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.sprite = RegularSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            SwitchSprite(true);

            PlayerScript.SwitchInputSprite();
        }      
    }

    private void Update()
    {
        if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
        if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);




        if (PlayerScript.PlayerAsInterract == true)
        {
            PlayerScript.PlayerAsInterract = false ;
            Collected();
        }
    }


    void Collected()
    {
        PlayerScript.SwitchInputSprite();
        Destroy(this.gameObject, 0.025f);              
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {
            SwitchSprite(false);
            PlayerScript.SwitchInputSprite();
        }
    }

    public void SwitchSprite(bool Hihglight)
    {
        if(!Hihglight)
            render.sprite = RegularSprite; 
        else        
            render.sprite = HighlightSprite;
    }
}
