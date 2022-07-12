using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RhythmManager : MonoBehaviour
{
    #region Fields

    private QuestSys questSys;

    #endregion

    #region Properties

    public static RhythmManager instance;

    #endregion

    #region UnityInspector

    //public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller beatScroller;


    [Header("Score")]
    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public float totalNotes, normalHits, goodHits, perfectHits, missedHits;

    [Header("SFX")]
    public AudioSource[] prep;
    public AudioSource[] cuisson;
    public AudioSource[] dressage;

    
    [Header("UI texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public TextMeshProUGUI percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public GameObject resultScreen;
    public GameObject DadMaster, dad, gameLauncher, NoteHold;

    [SerializeField] public PNJDialogue PNJCurrent ;
    public PlayerMovement player;
    private Vector2 OldPlayerPosition ;
    public Vector2 PlayerPosition ;

    public GameObject minigameCam;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = /*"Score: 0"*/ "0";
        currentMultiplier = 1;
        //totalNotes = GameObject.Find("NoteHolder").transform.childCount;
        //Invoke("DestroyGame", 52f);
        resultScreen.SetActive(false);

    }

    void OnEnable()
    {   
        dad.SetActive(false);
        StartCoroutine(MiniGameEnable());
    }

    IEnumerator MiniGameEnable()
    {
        yield return new WaitForSeconds(0.025f);
        PNJCurrent.gameObject.SetActive(false);
        player.StartActivity();          
       // player.GetComponent<PlayerScript>().LunchAnimationFadeIn();
        OldPlayerPosition = player.transform.position ;   
        player.GiveGoodAnimation(new Vector2(0,-1f));            
        if(PlayerPosition != Vector2.zero) player.transform.position = new Vector2(PlayerPosition.x, PlayerPosition.y);          
        SwitchCam();      
        player.GetComponent<PlayerScript>().InventoryUIIndestructible.SetActive(false);                 
        yield return new WaitForSeconds(0.75f);
        player.GetComponent<PlayerScript>().LunchFadeOut();

        yield return new WaitForSeconds(1f);
        dad.SetActive(true);             
    }

    private void Awake()
    {
        questSys = GameManager.Instance.gameCanvasManager.questManager;
      /*  scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        multiText = GameObject.Find("Best Score Text").GetComponent<TextMeshProUGUI>();
        percentHitText = GameObject.Find("Percent Hit Value").GetComponent<TextMeshProUGUI>();
        normalsText = GameObject.Find("Normal Hits Value").GetComponent<TextMeshProUGUI>();
        goodsText = GameObject.Find("Good Hits Value").GetComponent<TextMeshProUGUI>();
        perfectsText = GameObject.Find("Perfect Hits Value").GetComponent<TextMeshProUGUI>();
        missesText = GameObject.Find("Missed Hits Value").GetComponent<TextMeshProUGUI>();
        rankText = GameObject.Find("Rank Value").GetComponent<TextMeshProUGUI>();
        finalScoreText = GameObject.Find("Final Score Value").GetComponent<TextMeshProUGUI>();
        resultScreen = GameObject.Find("Results");
        DadMaster = GameObject.Find("Jeu de Rythme");
        dad = GameObject.Find("RythmoGamos");*/
        if (dad == null)
        {
            dad = GameObject.Find("CookBoy");
        }
        player = GameManager.Instance.player.GetComponent<PlayerMovement>();

    }

    public void Music()
    {
        //theMusic.Play();
    }
    void SwitchCam()
    {
        minigameCam.SetActive(true);
    }
    void SwitchBackCam()
    {
        minigameCam.SetActive(false);
    }

    void DestroyGame()
    {
        SwitchBackCam();
        player.EndActivity();
        questSys.Progression();
      /*  Destroy(gameLauncher);        
        Destroy(dad);*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteHit()
    {
        //Debug.Log("Hit On Time");
        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "x" + currentMultiplier;

        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = /*"Score: " +*/ ""+ currentScore; 
    }
    
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed()
    {
        //Debug.Log("Missed Note");
        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "x" + currentMultiplier;
        missedHits++;
    }

    public void Results()
    {
        if (!resultScreen.activeInHierarchy)
        {
            resultScreen.SetActive(true);

            normalsText.text = "" + normalHits;
            goodsText.text = "" + goodHits;
            perfectsText.text = "" + perfectHits;
            missesText.text = "" + missedHits;

            float totalHits = normalHits + goodHits + perfectHits;
            //Debug.Log("allez raconte");
            //Debug.Log(totalHits + " = " + normalHits + " + " + goodHits + " + " + perfectHits);
            //totalNotes = GameObject.Find("NoteHolder").transform.childCount ;//GetComponent<NotesSpawner>().notesCounter;
            float percentHit = (totalHits / totalNotes) * 100f ;

            percentHitText.text = percentHit.ToString("F1") + "%"; //F1= 1 float après la virgule

            string rankVal = "F";

            if(percentHit > 40)
            {
                rankVal = "D";
                if(percentHit > 55)
                {
                    rankVal = "C";
                    if(percentHit > 70)
                    {
                        rankVal = "B";
                        if(percentHit > 85)
                        {
                            rankVal = "A";
                            if(percentHit > 95)
                            {
                                rankVal = "S";
                            }
                        }
                    }
                }
            }

            rankText.text = rankVal;

            finalScoreText.text = currentScore.ToString();
            //Invoke("DestroyGame", 5f);
            StartCoroutine(WaitAndDisableGame());            
        }

    }


    IEnumerator WaitAndDisableGame()
    {
        yield return new WaitForSeconds(5f);
        SwitchBackCam();


        if(PNJCurrent == null)
        {
            player.GetComponent<PlayerScript>().LunchAnimationFadeIn();             
        } else {
            PNJCurrent.TellPlayerLunchFade();
        }                  
        yield return new WaitForSeconds(0.75f);
        PNJCurrent.gameObject.SetActive(true);
        player.GetComponent<PlayerScript>().InventoryUIIndestructible.SetActive(true);
        resultScreen.SetActive(false);
        NoteHold.transform.localPosition = new Vector2(NoteHold.transform.localPosition.x, 0);
        DadMaster.SetActive(false);
        player.transform.position = new Vector2(OldPlayerPosition.x, OldPlayerPosition.y);

        // Redonner la même direction qu'avant le mini jeu
        player.GiveGoodAnimation(PNJCurrent.OldLastMovePlayer);   
     

        //player.GetComponent<PlayerScript>().LunchFadeOut();
        player.enabled = true ;        
   
        if(PNJCurrent == null)
        {
            player.EndActivity();             
        }

    }


    #region Sound effects

    public void PrepSound()
    {
        prep[Random.Range(0, prep.Length)].Play();
    }
    public void FrySound()
    {
        cuisson[Random.Range(0, cuisson.Length)].Play();
    }
     public void DressSound()
     {
        prep[Random.Range(0, prep.Length)].Play();
     }
    #endregion

}


