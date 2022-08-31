using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using TMPro ;
using DG.Tweening ;
using UnityEngine.SceneManagement;
using AllosiusDev.Audio;
using Core.Session;
using AllosiusDev.TranslationSystem;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    #region Fields

    private bool SettingOpen = false ;

    private bool isHoverUi;

    #endregion

    #region Unity Inspector

    [SerializeField] private ToTranslateObject messageContinue;

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
        
    }
    void Start()
    {
        StartCoroutine(WaitTransitionAnim());


        PlaySound(menuMusic);
        PlaySound(birdsChippingAmbients);
    }


    void Update()
    {
        isHoverUi = IsHoverUI(Input.mousePosition);

        if (!SettingOpen && FadeImage.activeSelf == false && !(Input.GetMouseButtonDown(1)) && !(Input.GetMouseButtonDown(2)))
        {
            if((Input.anyKeyDown && !isHoverUi) || (isHoverUi && Input.anyKeyDown && !(Input.GetMouseButtonDown(0))))
            {
                FadeImage.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(Fade());
            }
        }

    }

    private bool IsHoverUI(Vector3 position)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = position;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        /*foreach (RaycastResult result in raycastResults)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }*/

        return raycastResults.Count > 0;
    }

    IEnumerator WaitTransitionAnim()
    {
        yield return new WaitForSeconds(0.05f) ;
        SettingPanel.gameObject.SetActive(false) ;
        yield return new WaitForSeconds(0.2f) ;
        
        SetMenuTextLangue();
        FadeImage.GetComponent<AnimationTransitionScene>().enabled = true ;
        yield return new WaitForSeconds(2f) ;
        FadeImage.SetActive(false) ;
    }
    
    IEnumerator Fade() 
    {
        ATS.ShouldReveal = false;
        yield return new WaitForSeconds(3f);
        SceneLoader.Instance.ChangeScene(SessionController.Instance.Game.CharacterCustomerScene);
        //SceneManager.LoadScene("Character Customer");
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

    public void SetMenuTextLangue()
    {
        messageContinue.Translation();
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
