using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using TMPro ;
using DG.Tweening ;
using UnityEngine.SceneManagement;
using AllosiusDev.Audio;

public class MenuManager : MonoBehaviour
{
    #region Fields

    private bool SettingOpen = false ;

    private CSVReader SettingPanelReader ;

    #endregion

    #region Unity Inspector

    public string NameScene ;


    public TextMeshProUGUI Message ;
    public AnimationTransitionScene ATS;

    [SerializeField] private RectTransform TitlePanel ;
    [SerializeField] private Button OpenSettingBtn;
    [SerializeField] private Button CloseSettingBtn ;

    [SerializeField] private RectTransform SettingPanel;
    [SerializeField] private RectTransform ControlsPanel;
    [SerializeField] private RectTransform CreditsPanel;

    [SerializeField] public GameObject FadeImage ;

    [Header("Sounds")]
    [SerializeField] private AudioData menuMusic;
    [SerializeField] private AudioData birdsChippingAmbients;

    #endregion

    #region Behaviour

    private void Awake()
    {
        PlayerPrefs.SetFloat("VolumeGlobal", 1.0f);
        PlayerPrefs.SetFloat("Musique", 1.0f);
        PlayerPrefs.SetFloat("SFX", 1.0f);
    }
    void Start()
    {
        //PlayerPrefs.SetInt("Langue", 0);
        StartCoroutine(WaitTransitionAnim());

        if(SettingPanel.GetComponent<CSVReader>() != null) SettingPanelReader = SettingPanel.GetComponent<CSVReader>() ;


        PlaySound(menuMusic);
        PlaySound(birdsChippingAmbients);
    
    
        /* A Supprimer */
        PlayerPrefs.SetInt("LaurentSayHello", 0);
    
    }


    void Update()
    {
        if (!SettingOpen && (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) && FadeImage.activeSelf == false)
        {
            FadeImage.SetActive(true);
            StopAllCoroutines();
            StartCoroutine("Fade");
        }

    }

    IEnumerator WaitTransitionAnim()
    {
        yield return new WaitForSeconds(0.05f) ;
        SettingPanel.gameObject.SetActive(false) ;
        yield return new WaitForSeconds(0.2f) ;
        
        SetMenuTextLangue(0);
        FadeImage.GetComponent<AnimationTransitionScene>().enabled = true ;
        yield return new WaitForSeconds(2f) ;
        FadeImage.SetActive(false) ;
    }
    
    IEnumerator Fade() 
    {
        ATS.ShouldReveal = false;
        AllosiusDev.Audio.AudioController.Instance.StopAllAmbients();
        AllosiusDev.Audio.AudioController.Instance.StopAllMusics();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Character Customer");
    }

 
    public void OpenSetting()
    {
        StartCoroutine(AnimationPanels(true, SettingPanel)); 
    }
    public void CloseSetting()
    {
        StartCoroutine(AnimationPanels(false, SettingPanel));
    }


    public void OpenControls()
    {
        StartCoroutine(AnimationPanels(true, ControlsPanel));
    }
    public void CloseControls()
    {
        StartCoroutine(AnimationPanels(false, ControlsPanel));
    }
    public void OpenCredits()
    {
        StartCoroutine(AnimationPanels(true, CreditsPanel));
    }
    public void CloseCredits()
    {
        StartCoroutine(AnimationPanels(false, CreditsPanel));
    }

    public void SetMenuTextLangue(int Langue) // 0 - FR et 1 - EN
    {
        if(Langue == 0)
            Message.text = SettingPanelReader.UIText.MenuFR[0] ;
        if(Langue == 1)
            Message.text = SettingPanelReader.UIText.MenuEN[0] ;
    }




    IEnumerator AnimationPanels(bool OpenSettings, RectTransform PanelAnimate)
    {
        if(OpenSettings)
        {
            SettingOpen = true ;

            PanelAnimate.DOAnchorPosY(500, 1f);
            OpenSettingBtn.interactable = false ;
            CloseSettingBtn.interactable = true ;

            TitlePanel.DOAnchorPosY(-50, 0.1f);
            yield return new WaitForSeconds(0.1f);
            TitlePanel.DOAnchorPosY(400, 1f);
            yield return new WaitForSeconds(0.75f);


            PanelAnimate.gameObject.SetActive(true);
            PanelAnimate.GetComponent<Image>().DOFade(0.75f, 1f);
            PanelAnimate.DOAnchorPosY(0, 1f);            
        } else {
            CloseSettingBtn.interactable = true ;

            PanelAnimate.DOAnchorPosY(-50, 0.1f);
            yield return new WaitForSeconds(0.1f);
            PanelAnimate.GetComponent<Image>().DOFade(0f, 1f);            
            PanelAnimate.DOAnchorPosY(500, 1f);
            yield return new WaitForSeconds(1f);


            PanelAnimate.gameObject.SetActive(false);
            TitlePanel.DOAnchorPosY(0, 1f);

            yield return new WaitForSeconds(1f);
            OpenSettingBtn.interactable = true ; 
            SettingOpen = false ;
        }
    }

    public void PlaySound(AudioData audioData)
    {
        AllosiusDev.Audio.AudioController.Instance.PlayAudio(audioData);
    }

    #endregion
}
