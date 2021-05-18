using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public bool questOn = false;
    bool activated = false;
    bool vPressed = false;

    [Header("Textes")]
    public TextMeshProUGUI text;


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


    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
      if (questOn == true)
        {
            QuestStart();
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
    }

    void QuestStart()
    {
        if (vPressed == false)
        {
            text.enabled = true;
        }
        if (vPressed == true)
        {
            text.enabled = false;
        }
    }

    void SwitchSound()
    {
        text.enabled = false;
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
