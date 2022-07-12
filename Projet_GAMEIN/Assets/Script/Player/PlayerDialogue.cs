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
    #region Fields

    private PlayerActionControls PlayerActionControllers ;

    #endregion

    #region UnityInspector

    public AudioSource SelectingQuestion;
    
    public AudioSource ValidatingChoiceSound;
    [Header ("Inputs")]

    [HideInInspector] public List<DialogueContainer> myDialogueAdhérentFR = new List<DialogueContainer>();
    [HideInInspector] public List<DialogueContainer> myDialogueAdhérentEN = new List<DialogueContainer>();

 
    [HideInInspector] public bool PlayerAsRead = false ;
    [HideInInspector] public int CurrentSelectQuestion = 0 ;

    [HideInInspector] public bool CanPassDialogue = true ;

    #endregion

    #region Behaviour

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

    void PlayingSelectingChoices(){
        SelectingQuestion.Play();        
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
            if(GameManager.Instance.gameCanvasManager.dialogCanvas != null) PlayingSelectingChoices();
            SetQuestionSelect(true);
        }
    }
    public void OnSelectNextQuestion(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if(GameManager.Instance.gameCanvasManager.dialogCanvas != null) PlayingSelectingChoices();
            SetQuestionSelect(false);
        }
    }


    public void ResetSelectQuestion()
    {
        CurrentSelectQuestion = 0 ;
    }

    private void SetQuestionSelect(bool IsAdd)
    {
        if(GameManager.Instance.gameCanvasManager.dialogCanvas != null || GameManager.Instance.gameCanvasManager.qcmPanel != null)
        {
            int RefChildCount = 0 ;     

            if(GameManager.Instance.gameCanvasManager.dialogCanvas != null)
            {
                DialogueDisplayerController DialogueBox = GameManager.Instance.gameCanvasManager.dialogCanvas;
                RefChildCount = DialogueBox.SetQuestionDisponible().Count - 1 ;            
            }                   
            
            if(GameManager.Instance.gameCanvasManager.qcmPanel != null)
            {
                QCMManager QCMManagement = GameManager.Instance.gameCanvasManager.qcmPanel;
                RefChildCount = QCMManagement.SetButtonDisponnible().Count - 1 ;          
            } 
            
            // Choix Précédent
            if(!IsAdd)
            {
                if((CurrentSelectQuestion + 1) > RefChildCount)
                {
                    CurrentSelectQuestion = 0 ;
                } else {
                    CurrentSelectQuestion ++ ;
                }
            } else {
                if((CurrentSelectQuestion - 1 < 0 ))
                {
                    CurrentSelectQuestion = RefChildCount;
                } else {
                    CurrentSelectQuestion -- ;
                }
            }            
        }

    }



    public void DialoguePass()
    {
        StopAllCoroutines();
        StartCoroutine(TimeBeforeResetPass());
    }

    IEnumerator TimeBeforeResetPass()
    {
        CanPassDialogue = false ;
        yield return new WaitForSeconds(0.1f);
        CanPassDialogue = true ;
    }

    #endregion

}