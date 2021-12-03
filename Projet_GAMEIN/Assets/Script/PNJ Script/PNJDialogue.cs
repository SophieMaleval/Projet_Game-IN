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


    [System.Serializable]    
    public class SerializableAnswer
    {
        public List<int> AnswerForQuestion ; 
    }
    [Header ("Réponse Question")]
    public List<SerializableAnswer> Answer = new List<SerializableAnswer>() ;

    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>() ; }    


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
        //PlayerScript.gameObject.GetComponent<PlayerMovement>().InDialog(true) ;

        DialogueCanvas.transform.parent.gameObject.SetActive(true) ;     
        DialogueCanvas.text = DialoguePNJ.OpeningDialogue ;
    }

    void StateDiscussion()
    {
        if(DialogueCanvas.text == DialoguePNJ.OpeningDialogue)
        {
            ShowDialogueChoice();
        }

        if(DialogueCanvas.text == DialoguePNJ.CloseDiscussion)
        {
            //PlayerScript.gameObject.GetComponent<PlayerMovement>().InDialog(false) ;             
            DialogueCanvas.transform.parent.gameObject.SetActive(false) ;  
            PlayerScript.InDiscussion = false ;
        }
    }

    public void ShowDialogueChoice()
    {
        DialogueCanvas.gameObject.SetActive(false);

        /* Afficher les Questions à afficher */
        BoxQuestion.SetActive(true);

        BoxQuestion.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DialoguePNJ.Question1 ;
        BoxQuestion.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DialoguePNJ.Question2 ;

        if(QuestionDisplay.z != 0)
        {
            BoxQuestion.transform.GetChild(2).GetComponent<Button>().interactable = true ;
            BoxQuestion.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = QuestionDisponible[(int) QuestionDisplay.z] ;
        } else {
            BoxQuestion.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "  " ;
            BoxQuestion.transform.GetChild(2).GetComponent<Button>().interactable = false ;
        }

        if(QuestionDisplay.w != 0)
        {
            BoxQuestion.transform.GetChild(3).GetComponent<Button>().interactable = true ;
            BoxQuestion.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = QuestionDisponible[(int) QuestionDisplay.w] ;
        } else {
            if(QuestionDisplay.z != 0)
            {
                BoxQuestion.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = DialoguePNJ.Aurevoir ;
                BoxQuestion.transform.GetChild(3).GetComponent<Button>().interactable = true ;                  
            } else {
                BoxQuestion.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "  " ;
                BoxQuestion.transform.GetChild(3).GetComponent<Button>().interactable = false ;                
            }

        }
    }

    void AnswerDiscussion(int DialogueDisplay, int StateAnswer)
    {
        BoxQuestion.SetActive(false);
   
        if(DialogueDisplay != 0)
        {
            Debug.Log("Blabla ") ;
            DialogueCanvas.text = AnswerDisponible[Answer[DialogueDisplay - 1].AnswerForQuestion[StateAnswer]] ;   
        } else {
            CloseDiscussion() ;
        }
       // DialogueCanvas.text = AnswerDisponible[Answer[DialogueDisplay -1].AnswerForQuestion[StateAnswer]] ;
    }

    void CloseDiscussion()
    {
        BoxQuestion.SetActive(false);
        DialogueCanvas.gameObject.SetActive(true);

        DialogueCanvas.text = DialoguePNJ.CloseDiscussion ;
    }

    public void ButtonChoix1()
    {
        AnswerDiscussion((int) QuestionDisplay.x, 0);
    }

    public void ButtonChoix2()
    {
        AnswerDiscussion((int) QuestionDisplay.y, 0);
    }

    public void ButtonChoix3()
    {
        AnswerDiscussion((int) QuestionDisplay.z, 0);
    }

    public void ButtonChoix4()
    {
        AnswerDiscussion((int) QuestionDisplay.w, 0);
    }

}
