using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public GameObject PopUp;

    void OnTriggerStay2D(Collider2D other)
    {
            if(other.name == "Bomb")
            {
                PopUp.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    other.GetComponent<Explosion>().Boom();
                }
            }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.name == "Bomb")
            {
                Debug.Log("ERE");
                PopUp.SetActive(false);
            }
    }
}
