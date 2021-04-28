using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimItemCustomer : MonoBehaviour
{
    [SerializeField] private CharacterCreater CharacterCreaterScript ;
    [SerializeField] private GridDeplacement PlayerDeplacement ;

    private bool AnimStartIsFinish = false ;
    private bool AnimaEndIsStart = false ;


    [Header ("Canvas Object")]
    [SerializeField] private RectTransform Fade ;
    [SerializeField] private RectTransform CustomisationPanel ;
    [Space]

    [SerializeField] private Button TurnLeftPlayer ;
    [SerializeField] private Button TurnRightPlayer ;
    [SerializeField] private Button RandomButton ;
    [SerializeField] private Button SubmitButton ; 
    [Space]

    [SerializeField] private RectTransform SkinButton ;
    [SerializeField] private RectTransform SkinTextButton ;
    [Space]
    [SerializeField] private RectTransform HairButton ;
    [SerializeField] private RectTransform HairTextButton ;
    [Space]
    [SerializeField] private RectTransform BodyButton ;
    [SerializeField] private RectTransform BodyTextButton ;
    [Space]
    [SerializeField] private RectTransform BottomButton ;
    [SerializeField] private RectTransform BottomTextButton ;
    [Space]
    [SerializeField] private RectTransform ShoeButton ;
    [SerializeField] private RectTransform ShoeTextButton ;
    private float EndPanelButtonHeight = 50f ;
    private Vector3 FinalTextButtonScale = new Vector3(0.75f, 0.75f, 0.75f);



    void Start()
    {
       StartCoroutine(DisplayCanvas()); 
    }

    void Update()
    {
        if(AnimStartIsFinish == false)
        {
            if (PlayerDeplacement.transform.position.x > -4.5)
            {
                PlayerDeplacement.AnimMovePlayer(new Vector2(-1f, 0f)) ;
            }

            if(PlayerDeplacement.transform.position.x < -4.5)
            {
                PlayerDeplacement.AnimMovePlayer(Vector2.zero);
            }            

            if(PlayerDeplacement.transform.position.x > -5.075f && PlayerDeplacement.transform.position.x < -4.925f)
            {
                AnimStartIsFinish = true ;  

                PlayerDeplacement.WalkSpeed = 0 ;     
                CharacterCreaterScript.enabled = true ;
                PlayerDeplacement.enabled = false ;     
                PlayerDeplacement.transform.position = new Vector2(-5f, 0f);
            }
        }

        if(AnimaEndIsStart == true)
        {
            if(PlayerDeplacement.transform.position.x < 11)
                PlayerDeplacement.AnimMovePlayer(new Vector2(1f, 0f)) ;
            if(PlayerDeplacement.transform.position.x > 11)
                PlayerDeplacement.AnimMovePlayer(Vector2.zero) ;
        }

    }


    public void LunchEndAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(HideCanvas());
        PlayerDeplacement.enabled = true ;          
        AnimaEndIsStart = true ;
        
        PlayerDeplacement.WalkSpeed = 3 ;     
        CharacterCreaterScript.enabled = false ;
    }



    IEnumerator DisplayCanvas()
    {
        yield return new WaitForSeconds(1f);
        Fade.DOAnchorPosX(1000f,1f);
        yield return new WaitForSeconds(2.5f);
        CustomisationPanel.DOAnchorPosY(0,0.5f);
        yield return new WaitForSeconds(0.5f);
        SkinButton.DOSizeDelta(new Vector2(SkinButton.sizeDelta.x, 50f), 0.5f);
        SkinTextButton.DOScale(FinalTextButtonScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        HairButton.DOSizeDelta(new Vector2(HairButton.sizeDelta.x, 50f), 0.5f);
        HairTextButton.DOScale(FinalTextButtonScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        BodyButton.DOSizeDelta(new Vector2(BodyButton.sizeDelta.x, 50f), 0.5f);
        BodyTextButton.DOScale(FinalTextButtonScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        BottomButton.DOSizeDelta(new Vector2(BottomButton.sizeDelta.x, 50f), 0.5f);
        BottomTextButton.DOScale(FinalTextButtonScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        ShoeButton.DOSizeDelta(new Vector2(ShoeButton.sizeDelta.x, 50f), 0.5f) ;
        ShoeTextButton.DOScale(FinalTextButtonScale, 0.5f);

        yield return new WaitForSeconds(0.25f);
        TurnLeftPlayer.interactable = true ;
        yield return new WaitForSeconds(0.25f);
        TurnRightPlayer.interactable = true ;
        yield return new WaitForSeconds(0.25f);
        RandomButton.interactable = true ;
        yield return new WaitForSeconds(0.25f);
        SubmitButton.interactable = true ;        
    }

    IEnumerator HideCanvas()
    {
        SubmitButton.interactable = false ;    
        yield return new WaitForSeconds(0.05f);
        RandomButton.interactable = false ;
        yield return new WaitForSeconds(0.05f);
        TurnRightPlayer.interactable = false ;                                      
        yield return new WaitForSeconds(0.05f);
        TurnLeftPlayer.interactable = false ;
        yield return new WaitForSeconds(0.05f);


        ShoeButton.DOSizeDelta(new Vector2(ShoeButton.sizeDelta.x, 0f), 0.5f) ;
        ShoeTextButton.DOScale(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.25f);
        BottomButton.DOSizeDelta(new Vector2(BottomButton.sizeDelta.x, 0f), 0.5f);
        BottomTextButton.DOScale(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.25f);
        BodyButton.DOSizeDelta(new Vector2(BodyButton.sizeDelta.x, 0f), 0.5f);
        BodyTextButton.DOScale(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.25f);
        HairButton.DOSizeDelta(new Vector2(HairButton.sizeDelta.x, 0f), 0.5f);
        HairTextButton.DOScale(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.25f);
        SkinButton.DOSizeDelta(new Vector2(SkinButton.sizeDelta.x, 0f), 0.5f);
        SkinTextButton.DOScale(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.25f);
        CustomisationPanel.DOAnchorPosY(25f,0.5f);

        yield return new WaitForSeconds(3.5f);
        Fade.DOAnchorPosX(-100,1f);
        yield return new WaitForSeconds(1.5f);
        AnimaEndIsStart = false ;
        PlayerDeplacement.transform.position = new Vector2(-12.5f, 0);
    }
}
