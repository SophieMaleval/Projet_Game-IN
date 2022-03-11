using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI ;

[System.Serializable]
public class UIPanelENTContainer
{
    public string NomEntrprise ;
    public string DescriptionEntreprise ;

    public string Valeurs ;

    public string TitreDernièreProd ;
    public string DescriptionDernièreProd ;
    public string TitreAvantDernièreProd ;
    public string DescriptionAvantDernièreProd ;
}

[System.Serializable]
public class ContenuArticleActivity
{
    public TextMeshProUGUI TitleActivité ;
    public Image IllustrationActivité ;
    public TextMeshProUGUI DescriptionAtivité ;
}

public class PannelENTManager : MonoBehaviour
{
    [Header ("Gestionnaire des Pages")]
    [SerializeField] private RectTransform FeuilleEnt ;
    [SerializeField] private RectTransform FeuilleRéseauxSociaux ;
    [SerializeField] private RectTransform FeuilleContact ;
    [SerializeField] private List<RectTransform> FeuilleActi ;
    [SerializeField] private List<RectTransform> FeuilleDecos ;
    public List<Vector3> DisplayTypeV1 ;
    public List<Vector3> DisplayTypeV2 ;
    public List<Vector3> DisplayTypeV3 ;



    [Header ("Information ENT")]
    [SerializeField] private TextMeshProUGUI NomEntreprise ;
    [SerializeField] private List<Image> LogoFondPage ;    
    [SerializeField] private TextMeshProUGUI TitleQuiSommesNous ;
    [SerializeField] private TextMeshProUGUI DesciptionEntreprise ;
    [SerializeField] private Image IllustrationDescriptionENT ;    
    [SerializeField] private TextMeshProUGUI TitleValeurs ;
    [SerializeField] private List<GameObject> ListValeurs ;


    [Header ("Information Contact")]
    [SerializeField] private TextMeshProUGUI OuNousTrouvez ;
    [SerializeField] private TextMeshProUGUI Localisation ;   
    [SerializeField] private TextMeshProUGUI NoteSiteWebTitle ;
    [SerializeField] private TextMeshProUGUI NoteSiteWebURLDisplay ;    
    [Space]
    [SerializeField] private GameObject ButtonFacebook ;
    [SerializeField] private GameObject ButtonInstagram ;
    [SerializeField] private GameObject ButtonTwitter ;
    [SerializeField] private GameObject ButtonLinkedIn ;
    [SerializeField] private GameObject ButtonDiscord ;
    [SerializeField] private GameObject ButtonSteam ;
    [SerializeField] private GameObject ButtonTwitch ;
    [SerializeField] private GameObject ButtonYoutube ;
    [Space]
    [SerializeField] private TextMeshProUGUI Contact ;
    [SerializeField] private GameObject PrefabNameCréateur ;


    [Header ("Activité Entreprise")]
    [SerializeField] private List<ContenuArticleActivity> ListContenuPage = new List<ContenuArticleActivity>();


    [Header ("List de Données")]/*
    [HideInInspector]*/ public PannelENTContainer InformationENT ;

    [HideInInspector] public List<string> UIPanelENTFR ;
    [HideInInspector] public List<string> UIPanelENTEN ;
    private List<string> UIPanelENT ;

    [HideInInspector] public UIPanelENTContainer InformationPannelENTFR ;
    [HideInInspector] public UIPanelENTContainer InformationPannelENTEN ;
    private UIPanelENTContainer InformationPannelENT ;
    private bool NewPanel = false ;

    void Start() 
    {
        UIPanelENTFR = GameObject.Find("Player Backpack").GetComponent<CSVReader>().UIText.PanelENTFR ;
        UIPanelENTEN = GameObject.Find("Player Backpack").GetComponent<CSVReader>().UIText.PanelENTEN ;
    }

    public void StartShow()
    {     
        SetPrincipalInformation();        
        SetButtonURL();
        SetListPersonneJoignable() ;
        SetActivity();  

        NewPanel = true ;  
    }


    void SetDispositionPage(int Disposition)
    {
        if(Disposition == 1)
        {
            SetPos(FeuilleEnt, DisplayTypeV1[0]) ;
            SetPos(FeuilleRéseauxSociaux, DisplayTypeV1[1]) ;
            SetPos(FeuilleContact, DisplayTypeV1[2]) ;

            SetPos(FeuilleActi[0], DisplayTypeV1[3]) ;
            SetPos(FeuilleActi[1], DisplayTypeV1[4]) ;
            SetPos(FeuilleActi[2], DisplayTypeV1[5]) ;
        }

        if(Disposition == 2)
        {
            SetPos(FeuilleEnt, DisplayTypeV2[0]) ;
            SetPos(FeuilleRéseauxSociaux, DisplayTypeV2[1]) ;
            SetPos(FeuilleContact, DisplayTypeV2[2]) ;

            SetPos(FeuilleActi[0], DisplayTypeV2[3]) ;
            SetPos(FeuilleActi[1], DisplayTypeV2[4]) ;
            SetPos(FeuilleActi[2], DisplayTypeV2[5]) ;
        }

        if(Disposition == 3)
        {
            SetPos(FeuilleEnt, DisplayTypeV3[0]) ;
            SetPos(FeuilleRéseauxSociaux, DisplayTypeV3[1]) ;
            SetPos(FeuilleContact, DisplayTypeV3[2]) ;

            SetPos(FeuilleActi[0], DisplayTypeV3[3]) ;
            SetPos(FeuilleActi[1], DisplayTypeV3[4]) ;
            SetPos(FeuilleActi[2], DisplayTypeV3[5]) ;
        }
    }

    void SetPos(RectTransform RectTModify , Vector3 DataPos)
    {
        float PosX = DataPos.x ;
        float PosY = DataPos.y ;
        float RotationZ = DataPos.z ;
        
        RectTModify.anchoredPosition = new Vector2(PosX, PosY);
        RectTModify.eulerAngles = new Vector3(0, 0, RotationZ);

    }

    void Update()
    {
        if(PlayerPrefs.GetInt("Langue") == 0 && UIPanelENT != UIPanelENTFR)
        {
            UIPanelENT = UIPanelENTFR ;
            InformationPannelENT = InformationPannelENTFR ;

            SetUIText();
            SetActivity();

            ENTInformation();
            SetTextPannel();            
        }

        if(PlayerPrefs.GetInt("Langue") == 1 && UIPanelENT != UIPanelENTEN)
        {
            UIPanelENT = UIPanelENTEN ;
            InformationPannelENT = InformationPannelENTEN ;

            SetUIText();
            SetActivity();

            ENTInformation();
            SetTextPannel();            
        }

        if(NewPanel)
        {
            NewPanel = false ;
            SetPrincipalInformation();        
            SetButtonURL();
        
            SetUIText();
            SetActivity();

            ENTInformation();
            SetTextPannel();  
        }
    }
    
    void SetUIText()
    {
        TitleQuiSommesNous.text = UIPanelENT[0] ;
        TitleValeurs.text = UIPanelENT[1] ;
        OuNousTrouvez.text = UIPanelENT[2] ;
        NoteSiteWebTitle.text = UIPanelENT[3] ;
        Localisation.text = UIPanelENT[4] + " " + InformationENT.Localisation ;
        Contact.text = UIPanelENT[6] ;
    }

    void ENTInformation()
    {
        DesciptionEntreprise.text = InformationPannelENT.DescriptionEntreprise ;

    }

    void SetActivity()
    {
        SetActivityText(0);
        SetActivityText(1);
        SetActivityText(2);

        if(FeuilleActi[0].gameObject.activeSelf && FeuilleActi[1].gameObject.activeSelf && FeuilleActi[2].gameObject.activeSelf) SetDispositionPage(1) ;
        if(FeuilleActi[0].gameObject.activeSelf && FeuilleActi[1].gameObject.activeSelf && !FeuilleActi[2].gameObject.activeSelf) SetDispositionPage(2) ;
        if(FeuilleActi[0].gameObject.activeSelf && !FeuilleActi[1].gameObject.activeSelf && !FeuilleActi[2].gameObject.activeSelf) SetDispositionPage(3) ;
    }

    void SetActivityText(int ActivityInt)
    {
        if(InformationENT.ListActivité[ActivityInt].NameActivité != "")
        {
            FeuilleActi[ActivityInt].gameObject.SetActive(true);
            ListContenuPage[ActivityInt].TitleActivité.text = InformationENT.ListActivité[ActivityInt].NameActivité ;
            ListContenuPage[ActivityInt].IllustrationActivité.sprite = InformationENT.ListActivité[ActivityInt].IllustrationActivité ;
            
            if(PlayerPrefs.GetInt("Langue") == 0) ListContenuPage[ActivityInt].DescriptionAtivité.text = InformationENT.ListActivité[ActivityInt].TextActivtéFR ;            
            if(PlayerPrefs.GetInt("Langue") == 1) ListContenuPage[ActivityInt].DescriptionAtivité.text = InformationENT.ListActivité[ActivityInt].TextActivtéEN ;            
        
            FeuilleActi[ActivityInt].sizeDelta = new Vector2(FeuilleActi[ActivityInt].sizeDelta.x,  (ListContenuPage[ActivityInt].TitleActivité.rectTransform.sizeDelta.y + ListContenuPage[ActivityInt].IllustrationActivité.rectTransform.sizeDelta.y + 90f + InformationENT.ListActivité[ActivityInt].HeightArticle));
        } else {
            FeuilleActi[ActivityInt].gameObject.SetActive(false);
        }        
    }

    void SetPrincipalInformation()
    {
        NomEntreprise.text = InformationENT.NomEntreprise ;
        NoteSiteWebURLDisplay.text = InformationENT.URLSiteWebDisplay ;

        for (int LFP = 0; LFP < LogoFondPage.Count; LFP++)
        {
            LogoFondPage[LFP].sprite = InformationENT.LogoENT ;
        }

        IllustrationDescriptionENT.sprite = InformationENT.IllustrationDescriptionENT ;
    }

    void SetTextPannel()
    {
        /* DESCRIPTION ENTREPRISE */
        if(IllustrationDescriptionENT.sprite == null) 
        {
            IllustrationDescriptionENT.gameObject.SetActive(false) ;
            //DesciptionEntreprise.GetComponent<RectTransform>().sizeDelta = new Vector2(325f, DesciptionEntreprise.GetComponent<RectTransform>().sizeDelta.y);
        } else {
            IllustrationDescriptionENT.gameObject.SetActive(true) ;
            //DesciptionEntreprise.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, DesciptionEntreprise.GetComponent<RectTransform>().sizeDelta.y);
        }
        if(PlayerPrefs.GetInt("Langue") == 0) DesciptionEntreprise.text = InformationENT.DescriptionENTFR ;
        if(PlayerPrefs.GetInt("Langue") == 1) DesciptionEntreprise.text = InformationENT.DescriptionENTEN ;

        /* VALEUR DE L'ENTREPRISE */
        for (int i = 0; i < ListValeurs.Count; i++)
        {
            if(i < InformationENT.ValeursENT.Count)
            {
                ListValeurs[i].SetActive(true);
                ListValeurs[i].GetComponentInChildren<Image>().sprite = InformationENT.ValeursENT[i].IllustrationValeur ;
                if(PlayerPrefs.GetInt("Langue") == 0) ListValeurs[i].GetComponentInChildren<TextMeshProUGUI>().text = InformationENT.ValeursENT[i].NomValeurFR ;
                if(PlayerPrefs.GetInt("Langue") == 1) ListValeurs[i].GetComponentInChildren<TextMeshProUGUI>().text = InformationENT.ValeursENT[i].NomValeurEN ;
            } else {
                ListValeurs[i].SetActive(false);
            }
        }
    }













    void SetButtonURL()
    {
        float HeightFeuilleContact = 210f ;
        float HeightLineIcon = 50f ;

        if(InformationENT.URLSiteWeb == "")
        {
           NoteSiteWebURLDisplay.gameObject.SetActive(false) ; 
           NoteSiteWebTitle.gameObject.SetActive(false);
        } else {
           NoteSiteWebURLDisplay.gameObject.SetActive(true) ; 
           NoteSiteWebTitle.gameObject.SetActive(true);
        }
        
        ShowOrHideButton(InformationENT.URLFacebook, ButtonFacebook.gameObject) ;
        ShowOrHideButton(InformationENT.URLInstagram, ButtonInstagram.gameObject) ;
        ShowOrHideButton(InformationENT.URLTwitter, ButtonTwitter.gameObject) ;
        ShowOrHideButton(InformationENT.URLLinkedIn, ButtonLinkedIn.gameObject) ;
        
        ShowOrHideButton(InformationENT.URLDiscord, ButtonDiscord.gameObject) ;
        ShowOrHideButton(InformationENT.URLSteam, ButtonSteam.gameObject) ;
        ShowOrHideButton(InformationENT.URLTwitch, ButtonTwitch.gameObject) ;
        ShowOrHideButton(InformationENT.URLYoutube, ButtonYoutube.gameObject) ;


        if(!ButtonDiscord.gameObject.activeSelf && !ButtonSteam.gameObject.activeSelf && !ButtonTwitch.gameObject.activeSelf && !ButtonYoutube.gameObject.activeSelf)
        {
            FeuilleRéseauxSociaux.sizeDelta = new Vector2(FeuilleRéseauxSociaux.sizeDelta.x, HeightFeuilleContact - HeightLineIcon) ;
        }
    }
    void ShowOrHideButton(string TextRef, GameObject ButtonGameObject)
    {
        if(TextRef == "")
        {
            ButtonGameObject.SetActive(false) ;
        } else {
            ButtonGameObject.SetActive(true) ;
        }
    }

    void SetListPersonneJoignable()
    {
        if(FeuilleContact.GetChild(1).childCount > 0)
        {
            for (int CPj = 0; CPj < FeuilleContact.GetChild(1).childCount; CPj++)
            {
                Destroy(FeuilleContact.GetChild(1).transform.GetChild(CPj).transform.gameObject) ; 
            }            
        }


        for (int Lj = 0; Lj < InformationENT.PersonneJoignable.Count; Lj++)
        {
            GameObject NameInstantiate = Instantiate(PrefabNameCréateur, FeuilleContact.GetChild(1).transform);
            NameInstantiate.transform.SetParent(FeuilleContact.GetChild(1).transform);
            NameInstantiate.GetComponent<TextMeshProUGUI>().text = InformationENT.PersonneJoignable[Lj];
        }


        FeuilleContact.sizeDelta = new Vector2(FeuilleContact.sizeDelta.x, (80f + 20f * InformationENT.PersonneJoignable.Count)) ;
    }



    public void SwitchTogglePannelDisplay()
    {
        if(gameObject.activeSelf == false)
        {

            gameObject.SetActive(!gameObject.activeSelf);
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartActivity();
            StartShow();            
        } else {
            gameObject.SetActive(!gameObject.activeSelf);
            GameObject.Find("Player").GetComponent<PlayerMovement>().EndActivity(); 
            NewPanel = false ;
        }
    }















    public void OpenWebSite()
    {
        if(InformationENT.URLSiteWeb != "") OpenURLLink(InformationENT.URLSiteWeb) ;
    }

    public void OpenFacebook()
    {
        if(InformationENT.URLFacebook != "") OpenURLLink(InformationENT.URLFacebook) ;
    }

    public void OpenInstagram()
    {
        if(InformationENT.URLInstagram != "") OpenURLLink(InformationENT.URLInstagram) ;
    }

    public void OpenTwitter()
    {
        if(InformationENT.URLTwitter != "") OpenURLLink(InformationENT.URLTwitter) ;
    }

    public void OpenLinkedIn()
    {
        if(InformationENT.URLLinkedIn != "") OpenURLLink(InformationENT.URLLinkedIn) ;
    }

    public void OpenDiscord()
    {
        if(InformationENT.URLDiscord != "") OpenURLLink(InformationENT.URLDiscord) ;
    }

    public void OpenSteam()
    {
        if(InformationENT.URLSteam != "") OpenURLLink(InformationENT.URLSteam) ;
    }

    public void OpenTwitch()
    {
        if(InformationENT.URLTwitch != "") OpenURLLink(InformationENT.URLTwitch) ;
    }

    public void OpenYoutube()
    {
        if(InformationENT.URLYoutube != "") OpenURLLink(InformationENT.URLYoutube) ;
    }

    public void OpenURLLink(string URLLink)
    {
        Application.ExternalEval("window.open('" + URLLink + "', '_blank')");      
    }
}
