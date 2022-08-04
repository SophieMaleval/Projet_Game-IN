using System.Collections;
using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AnimationCustomizer : MonoBehaviour
{
    #region Fields

    private int RotationState = 0 ;

    //private float SkinPanelHeight = 130f;
    //private float SkinPanelFondHeight = 80f;




    //private float OpenSkinGroupHeight = 165f;
    //private float OpenChoiceGroupHeight = 195f;
    //private float CloseGroupHeight = 70f;

    //private float ChoicePanelHeight = 160f;
    //private float ChoicePanelFondHeight = 111f;

    private float PanelAnimationSpeed = 1f;
    //private float ClosePanelSpeed = 0.5f;
    //private float OpenPanelSpeed = 1.5f;

    //private float PanelWidth = 445f;

    private int CurrentCategorie;

    #endregion

    #region UnityInspector

    [Header("Customizer Reference")]
    [SerializeField] private Customizer CustomizerReference;

    [Header("Animation Ouverture et Fermeture Scene")]
    [SerializeField] private Animator RideauxAnimator;

    [Header("Rotate Avatar")]
    public List<Animator> AnimatorsCustom;
    [SerializeField] private List<Vector2> DirectionView;

    [Header ("CustomerPanel")]
    [SerializeField] private RectTransform ContainerChoiceCatPanel ;
    [SerializeField] private RectTransform ContainerSkinPanel ;

    [Space(10)]

    [SerializeField] private RectTransform Panel;
    [SerializeField] private RectTransform ContainerChoicePanel;

    [Space(10)]

    [SerializeField] private RectTransform FondContainer;

    [Header("Button")]
    [SerializeField] private Button RotateAvatarLeft;
    [SerializeField] private Button RotateAvatarRight;

    [SerializeField] private Button PreviousCatBtn;
    [SerializeField] private Button NextCatBtn;



    [Header("UI Text")]
    [SerializeField] private ToTranslateObject TitleCategories;
    [SerializeField] private ToTranslateObject BtnCategorieSkin;
    [SerializeField] private ToTranslateObject BtnCategorieHair;
    [SerializeField] private ToTranslateObject BtnCategorieTop;
    [SerializeField] private ToTranslateObject BtnCategorieBottom;
    [SerializeField] private ToTranslateObject BtnCategorieShoe;
    [SerializeField] private GameObject BtnBackMenuCustomisation;

    [SerializeField] private ToTranslateObject NamingText;
    [SerializeField] private ToTranslateObject BtnRandomText;
    [SerializeField] private ToTranslateObject BtnSubmitText;

    [Header("Translation Keys")]
    [SerializeField] private string chooseSectionKey;
    [SerializeField] private string colorSkinKey;
    [SerializeField] private string hairKey;
    [SerializeField] private string topKey;
    [SerializeField] private string pantsKey;
    [SerializeField] private string shoesKey;
    [SerializeField] private string goodbyeKey;

    #endregion

    #region Behaviour

    private void Start() 
    {   
        StartCoroutine(WaitBeforeOpenningRideaux());

        if (GameManager.Instance.player != null)
        {
            AnimatorsCustom = GameManager.Instance.player.GetComponent<PlayerMovement>().Animators ;
        }

        SetCustomTextLangue();
    }


    IEnumerator WaitBeforeOpenningRideaux()
    {      
        yield return new WaitForSeconds(1f);    
        RideauxAnimator.enabled = true ;
    }

    
    // Rotate Avatar Function
    public void RotateProfilView(int RotationValueAdd)
    {
        CustomizerReference.ResetBtnSprite(RotateAvatarLeft);
        CustomizerReference.ResetBtnSprite(RotateAvatarRight);

        RotationState += RotationValueAdd ;
        UpdateRotationView();

        //Debug.Log(RotationState);
    }

    public void UpdateRotationView()
    {
        if (RotationState < 0) RotationState = DirectionView.Count - 1;
        if (RotationState > DirectionView.Count - 1) RotationState = 0;

        Animate();
    }

    void Animate() 
    {
        // Applique la même valeur à tout les Animator
        for (int i = 0; i < AnimatorsCustom.Count; i++)
        {
            if(AnimatorsCustom[i].runtimeAnimatorController != null)
            {
                // Set Up la direction du déplacement
                AnimatorsCustom[i].SetFloat("AnimLastMoveX", DirectionView[RotationState].x) ;
                AnimatorsCustom[i].SetFloat("AnimLastMoveY", DirectionView[RotationState].y) ;
            }

            if(RotationState == 3) AnimatorsCustom[i].gameObject.GetComponent<SpriteRenderer>().flipX = true ;
                else AnimatorsCustom[i].gameObject.GetComponent<SpriteRenderer>().flipX = false ;
        }
    }



    // Animation Panel
    IEnumerator ChangePanel(int CategorieNumber)
    {
        CurrentCategorie = CategorieNumber ;
        if(CurrentCategorie != 6)
        {
            Panel.DOAnchorPosY(-100f, 0.25f);
            yield return new WaitForSeconds(0.25f);
            Panel.DOAnchorPosY(1000f, 1f);

            yield return new WaitForSeconds(1.5f);
            ChangeTitleCategories(CategorieNumber);
            SwitchCat(CategorieNumber);

            if(CurrentCategorie == 0) BtnBackMenuCustomisation.SetActive(false);
            else BtnBackMenuCustomisation.SetActive(true);   

            if(CurrentCategorie == 1) BtnBackMenuCustomisation.GetComponent<RectTransform>().anchoredPosition = new Vector2(BtnBackMenuCustomisation.GetComponent<RectTransform>().anchoredPosition.x, -117.5f) ;         
            if(CurrentCategorie > 1) BtnBackMenuCustomisation.GetComponent<RectTransform>().anchoredPosition = new Vector2(BtnBackMenuCustomisation.GetComponent<RectTransform>().anchoredPosition.x, -200f) ;         

            Panel.DOAnchorPosY(-100f, .75f);
            yield return new WaitForSeconds(.75f);
            Panel.DOAnchorPosY(-25f, 0.25f);
            yield return new WaitForSeconds(0.25f);            
        }
    }

    void SwitchCat(int CatNumber)
    {
        if(CatNumber == 0)
        {
            FondContainer.sizeDelta = new Vector2(415f, 180f) ;

            ContainerChoiceCatPanel.gameObject.SetActive(true) ; 
            ContainerSkinPanel.gameObject.SetActive(false) ;
            ContainerChoicePanel.gameObject.SetActive(false) ;            
        }
     
        if(CatNumber == 1)
        {
            FondContainer.sizeDelta = new Vector2(400f, 175f + 20f) ;
    
            ContainerChoiceCatPanel.gameObject.SetActive(false) ; 
            ContainerSkinPanel.gameObject.SetActive(true) ;
            ContainerChoicePanel.gameObject.SetActive(false) ;    
        }
        if(CatNumber > 1 && CatNumber < 6)
        {
            FondContainer.sizeDelta = new Vector2(400f, 235f + 40f) ;
    
            ContainerChoiceCatPanel.gameObject.SetActive(false) ; 
            ContainerSkinPanel.gameObject.SetActive(false) ;
            ContainerChoicePanel.gameObject.SetActive(true) ;    
        } 


        CustomizerReference.CurrentCategorie = CurrentCategorie - 1 ; 
        CustomizerReference.ChangeCategorie();
    }

    public void PreviousCategrorie()
    {
        CustomizerReference.ResetBtnSprite(PreviousCatBtn);

        int PreviousCat ;
        if(CurrentCategorie-1 < 0 )
            CurrentCategorie = 5 ;
        else CurrentCategorie -- ;

        PreviousCat = CurrentCategorie ;
        
        StartCoroutine(ChangePanel(PreviousCat));
    }
    public void NextCategrorie()
    {
        CustomizerReference.ResetBtnSprite(NextCatBtn);

        int NextCat ;
        if(CurrentCategorie +1 > 5 )
            CurrentCategorie = 0 ;
        else CurrentCategorie ++ ;

        NextCat = CurrentCategorie ;

        StartCoroutine(ChangePanel(NextCat));
    }
    public void MenuCategorie()
    {
        CustomizerReference.ResetBtnSprite(RotateAvatarLeft);
        CustomizerReference.ResetBtnSprite(RotateAvatarRight);

        CurrentCategorie = 0 ;

        StartCoroutine(ChangePanel(0));
    }

    public void InterractSkinButton()
    {    
        StartCoroutine(ChangePanel(1));
    }   

    public void InterractHairButton()
    { 
        StartCoroutine(ChangePanel(2));
    }
    public void InterractTopButton()
    { 
        StartCoroutine(ChangePanel(3));
    }
    public void InterractPantsButton()
    {   
        StartCoroutine(ChangePanel(4));
    }
    public void InterractShoeButton()
    {   
        StartCoroutine(ChangePanel(5));
    }


    public void CustomizationFinish()
    {    
        StartCoroutine(ChangePanel(6));
        RideauxAnimator.SetBool("Quit Custom ?", true) ;
    }


    public void ChangeTitleCategories(int NextCat)
    {
        TitleCategories.GetComponent<RectTransform>().DOScaleY(0, PanelAnimationSpeed/4)
            .OnComplete(() => 
            {
                if (NextCat == 0)
                { TitleCategories.SetTranslationKey(chooseSectionKey); ; }
                if (NextCat == 1)
                { TitleCategories.SetTranslationKey(colorSkinKey); ; }
                if (NextCat == 2)
                { TitleCategories.SetTranslationKey(hairKey); ; }
                if (NextCat == 3)
                { TitleCategories.SetTranslationKey(topKey); ; }
                if (NextCat == 4)
                { TitleCategories.SetTranslationKey(pantsKey); ; }
                if (NextCat == 5)
                { TitleCategories.SetTranslationKey(shoesKey); ; }
                if (NextCat == 6)
                { TitleCategories.SetTranslationKey(goodbyeKey); ; }


                TitleCategories.GetComponent<RectTransform>().DOScaleY(0.75f, PanelAnimationSpeed/4);
            });
    }

    void SetCustomTextLangue()
    {
        TitleCategories.SetTranslationKey(chooseSectionKey);

        BtnCategorieSkin.Translation();
        BtnCategorieHair.Translation();
        BtnCategorieTop.Translation();
        BtnCategorieBottom.Translation();
        BtnCategorieShoe.Translation();

        NamingText.Translation();
        BtnRandomText.Translation();
        BtnSubmitText.Translation();
    }

    #endregion
}
