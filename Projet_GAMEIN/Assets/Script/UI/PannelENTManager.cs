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
    [Header ("UI Text")]
    [SerializeField] private TextMeshProUGUI NomEntreprise ;

    [SerializeField] private TextMeshProUGUI TitleQuiSommesNous ;
    [SerializeField] private TextMeshProUGUI DesciptionEntreprise ;
    [SerializeField] private TextMeshProUGUI TitleValeurs ;
    [SerializeField] private TextMeshProUGUI DescriptionValeur ;

    [SerializeField] private TextMeshProUGUI Contact ;
    [SerializeField] private TextMeshProUGUI NoteSiteWebTitle ;
        [SerializeField] private TextMeshProUGUI NoteSiteWebURLDisplay ;

    [SerializeField] private TextMeshProUGUI TitleNosProduction ;
    [SerializeField] private TextMeshProUGUI TitleLastProduction ;
    [SerializeField] private TextMeshProUGUI DescriptionLastProduction ;
    [SerializeField] private TextMeshProUGUI TitlePreLastProduction ;
    [SerializeField] private TextMeshProUGUI DescriptionPreLastProduction ;

    [Header ("UI Illustration")]
    [SerializeField] private List<Image> LogoFondPage ;
    [SerializeField] private Image IllustrationDescriptionENT ;
    [SerializeField] private Image IllustrationValeurENT ;
    [SerializeField] private Image IllustrationDescriptionDernierProjet ;
    [SerializeField] private Image IllustrationDescriptionAvantDernierProjet ;

    [Header ("List de Données")]
    [HideInInspector] public PannelENTContainer InformationENT ;

    [HideInInspector] public List<string> UIPanelENTFR ;
    [HideInInspector] public List<string> UIPanelENTEN ;
    private List<string> UIPanelENT ;

  /*  [HideInInspector]*/ public UIPanelENTContainer InformationPannelENTFR ;
   /* [HideInInspector]*/ public UIPanelENTContainer InformationPannelENTEN ;
    private UIPanelENTContainer InformationPannelENT ;

    void Start() 
    {
        SetPrincipalInformation();
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("Langue") == 0 && UIPanelENT != UIPanelENTFR)
        {
            UIPanelENT = UIPanelENTFR ;
            InformationPannelENT = InformationPannelENTFR ;

            SetUIText();
            ENTInformation();
        }

        if(PlayerPrefs.GetInt("Langue") == 1 && UIPanelENT != UIPanelENTEN)
        {
            UIPanelENT = UIPanelENTEN ;
            InformationPannelENT = InformationPannelENTEN ;

            SetUIText();
            ENTInformation();
        }
    }
    
    void SetUIText()
    {
        TitleQuiSommesNous.text = UIPanelENT[0] ;
        TitleValeurs.text = UIPanelENT[1] ;
        Contact.text = UIPanelENT[2] ;
        NoteSiteWebTitle.text = UIPanelENT[3] ;
        TitleNosProduction.text = UIPanelENT[4] ;
    }

    void ENTInformation()
    {
        DesciptionEntreprise.text = InformationPannelENT.DescriptionEntreprise ;
        DescriptionValeur.text = InformationPannelENT.Valeurs ;

        TitleLastProduction.text = InformationPannelENT.TitreDernièreProd ;
        DescriptionLastProduction.text = InformationPannelENT.DescriptionDernièreProd ;
        TitlePreLastProduction.text = InformationPannelENT.TitreAvantDernièreProd ;
        DescriptionPreLastProduction.text = InformationPannelENT.DescriptionAvantDernièreProd ;
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
        IllustrationValeurENT.sprite = InformationENT.IllustrationValeurENT ;
        IllustrationDescriptionDernierProjet.sprite = InformationENT.IllustrationDescriptionDernierProjet ;


        if(TitlePreLastProduction.text != "") IllustrationDescriptionAvantDernierProjet.sprite = InformationENT.IllustrationDescriptionAvantDernierProjet ;

    }

    public void SwitchTogglePannelDisplay()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void OpenWebSite()
    {
        if(InformationENT.URLSiteWeb != "")
            Application.OpenURL(InformationENT.URLSiteWeb);
    }
}
