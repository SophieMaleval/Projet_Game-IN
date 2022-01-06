using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro ;
using DG.Tweening ;

public class SettingManager : MonoBehaviour
{
    [Header ("Set Parameter")]
    [SerializeField] private AudioMixer MyAudioMixer;    
    [SerializeField] private GameObject ToggleFrançais;
    [SerializeField] private GameObject ToggleAnglais;

    [Header ("UI Text")]
    [SerializeField] private TextMeshProUGUI SettingTitle;
        [SerializeField] private TextMeshProUGUI SoundCatTitle;    
            [SerializeField] private TextMeshProUGUI VolumeGlobalTitle;
            [SerializeField] private TextMeshProUGUI VolumeMusicTitle;
            [SerializeField] private TextMeshProUGUI VolumeSFxTitle;
        [SerializeField] private TextMeshProUGUI LanguageCatTitle;
            [SerializeField] private TextMeshProUGUI FrenchTitle;
            [SerializeField] private TextMeshProUGUI EnglishTitle;
    private CSVReader TextUILocation ;

    private bool CanCheckLanguage = false ;

    private void Start() 
    {
        if(gameObject.GetComponent<CSVReader>() != null) // Récupère les Textes lorsque l'on est sur le menu
            TextUILocation = GetComponent<CSVReader>() ;
        
        if(gameObject.GetComponent<CSVReader>() == null && GameObject.Find("Player") != null)
            TextUILocation = GameObject.Find("Player Backpack").GetComponent<CSVReader>() ;

        if(TextUILocation != null)
        {
            StartCoroutine(WaitAndSetSettingsText());
        }            
    }

    private void OnEnable() 
    {
        if(PlayerPrefs.GetInt("Langue") == 0)
        {
            ToggleAnglais.SetActive(false);
            ToggleFrançais.SetActive(true);
        }

        if(PlayerPrefs.GetInt("Langue") == 1)
        {
            ToggleFrançais.SetActive(false);
            ToggleAnglais.SetActive(true);  
        }
    }

    IEnumerator WaitAndSetSettingsText()
    {
        yield return new WaitForSeconds(0.1f);
        SetSettingsTextLangue(PlayerPrefs.GetInt("Langue"));
        CanCheckLanguage = true ;        
    }

    private void  Update() 
    {
        if(CanCheckLanguage && TextUILocation != null)
        {
            if(PlayerPrefs.GetInt("Langue") == 0 && SettingTitle.text != TextUILocation.UIText.SettingFR[0] )    SetSettingsTextLangue(0);
            if(PlayerPrefs.GetInt("Langue") == 1 && SettingTitle.text != TextUILocation.UIText.SettingEN[0] )    SetSettingsTextLangue(1);
        }
    }


    public void SetVolumeMusique(float sliderValue)
    {    MyAudioMixer.SetFloat("Musique",Mathf.Log10(sliderValue) * 20 );       }
    
    public void SetVolumeSFX(float sliderValue)
    {    MyAudioMixer.SetFloat("SFX",Mathf.Log10(sliderValue) * 20 );    }

    public void SetVolumeGlobal(float sliderValue)
    {
        MyAudioMixer.SetFloat("SFX",Mathf.Log10(sliderValue) * 20 );
        MyAudioMixer.SetFloat("Ambient_Menu",Mathf.Log10(sliderValue) * 20 );
        MyAudioMixer.SetFloat("Musique",Mathf.Log10(sliderValue) * 20 );
    }




    public void SetFrenchLanguage() // PlayerPrefs("Langue") == 0 -> FR
    {
        PlayerPrefs.SetInt("Langue", 0);
        SetSettingsTextLangue(0);
        ToggleAnglais.SetActive(false);
        ToggleFrançais.SetActive(true);
    }
    public void SetEnglishLanguage() // PlayerPrefs("Langue") == 1 -> EN
    {
        PlayerPrefs.SetInt("Langue", 1);
        SetSettingsTextLangue(1);
        ToggleFrançais.SetActive(false);
        ToggleAnglais.SetActive(true);
    }

    void SetSettingsTextLangue(int Langue) // 0 - FR et 1 - EN
    {
        if(Langue == 0)
        {
            SettingTitle.text = TextUILocation.UIText.SettingFR[0] ;
                SoundCatTitle.text = TextUILocation.UIText.SettingFR[1] ;
                    VolumeGlobalTitle.text = TextUILocation.UIText.SettingFR[2] ;
                    VolumeMusicTitle.text = TextUILocation.UIText.SettingFR[3] ;
                    VolumeSFxTitle.text = TextUILocation.UIText.SettingFR[4] ;
                LanguageCatTitle.text = TextUILocation.UIText.SettingFR[5] ;
                    FrenchTitle.text = TextUILocation.UIText.SettingFR[6] ;
                    EnglishTitle.text = TextUILocation.UIText.SettingFR[7] ;
        }

        if(Langue == 1)
        {
            SettingTitle.text = TextUILocation.UIText.SettingEN[0] ;
                SoundCatTitle.text = TextUILocation.UIText.SettingEN[1] ;
                    VolumeGlobalTitle.text = TextUILocation.UIText.SettingEN[2] ;
                    VolumeMusicTitle.text = TextUILocation.UIText.SettingEN[3] ;
                    VolumeSFxTitle.text = TextUILocation.UIText.SettingEN[4] ;
                LanguageCatTitle.text = TextUILocation.UIText.SettingEN[5] ;
                    FrenchTitle.text = TextUILocation.UIText.SettingEN[6] ;
                    EnglishTitle.text = TextUILocation.UIText.SettingEN[7] ;
        }
    }
}
