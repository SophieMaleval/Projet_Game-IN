using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    inventoryController Ic;

    // Start is called before the first frame update

    private void Start() {
        Ic = GameObject.Find("InventoryCOntroler").GetComponent<inventoryController>();
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "Player"){

            Ic.collectedObject = this.gameObject;
            this.gameObject.SetActive(false);
        }
        
    }
}
