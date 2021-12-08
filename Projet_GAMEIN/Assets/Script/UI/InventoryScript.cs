using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro ;

public class InventoryScript : MonoBehaviour
{
    public PlayerScript PlayerScript ;
    public GameObject InventoryPanel ;

    public GameObject DisplayerInventory ;
    public List<Image> InventoryDisplayer ;

    private void Awake() 
    {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; 
        
        }

        SetInventoryCount();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToggleInventoryDisplay()
    {
        SetDisplayinventory();        
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
    }

    public void SetInventoryCount()
    {
        InventoryDisplayer.Clear();
        for (int Id = 0; Id < DisplayerInventory.transform.childCount; Id++)
        {
            InventoryDisplayer.Add(null);            
        }        

        if(PlayerScript != null && PlayerScript.Inventaire.Length != InventoryDisplayer.Count)
        {
            PlayerScript.Inventaire = new InteractibleObject[DisplayerInventory.transform.childCount] ;
        }
    }


    public void SetDisplayinventory()
    {
        for (int IDO = 0; IDO < DisplayerInventory.transform.childCount; IDO++)
        {
            if(PlayerScript.Inventaire[IDO] != null)
            {
                // Met le bon sprite
                DisplayerInventory.transform.GetChild(IDO).Find("Object").GetComponent<Image>().sprite = PlayerScript.Inventaire[IDO].UISprite;
                DisplayerInventory.transform.GetChild(IDO).Find("Object").GetComponent<Image>().enabled = true;

                // Affiche le bon nom
                DisplayerInventory.transform.GetChild(IDO).Find("Box Name Object").gameObject.SetActive(true) ;
                DisplayerInventory.transform.GetChild(IDO).Find("Box Name Object").GetComponentInChildren<TextMeshProUGUI>().text = PlayerScript.Inventaire[IDO].Name;
            } else {
                // Reset toute les valeur par defaut
                // Sprite
                DisplayerInventory.transform.GetChild(IDO).Find("Object").GetComponent<Image>().sprite = null;
                DisplayerInventory.transform.GetChild(IDO).Find("Object").GetComponent<Image>().enabled = false;

                // Nom
                DisplayerInventory.transform.GetChild(IDO).Find("Box Name Object").GetComponentInChildren<TextMeshProUGUI>().text = "Object";                
                DisplayerInventory.transform.GetChild(IDO).Find("Box Name Object").gameObject.SetActive(false) ;
            } 
        }
    }

}
