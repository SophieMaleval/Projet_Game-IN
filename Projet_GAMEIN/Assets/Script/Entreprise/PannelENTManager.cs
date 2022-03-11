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

public class PannelENTManager : MonoBehaviour
{
    [Header ("Gestionnaire des Pages")]
    [SerializeField] private RectTransform FeuilleEnt ;
    [SerializeField] private RectTransform FeuilleRéseauxSociaux ;
    [SerializeField] private RectTransform FeuilleContact ;
    [SerializeField] private RectTransform FeuilleProductions ;
    [SerializeField] private List<RectTransform> FeuilleDecos ;
    public int Disposition ;


    [Header ("UI Text")]
    [SerializeField] private TextMeshProUGUI NomEntreprise ;

    [Space]

    [SerializeField] private TextMeshProUGUI TitleQuiSommesNous ;
    [SerializeField] private TextMeshProUGUI DesciptionEntreprise ;
    [SerializeField] private TextMeshProUGUI TitleValeurs ;
    [SerializeField] private List<GameObject> ListValeurs ;
    [SerializeField] private TextMeshProUGUI DescriptionValeur ;

    [SerializeField] private GameObject PrefabNameCréateur ;

    [Space]

    [SerializeField] private TextMeshProUGUI OuNousTrouvez ;
    [SerializeField] private TextMeshProUGUI Contact ;
    [SerializeField] private TextMeshProUGUI Localisation ;
    [SerializeField] private TextMeshProUGUI NoteSiteWebTitle ;
    [SerializeField] private TextMeshProUGUI NoteSiteWebURLDisplay ;

    [SerializeField] private GameObject ButtonFacebook ;
    [SerializeField] private GameObject ButtonInstagram ;
    [SerializeField] private GameObject ButtonTwitter ;
    [SerializeField] private GameObject ButtonLinkedIn ;
    [SerializeField] private GameObject ButtonDiscord ;
    [SerializeField] private GameObject ButtonSteam ;
    [SerializeField] private GameObject ButtonTwitch ;
    [SerializeField] private GameObject ButtonYoutube ;

    [Space]

    [SerializeField] private TextMeshProUGUI TitleNosProduction ;
    [SerializeField] private TextMeshProUGUI TitleLastProduction ;
    [SerializeField] private TextMeshProUGUI DescriptionLastProduction ;
    [SerializeField] private TextMeshProUGUI TitlePreLastProduction ;
    [SerializeField] private TextMeshProUGUI DescriptionPreLastProduction ;

    [Header ("UI Illustration")]
    [SerializeField] private List<Image> LogoFondPage ;
    [SerializeField] private Image IllustrationDescriptionENT ;

    [SerializeField] private Image IllustrationDescriptionDernierProjet ;
    [SerializeField] private Image IllustrationDescriptionAvantDernierProjet ;

    [Header ("List de Données")]
    [HideInInspector] public PannelENTContainer InformationENT ;

    [HideInInspector] public List<string> UIPanelENTFR ;
    [HideInInspector] public List<string> UIPanelENTEN ;
    private List<string> UIPanelENT ;

    public UIPanelENTContainer InformationPannelENTFR ;
    public UIPanelENTContainer InformationPannelENTEN ;
    private UIPanelENTContainer InformationPannelENT ;

    void Start() 
    {
        SetPrincipalInformation();
        UIPanelENTFR = GameObject.Find("Player Backpack").GetComponent<CSVReader>().UIText.PanelENTFR ;
        UIPanelENTEN = GameObject.Find("Player Backpack").GetComponent<CSVReader>().UIText.PanelENTEN ;
        SetButtonURL();
        SetListPersonneJoignable() ;
    }


    void SetDispositionPage(int Disposition)
    {
        if(Disposition == 0)
        {
            FeuilleEnt = SetPos(-296f, 84f, -2.5f) ;
            FeuilleRéseauxSociaux = SetPos(29.4f, -203.1f, 6.633f) ;
            FeuilleProductions = SetPos(292f, 79f, 2f) ;

            FeuilleDecos[0] = SetPos(-300f, -127f, -1) ;
            FeuilleDecos[1] = SetPos(-9f, 5f, -6.5f) ;
            FeuilleDecos[2] = SetPos(320f, -108f, -1.5f) ;
        }

        if(Disposition == 1)
        {
            FeuilleEnt = SetPos(-253.1f, 115.5f, -2.41f) ;
            FeuilleRéseauxSociaux = SetPos(161f, 150f, 3.2f) ;
            FeuilleProductions = SetPos(168.2f, -120.8f, -4.3f) ;

            FeuilleDecos[0] = SetPos(-305f, -100f, 9.2f) ;
            FeuilleDecos[1] = SetPos(-36f, -39f, -6.1f) ;
            FeuilleDecos[2] = SetPos(270f, -144.6f, -5.7f) ;
        }
    }

    RectTransform SetPos(float PosX, float PosY, float RotationZ)
    {
        RectTransform NewPos = new RectTransform() ;
        
        NewPos.anchoredPosition = new Vector2(PosX, PosY);
        NewPos.eulerAngles = new Vector3(0, 0, RotationZ);

        return NewPos ;
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("Langue") == 0 && UIPanelENT != UIPanelENTFR)
        {
            UIPanelENT = UIPanelENTFR ;
            InformationPannelENT = InformationPannelENTFR ;

            SetUIText();

            ENTInformation();
            SetTextPannel();            
        }

        if(PlayerPrefs.GetInt("Langue") == 1 && UIPanelENT != UIPanelENTEN)
        {
            UIPanelENT = UIPanelENTEN ;
            InformationPannelENT = InformationPannelENTEN ;

            SetUIText();

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
        TitleNosProduction.text = UIPanelENT[5] ;
        Contact.text = UIPanelENT[6] ;
    }

    void ENTInformation()
    {
        DesciptionEntreprise.text = InformationPannelENT.DescriptionEntreprise ;
        DescriptionValeur.text = InformationPannelENT.Valeurs ;

       // TitleLastProduction.text = InformationPannelENT.TitreDernièreProd ;
       // DescriptionLastProduction.text = InformationPannelENT.DescriptionDernièreProd ;
      //  TitlePreLastProduction.text = InformationPannelENT.TitreAvantDernièreProd ;
      //  DescriptionPreLastProduction.text = InformationPannelENT.DescriptionAvantDernièreProd ;
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
        IllustrationDescriptionDernierProjet.sprite = InformationENT.IllustrationDescriptionDernierProjet ;


        if(TitlePreLastProduction.text != "") IllustrationDescriptionAvantDernierProjet.sprite = InformationENT.IllustrationDescriptionAvantDernierProjet ;
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

        if(InformationENT.URLSiteWeb == "") NoteSiteWebURLDisplay.gameObject.SetActive(false) ;
        
        
        if(InformationENT.URLFacebook == "") ButtonFacebook.gameObject.SetActive(false) ;
        if(InformationENT.URLInstagram == "") ButtonInstagram.gameObject.SetActive(false) ;
        if(InformationENT.URLTwitter == "") ButtonTwitter.gameObject.SetActive(false) ;
        if(InformationENT.URLLinkedIn == "") ButtonLinkedIn.gameObject.SetActive(false) ;


        if(InformationENT.URLDiscord == "") ButtonDiscord.gameObject.SetActive(false) ;
        if(InformationENT.URLSteam == "") ButtonSteam.gameObject.SetActive(false) ;
        if(InformationENT.URLTwitch == "") ButtonTwitch.gameObject.SetActive(false) ;
        if(InformationENT.URLYoutube == "") ButtonYoutube.gameObject.SetActive(false) ;

        if(!ButtonDiscord.gameObject.activeSelf && !ButtonSteam.gameObject.activeSelf && !ButtonTwitch.gameObject.activeSelf && !ButtonYoutube.gameObject.activeSelf)
        {
            FeuilleRéseauxSociaux.sizeDelta = new Vector2(FeuilleRéseauxSociaux.sizeDelta.x, HeightFeuilleContact - HeightLineIcon) ;
        }
    }

    void SetListPersonneJoignable()
    {
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
        } else {
            gameObject.SetActive(!gameObject.activeSelf);
            GameObject.Find("Player").GetComponent<PlayerMovement>().EndActivity(); 
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
