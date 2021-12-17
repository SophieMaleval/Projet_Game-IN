using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDialogue : MonoBehaviour
{
    [Header ("Inputs")]
    private PlayerActionControls PlayerActionControllers ;

    public CSVReader TextDialogueFR ;
    //public CSVReader TextDialogueEN ;

    public bool PlayerAsRead = false ;
    public int CurrentSelectQuestion = 0 ;


    private void OnEnable() {   PlayerActionControllers.Disable(); }
    private void OnDisable() { PlayerActionControllers.Disable(); }

    public void DialogueStart() { PlayerActionControllers.Enable(); ResetCurrentSelectQuestion(); }
    public void DialogueEnd() { PlayerActionControllers.Disable(); }

    private void Awake() 
    {
        TextDialogueFR = GetComponent<CSVReader>() ;

        PlayerActionControllers  = new PlayerActionControls();
        PlayerActionControllers.PlayerInDialogue.ValidateChoice.performed += OnValidateChoice ;
        PlayerActionControllers.PlayerInDialogue.SelectPreviousQuestion.performed += OnSelectPreviousQuestion ;
        PlayerActionControllers.PlayerInDialogue.SelectNextQuestion.performed += OnSelectNextQuestion ;
    }



    void ResetCurrentSelectQuestion()
    {
        CurrentSelectQuestion = 0 ;
    }

    public void OnValidateChoice (InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            PlayerAsRead = true ;
        }
    }

    // Faire la sélection de question avec des Pass Throught

    public void OnSelectPreviousQuestion(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            SetQuestionSelect(true);
        }
    }
    public void OnSelectNextQuestion(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            SetQuestionSelect(false);
        }
    }


    public void ResetSelectQuestion()
    {
        CurrentSelectQuestion = 0 ;
    }

    private void SetQuestionSelect(bool IsAdd)
    {
        if(GameObject.Find("Dialogue Canvas") != null)
        {
            DialogueDisplayerController DialogueBox = GameObject.Find("Dialogue Canvas").GetComponent<DialogueDisplayerController>() ;
            // Choix Précédent
            if(!IsAdd)
            {
                if((CurrentSelectQuestion + 1) > DialogueBox.SetQuestionDisponible().Count - 1)
                {
                    CurrentSelectQuestion = 0 ;
                } else {
                    CurrentSelectQuestion ++ ;
                }
            } else {
                if((CurrentSelectQuestion - 1 < 0 ))
                {
                    CurrentSelectQuestion = DialogueBox.SetQuestionDisponible().Count - 1;
                } else {
                    CurrentSelectQuestion -- ;
                }
            }            
        }

    }





}