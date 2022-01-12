using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI NomEntreprise ;

    [SerializeField] private TextMeshProUGUI TitleDescriptionENT ;
    [SerializeField] private TextMeshProUGUI DesciptionEntreprise ;
    [SerializeField] private TextMeshProUGUI TitleValeurs ;
    [SerializeField] private TextMeshProUGUI DescriptionValeur ;

    [SerializeField] private TextMeshProUGUI Contact ;
    [SerializeField] private TextMeshProUGUI NoteSiteWeb ;

    [SerializeField] private TextMeshProUGUI TitleProduction ;
    [SerializeField] private TextMeshProUGUI TitleLastProduction ;
    [SerializeField] private TextMeshProUGUI TitleLastDescriptionProduction ;
    [SerializeField] private TextMeshProUGUI TitlePreLastProduction ;
    [SerializeField] private TextMeshProUGUI TitlePreLastDescriptionProduction ;

    [Header ("List de Données")]
    public List<string> UIPanelENTFR ;
    public List<string> UIPanelENTEN ;
    private List<string> UIPanelENT ;

    public UIPanelENTContainer InformationPannelENTFR ;
    public UIPanelENTContainer InformationPannelENTEN ;
    private UIPanelENTContainer InformationPannelENT ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SetUIText()
    {
        TitleDescriptionENT.text = UIPanelENT[0] ;
        TitleValeurs.text = UIPanelENT[1] ;
        Contact.text = UIPanelENT[2] ;
        NoteSiteWeb.text = UIPanelENT[3] ;
        TitleProduction.text = UIPanelENT[4] ;
    }

    public void SwitchTogglePannelDisplay()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
