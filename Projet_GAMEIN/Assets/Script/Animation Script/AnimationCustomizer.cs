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
    [Header ("Customizer Reference")]
    [SerializeField] private Customizer CustomizerReference ;

    [Header ("Animation Ouverture et Fermeture Scene")]
    [SerializeField] private Animator RideauxAnimator ;

    [Header ("Rotate Avatar")]
    public List<Animator> AnimatorsCustom ;
    [SerializeField] private List<Vector2> DirectionView ;
    private int RotationState = 0 ;

    [Header ("CustomerPanel")]
    [SerializeField] private RectTransform ContainerChoiceCatPanel ;
    [SerializeField] private RectTransform ContainerSkinPanel ;
    private float SkinPanelHeight = 130f ;
    private float SkinPanelFondHeight = 80f ;
    
    [Space(10)]

    [SerializeField] private RectTransform Panel ;
    [SerializeField] private RectTransform ContainerChoicePanel ;

    [Space(10)]
    
    [Header ("Button")]
    [SerializeField] private Button RotateAvatarLeft ;
    [SerializeField] private Button RotateAvatarRight ;

    [SerializeField] private Button PreviousCatBtn ;
    [SerializeField] private Button NextCatBtn ;



    [Header ("UI Text")]
    [SerializeField] private TextMeshProUGUI TitleCategories ;
        [SerializeField] private TextMeshProUGUI BtnCategorieSkin ;
        [SerializeField] private TextMeshProUGUI BtnCategorieHair ;
        [SerializeField] private TextMeshProUGUI BtnCategorieTop ;
        [SerializeField] private TextMeshProUGUI BtnCategorieBottom ;
        [SerializeField] private TextMeshProUGUI BtnCategorieShoe ;

    [SerializeField] private TextMeshProUGUI NamingText ;
    [SerializeField] private TextMeshProUGUI BtnRandomText ;
    [SerializeField] private TextMeshProUGUI BtnSubmitText ;
    private CSVReader TextUILocation ;

    


    private float OpenSkinGroupHeight = 165f ;
    private float OpenChoiceGroupHeight = 195f ;
    private float CloseGroupHeight = 70f ;

    private float ChoicePanelHeight = 160f ;
    private float ChoicePanelFondHeight = 111f ;   

        private float PanelAnimationSpeed = 1f ;
        private float ClosePanelSpeed = 0.5f ;
        private float OpenPanelSpeed = 1.5f ;

    private float PanelWidth = 445f ;

    private int CurrentCategorie ;
    
    private void Start() 
    {   
        StartCoroutine(WaitBeforeOpenningRideaux());

        if(GameObject.Find("Player") != null)
        {
            AnimatorsCustom = GameObject.Find("Player").GetComponent<PlayerMovement>().Animators ;
            TextUILocation = GameObject.Find("Player Backpack").GetComponent<CSVReader>() ;
        }

        SetCustomTextLangue(PlayerPrefs.GetInt("Langue"));
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
        if(RotationState < 0) RotationState = DirectionView.Count - 1 ;
        if(RotationState > DirectionView.Count - 1) RotationState = 0 ;

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
            ContainerChoiceCatPanel.gameObject.SetActive(true) ; 
            ContainerSkinPanel.gameObject.SetActive(false) ;
            ContainerChoicePanel.gameObject.SetActive(false) ;            
        }
     
        if(CatNumber == 1)
        {
            ContainerChoiceCatPanel.gameObject.SetActive(false) ; 
            ContainerSkinPanel.gameObject.SetActive(true) ;
            ContainerChoicePanel.gameObject.SetActive(false) ;    
        }
        if(CatNumber > 1 && CatNumber < 6)
        {
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
            .OnComplete(() => {
                if(PlayerPrefs.GetInt("Langue") == 0)
                {
                    if(NextCat == 0)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationFR[0] ; ;}
                    if(NextCat == 1)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationFR[6] ; ;}
                    if(NextCat == 2)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationFR[2] ; ;}
                    if(NextCat == 3)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationFR[3] ; ;}
                    if(NextCat == 4)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationFR[4] ; ;}
                    if(NextCat == 5)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationFR[5] ; ;}
                    if(NextCat == 6)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationFR[10] ; ;}                    
                }

                if(PlayerPrefs.GetInt("Langue") == 1)
                {
                    if(NextCat == 0)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationEN[0] ; ;}
                    if(NextCat == 1)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationEN[6] ; ;}
                    if(NextCat == 2)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationEN[2] ; ;}
                    if(NextCat == 3)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationEN[3] ; ;}
                    if(NextCat == 4)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationEN[4] ; ;}
                    if(NextCat == 5)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationEN[5] ; ;}
                    if(NextCat == 6)
                        {TitleCategories.text = TextUILocation.UIText.CustomisationEN[10] ; ;}                    
                }        


                TitleCategories.GetComponent<RectTransform>().DOScaleY(0.75f, PanelAnimationSpeed/4);
            });
    }

    void SetCustomTextLangue(int Langue) // 0 - FR et 1 - EN
    {
        if(Langue == 0)
        {
            TitleCategories.text = TextUILocation.UIText.CustomisationFR[0] ;
        
            BtnCategorieSkin.text = TextUILocation.UIText.CustomisationFR[1] ;
            BtnCategorieHair.text = TextUILocation.UIText.CustomisationFR[2] ;
            BtnCategorieTop.text = TextUILocation.UIText.CustomisationFR[3] ;
            BtnCategorieBottom.text = TextUILocation.UIText.CustomisationFR[4] ;
            BtnCategorieShoe.text = TextUILocation.UIText.CustomisationFR[5] ;

            NamingText.text = TextUILocation.UIText.CustomisationFR[7] ;
            BtnRandomText.text = TextUILocation.UIText.CustomisationFR[8] ;
            BtnSubmitText.text = TextUILocation.UIText.CustomisationFR[9] ;
        }

        if(Langue == 1)
        {
            TitleCategories.text = TextUILocation.UIText.CustomisationEN[0] ;
        
            BtnCategorieSkin.text = TextUILocation.UIText.CustomisationEN[1] ;
            BtnCategorieHair.text = TextUILocation.UIText.CustomisationEN[2] ;
            BtnCategorieTop.text = TextUILocation.UIText.CustomisationEN[3] ;
            BtnCategorieBottom.text = TextUILocation.UIText.CustomisationEN[4] ;
            BtnCategorieShoe.text = TextUILocation.UIText.CustomisationEN[5] ;

            NamingText.text = TextUILocation.UIText.CustomisationEN[7] ;
            BtnRandomText.text = TextUILocation.UIText.CustomisationEN[8] ;
            BtnSubmitText.text = TextUILocation.UIText.CustomisationEN[9] ;
        }
    }
}
