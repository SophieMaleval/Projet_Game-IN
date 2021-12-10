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
    [HideInInspector]
    public bool MouseIsHover ;
    
    public PNJDialogue CurrentPNJDIscussion ;
    public TextMeshProUGUI DialogueCanvas ;

    public DialogueContainer DialoguePNJ ;
    [SerializeField] private PlayerDialogue PlayerDialogueManager;

    public Vector4 QuestionDisplay = new Vector4(1, 2, 3, 0);
    public GameObject BoxQuestion ;
    public List<string> QuestionDisponible ;
    public List<string> AnswerDisponible ;

    public bool Question1AsRead = false ;
    public bool Question2AsRead = false ;
    public bool Question3AsRead = false ;


    private bool PNJSpeak = false ;
    private int CurrentDialogueDisplay = 0 ;
    //private int CurrentDialogueStateDisplay = 0 ;
    
    private int CurrentDialogueLength = 0 ;
    private int CurrentDialogueState = 0 ;



    private int CurrentDialoguePlayerChoice = 0 ;
    private bool ChoiceValidation = false ;

    public bool WeAreInChoice = false ;
    private float DelayAnimationText = 0.1f ;

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



    public void StartDiscussion()
    {
        Question1AsRead = false ;
        Question2AsRead = false ;
        Question3AsRead = false ;

        DialogueCanvas.transform.parent.gameObject.SetActive(true) ;  
        StopAllCoroutines(); 
        DialogueCanvas.text = ""; //CurrentPNJDIscussion.DialoguePNJ.OpeningDialogue ;
        StartCoroutine(WriteText(CurrentPNJDIscussion.DialoguePNJ.OpeningDialogue, DialogueCanvas, DelayAnimationText));

        CurrentDialogueDisplay = 0 ;
    }

    public void StateDiscussion()
    {
        if(DialogueCanvas.text == CurrentPNJDIscussion.DialoguePNJ.OpeningDialogue)
        {
            ShowDialogueChoice();
        }

        if(DialogueCanvas.text == CurrentPNJDIscussion.DialoguePNJ.CloseDiscussion)
        {
            CurrentPNJDIscussion.DiscussionIsClose();
        }


        if(PNJSpeak)
        {
            if(CurrentDialogueState < CurrentPNJDIscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion.Count )
                TextDiscussion(false, CurrentDialogueDisplay, CurrentDialogueState + 1);
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
        WeAreInChoice = true ;

        /* Afficher les Questions à afficher */
        SetQuestion(Question1AsRead, 0, CurrentPNJDIscussion.DialoguePNJ.Question1); // Set QUestion 1
        SetQuestion(Question2AsRead, 1, CurrentPNJDIscussion.DialoguePNJ.Question2); // Set QUestion 2

        if(QuestionDisplay.z != 0)
        {
            SetQuestion(Question3AsRead, 2, QuestionDisponible[(int) QuestionDisplay.z]); // Set QUestion 3
        } else {
            SetQuestion(false, 2, QuestionDisponible[(int) QuestionDisplay.z]) ; // Set QUestion 3
        }

        SetQuestion(false, 3, CurrentPNJDIscussion.DialoguePNJ.Aurevoir); // Set QUestion 4

        SelectedButton();
    }

    void ResetDialogueContinuationValue(int NumDialogueList)
    {
        PNJSpeak = true ;    
        CurrentDialogueDisplay = NumDialogueList - 1;

        CurrentDialogueState = 0 ;
    }

    void TextDiscussion(bool ToggleDisplayBox , int DialogueDisplay, int StateAnswer)
    {
        GetComponent<Button>().interactable = true ;
        WeAreInChoice = false ;

        if(ToggleDisplayBox)
            SwitchBoxDisplay();
   
        StopAllCoroutines();
        DialogueCanvas.text = "";//AnswerDisponible[CurrentPNJDIscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]] ; 
        StartCoroutine(WriteText(AnswerDisponible[CurrentPNJDIscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]], DialogueCanvas, DelayAnimationText));
        CurrentDialogueState ++ ;
    }

    void CloseDiscussion()
    {
        GetComponent<Button>().interactable = true ;
        WeAreInChoice = false ;

        SwitchBoxDisplay();

        StopAllCoroutines();
        DialogueCanvas.text = ""; //CurrentPNJDIscussion.DialoguePNJ.CloseDiscussion ;
        StartCoroutine(WriteText(CurrentPNJDIscussion.DialoguePNJ.CloseDiscussion, DialogueCanvas, DelayAnimationText));

        PNJSpeak = false ;
    }


    protected IEnumerator WriteText(string Input, TextMeshProUGUI TextHolder, /*Color TextColor, Font TextFont, */float Delay) 
    {
        for (int i = 0; i < Input.Length; i++)
        {
            TextHolder.text += Input[i] ;
            yield return new WaitForSeconds(Delay);
        }
    }




    public void ButtonChoix1()
    {
        ResetDialogueContinuationValue(1);        
        TextDiscussion(true, 1, 0);



        if(Question1AsRead == false) Question1AsRead = true ;
    }

    public void ButtonChoix2()
    {
        ResetDialogueContinuationValue(2);
        TextDiscussion(true, 2, 0);

        if(Question2AsRead == false) Question2AsRead = true ;
    }

    public void ButtonChoix3()
    {
        ResetDialogueContinuationValue((int) QuestionDisplay.z);
        TextDiscussion(true, (int) QuestionDisplay.z, 0);

        if(Question3AsRead == false) Question3AsRead = true ;
    }

    public void ButtonChoix4()
    {
        CloseDiscussion();
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
