using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using AllosiusDev.DialogSystem;
using AllosiusDev.Core;
using AllosiusDev.Audio;

public class RhythmManager : MonoBehaviour
{
    #region Fields

    private string rankVal;
    private RythmeGameRank rythmeGameRank;

    private float baseMaxScore;

    private int currentScore;
    private int currentMultiplier;
    private int multiplierTracker;

    private float normalHits, goodHits, perfectHits, missedHits;

    private Vector2 OldPlayerPosition ;

    private bool gameEnded;

    #endregion

    #region Properties

    public static RhythmManager instance;

    public GameObject DadMaster { get; set; }

    public NpcConversant npcCurrent { get; set; }

    public PlayerMovement player { get; set; }
    public bool gameExit { get; protected set; }

    #endregion

    #region UnityInspector

    public bool startPlaying;

    public BeatScroller beatScroller;

    public NoteObject[] notes;

    [Header("Score")]
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;
    public int[] multiplierThresholds;
    public float totalNotes;

    [SerializeField] private int rankDPercentValue = 30;
    [SerializeField] private int rankCPercentValue = 55;
    [SerializeField] private int rankBPercentValue = 70;
    [SerializeField] private int rankAPercentValue = 85;
    [SerializeField] private int rankSPercentValue = 95;

    /*[Header("SFX")]
    public AudioSource[] prep;
    public AudioSource[] cuisson;
    public AudioSource[] dressage;*/


    [Header("UI texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public TextMeshProUGUI percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public GameObject resultScreen;
    public GameObject dad, NoteHold;

    [SerializeField] private DialogueGraph dialogueToLaunchAtDisableGame;

    public Vector2 PlayerPosition ;

    public GameObject minigameCam;

    [SerializeField] private string exitGameBoxMessage;
    [SerializeField] private float exitGameBoxMessageSize;

    [Space]

    [Header("Sounds")]

    [SerializeField] private AudioData sfxSuccessTouch;
    [SerializeField] private AudioData sfxEndParty;

    [Space]

    [SerializeField] private GameRequirements gameRequirements;
    [SerializeField] private GameActions gameActions;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = /*"Score: 0"*/ "0";
        currentMultiplier = 1;
        resultScreen.SetActive(false);

        baseMaxScore = totalNotes * scorePerPerfectNote;

    }

    private void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            ExitGameMenu();
        }
    }

    void OnEnable()
    {   
        dad.SetActive(false);
        StartCoroutine(MiniGameEnable());
    }

    IEnumerator MiniGameEnable()
    {
        yield return new WaitForSeconds(0.025f);
        /*if(npcCurrent != null)
        {
            npcCurrent.gameObject.SetActive(false);
        }*/
        player.StartActivity();          

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
        if (dad == null)
        {
            dad = GameObject.Find("CookBoy");
        }
        player = GameManager.Instance.player.GetComponent<PlayerMovement>();
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

        AudioController.Instance.PlayAudio(sfxSuccessTouch);
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
            gameEnded = true;

            resultScreen.SetActive(true);

            normalsText.text = "" + normalHits;
            goodsText.text = "" + goodHits;
            perfectsText.text = "" + perfectHits;
            missesText.text = "" + missedHits;

            float totalHits = normalHits * scorePerNote + goodHits * scorePerGoodNote + perfectHits * scorePerPerfectNote;
           
            float percentHit = (totalHits / baseMaxScore) * 100f ;

            percentHitText.text = percentHit.ToString("F1") + "%"; //F1= 1 float après la virgule

            rankVal = "F";
            rythmeGameRank = RythmeGameRank.F;

            if(percentHit > rankDPercentValue)
            {
                rankVal = "D";
                rythmeGameRank = RythmeGameRank.D;
                if (percentHit > rankCPercentValue)
                {
                    rankVal = "C";
                    rythmeGameRank = RythmeGameRank.C;
                    if (percentHit > rankBPercentValue)
                    {
                        rankVal = "B";
                        rythmeGameRank = RythmeGameRank.B;
                        if (percentHit > rankAPercentValue)
                        {
                            rankVal = "A";
                            rythmeGameRank = RythmeGameRank.A;
                            if (percentHit > rankSPercentValue)
                            {
                                rankVal = "S";
                                rythmeGameRank = RythmeGameRank.S;
                            }
                        }
                    }
                }
            }

            rankText.text = rankVal;

            GameCore.Instance.currentRythmeGameRank = rythmeGameRank;

            finalScoreText.text = currentScore.ToString();

            AudioController.Instance.PlayAudio(sfxEndParty);

            //Invoke("DestroyGame", 5f);
            StartCoroutine(WaitAndDisableGame(5f));            
        }

    }

    IEnumerator WaitAndDisableGame(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        SwitchBackCam();


        if(npcCurrent == null)
        {
            player.GetComponent<PlayerScript>().LunchAnimationFadeIn();             
        }
        
        yield return new WaitForSeconds(0.75f);

        if(npcCurrent != null)
        {
            npcCurrent.gameObject.SetActive(true);

            GameManager.Instance.player.GetComponent<PlayerConversant>().StartDialog(npcCurrent, dialogueToLaunchAtDisableGame);
        }
        player.GetComponent<PlayerScript>().InventoryUIIndestructible.SetActive(true);
        resultScreen.SetActive(false);
        NoteHold.transform.localPosition = new Vector2(NoteHold.transform.localPosition.x, 0);
        DadMaster.SetActive(false);
        player.transform.position = new Vector2(OldPlayerPosition.x, OldPlayerPosition.y);

        // Redonner la même direction qu'avant le mini jeu
        //player.GiveGoodAnimation(PNJCurrent.OldLastMovePlayer);   
        player.GiveGoodAnimation(GameCore.Instance.OldLastMovePlayer);
     

        //player.GetComponent<PlayerScript>().LunchFadeOut();
        player.enabled = true ;        
   
        if(npcCurrent == null)
        {
            player.EndActivity();             
        }

        if (gameEnded)
        {
            if (gameActions.actionsList.Count > 0)
            {
                if (gameRequirements.requirementsList.Count > 0)
                {
                    if (gameRequirements.ExecuteGameRequirements())
                        gameActions.ExecuteGameActions();
                }
                else
                {
                    gameActions.ExecuteGameActions();
                }
            }
        }

    }

    public void ExitGameMenu()
    {
        if(gameExit == false && gameEnded == false)
        {
            //AudioController.Instance.PauseAudio(beatScroller.music, true);
            //beatScroller.musicSource.Pause();
            AudioController.Instance.PauseSpecificAudio(beatScroller.musicSource);
            beatScroller.canPlay = false;
            beatScroller.tempDureeMusique = beatScroller.dureeMusique;
            beatScroller.dureeMusique = 0;

            MessageBox messageBox = GameManager.Instance.gameCanvasManager.CreateMessageBox(exitGameBoxMessage, exitGameBoxMessageSize, true);
            messageBox.YesButton.onClick.AddListener(() => ExitGame());
            messageBox.NoButton.onClick.AddListener(() => ReturnToTheGame());

            gameExit = true;
        }
    }

    public void ExitGame()
    {
        StartCoroutine(WaitAndDisableGame(0.1f));
    }

    public void ReturnToTheGame()
    {
        player.StartActivity();

        //AudioController.Instance.PauseAudio(beatScroller.music, false);
        //beatScroller.musicSource.UnPause();
        AudioController.Instance.ResumeSpecificAudio(beatScroller.musicSource);
        beatScroller.canPlay = true;
        beatScroller.dureeMusique = beatScroller.tempDureeMusique;
        StartCoroutine(beatScroller.GuyterHiro());
        gameExit = false;
    }

    #region Sound effects

    /*public void PrepSound()
    {
        prep[Random.Range(0, prep.Length)].Play();
    }*/
    /*public void FrySound()
    {
        cuisson[Random.Range(0, cuisson.Length)].Play();
    }*/
     /*public void DressSound()
     {
        prep[Random.Range(0, prep.Length)].Play();
     }*/
    #endregion

}

public enum RythmeGameRank
{
    F,
    D,
    C,
    B,
    A,
    S,
}


