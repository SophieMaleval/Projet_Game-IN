using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI ;

public class ControlsDIsplayer : MonoBehaviour
{
    #region Fields

    private int CurrentControls = 0;

    #endregion

    #region UnityInspector

    [SerializeField] private List<KeyContainerObject> controlsGroup;
    [SerializeField] private Transform ControlsParent ;

    [Header ("Sprite Effect")]
    public Color NormalColor ;
    public Color SelectedColor ;
    

    [Header ("Text")]

    [SerializeField] private ToTranslateObject principalTitle;
    [SerializeField] private ToTranslateObject titleControls;
    [SerializeField] private List<ToTranslateObject> moveInputs;
    [SerializeField] private List<ToTranslateObject> interactionInputs;
    [SerializeField] private List<ToTranslateObject> inventoryInputs;
    [SerializeField] private List<ToTranslateObject> scooterInputs;

    #endregion

    #region Behaviour

    private void OnEnable() {

        SetLangue();
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
            CurrentControls = controlsGroup.Count-1 ;            
        }

        ChangeControlsDisplay();
    }

    public void NextControls()
    {
        if(CurrentControls < controlsGroup.Count-1)
        {
            CurrentControls ++ ;            
        } else {
            CurrentControls = 0 ;            
        }

        ChangeControlsDisplay();
    }

    void ChangeControlsDisplay()
    {
        for (int CC = 0; CC < controlsGroup.Count; CC++)
        {
            if(CC == CurrentControls)
            {
                controlsGroup[CC].gameObject.SetActive(true);
                titleControls.SetTranslationKey(controlsGroup[CC].keyObject);
                ControlsParent.GetChild(CC).GetComponent<Button>().interactable = false ;
                ControlsParent.GetChild(CC).GetComponent<Image>().color = SelectedColor ;
                ControlsParent.GetChild(CC).localScale = new Vector3(0.75f, 0.75f, 0.75f) ;
            } else {
                controlsGroup[CC].gameObject.SetActive(false);
                ControlsParent.GetChild(CC).GetComponent<Button>().interactable = true ;
                ControlsParent.GetChild(CC).GetComponent<Image>().color = NormalColor ;
                ControlsParent.GetChild(CC).localScale = Vector3.one;
            } 
        }
    }

    void SetLangue()
    {
        principalTitle.Translation();
        for (int i = 0; i < moveInputs.Count; i++)
        {
            moveInputs[i].Translation();
        }
        for (int i = 0; i < interactionInputs.Count; i++)
        {
            interactionInputs[i].Translation();
        }
        for (int i = 0; i < inventoryInputs.Count; i++)
        {
            inventoryInputs[i].Translation();
        }
        for (int i = 0; i < scooterInputs.Count; i++)
        {
            scooterInputs[i].Translation();
        }
    }

    #endregion
}
