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
    
    [HideInInspector] public CSVReader TextDialogue ;

    [Header ("Dialogue Canvas Reference")]
    public DialogueDisplayerController DialogueCanvasBox ;       
    private TextMeshProUGUI DialogueCanvasDisplayerText ;


    private PlayerScript PlayerScript;
    private PlayerDialogue PlayerDialogueManager;
    private bool PlayerAround = false ;

    private GameObject BoxQuestion ;

    public int Question3IntDisplay = 3;  

    private DialogueContainer DialoguePNJ ;

    public List<string> QuestionDisponible ;
    [HideInInspector] public List<string> AnswerDisponible ;

    private int CurrentDialoguePlayerChoice = 0 ;

    [System.Serializable]    
    public class SerializableAnswer
    {
        public List<int> AnswerForQuestion ; 
    }


    [Header ("Réponse Question")]
    public List<SerializableAnswer> Answer = new List<SerializableAnswer>() ;


    private void Awake() 
    {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    
            PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; 
            PlayerDialogueManager = GameObject.Find("Player Backpack").GetComponent<PlayerDialogue>() ; 

            DialogueCanvasBox = PlayerScript.DialogueUIIndestructible.GetComponent<DialogueDisplayerController>() ;
            TextDialogue = GameObject.Find("Player Backpack").GetComponent<CSVReader>() ;

          /* DialogueCanvasBox.NamePNJ.text = NamePNJ;
            DialogueCanvasBox.gameObject.SetActive(true);*/
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
        PlayerDialogueManager.DialogueStart();
           
        DialogueCanvasBox.gameObject.SetActive(true);           
        DialogueCanvasBox.NamePNJ.text = NamePNJ;


        DialogueCanvasDisplayerText = DialogueCanvasBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        BoxQuestion = DialogueCanvasBox.transform.GetChild(1).gameObject ;     

        DialogueCanvasBox.CurrentPNJDiscussion = this ;        
        DialogueCanvasBox.DialoguePNJ = DialoguePNJ;
        DialogueCanvasBox.AnswerDisponible = AnswerDisponible;
        DialogueCanvasBox.QuestionDisponible = QuestionDisponible ;

        DialogueCanvasBox.StartDiscussion(false);
    }

    public void DiscussionIsClose()
    {
        DialogueCanvasBox.gameObject.SetActive(false);         
        PlayerScript.PlayerAsInterract = false ;        
        PlayerScript.InDiscussion = false ;
        PlayerScript.gameObject.GetComponent<PlayerMovement>().EndDialog() ; 
    }

}
