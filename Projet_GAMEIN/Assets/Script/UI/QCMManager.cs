using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class QCMManager : MonoBehaviour
{
    #region Fields

    private bool MouseIsHover ;

    private int CurrentQuestion = 0 ;

    private GameObject PlayerText ;

    private QCMContainer QCMContaine = new QCMContainer() ;

    private List<QuestionQCMContainer> TextQCMFR = new List<QuestionQCMContainer>() ;
    private List<QuestionQCMContainer> TextQCMEN = new List<QuestionQCMContainer>() ;

    #endregion

    #region UnityInspector

    [SerializeField] private QCMPresentationManager QCMPresentation ;

    [SerializeField] private RectTransform FondQCM ;
    [SerializeField] private RectTransform ContourQCM ;

    [SerializeField] private TextMeshProUGUI TitleQuestion ;

    [SerializeField] private List<GameObject> Choices ;

    [SerializeField] private Color HighlightColor ;
    [SerializeField] private Color GoodColor ;
    [SerializeField] private Color WrongColor ;
    public int CurrentChoice ;
    public int CurrentEnigme = 1 ;

    [HideInInspector] public PNJDialogue PNJCurrent ;

    [Header ("QCM Text")]
    public List<QuestionQCMContainer> TextQCM = new List<QuestionQCMContainer>() ;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerMovement PlayerApparance = GameManager.Instance.player.GetComponent<PlayerMovement>() ; 
        PlayerApparance.enabled = false ;                 
        PlayerScript PlayerPersonnality = PlayerApparance.GetComponent<PlayerScript>() ;

        for (int A = 0; A < PlayerApparance.Animators.Count; A++)
        {
            // Set Up la direction du Joueur en Face
            PlayerApparance.Animators[A].SetFloat("AnimLastMoveX", 0) ;
            PlayerApparance.Animators[A].SetFloat("AnimLastMoveY", -1) ;
        }   

        gameObject.GetComponent<RectTransform>().localScale = Vector3.zero ;
       // PlayerText.GetComponent<PlayerDialogue>().DialogueStart();
        CurrentQuestion = 0 ;
        StartCoroutine(WaitAndSet());
    }
    IEnumerator WaitAndSet()
    {
        yield return new WaitForSeconds(0.05f);
        SetChoiceDisp();
        gameObject.GetComponent<RectTransform>().localScale = Vector3.one ;
    }
    private void Start() 
    {
        if(GameManager.Instance.player.playerBackpack != null)  PlayerText = GameManager.Instance.player.playerBackpack.gameObject;

        PlayerText.GetComponentInParent<PlayerMovement>().StartActivity();
        PlayerText.GetComponent<PlayerDialogue>().DialogueStart();

        QCMContaine = PlayerText.GetComponent<CSVReader>().QCMCont ;
    }


    void SetEnigme()
    {
        if(CurrentEnigme == 1)
        {
            TextQCMFR = QCMContaine.QCMEnigmeFR1 ;
            TextQCMEN = QCMContaine.QCMEnigmeEN1 ;            
        }    

        if(CurrentEnigme == 2)
        {
            TextQCMFR = QCMContaine.QCMPresFR1 ;
            TextQCMEN = QCMContaine.QCMPresEN1 ;            
        }   

        if(CurrentEnigme == 3)
        {
            TextQCMFR = QCMContaine.QCMEnigmeFR2 ;
            TextQCMEN = QCMContaine.QCMEnigmeEN2 ;            
        }   

        if(PlayerPrefs.GetInt("Langue") == 0 && TextQCM != TextQCMFR)    TextQCM = TextQCMFR ;
        if(PlayerPrefs.GetInt("Langue") == 1 && TextQCM != TextQCMEN)    TextQCM = TextQCMFR ;
    }
    
    public void SetChoiceDisp()
    {
        SetEnigme();

        int NumOfQuestion = 0 ;
        PlayerText.GetComponent<PlayerDialogue>().CurrentSelectQuestion = 0 ;
        
        TitleQuestion.text = TextQCM[CurrentQuestion].QuestionsReponse[0];
        for (int SCD = 0; SCD < Choices.Count; SCD++)
        {
            if(SCD < TextQCM[CurrentQuestion].QuestionsReponse.Count - 1)
            {
                Choices[SCD].SetActive(true);
                Choices[SCD].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f) ;
                Choices[SCD].transform.GetChild(0).GetComponent<Image>().color = HighlightColor ;
                Choices[SCD].GetComponentInChildren<TextMeshProUGUI>().text = TextQCM[CurrentQuestion].QuestionsReponse[SCD + 1];
                Choices[SCD].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                NumOfQuestion ++ ;
            } 
            if(SCD >= TextQCM[CurrentQuestion].QuestionsReponse.Count - 1)
            {
                Choices[SCD].SetActive(false);
            }
        }

        FondQCM.sizeDelta = new Vector2(FondQCM.sizeDelta.x, (85f + 55f * NumOfQuestion));
        ContourQCM.sizeDelta = new Vector2(ContourQCM.sizeDelta.x, (67.5f + 27.5f * NumOfQuestion));
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerText != null)
        {
            if(CurrentChoice != PlayerText.GetComponent<PlayerDialogue>().CurrentSelectQuestion) CurrentChoice = PlayerText.GetComponent<PlayerDialogue>().CurrentSelectQuestion ;
        }

        SelectQuestionX();

        if(EventSystem.current.IsPointerOverGameObject())
        {
            MouseIsHover = true ;
        } else {
            MouseIsHover = false ;

            if(PlayerText.GetComponent<PlayerDialogue>().PlayerAsRead) ValidateButton();
        }



        if(PlayerPrefs.GetInt("Langue") == 0 && TextQCM != TextQCMFR)    TextQCM = TextQCMFR ;
        if(PlayerPrefs.GetInt("Langue") == 1 && TextQCM != TextQCMEN)    TextQCM = TextQCMFR ;

    }



    public void SubmitAnswer(int NumAnswer)
    {
        if(!QCMPresentation.enabled)
        {
            if(NumAnswer != TextQCM[CurrentQuestion].NuméroRéponse)
            {
                Choices[NumAnswer - 1].GetComponent<Image>().color = WrongColor ;
                Choices[NumAnswer - 1].transform.GetChild(0).GetComponent<Image>().color = WrongColor ;
                Choices[NumAnswer - 1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            } else {
                Choices[NumAnswer - 1].GetComponent<Image>().color = GoodColor ;
                Choices[NumAnswer - 1].transform.GetChild(0).GetComponent<Image>().color = GoodColor ;
                StartCoroutine(WaitAndShowNewQuestion(0.25f));
            }            
        } else {
            if(NumAnswer != TextQCM[CurrentQuestion].NuméroRéponse)
            {
                QCMPresentation.AddValueSlider(false);
            } else {
                QCMPresentation.AddValueSlider(true);
            }  
            StartCoroutine(WaitAndShowNewQuestion(0));            
        }

    }

    IEnumerator WaitAndShowNewQuestion(float WaitValue)
    {

        if(CurrentQuestion < TextQCM.Count - 1)
        {
            yield return new WaitForSeconds(WaitValue);            
            CurrentQuestion ++ ;
            SetChoiceDisp();
        } else {
            PlayerText.GetComponentInParent<PlayerScript>().TimeLineManager.Toggle();
            yield return new WaitForSeconds(WaitValue * 2);   
            CloseQCMManager();
        }
    }

    void CloseQCMManager()
    {
        if(PNJCurrent == null)
        {
            GameObject.Find("Quest Dialogue Manager").GetComponent<TalkQuest>().TalkedTo();   
            GameManager.Instance.gameCanvasManager.questManager.Progression();
            GameManager.Instance.player.GetComponent<PlayerMovement>().enabled = true ;
            PlayerText.GetComponentInParent<PlayerMovement>().EndActivity();
        } else {
            GameManager.Instance.player.GetComponent<PlayerMovement>().enabled = true ;
            PlayerText.GetComponentInParent<PlayerMovement>().StartActivity();

            PNJCurrent.TellPlayerLunchFade();
        }

        PNJCurrent = null ;        
        gameObject.SetActive(false);        
    }


    void SelectQuestionX()
    {
        if(!MouseIsHover)
        {
            for (int BQC = 0; BQC < SetButtonDisponnible().Count; BQC++)
            {
                Button ChoiceCurrent = SetButtonDisponnible()[BQC];
                ChoiceCurrent.interactable = false ;

                if(BQC == CurrentChoice) ChoiceCurrent.transform.Find("Contour").gameObject.SetActive(true);
                else ChoiceCurrent.transform.Find("Contour").gameObject.SetActive(false);
            }
        } else {
            for (int BQC = 0; BQC < Choices.Count; BQC++)
            {
                Button ChoiceCurrent = Choices[BQC].GetComponent<Button>();
                ChoiceCurrent.interactable = true ;
            }
        }
    }

    public List<Button> SetButtonDisponnible()
    {
        List<Button> ActiveButton = new List<Button>();
        for (int C = 0; C < Choices.Count; C++)
        {
            if(Choices[C].activeSelf) ActiveButton.Add(Choices[C].gameObject.GetComponent<Button>());
        }

        return ActiveButton ;
    }

    public void ValidateButton()
    {
        PlayerText.GetComponent<PlayerDialogue>().PlayerAsRead = false ;

        SetButtonDisponnible()[CurrentChoice].onClick.Invoke();
    }

    public void QCMIsPrez(bool State)
    {
        QCMPresentation.enabled = State ;
    }

    public float GetSliderPrezValue()
    {
        return QCMPresentation.AudienceBar.value ;
    }

    #endregion
}
