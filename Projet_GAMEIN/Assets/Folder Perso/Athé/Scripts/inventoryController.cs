using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryController : MonoBehaviour
{
    public List <GameObject> Slots;
    public List <GameObject> Items;

    public bool isActive;
    public GameObject collectedObject;
    public Sprite collectedSprite;
    public GameObject Inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.I)){
            if(isActive == true){

                isActive = false;
            } else{

                isActive = true; 

            }
            Inventory.SetActive(isActive);
        }

        if(collectedObject != null){

             Items.Add(collectedObject);

            collectedSprite = collectedObject.GetComponent<SpriteRenderer>().sprite;

            for (int i = 0; i < 9; i++)
            {

                if (Slots[i].transform.GetChild (0).GetComponent<Image>().sprite == null){

                    Slots[i].transform.GetChild (0).GetComponent<Image>().sprite = collectedSprite;
                    i = 8;

                }

            }
            
            collectedObject = null;
        } 

    }
}
