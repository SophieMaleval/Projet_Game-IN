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
    #region Fields

    private bool KeyPressed = false ;
    private Vector2 MousePos ;
    private Vector2 OldMousePos ; 
    
    private RectTransform ThisRect ;

    private QuestSys QuestSysManager ;

    private PlayerScript PlayerFadeScript ;
    private PlayerDialogue PlayerDialogueManager;


    private bool Question1AsRead, Question2AsRead, Question3AsRead, Question4AsRead, Question5AsRead, Question6AsRead, Question7AsRead, Question8AsRead, Question9AsRead, Question10AsRead = false;

    private bool PNJSpeak = false;

    private int CurrentDialogueLength = 0;

    private int CurrentDialoguePlayerChoice = 0;
    private bool ChoiceValidation = false;
    private string CurrentDialogue;
    private bool CanChangeCurrentDialogue = false;

    private float DelayAnimationText = 0.025f;

    private bool TextAsCompletelyDisplay = true;
    private bool TextOppeningDisplayCompletely = false;
    private bool TextCloseDisplayCompletely = true;

    private bool CanPass = true;

    #endregion

    #region UnityInspector

    [HideInInspector] public bool MouseIsHover ;

    public PNJDialogue CurrentPNJDiscussion ;
    public TextMeshProUGUI DialogueCanvas ;
    public TextMeshProUGUI NamePNJ ;

    public DialogueContainer DialoguePNJ ;
    public DialogueContainer DialoguePNJ_FR ;
    public DialogueContainer DialoguePNJ_EN ;

    public GameObject BoxQuestion ;
    public Color32 QuestionClassicColor ;
    public Color32 QuestionQuestColor ;


    public AudioSource SoundDialogueSpawning;

    [HideInInspector] public List<string> QuestionDisponible = new List<string>() ;
        [HideInInspector]  public List<string> QuestionDisponible_FR = new List<string>() ;
        [HideInInspector]  public List<string> QuestionDisponible_EN = new List<string>() ;
    [HideInInspector] public List<string> AnswerDisponible = new List<string>() ;
        [HideInInspector] public List<string> AnswerDisponible_FR = new List<string>() ;
        [HideInInspector]  public List<string> AnswerDisponible_EN = new List<string>() ;

    [HideInInspector] public float PNJQuestValueValidation = 0 ;
    
    public int CurrentDialogueDisplay = 0 ;

    public int CurrentDialogueState = 0 ;

    public AudioSource VoicePnj;

    [HideInInspector] public bool WeAreInChoice = false ;

    [SerializeField] private RectTransform PassTextImg ;

    #endregion

    #region Class

    [System.Serializable]
    public class SerializableAnswer
    {
        public List<int> AnswerForQuestion;
    }

    #endregion

    #region Behaviour

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

        PlayerFadeScript.AnimationBeMake = false ;
    }

    private void Awake() 
    {
        ThisRect = gameObject.GetComponent<RectTransform>() ;
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    
            PlayerFadeScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; 
            PlayerDialogueManager = GameObject.Find("Player Backpack").GetComponent<PlayerDialogue>() ; 
        }    
        QuestSysManager = GameObject.Find("QuestManager").GetComponent<QuestSys>();

    } 

    public void SetPNJNameWidth(string Name)
    {
        int NumOfChar = Name.Length ;
        NamePNJ.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2( (NumOfChar*14f) + 10f, NamePNJ.transform.parent.GetComponent<RectTransform>().sizeDelta.y) ;
    }

    

    void FixedUpdate()
    {       
        if(PlayerDialogueManager != null)
        {
            if(CurrentDialoguePlayerChoice != PlayerDialogueManager.CurrentSelectQuestion )
            {
                CurrentDialoguePlayerChoice = PlayerDialogueManager.CurrentSelectQuestion  ;   
               // KeyPressed = true ;                
            }

            SelectedButton();                  
        }

     /*   if(EventSystem.current.IsPointerOverGameObject()) // Regarde si la souris survol le champ de text
        {
            MouseIsHover = true ;
        } else {
            MouseIsHover = false ;
        }*/



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
        MouseIsHover = false ;
        Question1AsRead = false ;
        Question2AsRead = false ;
        Question3AsRead = false ;
        Question4AsRead = false ;
        Question5AsRead = false ;
        Question6AsRead = false ;
        Question7AsRead = false ;
        Question8AsRead = false ;
        Question9AsRead = false ;
        Question10AsRead = false ;
        PNJQuestValueValidation = 0 ;

        DialogueCanvas.transform.parent.gameObject.SetActive(true) ;
        
        CurrentDialogue = DialoguePNJ.OpeningDialogue ;
        if(!TextState)
        {
            StopAllCoroutines(); 
            DialogueCanvas.text = "";
            StartCoroutine(WriteText(DialoguePNJ.OpeningDialogue, DelayAnimationText));     
            TextAsCompletelyDisplay = false ;       
        } else {
            CurrentPNJDiscussion.PNJTalkAnimation(false);
            StopAllCoroutines();
            DialogueCanvas.text = DialoguePNJ.OpeningDialogue ;
            TextAsCompletelyDisplay = true ;
            TextOppeningDisplayCompletely = true ;
        }

        CurrentDialogueDisplay = -1 ;
    }

    public void StateDiscussionAfterFade()
    {
        TextDiscussion(false, false);
    }

    public void StateDiscussion()
    {
        if(!PlayerDialogueManager.transform.GetComponentInParent<PlayerScript>().InventoryUIIndestructible.GetComponent<InventoryScript>().InventoryPanel.activeSelf && this.gameObject.activeSelf)
        {
            CanChangeCurrentDialogue = true ;

            if(DialogueCanvas.text == DialoguePNJ.CloseDiscussion) CurrentPNJDiscussion.DiscussionIsClose();
            if(CurrentDialogueDisplay == -1 && !TextAsCompletelyDisplay && !TextCloseDisplayCompletely )    CloseDiscussion(true, true);    // Arrête l'animation et Affiche tout le texte de Fermeture  

            if(PNJSpeak)
            {
                if(CurrentPNJDiscussion.DiscussionWithQuestion)
                {
                    if(CurrentDialogueState < CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion.Count )
                    {
                        if(!TextAsCompletelyDisplay/*) /*if(*/ && PlayerDialogueManager.CanPassDialogue) TextDiscussion(false, true); // Arrête l'animation et affiche tout le texte
                        else {
                            if(CheckIfNeedLunchFade()) PlayerFadeScript.LunchAnimationFadeIn();
                            else if(PlayerDialogueManager.CanPassDialogue) TextDiscussion(false, false); // Affiche le prochain texte avec l'animation       
                        }             
                    } else {
                        ShowDialogueChoice(true);
                    }                  
                } else {
                    if(CurrentDialogueState < CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion.Count )
                    {
                        if(!TextAsCompletelyDisplay/*) if(*/ && PlayerDialogueManager.CanPassDialogue) TextDiscussion(false, true); // Arrête l'animation et affiche tout le texte
                        else {
                            if(CheckIfNeedLunchFade()) PlayerFadeScript.LunchAnimationFadeIn();
                            else if(PlayerDialogueManager.CanPassDialogue) TextDiscussion(false, false); // Affiche le prochain texte avec l'animation      
                        }         
                    } else ButtonClose();                           
                }
            }
            
            if(DialogueCanvas.text == DialoguePNJ.OpeningDialogue) 
            {   
                if(CurrentPNJDiscussion.DiscussionWithQuestion)    ShowDialogueChoice(true);
                if(!CurrentPNJDiscussion.DiscussionWithQuestion)
                {
                    ResetDialogueContinuationValue(1);        
                    if(PlayerDialogueManager.CanPassDialogue) TextDiscussion(true, false);
                }  
            }

            if(CurrentDialogueDisplay == -1 && !TextAsCompletelyDisplay && !TextOppeningDisplayCompletely )    StartDiscussion(true); // Arrête l'animation et Affiche tout le texte d'Openning
        }
    }

    bool CheckIfNeedLunchFade()
    {
        bool BoolReturned = false ;

        if(CurrentPNJDiscussion.DialogueLunchPrez && (CurrentDialogueDisplay == CurrentPNJDiscussion.PrezInfoLunch.x && CurrentDialogueState == CurrentPNJDiscussion.PrezInfoLunch.y) && !PlayerFadeScript.AnimationBeMake) BoolReturned = true ;
        if(CurrentPNJDiscussion.DialogueWithFadeAnimation && (CurrentDialogueDisplay == CurrentPNJDiscussion.QuestionAndDialogueLunchFade.x && CurrentDialogueState == CurrentPNJDiscussion.QuestionAndDialogueLunchFade.y) && !PlayerFadeScript.AnimationBeMake) BoolReturned = true ;
        if(CurrentPNJDiscussion.MiniGame != null) 
        {
            for (int i = 0; i < CurrentPNJDiscussion.MiniGameLunchInfo.Count; i++)
            {
                if((CurrentDialogueDisplay == CurrentPNJDiscussion.MiniGameLunchInfo[i].x && CurrentDialogueState == CurrentPNJDiscussion.MiniGameLunchInfo[i].y) && !PlayerFadeScript.AnimationBeMake) BoolReturned = true ;
            }            
        }


        return BoolReturned ;
    }

    void SwitchBoxDisplay()
    {
        DialogueCanvas.gameObject.SetActive(!DialogueCanvas.gameObject.activeSelf);
        BoxQuestion.SetActive(!BoxQuestion.activeSelf); 

        if(DialogueCanvas.gameObject.activeSelf) ThisRect.sizeDelta = new Vector2(ThisRect.sizeDelta.x, 96f) ;
    }

    void SetQuestion(bool QuestionAsRead, int ChildNum, string DialogueText)
    {
        if(!QuestionAsRead)
        {
            BoxQuestion.transform.GetChild(ChildNum).gameObject.SetActive(true) ;
            BoxQuestion.transform.GetChild(ChildNum).GetComponentInChildren<TextMeshProUGUI>().text = DialogueText ;   
            
           /* if(BoxQuestion.transform.GetChild(ChildNum).GetComponentInChildren<TextMeshProUGUI>().preferredWidth < this.GetComponent<RectTransform>().sizeDelta.x)*/ BoxQuestion.transform.GetChild(ChildNum).GetComponent<RectTransform>().sizeDelta = new Vector2(BoxQuestion.transform.GetChild(ChildNum).GetComponent<RectTransform>().sizeDelta.x, BoxQuestion.transform.GetChild(ChildNum).GetComponentInChildren<TextMeshProUGUI>().preferredHeight);
            //else BoxQuestion.transform.GetChild(ChildNum).GetComponent<RectTransform>().sizeDelta = new Vector2(BoxQuestion.transform.GetChild(ChildNum).GetComponent<RectTransform>().sizeDelta.x, (16.5f * 2f));
        } else {
            BoxQuestion.transform.GetChild(ChildNum).GetComponentInChildren<TextMeshProUGUI>().text = " " ;                   
            BoxQuestion.transform.GetChild(ChildNum).gameObject.SetActive(false) ;
        }
    }

    public void ShowDialogueChoice(bool ToggleDisplayBox)
    {
        if(ToggleDisplayBox && CurrentPNJDiscussion.DiscussionWithQuestion)    SwitchBoxDisplay();
        PlayerDialogueManager.ResetSelectQuestion();
        GetComponent<Button>().interactable = false ;
        TextOppeningDisplayCompletely = true ;        
        WeAreInChoice = true ;
        CurrentDialogue = "";
        /* Afficher les Questions à afficher */
        SetQuestion(false, 10, DialoguePNJ.Aurevoir); // Set Aurevoir      

        CheckAndSetquestion(DialoguePNJ.Question1, Question1AsRead, 0); // Set Question 1
        CheckAndSetquestion(DialoguePNJ.Question2, Question2AsRead, 1); // Set Question 2
        CheckAndSetquestion(DialoguePNJ.Question3, Question3AsRead, 2); // Set Question 3
        CheckAndSetquestion(DialoguePNJ.Question4, Question4AsRead, 3); // Set Question 4
        CheckAndSetquestion(DialoguePNJ.Question5, Question5AsRead, 4); // Set Question 5
        CheckAndSetquestion(DialoguePNJ.Question6, Question6AsRead, 5); // Set Question 6
        CheckAndSetquestion(DialoguePNJ.Question7, Question7AsRead, 6); // Set Question 7
        CheckAndSetquestion(DialoguePNJ.Question8, Question8AsRead, 7); // Set Question 8
        CheckAndSetquestion(DialoguePNJ.Question9, Question9AsRead, 8); // Set Question 9
        CheckAndSetquestion(DialoguePNJ.Question10, Question10AsRead, 9); // Set Question 10

        float HeightFinalBox = 30f ;
        for (int HChoiceQuestion = 0; HChoiceQuestion < BoxQuestion.transform.childCount; HChoiceQuestion++)
        {
            if(BoxQuestion.transform.GetChild(HChoiceQuestion).gameObject.activeSelf) HeightFinalBox += BoxQuestion.transform.GetChild(HChoiceQuestion).GetComponent<RectTransform>().sizeDelta.y ;
        }
        ThisRect.sizeDelta = new Vector2(ThisRect.sizeDelta.x, HeightFinalBox);


        SelectedButton();
    }

    void CheckAndSetquestion(string DialoguePNJQuestion, bool QuestionXRead, int ChildNum)
    {
        if(DialoguePNJQuestion != "")
        {
            if(ContainsZValue(ChildNum + 1)) // Couleur Question
            {
                if(CanDisplayQuestQuestion(ChildNum + 1))
                {
                    SetQuestion(QuestionXRead, ChildNum, DialoguePNJQuestion); 
                    BoxQuestion.transform.GetChild(ChildNum).GetComponentInChildren<TextMeshProUGUI>().color = QuestionQuestColor ;//QuestionQuestColor ;                    
                } else {
                    SetQuestion(true, ChildNum, DialoguePNJQuestion);  
                }                            
            } else {
                if(CanDisplayQuestQuestion(ChildNum + 1))
                {
                    SetQuestion(QuestionXRead, ChildNum, DialoguePNJQuestion); 
                    BoxQuestion.transform.GetChild(ChildNum).GetComponentInChildren<TextMeshProUGUI>().color = QuestionClassicColor ;                
                } else {
                    SetQuestion(true, ChildNum, DialoguePNJQuestion);  
                }
            }
        } else {
            SetQuestion(true, ChildNum, DialoguePNJQuestion);
        }
    }


    void ResetDialogueContinuationValue(int NumDialogueList)
    {
        PNJSpeak = true ;    
        CurrentDialogueDisplay = NumDialogueList;

        CurrentDialogueState = 0 ;
    }

    bool ContainsZValue(int ValueSearch)
    {
        bool BoolReturned = false ;
        for (int i = 0; i < CurrentPNJDiscussion.InformationQuestEtapeQuestion.Count; i++)
        {
            if(((int) CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].z == ValueSearch) ) BoolReturned = true ;
        }

        return BoolReturned ;
    }

    bool CanDisplayQuestQuestion(int ValueSearch)
    {
        bool Result = false ;
        bool ValueSearchNeedToBeHide = false ;
        for (int i = 0; i < CurrentPNJDiscussion.InformationQuestEtapeQuestion.Count; i++)
        {
           /* if(QuestSysManager.niveau == CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].x && QuestSysManager.etape == CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].y && ((int) CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].z == ValueSearch))
            {
                Result = true ;
            }*/
            //Result = ConditionForDisplayQuestion(i, ValueSearch) ;

            if(QuestSysManager.niveau == CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].x && QuestSysManager.etape == CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].y && ((int) CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].z == ValueSearch)) Result = true ;
        }

        if(CurrentPNJDiscussion.HideQuestionBeforeMomentX.Count != 0) 
        {
            for (int AllQuestionHide = 0; AllQuestionHide < CurrentPNJDiscussion.HideQuestionBeforeMomentX.Count; AllQuestionHide++)
            {
                if(ValueSearch == (int) CurrentPNJDiscussion.HideQuestionBeforeMomentX[AllQuestionHide].z)
                {
                    ValueSearchNeedToBeHide = true ;
                    if(QuestSysManager.DifferentQuestStep[(int) CurrentPNJDiscussion.HideQuestionBeforeMomentX[AllQuestionHide].x] > (int) CurrentPNJDiscussion.HideQuestionBeforeMomentX[AllQuestionHide].y) Result = true ;
                }
            }    
        }
        if(!Result && !ContainsZValue(ValueSearch) && !ValueSearchNeedToBeHide) Result = true ;        

        return Result ;
    }


    float ReturnWValue(int ValueSearch)
    {
        float FloatReturned = 0 ;
        for (int i = 0; i < CurrentPNJDiscussion.InformationQuestEtapeQuestion.Count; i++)
        {
            if((int) CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].z == ValueSearch) FloatReturned = CurrentPNJDiscussion.InformationQuestEtapeQuestion[i].w ;
        }

        return FloatReturned ;
    }

    void TextDiscussion(bool ToggleDisplayBox, bool TextState)
    {
        PlayerDialogueManager.DialoguePass();
        GetComponent<Button>().interactable = true ;
        WeAreInChoice = false ;

        if(ToggleDisplayBox && CurrentPNJDiscussion.DiscussionWithQuestion)    SwitchBoxDisplay();

        CurrentDialogue = AnswerDisponible[CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]];
        if(!TextState)
        {
            StopAllCoroutines();
            DialogueCanvas.text = "";
            StartCoroutine(WriteText(AnswerDisponible[CurrentPNJDiscussion.Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]], DelayAnimationText));
            //CurrentDialogueState ++ ;
            TextAsCompletelyDisplay = false ;
        } else {
            CurrentPNJDiscussion.PNJTalkAnimation(false);
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
            if(ToggleDisplayBox && CurrentPNJDiscussion.DiscussionWithQuestion)    SwitchBoxDisplay();

            StopAllCoroutines();
            DialogueCanvas.text = ""; 
            StartCoroutine(WriteText(DialoguePNJ.CloseDiscussion, DelayAnimationText));
            TextAsCompletelyDisplay = false ;
        } else {
            CurrentPNJDiscussion.PNJTalkAnimation(false);
            StopAllCoroutines();
            DialogueCanvas.text = DialoguePNJ.CloseDiscussion; 
            TextAsCompletelyDisplay = true ;
        }
        PNJSpeak = false ;
    }


    protected IEnumerator WriteText(string Input, float Delay) 
    {
        CurrentPNJDiscussion.PNJTalkAnimation(true);
        for (int i = 0; i < Input.Length; i++)
        {
            DialogueCanvas.text += Input[i] ;
            VoicePnj.Play();
            yield return new WaitForSeconds(Delay);
        }

        CurrentDialogueState ++ ;
        TextAsCompletelyDisplay = true ;
        CurrentPNJDiscussion.PNJTalkAnimation(false);

    }


    public void ButtonChoice(int Question)
    {
        ResetDialogueContinuationValue(Question);
        TextDiscussion(true, false);

        if(Question == 1 && !Question1AsRead) Question1AsRead = true ;
        if(Question == 2 && !Question2AsRead) Question2AsRead = true ;
        if(Question == 3 && !Question3AsRead) Question3AsRead = true ;
        if(Question == 4 && !Question4AsRead) Question4AsRead = true ;
        if(Question == 5 && !Question5AsRead) Question5AsRead = true ;
        if(Question == 6 && !Question6AsRead) Question6AsRead = true ;
        if(Question == 7 && !Question7AsRead) Question7AsRead = true ;
        if(Question == 8 && !Question8AsRead) Question8AsRead = true ;
        if(Question == 9 && !Question9AsRead) Question9AsRead = true ;
        if(Question == 10 && !Question10AsRead) Question10AsRead = true ;

        if(ContainsZValue(Question))
        {
            PNJQuestValueValidation += ReturnWValue(Question);
        }
    
    
        if(ContainsZValue(Question) && CurrentPNJDiscussion.ThisQuestionLunchReflexion) CurrentPNJDiscussion.PlayerAskQuestQuestion = true ; 
    
    }
    
    public void ButtonClose()
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
        if(KeyPrioritary())
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

    bool KeyPrioritary()
    {
        MousePos = Input.mousePosition ;
        if(MouseIsHover)
        {
            if(MousePos != OldMousePos)
            {
                OldMousePos = MousePos ;
                KeyPressed = false ;
                return false ;
            } else {
                if(!KeyPressed)     return false ;                        
                else    return true ;      
            }
        } else {
            return true ;
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

    #endregion
}
