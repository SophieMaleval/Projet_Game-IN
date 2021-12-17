using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using DG.Tweening ;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public string NameScene ;
    public AnimationTransitionScene ATS;


    public GameObject ToggleFrançais;
    public GameObject ToggleAnglais;

    [SerializeField] private RectTransform TitlePanel ;
    [SerializeField] private Button OpenSettingBtn;
    [SerializeField] private Button CloseSettingBtn ;
    private bool SettingOpen = false ;

    [SerializeField] private RectTransform SettingPanel;   

    [SerializeField] public GameObject FadeImage ;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitTransitionAnim());
    }

    // Update is called once per frame
    void Update()
    {

        if (!SettingOpen && Input.GetKeyDown(KeyCode.Space))
        {
            FadeImage.SetActive(true);
            
            StartCoroutine("Fade");
        
           // Debug.Log("A key or mouse click has been detected");
        }
        
    }


    IEnumerator Fade() 
    {
    
        ATS.ShouldReveal = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Character Customer");
    }

 
    public void OpenSetting()
    {
        //OptionsPanel.SetActive(true);
        StartCoroutine(AnimationPanels(true));
    }

    public void CloseSetting()
    {
        StartCoroutine(AnimationPanels(false));
    }

    public void SetEnglishLanguage()
    {
        Debug.Log("EN");

        ToggleFrançais.SetActive(false);
        ToggleAnglais.SetActive(true);
    }

    public void SetFrenchLanguage()
    {
        Debug.Log("FR");

        ToggleAnglais.SetActive(false);
        ToggleFrançais.SetActive(true);
    }


    IEnumerator WaitTransitionAnim()
    {
        yield return new WaitForSeconds(0.25f);
        FadeImage.GetComponent<AnimationTransitionScene>().enabled = true ;
        yield return new WaitForSeconds(2f) ;
        FadeImage.SetActive(false);
    }


    IEnumerator AnimationPanels(bool OpenSettings)
    {
        if(OpenSettings)
        {
            SettingOpen = true ;

            SettingPanel.DOAnchorPosY(500, 1f);
            OpenSettingBtn.interactable = false ;
            CloseSettingBtn.interactable = true ;

            TitlePanel.DOAnchorPosY(-50, 0.1f);
            yield return new WaitForSeconds(0.1f);
            TitlePanel.DOAnchorPosY(400, 1f);
            yield return new WaitForSeconds(0.75f);


            SettingPanel.gameObject.SetActive(true);
            SettingPanel.GetComponent<Image>().DOFade(0.75f, 1f);
            SettingPanel.DOAnchorPosY(0, 1f);            
        } else {
            CloseSettingBtn.interactable = true ;

            SettingPanel.DOAnchorPosY(-50, 0.1f);
            yield return new WaitForSeconds(0.1f);
            SettingPanel.GetComponent<Image>().DOFade(0f, 1f);            
            SettingPanel.DOAnchorPosY(500, 1f);
            yield return new WaitForSeconds(1f);


            SettingPanel.gameObject.SetActive(false);
            TitlePanel.DOAnchorPosY(0, 1f);

            yield return new WaitForSeconds(1f);
            OpenSettingBtn.interactable = true ; 
            SettingOpen = false ;
        }

    }
}
