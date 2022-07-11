using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI ;

public class ControlsDIsplayer : MonoBehaviour
{
    #region Fields

    private int CurrentControls = 0;

    private List<string> ListTitleControl = new List<string>() ;

    #endregion

    #region UnityInspector

    [SerializeField] private TextMeshProUGUI TitleControls ;
    [SerializeField] private List<GameObject> ControlsGroup ;
    [SerializeField] private Transform ControlsParent ;

    public CSVReader TextUILocation ;

    [Header ("Sprite Effect")]
    public Color NormalColor ;
    public Color SelectedColor ;
    

    [Header ("Text")]
    [SerializeField] private TextMeshProUGUI PrincipaleTitle ;
    [SerializeField] private TextMeshProUGUI TitleControl ;
    [SerializeField] private List<TextMeshProUGUI> MoveInput ;
    [SerializeField] private List<TextMeshProUGUI> InteractionInput ;
    [SerializeField] private List<TextMeshProUGUI> InventoryInput ;
    [SerializeField] private List<TextMeshProUGUI> ScooterInput ;

    #endregion

    #region Behaviour

    private void OnEnable() {
        if(TextUILocation == null)
            TextUILocation = GameManager.Instance.player.playerBackpack.GetComponent<CSVReader>() ;

        SetLangue(PlayerPrefs.GetInt("Langue"));
        ChangeControlsDisplay();
    }



    public void OpenXControls(int ControlsX)
    {
        CurrentControls = ControlsX ;

        ChangeControlsDisplay();
    }


    public void PreviousControls()
    {
        if(CurrentControls > 0)
        {
            CurrentControls -- ;            
        } else {
            CurrentControls = ControlsGroup.Count-1 ;            
        }

        ChangeControlsDisplay();
    }

    public void NextControls()
    {
        if(CurrentControls < ControlsGroup.Count-1)
        {
            CurrentControls ++ ;            
        } else {
            CurrentControls = 0 ;            
        }

        ChangeControlsDisplay();
    }

    void ChangeControlsDisplay()
    {
        for (int CC = 0; CC < ControlsGroup.Count; CC++)
        {
            if(CC == CurrentControls)
            {
                ControlsGroup[CC].SetActive(true);
                TitleControl.text = ListTitleControl[CC];
                ControlsParent.GetChild(CC).GetComponent<Button>().interactable = false ;
                ControlsParent.GetChild(CC).GetComponent<Image>().color = SelectedColor ;
                ControlsParent.GetChild(CC).localScale = new Vector3(0.75f, 0.75f, 0.75f) ;
            } else {
                ControlsGroup[CC].SetActive(false);
                ControlsParent.GetChild(CC).GetComponent<Button>().interactable = true ;
                ControlsParent.GetChild(CC).GetComponent<Image>().color = NormalColor ;
                ControlsParent.GetChild(CC).localScale = Vector3.one;
            } 
        }
    }

    void SetLangue(int Langue)
    {
        List<string> TextLangue = new List<string>() ;

        if(Langue == 0) TextLangue = TextUILocation.UIText.ControlFR ; 
        if(Langue == 1) TextLangue = TextUILocation.UIText.ControlEN ; 

            PrincipaleTitle.text = TextLangue[0] ;
            TitleControl.text = TextLangue[1] ;
                MoveInput[0].text = TextLangue[2] ;
                MoveInput[1].text = TextLangue[3] ;
                MoveInput[2].text = TextLangue[4] ;
                MoveInput[3].text = TextLangue[5] ;
            InteractionInput[0].text = TextLangue[7] ;
            InteractionInput[1].text = TextLangue[8] ;
                InventoryInput[0].text = TextLangue[10] ;
                InventoryInput[1].text = TextLangue[11] ;
            ScooterInput[0].text = TextLangue[13];
            ScooterInput[1].text = TextLangue[14];

            ListTitleControl.Clear();
                ListTitleControl.Add(TextLangue[1]);
                ListTitleControl.Add(TextLangue[6]);
                ListTitleControl.Add(TextLangue[9]);
                ListTitleControl.Add(TextLangue[12]);
    }

    #endregion
}
