using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI ;

public class PetiteAnnonceManager : MonoBehaviour
{
    [SerializeField] private GameObject PrefabAnnonce ;
    [HideInInspector] public PetiteAnnonceContainer InfoTableau ;
    private int ValueLangageOnDisplay = 0 ;


    void Start()
    {

    }
    

    void SetAnnonce()
    {
        ValueLangageOnDisplay = PlayerPrefs.GetInt("Langue");
        if(transform.childCount > 1)
        {
            for (int Ae = 0; Ae < transform.childCount -1; Ae++)
            {
                Destroy(transform.GetChild(Ae).transform.gameObject);
            }
        }


        for (int Ai = 0; Ai < InfoTableau.AnnonceDisponible.Count; Ai++)
        {
            InstantiateAnnonce(InfoTableau.AnnonceDisponible[Ai]);
        }
    }

    void InstantiateAnnonce(Annonce AnnonceNum)
    {
        // Instantiate and Set Transform
        GameObject Annonce = Instantiate(PrefabAnnonce);
        Annonce.transform.SetParent(this.transform);
        Annonce.transform.SetSiblingIndex(0);
        Annonce.GetComponent<RectTransform>().localScale = Vector3.one ; 
        
        Annonce.GetComponent<RectTransform>().sizeDelta = AnnonceNum.SizeAnnonce ;

        Annonce.GetComponent<RectTransform>().anchoredPosition = new Vector2(AnnonceNum.PosAndRotation.x, AnnonceNum.PosAndRotation.y) ;
        Annonce.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, AnnonceNum.PosAndRotation.z);


        // Set Visuel
        if(AnnonceNum.FlyerFR != null || AnnonceNum.FlyerEN != null) // Affiche Directement un Sprite de flyer 
        {
            Annonce.GetComponent<Image>().color = Color.white ;
            if(AnnonceNum.FlyerFR != null && AnnonceNum.FlyerEN == null) AnnonceNum.FlyerEN = AnnonceNum.FlyerFR ;
            if(AnnonceNum.FlyerFR == null && AnnonceNum.FlyerEN != null) AnnonceNum.FlyerFR = AnnonceNum.FlyerEN ;

            if(PlayerPrefs.GetInt("Langue") == 0) Annonce.GetComponent<Image>().sprite = AnnonceNum.FlyerFR ;
            if(PlayerPrefs.GetInt("Langue") == 1) Annonce.GetComponent<Image>().sprite = AnnonceNum.FlyerEN ;

            Annonce.transform.GetChild(0).gameObject.SetActive(false);
            Annonce.transform.GetChild(1).gameObject.SetActive(false);            
        } else { // Affiche un texte d'annonce
                Annonce.transform.GetChild(0).gameObject.SetActive(true);                    
                if(PlayerPrefs.GetInt("Langue") == 0) Annonce.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = AnnonceNum.TextAnnonceFR ;
                if(PlayerPrefs.GetInt("Langue") == 1) Annonce.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = AnnonceNum.TextAnnonceEN ;

            if(AnnonceNum.FondAnnonceFR != null || AnnonceNum.FondAnnonceEN != null) // Avec un fond spéciale
            {
                Annonce.GetComponent<Image>().color = Color.white ;
                if(AnnonceNum.FondAnnonceFR != null && AnnonceNum.FondAnnonceEN == null) AnnonceNum.FondAnnonceEN = AnnonceNum.FondAnnonceFR ;
                if(AnnonceNum.FondAnnonceFR == null && AnnonceNum.FondAnnonceEN != null) AnnonceNum.FondAnnonceFR = AnnonceNum.FondAnnonceEN ;

                if(PlayerPrefs.GetInt("Langue") == 0) Annonce.GetComponent<Image>().sprite = AnnonceNum.FondAnnonceFR ;    
                if(PlayerPrefs.GetInt("Langue") == 1) Annonce.GetComponent<Image>().sprite = AnnonceNum.FondAnnonceEN ;    

                Annonce.transform.GetChild(0).GetComponent<RectTransform>().offsetMin = new Vector2(20f, 20f);
                Annonce.transform.GetChild(0).GetComponent<RectTransform>().offsetMax = new Vector2(-20f, -20f);
                
                Annonce.transform.GetChild(1).gameObject.SetActive(false);                      
            } else { // Avec un fond de couleur et le nom de l'entreprise
                Annonce.GetComponent<Image>().color = AnnonceNum.BackgroundColorAnnonce ;
                Annonce.GetComponent<Image>().sprite = null ;

                Annonce.transform.GetChild(0).GetComponent<RectTransform>().offsetMin = new Vector2(25f, 65f);
                Annonce.transform.GetChild(0).GetComponent<RectTransform>().offsetMax = new Vector2(-20f, -25f);
                
                Annonce.transform.GetChild(1).gameObject.SetActive(true);                    
                Annonce.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = AnnonceNum.EntrepriseName ;
            }
        }

    }



    private void Update() 
    {
        if(PlayerPrefs.GetInt("Langue") != ValueLangageOnDisplay) SetAnnonce();
    }


    




    public void SwitchTogglePannelDisplay()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(!gameObject.activeSelf);
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartActivity();
            SetAnnonce();
        } else {
            gameObject.SetActive(!gameObject.activeSelf);
            GameObject.Find("Player").GetComponent<PlayerMovement>().EndActivity(); 
        }
    }

}
