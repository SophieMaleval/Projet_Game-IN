using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro ;
using DG.Tweening ;
using HeXa;

public class SettingManager : MonoBehaviour
{
    #region Fields

    private CSVReader TextUILocation ;

    private bool CanCheckLanguage = false ;

    #endregion

    #region UnityInspector

    [Header ("Set Parameter")]
    [SerializeField] private AudioMixer MyAudioMixer;    
    [SerializeField] private GameObject ToggleFrançais;
    [SerializeField] private GameObject ToggleAnglais;

    [Header ("UI Text")]

    [SerializeField] private ToTranslateObject settings;
    [SerializeField] private ToTranslateObject soundCat;
    [SerializeField] private ToTranslateObject volumeGlobal;
    [SerializeField] private ToTranslateObject volumeMusic;
    [SerializeField] private ToTranslateObject volumeSFx;
    [SerializeField] private ToTranslateObject languageCat;
    [SerializeField] private ToTranslateObject french;
    [SerializeField] private ToTranslateObject english;

    [Header("Sounds Parameters")]
    [SerializeField] private Slider volumeGlobalSlider;
    [SerializeField] private Slider volumeMusicSlider;
    [SerializeField] private Slider volumeSfxSlider;

    #endregion

    #region Behaviour

    private void Start() 
    {
        if(gameObject.GetComponent<CSVReader>() != null) // Récupère les Textes lorsque l'on est sur le menu
            TextUILocation = GetComponent<CSVReader>() ;

        if (gameObject.GetComponent<CSVReader>() == null)
        {
            if(GameManager.Instance.player != null)
            {
                TextUILocation = GameManager.Instance.player.playerBackpack.GetComponent<CSVReader>();
            }
            else
            {
                Debug.LogWarning("Player is null");
            }
            
        }

        if(TextUILocation != null)
        {
            StartCoroutine(WaitAndSetSettingsText());
        }

        //Debug.Log(PlayerPrefs.GetFloat("VolumeGlobal"));
        //Debug.Log(PlayerPrefs.GetFloat("Musique"));
        //Debug.Log(PlayerPrefs.GetFloat("SFX"));
        volumeGlobalSlider.value = PlayerPrefs.GetFloat("VolumeGlobal");
        volumeMusicSlider.value = PlayerPrefs.GetFloat("Musique");
        volumeSfxSlider.value = PlayerPrefs.GetFloat("SFX");
    }

    private void OnEnable() 
    {
        if(LangueManager.Instance.GetCurrentLangage() == HeXa.LocalisationManager.Langage.Francais)
        {
            SetFrenchLanguage();
        }

        if (LangueManager.Instance.GetCurrentLangage() == HeXa.LocalisationManager.Langage.Anglais)
        {
            SetEnglishLanguage();
        }
    }

    IEnumerator WaitAndSetSettingsText()
    {
        yield return new WaitForSeconds(0.25f);
        SetSettingsTextLangue();
        CanCheckLanguage = true ;        
    }

    private void  Update() 
    {
        //if(CanCheckLanguage && TextUILocation != null)
        //{
            //if (PlayerPrefs.GetInt("Langue") == 0 && SettingTitle.text != TextUILocation.UIText.SettingFR[0]) ;    //SetSettingsTextLangue(0);
            //if (PlayerPrefs.GetInt("Langue") == 1 && SettingTitle.text != TextUILocation.UIText.SettingEN[0]) ;    //SetSettingsTextLangue(1);
        //}
    }


    /*public void SetVolumeMusique(float sliderValue)
    {
        float newSliderValue = Mathf.Log10(sliderValue) * 40;
        MyAudioMixer.SetFloat("Musique", newSliderValue);

        PlayerPrefs.SetFloat("Musique", sliderValue);
        Debug.Log(PlayerPrefs.GetFloat("Musique"));
    }
    
    public void SetVolumeSFX(float sliderValue)
    {
        float newSliderValue = Mathf.Log10(sliderValue) * 40;
        MyAudioMixer.SetFloat("SFX", newSliderValue);

        PlayerPrefs.SetFloat("SFX", sliderValue);
        Debug.Log(PlayerPrefs.GetFloat("SFX"));
    }

    public void SetVolumeGlobal(float sliderValue)
    {
        float newSliderValue = Mathf.Log10(sliderValue) * 40;
        MyAudioMixer.SetFloat("SFX", newSliderValue);
        MyAudioMixer.SetFloat("Ambient_Menu", newSliderValue);
        MyAudioMixer.SetFloat("Musique", newSliderValue);

        PlayerPrefs.SetFloat("VolumeGlobal", sliderValue);
        Debug.Log(PlayerPrefs.GetFloat("VolumeGlobal"));
    }*/




    public void SetFrenchLanguage() // PlayerPrefs("Langue") == 0 -> FR
    {
        PlayerPrefs.SetInt("Langue", 0);
        LangueManager.Instance.ChangeLangage(LocalisationManager.Langage.Francais);
        SetSettingsTextLangue();
        ToggleAnglais.SetActive(false);
        ToggleFrançais.SetActive(true);
    }
    public void SetEnglishLanguage() // PlayerPrefs("Langue") == 1 -> EN
    {
        PlayerPrefs.SetInt("Langue", 1);
        LangueManager.Instance.ChangeLangage(LocalisationManager.Langage.Anglais);
        SetSettingsTextLangue();
        ToggleFrançais.SetActive(false);
        ToggleAnglais.SetActive(true);
    }

    void SetSettingsTextLangue()
    {
        settings.Translation();
        soundCat.Translation();
        volumeGlobal.Translation();
        volumeMusic.Translation();
        volumeSFx.Translation();
        languageCat.Translation();
        french.Translation();
        english.Translation();
    }

    #endregion
}
