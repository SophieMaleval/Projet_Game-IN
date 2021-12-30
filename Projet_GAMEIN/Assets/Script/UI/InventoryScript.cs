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

    public GameObject DisplayerInventory ;
    public List<Image> InventoryDisplayer ;


    [SerializeField] private RectTransform SettingPanel ;

    private void Awake() 
    {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    
            PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; 
            //SettingPanel = GameObject.Find("Settings Panel").GetComponent<RectTransform>() ;
        }

        SetInventoryCount();

    }
    private void OnEnable() 
    {
        //Debug.Log(transform.GetSiblingIndex());

    }
    private void OnDisable() {

    }

    public void SwitchToggleInventoryDisplay()
    {
        SetDisplayinventory();        
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        if(!InventoryPanel.activeSelf)
        {
            transform.SetSiblingIndex(0);
            if( (GameObject.Find("Dialogue Canvas") != null && GameObject.Find("Dialogue Canvas").gameObject.activeSelf == false) )
                PlayerScript.GetComponent<PlayerMovement>().EndDialog() ;
        } else {
            transform.SetSiblingIndex(1);            
            PlayerScript.GetComponent<PlayerMovement>().StartDialog() ;

        }
    }

    public void OpenSetting()
    {
        StartCoroutine(AnimationPanels(true));
    }
    public void CloseSetting()
    {
        StartCoroutine(AnimationPanels(false));
    }

    IEnumerator AnimationPanels(bool OpenSettings)
    {
        if(OpenSettings)
        {
            SettingPanel.DOAnchorPosY(1500, 0.01f);
            yield return new WaitForSeconds(0.01f);
            SettingPanel.gameObject.SetActive(true);
            SettingPanel.GetComponent<Image>().DOFade(0.75f, 1f);
            SettingPanel.DOAnchorPosY(0, 1f);            
        } else {

            SettingPanel.DOAnchorPosY(-50, 0.1f);
            yield return new WaitForSeconds(0.1f);
            SettingPanel.GetComponent<Image>().DOFade(0f, 1f);            
            SettingPanel.DOAnchorPosY(1500, 1f);
            yield return new WaitForSeconds(1f);


            SettingPanel.gameObject.SetActive(false);
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
