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
    public string Entrerpise ;
    public string NamePNJ ;
    
    [HideInInspector] public CSVReader TextDialogue ;
    public DialogueContainer DialoguePNJ ;

    public TextMeshProUGUI DialogueCanvas ;


    public DialogueDisplayerController DialogueCanvasBox ;   

    private PlayerScript PlayerScript;
    private PlayerDialogue PlayerDialogueManager;
    private bool PlayerAround = false ;

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

    [System.Serializable]    
    public class SerializableAnswer
    {
        public List<int> AnswerForQuestion ; 
    }


    [Header ("Réponse Question")]
    public List<SerializableAnswer> Answer = new List<SerializableAnswer>() ;


    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    
            PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; 
            PlayerDialogueManager = GameObject.Find("Player Backpack").GetComponent<PlayerDialogue>() ; 

            TextDialogue = GameObject.Find("Player Backpack").GetComponent<CSVReader>() ;
        }    
        
    } 

    public void GetDialogue()
    {
        for (int T = 0; T < TextDialogue.myDialogueAdhérent.Count; T++)
        {
            if(TextDialogue.myDialogueAdhérent[T].Entreprise == Entrerpise)
            {
                DialoguePNJ = TextDialogue.myDialogueAdhérent[T] ;
            }
        }
    }



    private void OnEnable() {    DialogueCanvas.text = DialoguePNJ.OpeningDialogue ; }
    private void OnDisable() {    DialogueCanvas.text = DialoguePNJ.OpeningDialogue ; }

    void Start()
    {
       StartCoroutine(CallDialogue());
    }



    IEnumerator CallDialogue()
    {
        yield return new WaitForSeconds(1f) ;
        GetDialogue();
        SetQuestionList(); 
        SetAnswerList();       
    }

    public void SetQuestionList()
    {
        QuestionDisponible.Add(DialoguePNJ.Question1);
        QuestionDisponible.Add(DialoguePNJ.Question2);
        QuestionDisponible.Add(DialoguePNJ.Question3);
        QuestionDisponible.Add(DialoguePNJ.Question4);
        QuestionDisponible.Add(DialoguePNJ.Question5);
        QuestionDisponible.Add(DialoguePNJ.Question6);
        QuestionDisponible.Add(DialoguePNJ.Question7);
        QuestionDisponible.Add(DialoguePNJ.Question8);
        QuestionDisponible.Add(DialoguePNJ.Question9);
        QuestionDisponible.Add(DialoguePNJ.Question10);
    }

    public void SetAnswerList()
    {
        AnswerDisponible.Add(DialoguePNJ.Dialogue1);
        AnswerDisponible.Add(DialoguePNJ.Dialogue2);
        AnswerDisponible.Add(DialoguePNJ.Dialogue3);
        AnswerDisponible.Add(DialoguePNJ.Dialogue4);
        AnswerDisponible.Add(DialoguePNJ.Dialogue5);
        AnswerDisponible.Add(DialoguePNJ.Dialogue6);
        AnswerDisponible.Add(DialoguePNJ.Dialogue7);
        AnswerDisponible.Add(DialoguePNJ.Dialogue8);
        AnswerDisponible.Add(DialoguePNJ.Dialogue9);
        AnswerDisponible.Add(DialoguePNJ.Dialogue10);
        AnswerDisponible.Add(DialoguePNJ.Dialogue11);
        AnswerDisponible.Add(DialoguePNJ.Dialogue12);
        AnswerDisponible.Add(DialoguePNJ.Dialogue13);
        AnswerDisponible.Add(DialoguePNJ.Dialogue14);
        AnswerDisponible.Add(DialoguePNJ.Dialogue15);
        AnswerDisponible.Add(DialoguePNJ.Dialogue16);
        AnswerDisponible.Add(DialoguePNJ.Dialogue17);
        AnswerDisponible.Add(DialoguePNJ.Dialogue18);
        AnswerDisponible.Add(DialoguePNJ.Dialogue19);
        AnswerDisponible.Add(DialoguePNJ.Dialogue20);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerAround = true ;
        }      
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerAround = false ;
        }      
    }

    void Update()
    {
        if(PlayerAround)
        {
            if(PlayerScript.PlayerAsInterract && !PlayerScript.InDiscussion)
            {
                PlayerScript.PlayerAsInterract = false ;

                PlayerScript.InDiscussion = true ;
                LunchDiscussion();                  
            }

            if(PlayerDialogueManager.PlayerAsRead) 
            {
                PlayerDialogueManager.PlayerAsRead = false ;

                if(!DialogueCanvasBox.WeAreInChoice)
                    DialogueCanvasBox.StateDiscussion();
                else
                    DialogueCanvasBox.ValidateButton();
            } 
        } 

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


    public void LunchDiscussion()
    {
        PlayerScript.gameObject.GetComponent<PlayerMovement>().StartDialog() ; 

        DialogueCanvasBox.gameObject.SetActive(true);
        DialogueCanvasBox.DialoguePNJ = DialoguePNJ;
        DialogueCanvasBox.AnswerDisponible = AnswerDisponible;
        DialogueCanvasBox.QuestionDisponible = QuestionDisponible ;

        DialogueCanvasBox.StartDiscussion();
    }

    public void DiscussionIsClose()
    {
        DialogueCanvasBox.gameObject.SetActive(false);         
        PlayerScript.PlayerAsInterract = false ;        
        PlayerScript.InDiscussion = false ;
        PlayerScript.gameObject.GetComponent<PlayerMovement>().EndDialog() ; 
    }

}
