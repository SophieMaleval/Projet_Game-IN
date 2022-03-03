using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro ;
using DG.Tweening;

public class InventoryScript : MonoBehaviour
{
    public PlayerScript PlayerScript ;
    public GameObject InventoryPanel ;
    public GameObject DialogueCanvas ;
    public GameObject PannelENTCanvas ;

    public GameObject DisplayerInventory ;
    public List<Image> InventoryDisplayer ;


    [SerializeField] private RectTransform SettingPanel ;
    [SerializeField] private RectTransform ControlsPanel;

    private void Awake() 
    {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    
            PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; 
        }
        SetInventoryCount();

    }
    private void OnEnable() 
    {
        //DialogueCanvas = GameObject.Find("Dialogue Canvas") ;
        //PannelENTCanvas = GameObject.Find("Pannel ENT");        
        //Debug.Log(transform.GetSiblingIndex());

    }
    private void OnDisable() {

    }

    public void SwitchToggleInventoryDisplay()
    {
        SetDisplayInventory();        
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        if(!InventoryPanel.activeSelf)
        {
            transform.SetSiblingIndex(0);

            if(DialogueCanvas.activeSelf == true)    GameObject.Find("Player Backpack").GetComponent<PlayerDialogue>().ResumeDialogue();                   

            if(DialogueCanvas.activeSelf == false && PannelENTCanvas.activeSelf == false )                PlayerScript.GetComponent<PlayerMovement>().EndActivity() ;   


        } else {
            transform.SetSiblingIndex(2);            

            PlayerScript.GetComponent<PlayerMovement>().StartActivity() ;
            GameObject.Find("Player Backpack").GetComponent<PlayerDialogue>().PausedInDialogue();
        }
    }

    public void OpenSetting()
    {
        StartCoroutine(AnimationPanels(true, SettingPanel));
    }
    public void CloseSetting()
    {
        StartCoroutine(AnimationPanels(false, SettingPanel));
    }

    public void OpenControls()
    {
        StartCoroutine(AnimationPanels(true, ControlsPanel));
    }
    public void CloseControls()
    {
        StartCoroutine(AnimationPanels(false, ControlsPanel));
    }


    IEnumerator AnimationPanels(bool OpenSettings, RectTransform PanelAnimate)
    {
        if(OpenSettings)
        {
            PanelAnimate.DOAnchorPosY(1500, 0.01f);
            yield return new WaitForSeconds(0.01f);
            PanelAnimate.gameObject.SetActive(true);
            PanelAnimate.GetComponent<Image>().DOFade(0.75f, 1f);
            PanelAnimate.DOAnchorPosY(0, 1f);            
        } else {
            PanelAnimate.DOAnchorPosY(-50, 0.1f);
            yield return new WaitForSeconds(0.1f);
            PanelAnimate.GetComponent<Image>().DOFade(0f, 1f);            
            PanelAnimate.DOAnchorPosY(1500, 1f);
            yield return new WaitForSeconds(1f);

            PanelAnimate.gameObject.SetActive(false);
        }
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


    public void SetDisplayInventory()
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

                if (PlayerScript.Inventaire[IDO].multipleEntries)
                {
                    //Affiche l'image de fond du compteur
                    DisplayerInventory.transform.GetChild(IDO).Find("Compteur").GetComponent<Image>().enabled = true;

                    //Modifie valeurs du compteur
                    DisplayerInventory.transform.GetChild(IDO).Find("Compteur").GetComponentInChildren<TextMeshProUGUI>().text = PlayerScript.Inventaire[IDO].unité.ToString() + " / " + PlayerScript.Inventaire[IDO].valeurMax.ToString();
                    DisplayerInventory.transform.GetChild(IDO).Find("Compteur").GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                }
                else
                {
                    DisplayerInventory.transform.GetChild(IDO).Find("Compteur").GetComponent<Image>().enabled = false;
                    DisplayerInventory.transform.GetChild(IDO).Find("Compteur").GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                }

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
