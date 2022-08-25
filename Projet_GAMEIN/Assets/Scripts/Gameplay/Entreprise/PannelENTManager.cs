using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI ;
using AllosiusDev.TranslationSystem;
using AllosiusDev.Audio;

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
    public ToTranslateObject titleActivity;
    public Image IllustrationActivité ;
    public string activityUrlSteam { get; set; }
    public ToTranslateTextArea descriptionActivity;
}

public class PannelENTManager : MonoBehaviour
{
    #region Fields

    private bool NewPanel = false;

    #endregion

    #region UnityInspector

    [Header ("Gestionnaire des Pages")]
    [SerializeField] private RectTransform FeuilleEnt ;
    [SerializeField] private RectTransform FeuilleRéseauxSociaux ;
    [SerializeField] private RectTransform FeuilleContact ;
    [SerializeField] private List<RectTransform> FeuilleActi ;
    [SerializeField] private List<RectTransform> FeuilleDecos ;
    public List<Vector3> DisplayTypeV1 ;
    public List<Vector3> DisplayTypeV2 ;
    public List<Vector3> DisplayTypeV3 ;
    public List<Vector3> DisplayTypeV4 ;


    [Header ("Information ENT")]
    [SerializeField] private TextMeshProUGUI NomEntreprise ;
    [SerializeField] private List<Image> LogoFondPage ;    
    [SerializeField] private ToTranslateObject titleQuiSommesNous;
    [SerializeField] private ToTranslateTextArea descriptionEntreprise;
    [SerializeField] private Image IllustrationDescriptionENT ;    
    [SerializeField] private ToTranslateObject titleValeurs;
    [SerializeField] private List<GameObject> ListValeurs ;


    [Header ("Information Contact")]
    [SerializeField] private ToTranslateObject ouNousTrouver;
    [SerializeField] private ToTranslateObject localisation;
    [SerializeField] private TextMeshProUGUI localisationAmount;
    [SerializeField] private ToTranslateObject notreSiteWebTitle;
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
    [SerializeField] private ToTranslateObject contact;
    [SerializeField] private TextMeshProUGUI MailButton ;
    [SerializeField] private GameObject PrefabNameCréateur ;


    [Header ("Activité Entreprise")]
    [SerializeField] private List<ContenuArticleActivity> ListContenuPage = new List<ContenuArticleActivity>();


    [Header ("List de Données")]/*
    [HideInInspector]*/ public PannelENTContainer InformationENT ;

    [Header("Sounds")]

    [SerializeField] private AudioData sfxOpenPanel;
    [SerializeField] private AudioData sfxClosePanel;

    #endregion

    #region Behaviour

    void Start() 
    {
        SetUIText();
        SetActivity();

        SetTextPannel();

        LangueManager.Instance.onLangageUpdated += SetUIText;
        LangueManager.Instance.onLangageUpdated += SetActivity;
        LangueManager.Instance.onLangageUpdated += SetTextPannel;
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
            SetPos(FeuilleActi[3], DisplayTypeV1[6]) ;
        }

        if(Disposition == 2)
        {
            SetPos(FeuilleEnt, DisplayTypeV2[0]) ;
            SetPos(FeuilleRéseauxSociaux, DisplayTypeV2[1]) ;
            SetPos(FeuilleContact, DisplayTypeV2[2]) ;

            SetPos(FeuilleActi[0], DisplayTypeV2[3]) ;
            SetPos(FeuilleActi[1], DisplayTypeV2[4]) ;
            SetPos(FeuilleActi[2], DisplayTypeV2[5]) ;
            SetPos(FeuilleActi[3], DisplayTypeV2[6]) ;
        }

        if(Disposition == 3)
        {
            SetPos(FeuilleEnt, DisplayTypeV3[0]) ;
            SetPos(FeuilleRéseauxSociaux, DisplayTypeV3[1]) ;
            SetPos(FeuilleContact, DisplayTypeV3[2]) ;

            SetPos(FeuilleActi[0], DisplayTypeV3[3]) ;
            SetPos(FeuilleActi[1], DisplayTypeV3[4]) ;
            SetPos(FeuilleActi[2], DisplayTypeV3[5]) ;
            SetPos(FeuilleActi[3], DisplayTypeV3[6]) ;
        }

        if(Disposition == 4)
        {
            SetPos(FeuilleEnt, DisplayTypeV4[0]) ;
            SetPos(FeuilleRéseauxSociaux, DisplayTypeV4[1]) ;
            SetPos(FeuilleContact, DisplayTypeV4[2]) ;

            SetPos(FeuilleActi[0], DisplayTypeV4[3]) ;
            SetPos(FeuilleActi[1], DisplayTypeV4[4]) ;
            SetPos(FeuilleActi[2], DisplayTypeV4[5]) ;
            SetPos(FeuilleActi[3], DisplayTypeV4[6]) ;
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
        if (NewPanel)
        {
            NewPanel = false ;
            SetPrincipalInformation();        
            SetButtonURL();
        
            SetUIText();
            SetActivity();

            SetTextPannel();  
        }
    }
    
    void SetUIText()
    {
        titleQuiSommesNous.Translation();
        titleValeurs.Translation();
        ouNousTrouver.Translation();
        notreSiteWebTitle.Translation();
        localisation.Translation();
        localisationAmount.text = InformationENT.Localisation;
        contact.Translation();
    }

    void SetActivity()
    {
        SetActivityText(0);
        SetActivityText(1);
        SetActivityText(2);
        SetActivityText(3);

        if(FeuilleActi[0].gameObject.activeSelf && FeuilleActi[1].gameObject.activeSelf && FeuilleActi[2].gameObject.activeSelf && !FeuilleActi[3].gameObject.activeSelf) SetDispositionPage(1) ;
        if(FeuilleActi[0].gameObject.activeSelf && FeuilleActi[1].gameObject.activeSelf && !FeuilleActi[2].gameObject.activeSelf && !FeuilleActi[3].gameObject.activeSelf) SetDispositionPage(2) ;
        if(FeuilleActi[0].gameObject.activeSelf && !FeuilleActi[1].gameObject.activeSelf && !FeuilleActi[2].gameObject.activeSelf && !FeuilleActi[3].gameObject.activeSelf) SetDispositionPage(3) ;
        if(FeuilleActi[0].gameObject.activeSelf && FeuilleActi[1].gameObject.activeSelf && FeuilleActi[2].gameObject.activeSelf && FeuilleActi[3].gameObject.activeSelf) SetDispositionPage(4) ;
    }

    void SetActivityText(int ActivityInt)
    {
        if(InformationENT.ListActivité.Count > ActivityInt)
        {
            if(InformationENT.ListActivité[ActivityInt].nameActivityTranslationKey != "")
            {
                FeuilleActi[ActivityInt].gameObject.SetActive(true);

                ListContenuPage[ActivityInt].titleActivity.SetTranslationKey(InformationENT.ListActivité[ActivityInt].nameActivityTranslationKey, TypeDictionary.InformationsPanelsTexts);
                ListContenuPage[ActivityInt].descriptionActivity.SetTranslationKey(InformationENT.ListActivité[ActivityInt].textsActivitiesTranslationKeys, TypeDictionary.InformationsPanelsTexts);

                if (InformationENT.ListActivité[ActivityInt].IllustrationActivité != null)
                {
                    ListContenuPage[ActivityInt].IllustrationActivité.sprite = InformationENT.ListActivité[ActivityInt].IllustrationActivité ;
                    ListContenuPage[ActivityInt].activityUrlSteam = InformationENT.ListActivité[ActivityInt].URLSteam;
                    Vector2 SizeIllustration = InformationENT.ListActivité[ActivityInt].IllustrationActivité.bounds.size ;

                    SizeIllustration *= 100f ;
                    float DivisionImage = 1 ;

                    while((SizeIllustration.x / DivisionImage) > (FeuilleActi[ActivityInt].sizeDelta.x -50f))
                    {
                        DivisionImage += 0.1f ;                  
                    }

                    ListContenuPage[ActivityInt].IllustrationActivité.GetComponent<RectTransform>().sizeDelta = new Vector2(SizeIllustration.x / DivisionImage, SizeIllustration.y / DivisionImage) ;
                }

                FeuilleActi[ActivityInt].sizeDelta = new Vector2(FeuilleActi[ActivityInt].sizeDelta.x,  (ListContenuPage[ActivityInt].titleActivity.GetComponent<RectTransform>().sizeDelta.y + ListContenuPage[ActivityInt].IllustrationActivité.GetComponent<RectTransform>().sizeDelta.y + 90f + InformationENT.ListActivité[ActivityInt].HeightArticle));                
            } else {
                FeuilleActi[ActivityInt].gameObject.SetActive(false);
            }
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
            if(InformationENT.LogoENT != null)
            {   
                LogoFondPage[LFP].enabled = true ;
                LogoFondPage[LFP].sprite = InformationENT.LogoENT ;
            } else {
                LogoFondPage[LFP].enabled = false ;
                LogoFondPage[LFP].sprite = null ;
            }
        }


        IllustrationDescriptionENT.sprite = InformationENT.IllustrationDescriptionENT ;


        // Set Size Illu ENT
        if(IllustrationDescriptionENT.sprite != null)
        {
            Vector2 SizeIllustration = IllustrationDescriptionENT.sprite.bounds.size ;
            SizeIllustration *= 100f ;
            float DivisionImage = 1 ;

            while((SizeIllustration.x / DivisionImage) > IllustrationDescriptionENT.GetComponent<RectTransform>().sizeDelta.x)
            {
                DivisionImage ++ ;                  
            }

            IllustrationDescriptionENT.GetComponent<RectTransform>().sizeDelta = new Vector2(SizeIllustration.x / DivisionImage, SizeIllustration.y / DivisionImage) ;            
        }

    }

    void SetTextPannel()
    {
        float HeightPanelENT = 0 ;   
        float HeightImgDescriptENT = 0 ;     
        /* DESCRIPTION ENTREPRISE */
        if(IllustrationDescriptionENT.sprite == null) 
        {
            IllustrationDescriptionENT.gameObject.SetActive(false) ;
            HeightImgDescriptENT = 0 ; 
        } else {
            IllustrationDescriptionENT.gameObject.SetActive(true) ;
            HeightImgDescriptENT = IllustrationDescriptionENT.GetComponent<RectTransform>().sizeDelta.y + 5f ; 

        }


        descriptionEntreprise.SetTranslationKey(InformationENT.descriptionsENTTranslationKeys, TypeDictionary.InformationsPanelsTexts);

        descriptionEntreprise.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionEntreprise.GetComponent<RectTransform>().sizeDelta.x, InformationENT.HeightDescription);

        HeightPanelENT = 10f + 125f + 5f + 26f + 5f + InformationENT.HeightDescription + HeightImgDescriptENT ;   
        /* VALEUR DE L'ENTREPRISE */
        if(InformationENT.ValeursENT.Count == 0)
        {
            titleValeurs.gameObject.SetActive(false) ;
            FeuilleEnt.sizeDelta = new Vector2(FeuilleEnt.sizeDelta.x, HeightPanelENT) ;
        } else {
            titleValeurs.gameObject.SetActive(true) ;
            FeuilleEnt.sizeDelta = new Vector2(FeuilleEnt.sizeDelta.x, HeightPanelENT + 150f) ;

            for (int i = 0; i < ListValeurs.Count; i++)
            {
                if(i < InformationENT.ValeursENT.Count)
                {
                    ListValeurs[i].SetActive(true);
                    ListValeurs[i].GetComponentInChildren<Image>().sprite = InformationENT.ValeursENT[i].IllustrationValeur ;
                    /*if (PlayerPrefs.GetInt("Langue") == 0) ; ListValeurs[i].GetComponentInChildren<TextMeshProUGUI>().text = InformationENT.ValeursENT[i].NomValeurFR ;
                        if (PlayerPrefs.GetInt("Langue") == 1) ; ListValeurs[i].GetComponentInChildren<TextMeshProUGUI>().text = InformationENT.ValeursENT[i].NomValeurEN ;*/
                } 
                else 
                {
                    ListValeurs[i].SetActive(false);
                }
            }            
        }

    }




    void SetButtonURL()
    {
        float MinimalHeightFeuilleReseauxSociaux = 110f ;//210f ;
        float HeightLineIcon = 50f ;

        if(InformationENT.URLSiteWeb == "")
        {
           NoteSiteWebURLDisplay.gameObject.SetActive(false) ; 
           notreSiteWebTitle.gameObject.SetActive(false);
        } else {
           NoteSiteWebURLDisplay.gameObject.SetActive(true) ; 
           notreSiteWebTitle.gameObject.SetActive(true);
        }
        
        ShowOrHideButton(InformationENT.URLFacebook, ButtonFacebook.gameObject) ;
        ShowOrHideButton(InformationENT.URLInstagram, ButtonInstagram.gameObject) ;
        ShowOrHideButton(InformationENT.URLTwitter, ButtonTwitter.gameObject) ;
        ShowOrHideButton(InformationENT.URLLinkedIn, ButtonLinkedIn.gameObject) ;
        
        ShowOrHideButton(InformationENT.URLDiscord, ButtonDiscord.gameObject) ;
        ShowOrHideButton(InformationENT.URLSteam, ButtonSteam.gameObject) ;
        ShowOrHideButton(InformationENT.URLTwitch, ButtonTwitch.gameObject) ;
        ShowOrHideButton(InformationENT.URLYoutube, ButtonYoutube.gameObject) ;

        float ValueHeightFeuilleRS = MinimalHeightFeuilleReseauxSociaux ;
        float HeightLineIcon1 = 0 ;
        float HeightLineIcon2 = 0 ;
        if(ButtonFacebook.gameObject.activeSelf || ButtonInstagram.gameObject.activeSelf || ButtonTwitter.gameObject.activeSelf || ButtonLinkedIn.gameObject.activeSelf)    HeightLineIcon1 = HeightLineIcon ;

        if(ButtonDiscord.gameObject.activeSelf || ButtonSteam.gameObject.activeSelf || ButtonTwitch.gameObject.activeSelf || ButtonYoutube.gameObject.activeSelf)    HeightLineIcon2 = HeightLineIcon ;

        ValueHeightFeuilleRS += HeightLineIcon1 + HeightLineIcon2 ;
        FeuilleRéseauxSociaux.sizeDelta = new Vector2(FeuilleRéseauxSociaux.sizeDelta.x, ValueHeightFeuilleRS) ;
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
        if(InformationENT.ContactEmail != null ||  InformationENT.PersonneJoignable.Count != 0)
        {
            FeuilleContact.gameObject.SetActive(true);

            if(FeuilleContact.GetChild(2).childCount > 0)
            {
                for (int CPj = 0; CPj < FeuilleContact.GetChild(2).childCount; CPj++)
                {
                    Destroy(FeuilleContact.GetChild(2).transform.GetChild(CPj).transform.gameObject) ; 
                }            
            }
    
            float ContactMailHeightAdd = 0f ;
            MailButton.text = InformationENT.ContactEmail ;        
            if(InformationENT.ContactEmail != "")
            {
                MailButton.gameObject.SetActive(true);
                ContactMailHeightAdd = 20f ;
                FeuilleContact.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(25f, -75f);
            } else {
                MailButton.gameObject.SetActive(false);
                ContactMailHeightAdd = 0f ;
                FeuilleContact.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(25f, -50f);
            }
    
    
            for (int Lj = 0; Lj < InformationENT.PersonneJoignable.Count; Lj++)
            {
                GameObject NameInstantiate = Instantiate(PrefabNameCréateur, FeuilleContact.GetChild(2).transform);
                NameInstantiate.transform.SetParent(FeuilleContact.GetChild(2).transform);
                NameInstantiate.GetComponent<TextMeshProUGUI>().text = InformationENT.PersonneJoignable[Lj];
            }
    
    
            FeuilleContact.sizeDelta = new Vector2(FeuilleContact.sizeDelta.x, (80f + ContactMailHeightAdd + 20f * InformationENT.PersonneJoignable.Count)) ;
        } else {
            FeuilleContact.gameObject.SetActive(false);
        } 
    }



    public void SwitchTogglePannelDisplay()
    {
        if(gameObject.activeSelf == false)
        {

            gameObject.SetActive(!gameObject.activeSelf);
            GameManager.Instance.player.GetComponent<PlayerMovement>().StartActivity();

            AudioController.Instance.PlayAudio(sfxOpenPanel);

            StartShow();            
        } else {
            gameObject.SetActive(!gameObject.activeSelf);
            GameManager.Instance.player.GetComponent<PlayerMovement>().EndActivity();

            AudioController.Instance.PlayAudio(sfxClosePanel);

            NewPanel = false ;
        }
    }

    #region URL

    public void OpenWebSite()
    {
        if(InformationENT.URLSiteWeb != "") OpenURLLink(InformationENT.URLSiteWeb) ;
    }

    public void OpenMailContact()
    {
        if(InformationENT.ContactEmail != "") OpenURLLink(InformationENT.ContactEmail) ;
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

    public void OpenSteamActivity(int index)
    {
        if(ListContenuPage[index].activityUrlSteam != "")
        {
            OpenURLLink(ListContenuPage[index].activityUrlSteam);
        }
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

    #endregion

    private void OnDestroy()
    {
        LangueManager.Instance.onLangageUpdated -= SetUIText;
        LangueManager.Instance.onLangageUpdated -= SetActivity;
        LangueManager.Instance.onLangageUpdated -= SetTextPannel;
    }

    #endregion
}
