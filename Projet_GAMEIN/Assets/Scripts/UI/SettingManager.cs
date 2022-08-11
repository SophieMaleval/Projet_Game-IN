using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro ;
using DG.Tweening ;
using AllosiusDev.TranslationSystem;

public class SettingManager : MonoBehaviour
{
    #region Fields

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
        StartCoroutine(WaitAndSetSettingsText());
        //Debug.Log(PlayerPrefs.GetFloat("VolumeGlobal"));
        //Debug.Log(PlayerPrefs.GetFloat("Musique"));
        //Debug.Log(PlayerPrefs.GetFloat("SFX"));
        volumeGlobalSlider.value = PlayerPrefs.GetFloat("VolumeGlobal");
        volumeMusicSlider.value = PlayerPrefs.GetFloat("Musique");
        volumeSfxSlider.value = PlayerPrefs.GetFloat("SFX");
    }

    private void OnEnable() 
    {
        if(LangueManager.Instance.GetCurrentLangage() == AllosiusDev.TranslationSystem.LocalisationManager.Langage.Francais)
        {
            SetFrenchLanguage();
        }

        if (LangueManager.Instance.GetCurrentLangage() == AllosiusDev.TranslationSystem.LocalisationManager.Langage.Anglais)
        {
            SetEnglishLanguage();
        }
    }

    IEnumerator WaitAndSetSettingsText()
    {
        yield return new WaitForSeconds(0.25f);
        SetSettingsTextLangue();     
    }

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
