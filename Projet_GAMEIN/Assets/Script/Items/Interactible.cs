using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public Sprite regularItem; //sprite normal
    public Sprite selectableItem; //sprite si sélectionnable
    private SpriteRenderer render;
    public PlayerScript playerScript; //se trouve automatiquement dans le start, mis en public pour vérif dans éditeur

    private void Awake() {
        if(GameObject.Find("Player") != null)
        {
            playerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ;
        }
    }
    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        render.sprite = regularItem;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            render.sprite = selectableItem;
            playerScript.canInteract = true;
        }      
    }

    private void Update()
    {
        if (playerScript.didFunction == true && playerScript.canInteract)
        {
            BeenCollected();
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == ("Player") && playerScript.didFunction)
        {
            BeenCollected();
        }
    }*/

    void BeenCollected()
    {
        //playerScript.didFunction = false;
        Debug.Log("Destroyed !!!!");
        Destroy(this.gameObject);              
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            render.sprite = regularItem;
            playerScript.canInteract = false;
            //playerScript.didFunction = false;
        }
    }
}
