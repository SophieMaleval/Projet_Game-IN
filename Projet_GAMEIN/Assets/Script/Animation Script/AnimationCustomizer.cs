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
    [SerializeField] private GameObject FadeImage ;
    [SerializeField] private Animator RideauxAnimator ;

    [Header ("Rotate Avatar")]
    public List<Animator> AnimatorsCustom ;
    [SerializeField] private List<Vector2> DirectionView ;
    private int RotationState = 0 ;


    [Header ("CustomerPanel")]
    //[SerializeField] private RectTransform SkinGroup ;
    //[SerializeField] private RectTransform SkinPanel ;
    [SerializeField] private RectTransform ContainerSkinPanel ;
    //[SerializeField] private RectTransform FondContainerSkinPanel ;
    private float SkinPanelHeight = 130f ;
    private float SkinPanelFondHeight = 80f ;
    
    [Space(10)]

    [SerializeField] private RectTransform ChoisingGroup ;
    [SerializeField] private RectTransform ChoisingPanel ;
    [SerializeField] private RectTransform ChoiceContainerPanel ;
    [SerializeField] private RectTransform FondChoiceContainerPanel ;

    [Space(10)]
    
    [SerializeField] private TextMeshProUGUI TitleCategories ;


    private float OpenSkinGroupHeight = 165f ;
    private float OpenChoiceGroupHeight = 195f ;
    private float CloseGroupHeight = 70f ;

    private float ChoicePanelHeight = 160f ;
    private float ChoicePanelFondHeight = 111f ;   

        private float PanelAnimationSpeed = 1f ;
        private float ClosePanelSpeed = 0.5f ;
        private float OpenPanelSpeed = 1.5f ;

    private float PanelWidth = 445f ;

    
    private void Start() 
    {   StartCoroutine(WaitBeforeOpenningRideaux());    }

    IEnumerator WaitBeforeOpenningRideaux()
    {
        FadeImage.GetComponent<Image>().DOFade(0, 1f);        
        yield return new WaitForSeconds(1f);
        FadeImage.SetActive(false);        
        RideauxAnimator.enabled = true ;
    }

    
    // Rotate Avatar Function
    public void RotateProfilView(int RotationValueAdd)
    {
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
    void InterractPanelButton(int StateChoice, RectTransform CategorieGroup, RectTransform CategoriePanel, RectTransform FondContainerCategoriePanel, RectTransform ContainerCategoriePanel, float CategoriePanelHeight, float CategoriePanelFondHeight, float OpenCategorieGroupHeight, int ChangeTitleCategoriesInt)
    {
        // Ferme le Panel
        if(StateChoice == 0)
        {
            CategoriePanel.DOSizeDelta(new Vector2(PanelWidth,0f), PanelAnimationSpeed);  
            FondContainerCategoriePanel.DOSizeDelta(new Vector2(144f, 0f), PanelAnimationSpeed).OnComplete(() => {FondContainerCategoriePanel.sizeDelta = new Vector2(FondContainerCategoriePanel.sizeDelta.x, 20f);});
            CategorieGroup.DOSizeDelta(new Vector2(ChoisingGroup.sizeDelta.x, CloseGroupHeight), PanelAnimationSpeed);
            ContainerCategoriePanel.DOKill();
            ContainerCategoriePanel.DOScale(Vector3.zero, ClosePanelSpeed);                
        }
        // Ouvre le Panel
        if(StateChoice == 1)
        {
            // Referme le panel si ça taille est grande
            if(CategoriePanel.sizeDelta.y > 70f)
            {
                ChangeTitleCategories(0);   
                InterractPanelButton(0, CategorieGroup, CategoriePanel, FondContainerCategoriePanel, ContainerCategoriePanel, CategoriePanelHeight, CategoriePanelFondHeight, OpenCategorieGroupHeight, 0) ;
            } else {
                ChangeTitleCategories(ChangeTitleCategoriesInt);   
                CategoriePanel.DOSizeDelta(new Vector2(PanelWidth, CategoriePanelHeight), PanelAnimationSpeed);
                FondContainerCategoriePanel.DOSizeDelta(new Vector2(144f, CategoriePanelFondHeight), PanelAnimationSpeed);
                CategorieGroup.DOSizeDelta(new Vector2(CategorieGroup.sizeDelta.x, OpenCategorieGroupHeight), PanelAnimationSpeed)/*.OnComplete(() => {CategorieGroup.transform.SetSiblingIndex(5); })*/;
                CategorieGroup.transform.SetSiblingIndex(5);
                ContainerCategoriePanel.DOKill();
                ContainerCategoriePanel.DOScale(Vector3.one, OpenPanelSpeed);   
            }
        }
    }

    public void InterractSkinButton(int StateChoice)
    {    
       /* InterractPanelButton(StateChoice, ChoisingGroup, ChoisingPanel, FondChoiceContainerPanel, ContainerSkinPanel, SkinPanelHeight, SkinPanelFondHeight, OpenSkinGroupHeight, 1);
        InterractPanelButton(0, ChoisingGroup, ChoisingPanel, FondChoiceContainerPanel, ChoiceContainerPanel, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight, 0);
        CustomizerReference.CurrentCategorie = 0 ; */
        InterractChoiceButton(StateChoice, 1, 0, CustomizerReference.HairChoice, SkinPanelHeight, SkinPanelFondHeight, OpenSkinGroupHeight) ;

    }   

    public void InterractChoiceButton(int StateChoice, int TitleNumber, int CustomCategorieNumber, List<ItemCustomer> CategorieChoice, float PanelHeight, float PanelFondHeight, float OpenGroupHeight)
    {  


        if(/*ChoisingPanel.sizeDelta.y < 10f*/CustomizerReference.CurrentCategorie == CustomCategorieNumber)
        {
            InterractPanelButton(StateChoice, ChoisingGroup, ChoisingPanel, FondChoiceContainerPanel, ChoiceContainerPanel, PanelHeight, PanelFondHeight, OpenGroupHeight, TitleNumber);
            CustomizerReference.CurrentCategorie = CustomCategorieNumber ; 
            CustomizerReference.ChoiceDisplay(CustomizerReference.CurrentCategorie, CategorieChoice, 1);
        
        } else {
            StartCoroutine(WaitAndOpenPanel(TitleNumber)) ; 
        }

        // Si c'est la même catégorie : ferme juste
        // Si c'est la mauvaise, ferme et réouvre
    }

    IEnumerator WaitAndOpenPanel(int CategorieOpenning)
    {

        InterractPanelButton(0, ChoisingGroup, ChoisingPanel, FondChoiceContainerPanel, ChoiceContainerPanel, ChoicePanelFondHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight, 0);

        yield return new WaitForSeconds(0.75f);
            InterractPanelButton(1, ChoisingGroup, ChoisingPanel, FondChoiceContainerPanel, ChoiceContainerPanel, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight, CategorieOpenning);

    }

    public void InterractHairButton(int StateChoice)
    { 
        InterractChoiceButton(StateChoice, 2, 1, CustomizerReference.HairChoice, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight) ;
    }
    public void InterractTopButton(int StateChoice)
    { 
        InterractChoiceButton(StateChoice, 3, 2, CustomizerReference.TopChoice, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight) ;
    }
    public void InterractPantsButton(int StateChoice)
    {   
        InterractChoiceButton(StateChoice, 4, 3, CustomizerReference.PantsChoice, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight) ;
    }
    public void InterractShoeButton(int StateChoice)
    {   
        InterractChoiceButton(StateChoice, 5, 4, CustomizerReference.ShoeChoice, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight) ;
    }


  /*  public void CloseAllPanel()
    {       }*/


    public void ChangeTitleCategories(int NextCat)
    {
        TitleCategories.GetComponent<RectTransform>().DOScaleY(0, PanelAnimationSpeed/4)
            .OnComplete(() => {
                if(NextCat == 0)
                    {TitleCategories.text = "Choissisez une section" ;}
                if(NextCat == 1)
                    {TitleCategories.text = "Couleur de peau" ;}
                if(NextCat == 2)
                    {TitleCategories.text = "Cheveux" ;}
                if(NextCat == 3)
                    {TitleCategories.text = "Hauts" ;}
                if(NextCat == 4)
                    {TitleCategories.text = "Bas" ;}
                if(NextCat == 5)
                    {TitleCategories.text = "Chaussures" ;}
                if(NextCat == 6)
                    {TitleCategories.text = "Aurevoir" ;}

                TitleCategories.GetComponent<RectTransform>().DOScaleY(0.75f, PanelAnimationSpeed/4);
            });
    }
}
