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

    [SerializeField] private RectTransform Group ;
    [SerializeField] private RectTransform Panel ;
    [SerializeField] private RectTransform ContainerChoicePanel ;
    [SerializeField] private RectTransform FondContainerPanel ;

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
    {   
        StartCoroutine(WaitBeforeOpenningRideaux());

        if(GameObject.Find("Player") != null)
        {
            AnimatorsCustom = GameObject.Find("Player").GetComponent<PlayerMovement>().Animators ;
        }
    }

    IEnumerator WaitBeforeOpenningRideaux()
    {      
        yield return new WaitForSeconds(1f);    
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
    void InterractPanelButton(int StateChoice, RectTransform ContainerCategoriePanel, float CategoriePanelHeight, float CategoriePanelFondHeight, float OpenCategorieGroupHeight, int ChangeTitleCategoriesInt)
    {
        // Ferme le Panel
        if(StateChoice == 0)
        {
            Panel.DOSizeDelta(new Vector2(PanelWidth,0f), PanelAnimationSpeed);  
            FondContainerPanel.DOSizeDelta(new Vector2(144f, 0f), PanelAnimationSpeed).OnComplete(() => {FondContainerPanel.sizeDelta = new Vector2(FondContainerPanel.sizeDelta.x, 20f);});
            Group.DOSizeDelta(new Vector2(Group.sizeDelta.x, CloseGroupHeight), PanelAnimationSpeed);
                ContainerSkinPanel.DOKill();
                ContainerChoicePanel.DOKill();   
            ContainerSkinPanel.DOScale(Vector3.zero, ClosePanelSpeed);
            ContainerChoicePanel.DOScale(Vector3.zero, ClosePanelSpeed);
        }

        // Ouvre le Panel
        if(StateChoice == 1)
        {
            // Referme le panel si ça taille est grande
            if(Panel.sizeDelta.y > 70f)
            {
                ChangeTitleCategories(0);   
                InterractPanelButton(0, ContainerCategoriePanel, CategoriePanelHeight, CategoriePanelFondHeight, OpenCategorieGroupHeight, 0) ;
            } else {
                // Active ou Désactive SkinContainer ou Container en fonction du Titre
                if(ChangeTitleCategoriesInt <= 1)
                {
                    ContainerSkinPanel.gameObject.SetActive(true);
                    ContainerChoicePanel.gameObject.SetActive(false);
                } else {
                    ContainerSkinPanel.gameObject.SetActive(false);
                    ContainerChoicePanel.gameObject.SetActive(true); 
                }
                ChangeTitleCategories(ChangeTitleCategoriesInt);   

                Panel.DOSizeDelta(new Vector2(PanelWidth, CategoriePanelHeight), PanelAnimationSpeed);
                FondContainerPanel.DOSizeDelta(new Vector2(144f, CategoriePanelFondHeight), PanelAnimationSpeed);
                Group.DOSizeDelta(new Vector2(Group.sizeDelta.x, OpenCategorieGroupHeight), PanelAnimationSpeed) ;
                ContainerSkinPanel.DOKill();
                ContainerChoicePanel.DOKill();

                ContainerCategoriePanel.DOScale(Vector3.one, OpenPanelSpeed);   
            }
        }
    }

    public void InterractChoiceButton(int StateChoice, int TitleNumber, int CustomCategorieNumber, RectTransform ContainerPanel, float PanelHeight, float PanelFondHeight, float OpenGroupHeight)
    {
        if(CustomizerReference.CurrentCategorie == CustomCategorieNumber)
        {
            InterractPanelButton(0, ContainerPanel, PanelHeight, PanelFondHeight, OpenGroupHeight, TitleNumber);
            CustomizerReference.CurrentCategorie = 10;
            ChangeTitleCategories(0);   
        } else {
            if(Panel.sizeDelta.y > 70)
                StartCoroutine(WaitAndOpenPanel(ContainerPanel, PanelHeight, PanelFondHeight, OpenGroupHeight, TitleNumber)) ;  
            else 
                InterractPanelButton(StateChoice, ContainerPanel, PanelHeight, PanelFondHeight, OpenGroupHeight, TitleNumber); 

            CustomizerReference.CurrentCategorie = CustomCategorieNumber ; 
            CustomizerReference.ChangeCategorie();
        }
    }

    IEnumerator WaitAndOpenPanel(RectTransform ContainerCategoriePanel, float CategoriePanelHeight, float CategoriePanelFondHeight, float OpenCategorieGroupHeight , int CategorieOpenning)
    {
        InterractPanelButton(0, ContainerCategoriePanel, CategoriePanelHeight, CategoriePanelFondHeight, OpenCategorieGroupHeight, 0);

        yield return new WaitForSeconds(0.75f);
            InterractPanelButton(1, ContainerCategoriePanel, CategoriePanelHeight, CategoriePanelFondHeight, OpenCategorieGroupHeight, CategorieOpenning);
    }




    public void InterractSkinButton(int StateChoice)
    {    
        InterractChoiceButton(StateChoice, 1, 0, ContainerSkinPanel, SkinPanelHeight, SkinPanelFondHeight, OpenSkinGroupHeight) ;
    }   

    public void InterractHairButton(int StateChoice)
    { 
        InterractChoiceButton(StateChoice, 2, 1, ContainerChoicePanel, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight) ;
    }
    public void InterractTopButton(int StateChoice)
    { 
        InterractChoiceButton(StateChoice, 3, 2, ContainerChoicePanel, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight) ;
    }
    public void InterractPantsButton(int StateChoice)
    {   
        InterractChoiceButton(StateChoice, 4, 3, ContainerChoicePanel, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight) ;
    }
    public void InterractShoeButton(int StateChoice)
    {   
        InterractChoiceButton(StateChoice, 5, 4, ContainerChoicePanel, ChoicePanelHeight, ChoicePanelFondHeight, OpenChoiceGroupHeight) ;
    }


    public void CustomizationFinish()
    {    
        InterractPanelButton(0, ContainerSkinPanel, SkinPanelHeight, SkinPanelFondHeight, OpenSkinGroupHeight, 0);
        RideauxAnimator.SetBool("Quit Custom ?", true) ;
    }


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
