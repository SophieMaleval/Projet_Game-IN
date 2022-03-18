using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RhythmManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller beatScroller;
    public static RhythmManager instance;


    [Header("Score")]
    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public float totalNotes, normalHits, goodHits, perfectHits, missedHits;
    
    [Header("UI texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public TextMeshProUGUI percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public GameObject resultScreen;
    public GameObject dad;

    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        totalNotes = GameObject.Find("NoteHolder").GetComponent<NotesSpawner>().notesCounter;
        Invoke("LoadScene", 25f);
        resultScreen.SetActive(false);      
    }

    private void Awake()
    {
        
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        multiText = GameObject.Find("MultiplierText").GetComponent<TextMeshProUGUI>();
        percentHitText = GameObject.Find("Percent Hit Value").GetComponent<TextMeshProUGUI>();
        normalsText = GameObject.Find("Normal Hits Value").GetComponent<TextMeshProUGUI>();
        goodsText = GameObject.Find("Good Hits Value").GetComponent<TextMeshProUGUI>();
        perfectsText = GameObject.Find("Perfect Hits Value").GetComponent<TextMeshProUGUI>();
        missesText = GameObject.Find("Missed Hits Value").GetComponent<TextMeshProUGUI>();
        rankText = GameObject.Find("Rank Value").GetComponent<TextMeshProUGUI>();
        finalScoreText = GameObject.Find("Final Score Value").GetComponent<TextMeshProUGUI>();
        resultScreen = GameObject.Find("Results");
        
    }

    void DestroyGame()
    {
        Destroy(dad);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore; 
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

        multiText.text = "Multiplier: x" + currentMultiplier;
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
            Debug.Log("allez raconte");
            Debug.Log(totalHits + " = " + normalHits + " + " + goodHits + " + " + perfectHits);
            totalNotes = GameObject.Find("NoteHolder").GetComponent<NotesSpawner>().notesCounter;
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
        }

    }
}
