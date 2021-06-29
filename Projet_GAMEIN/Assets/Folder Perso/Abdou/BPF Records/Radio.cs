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
    //private GameObject radioText;
    private GameObject drumsObj;
    private GameObject riffObj;
    private GameObject subbObj;
    private GameObject bkgMusic;

    private Scene currentScene;
    private string sceneName;


    // Start is called before the first frame update
    void Start()
    {
        /*Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Main")
        {
            StartCoroutine(RecupObj());
        }
        else
        {
            return;
        }
        
       /* //finders
        bpfSounds = GameObject.FindGameObjectWithTag("BPF Sounds");
        drumsObj = GameObject.FindGameObjectWithTag("Drums");
        riffObj = GameObject.FindGameObjectWithTag("Riff");
        subbObj = GameObject.FindGameObjectWithTag("Subb");

        //questEffects = GameObject.FindGameObjectWithTag("QuestEffects");

        // --UI
        radioText = GameObject.FindGameObjectWithTag("RadioText").GetComponent<TextMeshProUGUI>();

        //disabler/enabler
        bpfSounds.SetActive(false);
       // questEffects.SetActive(false);
        radioText.enabled = false;

        //sounds
        drums = drumsObj.GetComponent<AudioSource>();
        riff = riffObj.GetComponent<AudioSource>();
        subb = subbObj.GetComponent<AudioSource>();*/
    }

    IEnumerator RecupObj()
    {
        yield return new WaitForSeconds(3f);
        //finders
        bpfSounds = GameObject.FindGameObjectWithTag("BPF Sounds");
        drumsObj = GameObject.FindGameObjectWithTag("Drums");
        riffObj = GameObject.FindGameObjectWithTag("Riff");
        subbObj = GameObject.FindGameObjectWithTag("Subb");
        // --UI
        radioText = GameObject.FindGameObjectWithTag("RadioText").GetComponent<TextMeshProUGUI>();
        //disabler/enabler
        bpfSounds.SetActive(false);
        // questEffects.SetActive(false);
        radioText.enabled = false;

        //sounds
        drums = drumsObj.GetComponent<AudioSource>();
        riff = riffObj.GetComponent<AudioSource>();
        subb = subbObj.GetComponent<AudioSource>();

    }
    void Update()
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
        }
      else
        {
            melody.mute = false;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bpfSounds = GameObject.FindGameObjectWithTag("BPF Sounds");
        drumsObj = GameObject.FindGameObjectWithTag("Drums");
        riffObj = GameObject.FindGameObjectWithTag("Riff");
        subbObj = GameObject.FindGameObjectWithTag("Subb");
        bkgMusic = GameObject.FindGameObjectWithTag("Melody");
        // --UI
        radioText = GameObject.FindGameObjectWithTag("RadioText").GetComponent<TextMeshProUGUI>();
        //disabler/enabler
        bpfSounds.SetActive(false);
        // questEffects.SetActive(false);
        radioText.enabled = false;

        //sounds
        drums = drumsObj.GetComponent<AudioSource>();
        riff = riffObj.GetComponent<AudioSource>();
        subb = subbObj.GetComponent<AudioSource>();
        melody = bkgMusic.GetComponent<AudioSource>();
    }



    void QuestStart()
    {
        if (vPressed == false)
        {
            radioText.enabled = true;
        }
        if (vPressed == true)
        {
            radioText.enabled = false;
        }
    }

    void SwitchSound()
    {
        radioText.enabled = false;
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
            emissionA.rateOverTime = slowA;
            emissionB.rateOverTime = slowB;
        } 
    }
}
