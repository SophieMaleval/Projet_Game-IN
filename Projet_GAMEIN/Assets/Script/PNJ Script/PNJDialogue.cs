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
    
    public CSVReader TextDialogue ;
    public DialogueContainer DialoguePNJ ;

    public TextMeshProUGUI DialogueCanvas ;

    [SerializeField] private PlayerScript PlayerScript;
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
    private int CurrentDialogueStateDisplay = 0 ;
    
    private int CurrentDialogueLength = 0 ;
    private int CurrentDialogueState = 0 ;



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
        if(PlayerAround && PlayerScript.PlayerAsInterract)
        {
            PlayerScript.PlayerAsInterract = false ;
            if(!PlayerScript.InDiscussion)
            {
                PlayerScript.InDiscussion = true ;
                StartDiscussion();                   
            } else {
                StateDiscussion();
            }
        } 
    }


    public void StartDiscussion()
    {
        PlayerScript.gameObject.GetComponent<PlayerMovement>().StartDialog() ;    

        Question1AsRead = false ;
        Question2AsRead = false ;
        Question3AsRead = false ;

        DialogueCanvas.transform.parent.gameObject.SetActive(true) ;     
        DialogueCanvas.text = DialoguePNJ.OpeningDialogue ;

        CurrentDialogueDisplay = 0 ;
        //CurrentDialogueStateDisplay = 0 ;//
    }

    void StateDiscussion()
    {
        if(DialogueCanvas.text == DialoguePNJ.OpeningDialogue)
        {
            ShowDialogueChoice();
        }

        if(DialogueCanvas.text == DialoguePNJ.CloseDiscussion)
        {
            PlayerScript.gameObject.GetComponent<PlayerMovement>().EndDialog() ;             
            DialogueCanvas.transform.parent.gameObject.SetActive(false) ;  
            PlayerScript.InDiscussion = false ;
        }

       /* Debug.Log();*/
        if(PNJSpeak)
        {
            Debug.Log(CurrentDialogueDisplay + " " + CurrentDialogueState + " " + Answer[CurrentDialogueDisplay].AnswerForQuestion.Count);
            //Debug.Log(CurrentDialogueStateDisplay + " " + (int.Parse(AnswerDisponible[Answer[CurrentDialogueDisplay-1].AnswerForQuestion.Count]) -1) );
            if(CurrentDialogueState < Answer[CurrentDialogueDisplay].AnswerForQuestion.Count )
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

        /* Afficher les Questions à afficher */
        SetQuestion(Question1AsRead, 0, DialoguePNJ.Question1); // Set QUestion 1
        SetQuestion(Question2AsRead, 1, DialoguePNJ.Question2); // Set QUestion 2

        if(QuestionDisplay.z != 0)
        {
            SetQuestion(Question3AsRead, 2, QuestionDisponible[(int) QuestionDisplay.z]); // Set QUestion 3
        } else {
            SetQuestion(false, 2, QuestionDisponible[(int) QuestionDisplay.z]) ; // Set QUestion 3
        }

        SetQuestion(false, 3, DialoguePNJ.Aurevoir); // Set QUestion 4
    }

    void ResetDialogueContinuationValue(int NumDialogueList)
    {
        PNJSpeak = true ;    
        CurrentDialogueDisplay = NumDialogueList - 1;
        //CurrentDialogueLength = Answer[CurrentDialogueDisplay].AnswerForQuestion.Count ;
        CurrentDialogueState = 0 ;
    }

    void TextDiscussion(bool ToggleDisplayBox , int DialogueDisplay, int StateAnswer)
    {
        if(ToggleDisplayBox)
            SwitchBoxDisplay();
   
        DialogueCanvas.text = AnswerDisponible[Answer[CurrentDialogueDisplay].AnswerForQuestion[CurrentDialogueState]] ; 
        CurrentDialogueState ++ ;
    }

    void CloseDiscussion()
    {
        SwitchBoxDisplay();

        DialogueCanvas.text = DialoguePNJ.CloseDiscussion ;
        PNJSpeak = false ;
    }







    public void ButtonChoix1()
    {
        ResetDialogueContinuationValue(1);        
        TextDiscussion(true, /*(int) QuestionDisplay.x*/ 1, 0);



        if(Question1AsRead == false) Question1AsRead = true ;
    }

    public void ButtonChoix2()
    {
        ResetDialogueContinuationValue(2);
        TextDiscussion(true, /*(int) QuestionDisplay.y*/ 2, 0);

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
        /*ResetDialogueContinuationValue((int) QuestionDisplay.w);
        TextDiscussion(true, (int) QuestionDisplay.w, 0);*/
    }

}
