using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public bool questOn = false;

    bool activated = false;
    bool vPressed = false;

    [Header("Textes")]
    public TextMeshProUGUI radioText;


    [Header("Effet de Particule")]
    public ParticleSystem notesA;
    public ParticleSystem notesB;
    public float rateA = 5.0f;
    public float rateB = 5.0f;
    public float slowA = 5.0f;
    public float slowB = 5.0f;
    public Color defaultColor;
    public Color changedColor;

    [Header("Sounds")]
    public AudioSource drums;
    public AudioSource riff;
    public AudioSource subb;
    public AudioSource radioSwitchOn;
    public AudioSource radioSwitchOff;
    public AudioSource melody;

    private GameObject bpfSounds;
    private GameObject questEffects;
    public GameObject tutoRadio;
    private GameObject drumsObj;
    private GameObject riffObj;
    private GameObject subbObj;
    private GameObject bkgMusic;

    private Scene currentScene;
    private string sceneName;
    private bool seekComponents = false;
    private bool initialized = false;



    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene currentScene, LoadSceneMode mode)
    {
        if(currentScene.name == "Main")
        {
            bpfSounds = GameObject.FindGameObjectWithTag("BPF Sounds");
            drumsObj = GameObject.FindGameObjectWithTag("Drums");
            riffObj = GameObject.FindGameObjectWithTag("Riff");
            subbObj = GameObject.FindGameObjectWithTag("Subb");
            bkgMusic = GameObject.FindGameObjectWithTag("Melody");
            tutoRadio = GameObject.Find("TextRadio");
            // --UI
            //radioText = GameObject.FindGameObjectWithTag("RadioText").GetComponent<TextMeshProUGUI>();
            //disabler/enabler

            // questEffects.SetActive(false);
            //radioText.enabled = false;
            //tutoRadio.SetActive(true);
            bpfSounds.SetActive(true);
            //sounds
            drums = drumsObj.GetComponent<AudioSource>();
            riff = riffObj.GetComponent<AudioSource>();
            subb = subbObj.GetComponent<AudioSource>();
            melody = bkgMusic.GetComponent<AudioSource>();
        }
            
    }
    public void GetTutoRadio()
    {
        tutoRadio = GameObject.Find("TextRadio");
        tutoRadio.SetActive(false);
    }


    void GetVariables()
    {     
        
            bpfSounds = GameObject.FindGameObjectWithTag("BPF Sounds");
            drumsObj = GameObject.FindGameObjectWithTag("Drums");
            riffObj = GameObject.FindGameObjectWithTag("Riff");
            subbObj = GameObject.FindGameObjectWithTag("Subb");
            bkgMusic = GameObject.FindGameObjectWithTag("Melody");
            tutoRadio = GameObject.Find("TextRadio");
        // --UI
        //radioText = GameObject.FindGameObjectWithTag("RadioText").GetComponent<TextMeshProUGUI>();
            //disabler/enabler
            
            // questEffects.SetActive(false);
            //radioText.enabled = false;
            //tutoRadio.SetActive(true);
            bpfSounds.SetActive(true);
            //sounds
            drums = drumsObj.GetComponent<AudioSource>();
            riff = riffObj.GetComponent<AudioSource>();
            subb = subbObj.GetComponent<AudioSource>();
            melody = bkgMusic.GetComponent<AudioSource>();        
              
    }
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Main")
        {
            if (questOn == true)
                { 
                    QuestStart();
                    bkgMusic.SetActive(false);
                    melody.mute = true; 
                    //questEffects.SetActive(true);
                    //text.enabled = true;
                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        vPressed = true;
                        activated = !activated;
                        //text.enabled = false;
                        SwitchSound();
                        RadioOn();
                    }   
                
                    
                    //GetVariables();
                      
            }
               
            else if (questOn == false)
               {
                    bkgMusic.SetActive(true);
                    melody.mute = false;
                    activated = false;
                    RadioOn();
                    RadioOff();
               }
        }
      
    }
    void QuestStart()
        // Text de début de quête, tuto activation de la radio
    {
        
                if (vPressed == false)
                {
                    tutoRadio.SetActive(true);
                }
                if (vPressed == true)
                {
                    tutoRadio.SetActive(false);
                }
        
       
    }

    void SwitchSound()
    {

        //radioText.enabled = false;
        tutoRadio.SetActive(false);
        if (activated == true)
        {
            radioSwitchOn.Play();
        }
        if (activated == false)
        {
            radioSwitchOff.Play();
        }
    }

    void RadioOn()
    {
        var emissionA = notesA.emission;
        var emissionB = notesB.emission;

            if (activated == true)
            {
                emissionA.rateOverTime = rateA;
                emissionB.rateOverTime = rateB;
                notesA.startColor = changedColor;
                notesB.startColor = changedColor;
                riff.mute = false;
                drums.mute = false;
                subb.mute = false;
            }

            if (activated == false)
            {
                emissionA.rateOverTime = slowA;
                emissionB.rateOverTime = slowB;
                notesA.startColor = defaultColor;
                notesB.startColor = defaultColor;
                riff.mute = true;
                drums.mute = true;
                subb.mute = true;
            }
    }

    void RadioOff()
    {
        var emissionA = notesA.emission;
        var emissionB = notesB.emission;

        if (questOn == false)
        {
            emissionA.rateOverTime = 0;
            emissionB.rateOverTime = 0;
        } 
    }
}
