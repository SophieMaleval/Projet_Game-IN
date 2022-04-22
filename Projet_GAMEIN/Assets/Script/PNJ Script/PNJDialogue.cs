using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using System.Text;

public class PNJDialogue : MonoBehaviour
{
    [Header ("PNJ Information")]
    public string Entrerpise ;
    public string NamePNJ ;
    public string NamePNJDisplay ;
    [SerializeField] private int PNJInENT;
    
    [HideInInspector] public CSVReader TextDialogue ;

    [Header ("Dialogue Canvas Reference")]
    [HideInInspector] public DialogueDisplayerController DialogueCanvasBox ;       
    private TextMeshProUGUI DialogueCanvasDisplayerText ;


    private PlayerScript PlayerScript;
    private PlayerDialogue PlayerDialogueManager;
    private bool PlayerAround = false ;

    private GameObject BoxQuestion ;

    [Header ("Dialogue Animation")]
    public bool ThisQuestionLunchReflexion = false ;
    public int EnigmeQCM = 0 ;
    [HideInInspector] public bool PlayerAskQuestQuestion = false ;
    
    [Space(10)]
    public bool DialogueWithFadeAnimation = false ;
    public Vector2 QuestionAndDialogueLunchFade ;
    private bool QCMPannelAsBeOpen = false ;
    [Space(10)]
    public bool DialogueLunchPrez = false ;
    public Vector2 PrezInfoLunch ;
    [Space(10)]
    public Vector4 NewPosPNJAndPlayer ;   
    private Vector4 LastPosPNJAndPlayer ;
    private QuestSys questSys;

    private DialogueContainer DialoguePNJ_FR ;
    private DialogueContainer DialoguePNJ_EN ;

        private List<string> QuestionDisponible_FR ;
        private List<string> QuestionDisponible_EN ;
        private List<string> AnswerDisponible_FR ;
        private List<string> AnswerDisponible_EN ;

    private int CurrentDialoguePlayerChoice = 0 ;

    [Header ("Quest Gestion Question")]
    public List<Vector4> InformationQuestEtapeQuestion ; 

    [System.Serializable]    
    public class SerializableAnswer
    {
        public List<int> AnswerForQuestion ; 
    }


    [Header ("Réponse Question")]
    public bool DiscussionWithQuestion = true ;
    public List<SerializableAnswer> Answer = new List<SerializableAnswer>() ;


    private void Awake() 
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        if (GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    
            PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; 
            PlayerDialogueManager = GameObject.Find("Player Backpack").GetComponent<PlayerDialogue>() ; 

            DialogueCanvasBox = PlayerScript.DialogueUIIndestructible.GetComponent<DialogueDisplayerController>() ;
        }   

        GetComponent<Animator>().SetInteger("PNJ Need", PNJInENT) ;


        if(NamePNJDisplay == "") NamePNJDisplay = NamePNJ ;
    } 

    public void GetDialogue()
    {
        for (int T = 0; T < PlayerDialogueManager.myDialogueAdhérentFR.Count; T++)
        {
            if(PlayerDialogueManager.myDialogueAdhérentFR[T].Entreprise == Entrerpise && PlayerDialogueManager.myDialogueAdhérentFR[T].Name == NamePNJ)
            {
                DialoguePNJ_FR = PlayerDialogueManager.myDialogueAdhérentFR[T] ;
            }
        }

        for (int T = 0; T < PlayerDialogueManager.myDialogueAdhérentEN.Count; T++)
        {
            if(PlayerDialogueManager.myDialogueAdhérentEN[T].Entreprise == Entrerpise && PlayerDialogueManager.myDialogueAdhérentEN[T].Name == NamePNJ)
            {
                DialoguePNJ_EN = PlayerDialogueManager.myDialogueAdhérentEN[T] ;
            }
        }
    }

    void Start()
    {
       StartCoroutine(CallDialogue());
    }



    IEnumerator CallDialogue()
    {
        yield return new WaitForSeconds(0.5f) ;
        GetDialogue();        
        yield return new WaitForSeconds(1f) ;


        QuestionDisponible_FR = SetQuestionList(DialoguePNJ_FR); 
        AnswerDisponible_FR = SetAnswerList(DialoguePNJ_FR); 

        QuestionDisponible_EN = SetQuestionList(DialoguePNJ_EN); 
        AnswerDisponible_EN = SetAnswerList(DialoguePNJ_EN); 

    }

    List<string> SetQuestionList(DialogueContainer DialoguePNJLangue)
    {
        List<string> QuestionDisponibleLangue = new List<string>() ;
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question1);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question2);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question3);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question4);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question5);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question6);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question7);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question8);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question9);
        QuestionDisponibleLangue.Add(DialoguePNJLangue.Question10);

        return QuestionDisponibleLangue ;
    }

    List<string> SetAnswerList(DialogueContainer DialoguePNJLangue)
    {
        List<string>  AnswerDisponibleLangue = new List<string>();
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue1);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue2);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue3);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue4);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue5);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue6);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue7);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue8);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue9);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue10);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue11);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue12);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue13);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue14);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue15);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue16);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue17);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue18);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue19);
        AnswerDisponibleLangue.Add(DialoguePNJLangue.Dialogue20);

        return AnswerDisponibleLangue ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerAround = true ;
            PlayerScript.SwitchInputSprite();
        }      
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerAround = false ;
            PlayerScript.SwitchInputSprite();
        }      
    }

    void Update()
    {
        if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
        if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);

        if(PlayerScript.InAnimationFade && (DialogueCanvasBox != null && DialogueCanvasBox.gameObject.activeSelf))
        {
            if(PlayerScript.FadeMake)
            {
                if(DialogueLunchPrez)
                {
                    if(!QCMPannelAsBeOpen)
                    {
                        if(NewPosPNJAndPlayer != Vector4.zero) MovePNJAndPlayer(NewPosPNJAndPlayer);  
                        OpenQCMPanel();                                              
                    } else {
                        if(NewPosPNJAndPlayer != Vector4.zero) MovePNJAndPlayer(LastPosPNJAndPlayer);                        
                        QCMPresentationClose() ;
                    } 
                } else {
                   // DialogueCanvasBox.StateDiscussion(); 
                    DialogueCanvasBox.StateDiscussionAfterFade();
                    if(NewPosPNJAndPlayer != Vector4.zero) MovePNJAndPlayer(NewPosPNJAndPlayer);

                }

                PlayerScript.LunchFadeOut();        
            }  
        }
        


        if(PlayerAround && !PlayerScript.QCMPanelUIIndestructible.activeSelf)
        {
            if(PlayerScript.PlayerAsInterract && !PlayerScript.InDiscussion)
            {
                LunchDiscussion();               
            }

            if(PlayerDialogueManager.PlayerAsRead) 
            {
                PlayerDialogueManager.PlayerAsRead = false ;

                if(!DialogueCanvasBox.WeAreInChoice)
                {
                    if(!PlayerScript.InAnimationFade) DialogueCanvasBox.StateDiscussion(); 
                } else {
                    DialogueCanvasBox.ValidateButton();
                }  
            } 
        } 
    }

    public void TellPlayerLunchFade()
    {
        PlayerScript.LunchAnimationFadeIn() ;
        QCMPannelAsBeOpen = true ;
    }

    public void QCMPresentationClose()
    {
        DialogueCanvasBox.gameObject.SetActive(true);  

//        DialogueCanvasBox.StateDiscussion();
        DialogueCanvasBox.ShowDialogueChoice(true);
        if(PlayerScript.QCMPanelUIIndestructible.GetComponent<QCMManager>().GetSliderPrezValue() >= 0.5f) DialogueCanvasBox.ButtonChoice(10); 
        else DialogueCanvasBox.ButtonChoice(9) ;
    }

    private void FixedUpdate() 
    {
        if(PlayerDialogueManager != null)
        {
            if(CurrentDialoguePlayerChoice != PlayerDialogueManager.CurrentSelectQuestion )
            {
                CurrentDialoguePlayerChoice = PlayerDialogueManager.CurrentSelectQuestion  ;   
            }
        }
    }

    public void PNJTalkAnimation(bool IsTalking)
    {
        GetComponent<Animator>().SetBool("Talk", IsTalking) ;        
    }

    public void LunchDiscussion()
    {
        PlayerScript.PlayerAsInterract = false ;
        PlayerDialogueManager.PlayerAsRead = false ;
        PlayerScript.InDiscussion = true ;
        QCMPannelAsBeOpen = false ;

        PNJTalkAnimation(true) ;


        PlayerScript.gameObject.GetComponent<PlayerMovement>().StartActivity() ; 
        PlayerDialogueManager.DialogueStart();
           
        DialogueCanvasBox.gameObject.SetActive(true);           
        DialogueCanvasBox.NamePNJ.text = NamePNJDisplay;
        DialogueCanvasBox.SetPNJNameWidth(NamePNJDisplay);


        DialogueCanvasDisplayerText = DialogueCanvasBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
        BoxQuestion = DialogueCanvasBox.transform.GetChild(1).gameObject ;     

        DialogueCanvasBox.CurrentPNJDiscussion = gameObject.GetComponent<PNJDialogue>() ;   
 

        DialogueCanvasBox.DialoguePNJ_FR = DialoguePNJ_FR;
        DialogueCanvasBox.DialoguePNJ_EN = DialoguePNJ_EN;
        if(PlayerPrefs.GetInt("Langue") == 0) DialogueCanvasBox.DialoguePNJ = DialoguePNJ_FR;
        if(PlayerPrefs.GetInt("Langue") == 1) DialogueCanvasBox.DialoguePNJ = DialoguePNJ_EN;

        DialogueCanvasBox.QuestionDisponible_FR = QuestionDisponible_FR;
        DialogueCanvasBox.QuestionDisponible_EN = QuestionDisponible_EN;
        if(PlayerPrefs.GetInt("Langue") == 0) DialogueCanvasBox.QuestionDisponible = QuestionDisponible_FR;
        if(PlayerPrefs.GetInt("Langue") == 1) DialogueCanvasBox.QuestionDisponible = QuestionDisponible_EN;

        DialogueCanvasBox.AnswerDisponible_FR = AnswerDisponible_FR;
        DialogueCanvasBox.AnswerDisponible_EN = AnswerDisponible_EN;
        if(PlayerPrefs.GetInt("Langue") == 0) DialogueCanvasBox.AnswerDisponible = AnswerDisponible_FR;
        if(PlayerPrefs.GetInt("Langue") == 1) DialogueCanvasBox.AnswerDisponible = AnswerDisponible_EN;

        DialogueCanvasBox.StartDiscussion(false);
    }


    public void DiscussionIsClose()
    {
        GetComponent<Animator>().SetBool("Talk", false) ;


        DialogueCanvasBox.ResetAllValue();         
        DialogueCanvasBox.gameObject.SetActive(false);      
        DialogueCanvasBox.DialogueCanvas.text = "";           
        PlayerScript.PlayerAsInterract = false ;        
        PlayerScript.InDiscussion = false ;


        if(DialogueCanvasBox.PNJQuestValueValidation >= 1)
        {
            if(ThisQuestionLunchReflexion == true && PlayerAskQuestQuestion == true)
            {
                OpenEnigme();
            } else {
                for (int TQC = 1; TQC < transform.childCount; TQC++)
                {
                    transform.GetChild(TQC).gameObject.GetComponent<TalkQuest>().TalkedTo();  
                }
                questSys.Progression();
                PlayerScript.gameObject.GetComponent<PlayerMovement>().EndActivity() ;   
            }        
        } else {
          PlayerScript.gameObject.GetComponent<PlayerMovement>().EndActivity() ;   
        } 
    }

    public void OpenEnigme()
    {
        PlayerScript.TimeLineManager.gameObject.SetActive(true);
        PlayerScript.TimeLineManager.Toggle();
        StartCoroutine(WaitAppartéFinish());
    }

    public void OpenQCMPanel()
    {
        PlayerScript.TimeLineManager.gameObject.SetActive(true);
        PlayerScript.TimeLineManager.Toggle();

        PlayerScript.QCMPanelUIIndestructible.GetComponent<QCMManager>().CurrentEnigme = EnigmeQCM ; 
        PlayerScript.QCMPanelUIIndestructible.SetActive(true) ;
        PlayerScript.QCMPanelUIIndestructible.GetComponent<QCMManager>().QCMIsPrez(true) ;      
        PlayerScript.QCMPanelUIIndestructible.GetComponent<QCMManager>().PNJCurrent = this ;
        DialogueCanvasBox.gameObject.SetActive(false);  
    }

    IEnumerator WaitAppartéFinish()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerScript.QCMPanelUIIndestructible.GetComponent<QCMManager>().CurrentEnigme = EnigmeQCM ; 
        PlayerScript.QCMPanelUIIndestructible.SetActive(true);
    }


    void MovePNJAndPlayer(Vector4 NewPoss)
    {
        LastPosPNJAndPlayer = new Vector4(transform.position.x, transform.position.y, PlayerScript.transform.position.x, PlayerScript.transform.position.y) ;

        transform.position = new Vector2(NewPoss.x, NewPoss.y);
        PlayerScript.transform.position = new Vector2(NewPoss.z, NewPoss.w);
    }

}
