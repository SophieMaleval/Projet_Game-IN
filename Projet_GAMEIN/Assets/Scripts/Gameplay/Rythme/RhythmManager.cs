using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using AllosiusDev.DialogSystem;
using AllosiusDev.Core;

public class RhythmManager : MonoBehaviour
{
    #region Fields

    private string rankVal;
    private RythmeGameRank rythmeGameRank;

    #endregion

    #region Properties

    public static RhythmManager instance;

    #endregion

    #region UnityInspector

    public bool startPlaying;

    public BeatScroller beatScroller;

    public NoteObject[] notes;

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

    public NpcConversant npcCurrent;
    [SerializeField] private DialogueGraph dialogueToLaunchAtDisableGame;

    public PlayerMovement player;
    private Vector2 OldPlayerPosition ;
    public Vector2 PlayerPosition ;

    public GameObject minigameCam;

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

    }

    void OnEnable()
    {   
        dad.SetActive(false);
        StartCoroutine(MiniGameEnable());
    }

    IEnumerator MiniGameEnable()
    {
        yield return new WaitForSeconds(0.025f);
        if(npcCurrent != null)
        {
            npcCurrent.gameObject.SetActive(false);
        }
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
           
            float percentHit = (totalHits / totalNotes) * 100f ;

            percentHitText.text = percentHit.ToString("F1") + "%"; //F1= 1 float après la virgule

            rankVal = "F";
            rythmeGameRank = RythmeGameRank.F;

            if(percentHit > 40)
            {
                rankVal = "D";
                rythmeGameRank = RythmeGameRank.D;
                if (percentHit > 55)
                {
                    rankVal = "C";
                    rythmeGameRank = RythmeGameRank.C;
                    if (percentHit > 70)
                    {
                        rankVal = "B";
                        rythmeGameRank = RythmeGameRank.B;
                        if (percentHit > 85)
                        {
                            rankVal = "A";
                            rythmeGameRank = RythmeGameRank.A;
                            if (percentHit > 95)
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
            //Invoke("DestroyGame", 5f);
            StartCoroutine(WaitAndDisableGame());            
        }

    }


    IEnumerator WaitAndDisableGame()
    {
        yield return new WaitForSeconds(5f);
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

        if(gameActions.actionsList.Count > 0)
        {
            if(gameRequirements.requirementsList.Count > 0)
            {
                if(gameRequirements.ExecuteGameRequirements())
                    gameActions.ExecuteGameActions();
            }
            else
            {
                gameActions.ExecuteGameActions();
            }
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

public enum RythmeGameRank
{
    F,
    D,
    C,
    B,
    A,
    S,
}


