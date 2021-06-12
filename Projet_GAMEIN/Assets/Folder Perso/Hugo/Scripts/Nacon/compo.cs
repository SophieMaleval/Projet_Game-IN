using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compo : MonoBehaviour
{
    public bool CompoPourLaSoiree = false;
    public bool TakeCompo = false;

    void OnTriggerEnter2D(Collider2D other) 
    {
        // Debug.Log("A");
        if(other.tag == "Player")
        {
            Debug.Log("Je peux prendre les compo");
            TakeCompo = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            Debug.Log("Je ne peux plus prendre les compo");
            TakeCompo = false;
        }
    }
    void Update() 
    {
        if(TakeCompo == true && Input.GetKeyDown(KeyCode.A))
        {
            CompoPourLaSoiree = true;
            Debug.Log("J'ai pris les compo");
        }
    }
}
