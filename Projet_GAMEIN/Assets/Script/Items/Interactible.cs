using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public Sprite regularItem;
    public Sprite selectableItem;
    // Start is called before the first frame update
    private SpriteRenderer render;
    public PlayerScript playerScript;

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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            render.sprite = regularItem;
            playerScript.canInteract = false;
        }
    }
}
