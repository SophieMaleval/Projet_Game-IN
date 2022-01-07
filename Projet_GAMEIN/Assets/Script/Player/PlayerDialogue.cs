using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogueContainer
{
    public string Entreprise ;
    public string Name ;
    public string OpeningDialogue ;        
    public string CloseDiscussion ;        

    /* Les Questions Disponnible */
    public string Question1 ;
    public string Question2 ;
    public string Question3 ;
    public string Question4 ;
    public string Question5 ;
    public string Question6 ;
    public string Question7 ;
    public string Question8 ;
    public string Question9 ;
    public string Question10 ;
    public string Aurevoir ;


    /* Les Dialogue */
    public string Dialogue1 ;
    public string Dialogue2 ;
    public string Dialogue3 ;
    public string Dialogue4 ;
    public string Dialogue5 ;
    public string Dialogue6 ;
    public string Dialogue7 ;
    public string Dialogue8 ;
    public string Dialogue9 ;
    public string Dialogue10 ;
    public string Dialogue11 ;
    public string Dialogue12 ;
    public string Dialogue13 ;
    public string Dialogue14 ;
    public string Dialogue15 ;
    public string Dialogue16 ;
    public string Dialogue17 ;
    public string Dialogue18 ;
    public string Dialogue19 ;
    public string Dialogue20 ;
}

public class PlayerDialogue : MonoBehaviour
{
    [Header ("Inputs")]
    private PlayerActionControls PlayerActionControllers ;

    public List<DialogueContainer> myDialogueAdhérentFR = new List<DialogueContainer>();
    public List<DialogueContainer> myDialogueAdhérentEN = new List<DialogueContainer>();

 
    public bool PlayerAsRead = false ;
    public int CurrentSelectQuestion = 0 ;

    [Header ("Adaptation de texte")]
    [SerializeField] private PlayerScript PlayerInformations ;
    private string[] Pronoms = new string[]{"un", "une", "un.e"};
    private string[] Terminaisons = new string[]{"un", "une", "un.e"};





    private void OnEnable() {   PlayerActionControllers.Disable(); }
    private void OnDisable() { PlayerActionControllers.Disable(); }

    public void DialogueStart() { PlayerActionControllers.Enable(); ResetCurrentSelectQuestion(); }
    public void DialogueEnd() { PlayerActionControllers.Disable(); }

    public void PausedInDialogue() {   PlayerActionControllers.Disable(); }
    public void ResumeDialogue() { PlayerActionControllers.Enable(); }

    private void Awake() 
    {
        //TextDialogueFR = GetComponent<CSVReader>() ;

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