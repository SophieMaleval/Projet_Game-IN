using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using TMPro ;
using DG.Tweening ;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string NameScene ;


    public TextMeshProUGUI Message ;
    public AnimationTransitionScene ATS;

    [SerializeField] private RectTransform TitlePanel ;
    [SerializeField] private Button OpenSettingBtn;
    [SerializeField] private Button CloseSettingBtn ;
    private bool SettingOpen = false ;

    [SerializeField] private RectTransform SettingPanel;   

    [SerializeField] public GameObject FadeImage ;

    private CSVReader SettingPanelReader ;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Langue", 0);
        StartCoroutine(WaitTransitionAnim());

        if(SettingPanel.GetComponent<CSVReader>() != null) SettingPanelReader = SettingPanel.GetComponent<CSVReader>() ;
    }

    // Update is called once per frame
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

    public void SetMenuTextLangue(int Langue) // 0 - FR et 1 - EN
    {
        if(Langue == 0)
            Message.text = SettingPanelReader.UIText.MenuFR[0] ;
        if(Langue == 1)
            Message.text = SettingPanelReader.UIText.MenuEN[0] ;
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
