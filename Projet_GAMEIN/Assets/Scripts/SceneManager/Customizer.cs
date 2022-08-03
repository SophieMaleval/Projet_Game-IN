using System.Collections;
using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Cinemachine;
using AllosiusDev.Audio;
using Core;
using Core.Session;

public class Customizer : MonoBehaviour
{
    #region Fields

    private AnimationCustomizer animationCustomizer;

    //private string[] RandomNames = new string[]  // Une ligne par genre : Homme, Femme, Non-Binaire
    //{
    ///*16*/"Olivier", "Sébastien", "Patrick", "Lucas", "Richard", "Frédéric", "Louis", "Mathieu", "Alexandre", "William", "Vincent", "Théo", "Simon", "Jules", "Romain", "Aubin",
    ///*17*/"Charlotte", "Elise", "Margot", "Justine", "Ines", "Laetitia", "Emilie", "Marine", "Marie", "Manon", "Lucie", "Lisa", "Cécile", "Julie", "Clara", "Kim", "Cassandre",
    ///*5*/ "Camille", "Dominique",
    //};

    private string WhoIsIt = "§ est ¤ !";

    private bool avatarIsInit;

    private PlayerMovement PlayerApparance;
    private PlayerScript PlayerPersonnality;

    #endregion

    #region UnityInspector

    [Header("Custom Avatar")]
    //[SerializeField] private GameObject PlayerPrefab;

    [SerializeField] private Vector3 PlayerPositionCustom = new Vector3(-0.5f, -0.335f, 0f);


    [SerializeField] private Button RandomButton;
    [SerializeField] private Button SubmitButton;
    [SerializeField] private AnimationTransitionScene FadeImage;


    [Header("CustomerPanel")]
    [SerializeField] private Slider SkinSlider;
    public int CurrentCategorie;
    //public List<int> ChoiceInCategorie;

    [SerializeField] private List<Image> ChoiceDisplayer;
    [SerializeField] private Button PreviousChoiceButton;
    [SerializeField] private Button NextChoiceButton;

    public List<Button> AllChoiceColorButton;

    [Header("Player Information")]
    public InputField NamingField;
    //[SerializeField] private PlayerNamesSelection playerNamesSelection;


    [Header("Gender")]
    public int Gender; // 0 -> Femme | 1 -> Homme | 2 -> Non-Binaire
    public List<Button> GenderButton;
    public Color GenderNotSelected;
    public Color GenderSelected;


    /*[Header("Item Disponible")]
    public List<ItemCustomer> HairChoice;
    public List<ItemCustomer> TopChoice;
    public List<ItemCustomer> PantsChoice;
    public List<ItemCustomer> ShoeChoice;*/
    [Space]
    [SerializeField] private Color ColorDoNotUse;
    [SerializeField] private Color ColorUse;


    [Header("Custom Color Possible")]
    public List<Color> SkinGradient;
    //public List<Color> ColorCustomList;

    /*[Header("Canvas")]
    [SerializeField] private GameObject CanvasPrefab;
    [SerializeField] private GameObject DialogueUIPrefab;
    [SerializeField] private GameObject InventoryUIPrefab;
    [SerializeField] private GameObject PannelENTUIPrefab;
    [SerializeField] private GameObject PannelAnnonceUIPrefab;
    [SerializeField] private GameObject QCMPanelPrefab;
    [SerializeField] private GameObject EventSystemPrefab;*/

    [Header("Sounds")]
    [SerializeField] private AudioData music;

    #endregion

    #region Behaviour
    private void Awake()
    {
        animationCustomizer = GetComponent<AnimationCustomizer>();

        if (GameManager.Instance.player == null)
        {
            GameManager.Instance.CreatePlayer(PlayerPositionCustom);

            PlayerApparance = GameManager.Instance.player.GetComponent<PlayerMovement>();
            PlayerPersonnality = GameManager.Instance.player;
        }
        else
        {
            PlayerApparance = GameManager.Instance.player.GetComponent<PlayerMovement>();
            PlayerApparance.enabled = false;
            PlayerPersonnality = GameManager.Instance.player;

            PlayerApparance.transform.position = PlayerPositionCustom;
            for (int A = 0; A < PlayerApparance.Animators.Count; A++)
            {
                // Set Up la direction du Joueur en Face
                PlayerApparance.Animators[A].SetFloat("AnimLastMoveX", 0);
                PlayerApparance.Animators[A].SetFloat("AnimLastMoveY", -1);
            }
            RecupInfoPlayer();
            SetAvatar();


            PlayerPersonnality.CanvasIndestrucitble.SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(WaitTransitionAnim());

        SetAvatar();
        avatarIsInit = true;

        PlaySound(music);
    }

    IEnumerator WaitTransitionAnim()
    {
        yield return new WaitForSeconds(0.25f);
        FadeImage.enabled = true;
        FadeImage.ShouldReveal = true;
        yield return new WaitForSeconds(2f);
        FadeImage.gameObject.SetActive(false);
    }


    void Update()
    {
        // Valider seulement si le nom et le genre son référencé
        if (PlayerPersonnality.PlayerName == "" || PlayerPersonnality.PlayerSexualGenre == -1)
        {
            SubmitButton.targetGraphic.color = new Color(0.3888f, 0.3921f, 0.3766f, 1);
            SubmitButton.interactable = false;
        }
        if (PlayerPersonnality.PlayerName != "" && PlayerPersonnality.PlayerSexualGenre != -1)
        {
            SubmitButton.targetGraphic.color = new Color(0.3888f, 0.8773f, 0.3766f, 1);
            SubmitButton.interactable = true;
        }
    }

    public void SetPlayerName(string NameEnter)
    { PlayerApparance.GetComponent<PlayerScript>().PlayerName = NameEnter; }
    public void SetGender(int GenderValue)
    {
        GameManager.Instance.SetPlayerGender(GenderValue);
        for (int G = 0; G < GenderButton.Count; G++)
        {
            if (G == PlayerApparance.GetComponent<PlayerScript>().PlayerSexualGenre)
            {
                GenderButton[G].interactable = false;
                GenderButton[G].GetComponent<CustomerButton>().Icon.color = GenderSelected;
            }
            else
            {
                GenderButton[G].interactable = true;
                GenderButton[G].GetComponent<CustomerButton>().Icon.color = GenderNotSelected;
            }
        }
    }

    // Categorie Num
    // 0 - Skin
    // 1 - Hair
    // 2 - Top
    // 3 - Pant
    // 4 - Shoe
    public void ChangeCategorie()
    {
        ResetBtnSprite(PreviousChoiceButton);
        ResetBtnSprite(NextChoiceButton);

        if (CurrentCategorie == 1)
            ChoiceDisplay(GameManager.Instance.HairChoice);
        if (CurrentCategorie == 2)
            ChoiceDisplay(GameManager.Instance.TopChoice);
        if (CurrentCategorie == 3)
            ChoiceDisplay(GameManager.Instance.PantsChoice);
        if (CurrentCategorie == 4)
            ChoiceDisplay(GameManager.Instance.ShoeChoice);
    }

    public void ChoiceDisplay(List<ItemCustomer> CategorieChoice)
    {
        // Affiche dans la liste le : dernier PREMIER second
        if (GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] == 0)
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[CategorieChoice.Count - 1].DisplayCustomisation;
            ChoiceDisplayer[2].sprite = CategorieChoice[GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] + 1].DisplayCustomisation;
        }

        // Set sur le Player la sélection
        ChoiceDisplayer[1].sprite = CategorieChoice[GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1]].DisplayCustomisation;

        // Affiche dans la liste le : avant-dernier DERNIER premier
        if (GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] == CategorieChoice.Count - 1)
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] - 1].DisplayCustomisation;
            ChoiceDisplayer[2].sprite = CategorieChoice[0].DisplayCustomisation;
        }

        // Affiche dans la liste le : n-1 N n+1
        if ((GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] != 0) && (GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] != CategorieChoice.Count - 1))
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] - 1].DisplayCustomisation;
            ChoiceDisplayer[2].sprite = CategorieChoice[GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] + 1].DisplayCustomisation;
        }

        //if(CurrentCategorie == 1)PlayerApparance.Animators[CurrentCategorie - 1].SetFloat("HairChoice", 1);
        PlayerApparance.SpriteDisplay[CurrentCategorie - 1] = CategorieChoice[GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1]].Animator;
        SetAvatar();
    }

    // Fonction pour l'interaction -/+ sur les changement de custom
    public void ChangeDisplay(int ValueAdd)
    {
        ResetBtnSprite(PreviousChoiceButton);
        ResetBtnSprite(NextChoiceButton);

        if (CurrentCategorie == 1)
            ChangeChoice(ValueAdd, GameManager.Instance.HairChoice, 1);
        if (CurrentCategorie == 2)
            ChangeChoice(ValueAdd, GameManager.Instance.TopChoice, 2);
        if (CurrentCategorie == 3)
            ChangeChoice(ValueAdd, GameManager.Instance.PantsChoice, 3);
        if (CurrentCategorie == 4)
            ChangeChoice(ValueAdd, GameManager.Instance.ShoeChoice, 4);
    }

    void ChangeChoice(int ChoiceValueAdd, List<ItemCustomer> CategorieChoice, int CategorieNum)
    {
        // Choix Précédent --
        if (ChoiceValueAdd < 0)
        {
            if (GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] == 0)
                GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] = CategorieChoice.Count - 1;
            else
                GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1]--;
        }

        // Choix Suivant ++
        if (ChoiceValueAdd > 0)
        {
            if (GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] == CategorieChoice.Count - 1)
                GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1] = 0;
            else
                GameManager.Instance.ChoiceInCategorie[CurrentCategorie - 1]++;
        }

        ChoiceDisplay(CategorieChoice);
    }



    void RecupInfoPlayer()
    {
        // Récupère le nom 
        NamingField.text = PlayerPersonnality.PlayerName;

        // Récupère le genre 
        SetGender(PlayerPersonnality.PlayerSexualGenre);

        // Recupère la couleur de peau
        SkinSlider.value = PlayerApparance.ValueColorDisplay[0];

        // Récupère les choix de Hair
        for (int ChoiceInCategorieHair = 0; ChoiceInCategorieHair < GameManager.Instance.HairChoice.Count; ChoiceInCategorieHair++)
        {
            if (GameManager.Instance.HairChoice[ChoiceInCategorieHair].Animator == PlayerApparance.SpriteDisplay[0])
                GameManager.Instance.ChoiceInCategorie[0] = ChoiceInCategorieHair;
        }

        // Récupère les choix de Top
        for (int ChoiceInCategorieTop = 0; ChoiceInCategorieTop < GameManager.Instance.TopChoice.Count; ChoiceInCategorieTop++)
        {
            if (GameManager.Instance.TopChoice[ChoiceInCategorieTop].Animator == PlayerApparance.SpriteDisplay[1])
                GameManager.Instance.ChoiceInCategorie[1] = ChoiceInCategorieTop;
        }

        // Récupère les choix de Pants
        for (int ChoiceInCategoriePants = 0; ChoiceInCategoriePants < GameManager.Instance.PantsChoice.Count; ChoiceInCategoriePants++)
        {
            if (GameManager.Instance.PantsChoice[ChoiceInCategoriePants].Animator == PlayerApparance.SpriteDisplay[2])
                GameManager.Instance.ChoiceInCategorie[2] = ChoiceInCategoriePants;
        }

        // Récupère les choix de Shoe
        for (int ChoiceInCategorieShoe = 0; ChoiceInCategorieShoe < GameManager.Instance.ShoeChoice.Count; ChoiceInCategorieShoe++)
        {
            if (GameManager.Instance.ShoeChoice[ChoiceInCategorieShoe].Animator == PlayerApparance.SpriteDisplay[3])
                GameManager.Instance.ChoiceInCategorie[3] = ChoiceInCategorieShoe;
        }
    }

    public void SkinColor()
    {
        PlayerApparance.ValueColorDisplay[0] = SkinSlider.value;
        PlayerApparance.ColorsDisplay[0] = Color.Lerp(SkinGradient[0], SkinGradient[1], PlayerApparance.ValueColorDisplay[0]);
        SetAvatar();
    }

    public void SetChoiceColor(int ColorNumList)
    {
        PlayerApparance.ValueColorDisplay[CurrentCategorie] = ColorNumList;
        PlayerApparance.ColorsDisplay[CurrentCategorie] = GameManager.Instance.ColorCustomList[ColorNumList];
        SetAvatar();
    }

    void DisableButtonColorChoice()
    {
        if (CurrentCategorie > 0 && CurrentCategorie <= 4)
        {
            for (int c = 0; c < AllChoiceColorButton.Count; c++)
            {
                if (c == PlayerApparance.ValueColorDisplay[CurrentCategorie])
                {
                    AllChoiceColorButton[c].interactable = false;
                    AllChoiceColorButton[c].GetComponent<ColorButton>().ContourCouleur.color = ColorUse;
                }
                else
                {
                    AllChoiceColorButton[c].interactable = true;
                    AllChoiceColorButton[c].GetComponent<ColorButton>().ContourCouleur.color = ColorDoNotUse;
                }
            }
        }
    }

    public void SetAvatar()
    {
        GameManager.Instance.SetAvatarColor();
        DisableButtonColorChoice();


        GameManager.Instance.UpdateAvatarAnimators();

        if(avatarIsInit)
            animationCustomizer.UpdateRotationView();

    }

    public void ResetBtnSprite(Button TargetBtn)
    {
        TargetBtn.interactable = false;
        TargetBtn.interactable = true;
    }
    // Final Button
    public void RandomCustom()
    {
        ResetBtnSprite(RandomButton);

        // Random Gender
        SetGender(Random.Range(0, GenderButton.Count));

        // Random Name
        NamingField.text = GameManager.Instance.RandomPlayerName();


        // Random Skin
        GameManager.Instance.RandomSkinPlayer();
        SkinSlider.value = PlayerApparance.ValueColorDisplay[0];

        GameManager.Instance.RandomAppearancePlayer();

        ChangeDisplay(0);
        SetAvatar();
    }




    public void SubmitCustom()
    {
        GetComponent<AnimationCustomizer>().CustomizationFinish();
        FadeImage.gameObject.SetActive(true);
        GetComponent<AnimationCustomizer>().ChangeTitleCategories(6);

        GameManager.Instance.SetupDialogues();


        StartCoroutine(WaitBeforeChangeScene());
    }

    IEnumerator WaitBeforeChangeScene()
    {
        yield return new WaitForSeconds(1.75f);
        FadeImage.GetComponent<AnimationTransitionScene>().ShouldReveal = false;
        yield return new WaitForSeconds(1.75f);
        PlayerApparance.enabled = true;
        yield return new WaitForSeconds(1.75f);


        PlayerPersonnality.CanvasIndestrucitble.SetActive(true);
        PlayerPersonnality.PannelENTUIIndestructible.SetActive(false);
        PlayerPersonnality.PannelAnnonceUIIndestructible.SetActive(false);

        //PlayerPersonnality.PreviousSceneName = SceneManager.GetActiveScene().name;
        PlayerPersonnality.PreviousSceneName = SessionController.Instance.Game.CharacterCustomerScene;
        yield return new WaitForSeconds(0.5f);
        SceneLoader.Instance.ChangeScene(SessionController.Instance.Game.StartLevelScene);
    }


    public void PlaySound(AudioData audioData)
    {
        AllosiusDev.Audio.AudioController.Instance.PlayAudio(audioData);
    }

    #endregion




}
