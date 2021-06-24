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

 

    public bool spriteIsActivated;
    
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

            for (int i = 0; i < 8; i++)
            {

                if (Slots[i].transform.GetChild (0).GetComponent<Image>().sprite == null){

                    Slots[i].transform.GetChild (0).GetComponent<Image>().sprite = collectedSprite;
                    Slots[i].transform.GetChild (1).gameObject.SetActive(true);
                    
                    i = 8;

                }
                  
            }
                    
        collectedObject = null;
        } 
        

    }

    public void PopUpActivate(int slot)
    {
        if(Slots[slot].transform.GetChild (0).GetComponent<Image>().sprite.name == "Poulet"){
            Slots[slot].transform.GetChild (2).gameObject.SetActive(true);
            Slots[slot].transform.GetChild (2).gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().text = "Ratchet.";
        }

        if(Slots[slot].transform.GetChild (0).GetComponent<Image>().sprite.name == "gateau"){
            Slots[slot].transform.GetChild (2).gameObject.SetActive(true);
            Slots[slot].transform.GetChild (2).gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().text = "super le gateau";
        }
         if(Slots[slot].transform.GetChild (0).GetComponent<Image>().sprite.name == "pomme"){
            Slots[slot].transform.GetChild (2).gameObject.SetActive(true);
            Slots[slot].transform.GetChild (2).gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().text = "c'est pas une poire, c'est une pomme.";
        }
         if(Slots[slot].transform.GetChild (0).GetComponent<Image>().sprite.name == "telephone"){
            Slots[slot].transform.GetChild (2).gameObject.SetActive(true);
            Slots[slot].transform.GetChild (2).gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().text = "Ah, ça doit appartenir à quelqu'un...";
        
        }
           if(Slots[slot].transform.GetChild (0).GetComponent<Image>().sprite.name == "radioaq"){
            Slots[slot].transform.GetChild (2).gameObject.SetActive(true);
            Slots[slot].transform.GetChild (2).gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().text = "elle n'a pas l'air fonctionelle...";
       }

        if(Slots[slot].transform.GetChild (0).GetComponent<Image>().sprite.name == "banderoles"){
            Slots[slot].transform.GetChild (2).gameObject.SetActive(true);
            Slots[slot].transform.GetChild (2).gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().text = "sisi ça tue";
       }
   }

    public void PopUpClear()
    {

        for (int i = 0; i < 8; i++)
        {

            Slots[i].transform.GetChild (2).gameObject.SetActive(false);
    
        }
    }

}

