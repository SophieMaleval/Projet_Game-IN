using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using System.Text;

public class DialogueDisplayerController : MonoBehaviour
{
    private bool MouseIsHover ;
    
    [HideInInspector] public PNJDialogue CurrentPNJDiscussion ;
    public TextMeshProUGUI DialogueCanvas ;

    [HideInInspector] public DialogueContainer DialoguePNJ ;
    [SerializeField] private PlayerDialogue PlayerDialogueManager;

    [HideInInspector] public int Question3IntDisplay = 3;
    public GameObject BoxQuestion ;

    
    [HideInInspector] 
    public List<string> QuestionDisponible ;
    [HideInInspector] 
    public List<string> AnswerDisponible ;


    private bool Question1AsRead = false ;
    private bool Question2AsRead = false ;
    private bool Question3AsRead = false ;


    private bool PNJSpeak = false ;
    private int CurrentDialogueDisplay = 0 ;
    
    private int CurrentDialogueLength = 0 ;
    private int CurrentDialogueState = 0 ;



    private int CurrentDialoguePlayerChoice = 0 ;
    private bool ChoiceValidation = false ;

    [HideInInspector] public bool WeAreInChoice = false ;
    private float DelayAnimationText = 0.1f ;

    private bool TextAsCompletelyDisplay = true ;
    private bool TextOppeningDisplayCompletely = false ;
    private bool TextCloseDisplayCompletely = true ;

    [System.Serializable]    
    public class SerializableAnswer
    {
        public List<int> AnswerForQuestion ; 
    }



    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    
            PlayerDialogueManager = GameObject.Find("Player Backpack").GetComponent<PlayerDialogue>() ; 
        }    
    } 
    

    void FixedUpdate()
    {       
        if(PlayerDialogueManager != null)
        {
            if(CurrentDialoguePlayerChoice != PlayerDialogueManager.CurrentSelectQuestion )
            {
                CurrentDialoguePlayerChoice = PlayerDialogueManager.CurrentSelectQuestion  ;   
            }

            SelectedButton();                  
        }

        if(EventSystem.current.IsPointerOverGameObject()) // Regarde si la souris survol le champ de text
        {
            MouseIsHover = true ;
        } else {
            MouseIsHover = false ;
        }
    }



    public void StartDiscussion(bool TextState)
    {
        Question1AsRead = false ;
        Question2AsRead = false ;
        Question3AsRead = false ;

        DialogueCanvas.transform.parent.gameObject.SetActive(true) ;  
        if(!TextState)
        {
            StopAllCoroutines(); 
            DialogueCanvas.text = "";
            StartCoroutine(WriteText(DialoguePNJ.OpeningDialogue, DialogueCanvas, DelayAnimationText));     
            TextAsCompletelyDisplay = false ;       
        } else {
            StopAllCoroutines();
            DialogueCanvas.text = DialoguePNJ.OpeningDialogue ;
            TextAsCompletelyDisplay = true ;
            TextOppeningDisplayCompletely = true ;
        }


        CurrentDialogueDisplay = 0 ;
    }

    public void StateDiscussion()
    {
        if(DialogueCanvas.text == DialoguePNJ.OpeningDialogue) ShowDialogueChoice();
   
        if(CurrentDialogueDisplay == 0 && !TextAsCompletelyDisplay && !TextOppeningDisplayCompletely )    StartDiscussion(true); // Arrête l'animation et Affiche tout le texte d'Openning


        if(DialogueCanvas.text == DialoguePNJ.CloseDiscussion) CurrentPNJDiscussion.DiscussionIsClose();

        if(CurrentDialogueDisplay == 0 && !TextAsCompletelyDisplay && !TextCloseDisplayCompletely )    CloseDiscussion(true);    // Arrête l'animation et Affiche tout le texte de Fermeture  

        if(PNJSpeak)
        {
            if(CurrentDialogueState < CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion.Count )
                if(!TextAsCompletelyDisplay) TextDiscussion(false, CurrentDialogueDisplay, CurrentDialogueState + 1, true); // Arrête l'animation et affiche tout le texte
                else TextDiscussion(false, CurrentDialogueDisplay, CurrentDialogueState + 1, false); // Affiche le prochain texte avec l'animation
            else
                ShowDialogueChoice();
        }
    }

    void SwitchBoxDisplay()
    {
        DialogueCanvas.gameObject.SetActive(!DialogueCanvas.gameObject.activeSelf);
        BoxQuestion.SetActive(!BoxQuestion.activeSelf); 
    }

    void SetQuestion(bool QuestionAsRead, int ChildNum, string DialogueText)
    {
        if(!QuestionAsRead)
        {
            BoxQuestion.transform.GetChild(ChildNum).gameObject.SetActive(true) ;
            BoxQuestion.transform.GetChild(ChildNum).GetComponent<TextMeshProUGUI>().text = DialogueText ;            
        } else {
            BoxQuestion.transform.GetChild(ChildNum).GetComponent<TextMeshProUGUI>().text = " " ;                   
            BoxQuestion.transform.GetChild(ChildNum).gameObject.SetActive(false) ;
        }
    }

    public void ShowDialogueChoice()
    {
        SwitchBoxDisplay();
        PlayerDialogueManager.ResetSelectQuestion();
        GetComponent<Button>().interactable = false ;
        TextOppeningDisplayCompletely = true ;        
        WeAreInChoice = true ;

        /* Afficher les Questions à afficher */
        SetQuestion(Question1AsRead, 0, DialoguePNJ.Question1); // Set QUestion 1
        SetQuestion(Question2AsRead, 1, DialoguePNJ.Question2); // Set QUestion 2

        if(Question3IntDisplay != 0)
        {
            SetQuestion(Question3AsRead, 2, QuestionDisponible[(int) Question3IntDisplay]); // Set QUestion 3
        } else {
            SetQuestion(false, 2, QuestionDisponible[(int) Question3IntDisplay]) ; // Set QUestion 3
        }

        SetQuestion(false, 3, DialoguePNJ.Aurevoir); // Set QUestion 4

        SelectedButton();
    }

    void ResetDialogueContinuationValue(int NumDialogueList)
    {
        PNJSpeak = true ;    
        CurrentDialogueDisplay = NumDialogueList - 1;

        CurrentDialogueState = 0 ;
    }

    void TextDiscussion(bool ToggleDisplayBox , int DialogueDisplay, int StateAnswer, bool TextState)
    {
        GetComponent<Button>().interactable = true ;
        WeAreInChoice = false ;

        if(ToggleDisplayBox)    SwitchBoxDisplay();

        if(!TextState)
        {
            StopAllCoroutines();
            DialogueCanvas.text = "";
            StartCoroutine(WriteText(AnswerDisponible[CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]], DialogueCanvas, DelayAnimationText));
            //CurrentDialogueState ++ ;
            TextAsCompletelyDisplay = false ;
        } else {
            StopAllCoroutines();
            DialogueCanvas.text = AnswerDisponible[CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]];
            CurrentDialogueState ++ ;
            TextAsCompletelyDisplay = true ;
        }

    }

    void CloseDiscussion(bool TextState)
    {
        GetComponent<Button>().interactable = true ;
        WeAreInChoice = false ;

        if(!TextState)
        {
            SwitchBoxDisplay();

            StopAllCoroutines();
            DialogueCanvas.text = ""; 
            StartCoroutine(WriteText(DialoguePNJ.CloseDiscussion, DialogueCanvas, DelayAnimationText));
            TextAsCompletelyDisplay = false ;
        } else {
            StopAllCoroutines();
            DialogueCanvas.text = DialoguePNJ.CloseDiscussion; 
            TextAsCompletelyDisplay = true ;
        }
        PNJSpeak = false ;
    }


    protected IEnumerator WriteText(string Input, TextMeshProUGUI TextHolder, float Delay) 
    {
        for (int i = 0; i < Input.Length; i++)
        {
            TextHolder.text += Input[i] ;
            yield return new WaitForSeconds(Delay);
        }

        CurrentDialogueState ++ ;
        TextAsCompletelyDisplay = true ;
    }




    public void ButtonChoix1()
    {
        ResetDialogueContinuationValue(1);        
        TextDiscussion(true, 1, 0, false);

        if(Question1AsRead == false) Question1AsRead = true ;
    }

    public void ButtonChoix2()
    {
        ResetDialogueContinuationValue(2);
        TextDiscussion(true, 2, 0, false);

        if(Question2AsRead == false) Question2AsRead = true ;
    }

    public void ButtonChoix3()
    {
        ResetDialogueContinuationValue((int) Question3IntDisplay);
        TextDiscussion(true, (int) Question3IntDisplay, 0, false);

        if(Question3AsRead == false) Question3AsRead = true ;
    }

    public void ButtonChoix4()
    {
        TextCloseDisplayCompletely = false ;
        CurrentDialogueDisplay = 0 ;
        CloseDiscussion(false);
    }





    // Utilisation de touche pour les boutons
    void SelectedButton()
    {
        if(MouseIsHover == false)
        {
            for (int BQC = 0; BQC < SetQuestionDisponible().Count; BQC++)
            {
                Button QuestionCurrent = SetQuestionDisponible()[BQC] ;                
                QuestionCurrent.interactable = false ; 

                if(BQC == CurrentDialoguePlayerChoice)
                {        
                   QuestionCurrent.GetComponent<TextMeshProUGUI>().color = QuestionCurrent.colors.highlightedColor;
                } else {
                    QuestionCurrent.GetComponent<TextMeshProUGUI>().color = QuestionCurrent.colors.normalColor;    
                }
            }                
        } else {
            for (int BQC = 0; BQC < BoxQuestion.transform.childCount; BQC++)
            {
                Button QuestionCurrent = BoxQuestion.transform.GetChild(BQC).GetComponent<Button>() ;                
                QuestionCurrent.interactable = true ; 
                QuestionCurrent.GetComponent<TextMeshProUGUI>().color = QuestionCurrent.colors.disabledColor;    
            }
        }

    }

    public List<Button> SetQuestionDisponible()
    {
        List<Button> ActiveButton = new List<Button>();
        for (int BD = 0; BD < BoxQuestion.transform.childCount; BD++)
        {
            if(BoxQuestion.transform.GetChild(BD).gameObject.activeSelf == true)
                ActiveButton.Add(BoxQuestion.transform.GetChild(BD).gameObject.GetComponent<Button>()) ;
        }

        return ActiveButton;
    }

    public void ValidateButton()
    {
        PlayerDialogueManager.PlayerAsRead = false ;
        
        SetQuestionDisponible()[CurrentDialoguePlayerChoice].onClick.Invoke();     
    }


}
