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
    public TextMeshProUGUI NamePNJ ;

    [HideInInspector] public DialogueContainer DialoguePNJ ;
    [HideInInspector] public DialogueContainer DialoguePNJ_FR ;
    [HideInInspector] public DialogueContainer DialoguePNJ_EN ;
    private PlayerDialogue PlayerDialogueManager;

    [HideInInspector] public int Question3IntDisplay = 3;
    public GameObject BoxQuestion ;

    public AudioSource SoundDialogueSpawning;
    
 



    [HideInInspector] public List<string> QuestionDisponible = new List<string>() ;
        [HideInInspector]  public List<string> QuestionDisponible_FR = new List<string>() ;
        [HideInInspector]  public List<string> QuestionDisponible_EN = new List<string>() ;
    [HideInInspector] public List<string> AnswerDisponible = new List<string>() ;
        [HideInInspector] public List<string> AnswerDisponible_FR = new List<string>() ;
        [HideInInspector]  public List<string> AnswerDisponible_EN = new List<string>() ;


    private bool Question1AsRead = false ;
    private bool Question2AsRead = false ;
    private bool Question3AsRead = false ;


    private bool PNJSpeak = false ;
    private int CurrentDialogueDisplay = 0 ;
    
    private int CurrentDialogueLength = 0 ;
    private int CurrentDialogueState = 0 ;



    private int CurrentDialoguePlayerChoice = 0 ;
    private bool ChoiceValidation = false ;
    private string CurrentDialogue ;
    private bool CanChangeCurrentDialogue = false ;
    
    public AudioSource VoicePnj;

    [HideInInspector] public bool WeAreInChoice = false ;
    private float DelayAnimationText = 0.1f ;

    [SerializeField] private RectTransform PassTextImg ;
    private bool TextAsCompletelyDisplay = true ;
    private bool TextOppeningDisplayCompletely = false ;
    private bool TextCloseDisplayCompletely = true ;

    [System.Serializable]    
    public class SerializableAnswer
    {
        public List<int> AnswerForQuestion ; 
    }

    public void ResetAllValue()
    {

        PNJSpeak = false ;
        CurrentDialogueDisplay = 0 ;
        CurrentDialogueLength = 0 ; 
        CurrentDialogueState = 0 ;
        CurrentDialoguePlayerChoice = 0 ;
        ChoiceValidation = false ;
        WeAreInChoice = false ;
        TextAsCompletelyDisplay = true ;
        TextOppeningDisplayCompletely = false ;
        TextCloseDisplayCompletely = true ;
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



        if(PlayerPrefs.GetInt("Langue") == 0 && DialoguePNJ != DialoguePNJ_FR)
        {
            DialoguePNJ = DialoguePNJ_FR ;   
            AnswerDisponible = AnswerDisponible_FR ;
            QuestionDisponible = QuestionDisponible_FR ;   

            DialogueLanguageChangeDuringDialogue();
        }
        if(PlayerPrefs.GetInt("Langue") == 1 && DialoguePNJ != DialoguePNJ_EN)
        {
            DialoguePNJ = DialoguePNJ_EN ;        
            AnswerDisponible = AnswerDisponible_EN ;
            QuestionDisponible = QuestionDisponible_EN ;   

            DialogueLanguageChangeDuringDialogue();    
        }


        if(TextAsCompletelyDisplay && PassTextImg.gameObject.activeSelf == false)
        {
            
            PassTextImg.gameObject.SetActive(true);
            PassTextImg.anchoredPosition = new Vector2(PassTextImg.anchoredPosition.x, -2.5f);
            StopCoroutine(AnimationPassText());
            StartCoroutine(AnimationPassText());
        }
        if(!TextAsCompletelyDisplay && PassTextImg.gameObject.activeSelf == true) 
        {
            SoundDialogueSpawning.Play();
            PassTextImg.gameObject.SetActive(false);
            StopCoroutine(AnimationPassText());
            StartCoroutine(AnimationPassText());

        }
    }



    IEnumerator AnimationPassText()
    {
        PassTextImg.anchoredPosition = new Vector2(PassTextImg.anchoredPosition.x, PassTextImg.anchoredPosition.y + 2.5f) ;
        yield return new WaitForSeconds(0.5f);
        PassTextImg.anchoredPosition =new Vector2(PassTextImg.anchoredPosition.x, PassTextImg.anchoredPosition.y - 2.5f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AnimationPassText());
    }



    public void StartDiscussion(bool TextState)
    {
        Question1AsRead = false ;
        Question2AsRead = false ;
        Question3AsRead = false ;

        DialogueCanvas.transform.parent.gameObject.SetActive(true) ;
        
        CurrentDialogue = DialoguePNJ.OpeningDialogue ;
        if(!TextState)
        {
            StopAllCoroutines(); 
            DialogueCanvas.text = "";
            StartCoroutine(WriteText(DialoguePNJ.OpeningDialogue, DelayAnimationText));     
            TextAsCompletelyDisplay = false ;       
        } else {
            StopAllCoroutines();
            DialogueCanvas.text = DialoguePNJ.OpeningDialogue ;
            TextAsCompletelyDisplay = true ;
            TextOppeningDisplayCompletely = true ;
        }

        CurrentDialogueDisplay = -1 ;
    }

    public void StateDiscussion()
    {

        

        if(!PlayerDialogueManager.transform.GetComponentInParent<PlayerScript>().InventoryUIIndestructible.GetComponent<InventoryScript>().InventoryPanel.activeSelf)
        {
            CanChangeCurrentDialogue = true ;
           

            if(DialogueCanvas.text == DialoguePNJ.OpeningDialogue) ShowDialogueChoice(true);
            if(CurrentDialogueDisplay == -1 && !TextAsCompletelyDisplay && !TextOppeningDisplayCompletely )    StartDiscussion(true); // Arrête l'animation et Affiche tout le texte d'Openning

            if(DialogueCanvas.text == DialoguePNJ.CloseDiscussion) CurrentPNJDiscussion.DiscussionIsClose();
            if(CurrentDialogueDisplay == -1 && !TextAsCompletelyDisplay && !TextCloseDisplayCompletely )    CloseDiscussion(true, true);    // Arrête l'animation et Affiche tout le texte de Fermeture  

            if(PNJSpeak)
            {
                if(CurrentDialogueState < CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion.Count )
                    if(!TextAsCompletelyDisplay) TextDiscussion(false, true); // Arrête l'animation et affiche tout le texte
                    else TextDiscussion(false, false); // Affiche le prochain texte avec l'animation
                else
                    ShowDialogueChoice(true);            
            }
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
            BoxQuestion.transform.GetChild(ChildNum).GetComponentInChildren<TextMeshProUGUI>().text = DialogueText ;            
        } else {
            BoxQuestion.transform.GetChild(ChildNum).GetComponentInChildren<TextMeshProUGUI>().text = " " ;                   
            BoxQuestion.transform.GetChild(ChildNum).gameObject.SetActive(false) ;
        }
    }

    public void ShowDialogueChoice(bool ToggleDisplayBox)
    {
        if(ToggleDisplayBox)    SwitchBoxDisplay();
        PlayerDialogueManager.ResetSelectQuestion();
        GetComponent<Button>().interactable = false ;
        TextOppeningDisplayCompletely = true ;        
        WeAreInChoice = true ;
        CurrentDialogue = "";
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



    void TextDiscussion(bool ToggleDisplayBox, bool TextState)
    {
        GetComponent<Button>().interactable = true ;
        WeAreInChoice = false ;

        if(ToggleDisplayBox)    SwitchBoxDisplay();

        CurrentDialogue = AnswerDisponible[CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]];
        if(!TextState)
        {
            StopAllCoroutines();
            DialogueCanvas.text = "";
            StartCoroutine(WriteText(AnswerDisponible[CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]], DelayAnimationText));
            //CurrentDialogueState ++ ;
            TextAsCompletelyDisplay = false ;
        } else {
            StopAllCoroutines();
            DialogueCanvas.text = AnswerDisponible[CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]];
            CurrentDialogueState ++ ;
            TextAsCompletelyDisplay = true ;
        }
    }

    void CloseDiscussion(bool ToggleDisplayBox,bool TextState)
    {
        GetComponent<Button>().interactable = true ;
        WeAreInChoice = false ;

        CurrentDialogue = DialoguePNJ.CloseDiscussion ;
        if(!TextState)
        {
            if(ToggleDisplayBox)    SwitchBoxDisplay();

            StopAllCoroutines();
            DialogueCanvas.text = ""; 
            StartCoroutine(WriteText(DialoguePNJ.CloseDiscussion, DelayAnimationText));
            TextAsCompletelyDisplay = false ;
        } else {
            StopAllCoroutines();
            DialogueCanvas.text = DialoguePNJ.CloseDiscussion; 
            TextAsCompletelyDisplay = true ;
        }
        PNJSpeak = false ;
    }


    protected IEnumerator WriteText(string Input, float Delay) 
    {
        for (int i = 0; i < Input.Length; i++)
        {
            DialogueCanvas.text += Input[i] ;
            VoicePnj.Play();
            yield return new WaitForSeconds(Delay);
        }

        CurrentDialogueState ++ ;
        TextAsCompletelyDisplay = true ;
    }




    public void ButtonChoix1()
    {
        ResetDialogueContinuationValue(1);        
        TextDiscussion(true, false);

        if(Question1AsRead == false) Question1AsRead = true ;
    }

    public void ButtonChoix2()
    {
        ResetDialogueContinuationValue(2);
        TextDiscussion(true, false);

        if(Question2AsRead == false) Question2AsRead = true ;
    }

    public void ButtonChoix3()
    {
        ResetDialogueContinuationValue((int) Question3IntDisplay);
        TextDiscussion(true, false);

        CurrentPNJDiscussion.PlayerAskQuestQuestion = true ;

        if(Question3AsRead == false) Question3AsRead = true ;
    }

    public void ButtonChoix4()
    {
        TextCloseDisplayCompletely = false ;
        CurrentDialogueDisplay = -1 ;
        CloseDiscussion(true, false);
    }


    // Traduction à tout moment
    void DialogueLanguageChangeDuringDialogue()
    {
        if(!WeAreInChoice)
        {       
            //bool IsInDialogue = true ; 
            if(CurrentDialogue == DialoguePNJ_FR.OpeningDialogue || CurrentDialogue == DialoguePNJ_EN.OpeningDialogue)
            {
                StartDiscussion(false);
            }

            if(CurrentDialogue == DialoguePNJ_FR.CloseDiscussion || CurrentDialogue == DialoguePNJ_EN.CloseDiscussion)
            {
                CloseDiscussion(false, false);
            }   

            if(PNJSpeak)
            {
                if(!TextAsCompletelyDisplay)
                {
                    TextDiscussion(false, false) ;
                } else {
                    if(CanChangeCurrentDialogue)
                    {
                        CanChangeCurrentDialogue = false ;
                        CurrentDialogueState -- ;                         
                    }

                    
                    TextDiscussion(false, false) ;
                }
            }             
        } else {
            ShowDialogueChoice(false);
        }      
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
                    QuestionCurrent.transform.GetChild(0).GetComponent<Image>().color = QuestionCurrent.colors.highlightedColor;
                } else {
                    QuestionCurrent.transform.GetChild(0).GetComponent<Image>().color = QuestionCurrent.colors.normalColor;    
                }
            }                
        } else {
            for (int BQC = 0; BQC < BoxQuestion.transform.childCount; BQC++)
            {
                Button QuestionCurrent = BoxQuestion.transform.GetChild(BQC).GetComponent<Button>() ;                
                QuestionCurrent.interactable = true ; 
                QuestionCurrent.transform.GetChild(0).GetComponent<Image>().color = QuestionCurrent.colors.disabledColor;    
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
