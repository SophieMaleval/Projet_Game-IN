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
    [SerializeField] private GameObject PlayerPrefab;

    [SerializeField] private Vector3 PlayerPositionCustom = new Vector3(-0.5f, -0.335f, 0f);


    [SerializeField] private Button RandomButton;
    [SerializeField] private Button SubmitButton;
    [SerializeField] private AnimationTransitionScene FadeImage;


    [Header("CustomerPanel")]
    [SerializeField] private Slider SkinSlider;
    public int CurrentCategorie;
    public List<int> ChoiceInCategorie;

    [SerializeField] private List<Image> ChoiceDisplayer;
    [SerializeField] private Button PreviousChoiceButton;
    [SerializeField] private Button NextChoiceButton;

    [SerializeField] private List<Button> AllChoiceColorButton;

    [Header("Player Information")]
    public InputField NamingField;
    [SerializeField] private PlayerNamesSelection playerNamesSelection;


    [Header("Gender")]
    public int Gender; // 0 -> Femme | 1 -> Homme | 2 -> Non-Binaire
    public List<Button> GenderButton;
    public Color GenderNotSelected;
    public Color GenderSelected;


    [Header("Item Disponible")]
    public List<ItemCustomer> HairChoice;
    public List<ItemCustomer> TopChoice;
    public List<ItemCustomer> PantsChoice;
    public List<ItemCustomer> ShoeChoice;
    [Space]
    [SerializeField] private Color ColorDoNotUse;
    [SerializeField] private Color ColorUse;


    [Header("Custom Color Possible")]
    public List<Color> SkinGradient;
    public List<Color> ColorCustomList;

    [Header("Canvas")]
    [SerializeField] private GameObject CanvasPrefab;
    [SerializeField] private GameObject DialogueUIPrefab;
    [SerializeField] private GameObject InventoryUIPrefab;
    [SerializeField] private GameObject PannelENTUIPrefab;
    [SerializeField] private GameObject PannelAnnonceUIPrefab;
    [SerializeField] private GameObject QCMPanelPrefab;
    [SerializeField] private GameObject EventSystemPrefab;

    [Header("Sounds")]
    [SerializeField] private AudioData music;

    #endregion

    #region Behaviour
    private void Awake()
    {
        animationCustomizer = GetComponent<AnimationCustomizer>();

        if (GameManager.Instance.player == null)
        {
            GameObject PlayerInstantiate = Instantiate(PlayerPrefab, PlayerPositionCustom, Quaternion.identity);
            PlayerApparance = PlayerInstantiate.GetComponent<PlayerMovement>();
            PlayerPersonnality = PlayerInstantiate.GetComponent<PlayerScript>();
            PlayerApparance.enabled = false;
            PlayerApparance.gameObject.name = "Player";

            GameManager.Instance.player = PlayerPersonnality;


            GameObject CanvasInstatiate = Instantiate(CanvasPrefab);
            GameObject DialogueUIInstatiate = Instantiate(DialogueUIPrefab);
            GameObject InventoryUIInstatiate = Instantiate(InventoryUIPrefab);
            GameObject PannelENTUIInstatiate = Instantiate(PannelENTUIPrefab);
            GameObject PannelAnnonceUIInstatiate = Instantiate(PannelAnnonceUIPrefab);
            GameObject QCMPanelInstantiate = Instantiate(QCMPanelPrefab);
            GameObject EventSystemInstantiate = Instantiate(EventSystemPrefab);

            DialogueUIInstatiate.transform.SetParent(CanvasInstatiate.transform);
            DialogueUIInstatiate.transform.SetSiblingIndex(0);
            DialogueUIInstatiate.name = "Dialogue Canvas";

            PannelENTUIInstatiate.transform.SetParent(CanvasInstatiate.transform);
            PannelENTUIInstatiate.transform.SetSiblingIndex(1);
            PannelENTUIInstatiate.name = "Pannel ENT";

            PannelAnnonceUIInstatiate.transform.SetParent(CanvasInstatiate.transform);
            PannelAnnonceUIInstatiate.transform.SetSiblingIndex(1);
            PannelAnnonceUIInstatiate.name = "Pannel Annonce";


            QCMPanelInstantiate.transform.SetParent(CanvasInstatiate.transform);
            QCMPanelInstantiate.transform.SetSiblingIndex(3);
            QCMPanelInstantiate.name = "QCM Panel";

            InventoryUIInstatiate.transform.SetParent(CanvasInstatiate.transform);
            InventoryUIInstatiate.transform.SetSiblingIndex(4);
            InventoryUIInstatiate.name = "Inventory";

            PlayerPersonnality.playerBackpack.GetComponent<CSVReader>().QuestManager = InventoryUIInstatiate.GetComponentInChildren<QuestSys>();


            GameCanvasManager gameCanvasManager = CanvasInstatiate.GetComponent<GameCanvasManager>();

            GameManager.Instance.gameCanvasManager = gameCanvasManager;

            gameCanvasManager.inventory = InventoryUIInstatiate.GetComponent<InventoryScript>();
            gameCanvasManager.questManager = InventoryUIInstatiate.GetComponentInChildren<QuestSys>();

            gameCanvasManager.dialogCanvas = DialogueUIInstatiate.GetComponent<DialogueDisplayerController>();

            gameCanvasManager.qcmPanel = QCMPanelInstantiate.GetComponent<QCMManager>();

            DontDestroyOnLoad(PlayerApparance.gameObject);
            DontDestroyOnLoad(CanvasInstatiate.gameObject);
            DontDestroyOnLoad(DialogueUIInstatiate.gameObject);
            DontDestroyOnLoad(InventoryUIInstatiate.gameObject);
            DontDestroyOnLoad(PannelENTUIInstatiate.gameObject);
            DontDestroyOnLoad(PannelAnnonceUIInstatiate.gameObject);
            DontDestroyOnLoad(QCMPanelInstantiate.gameObject);
            DontDestroyOnLoad(EventSystemInstantiate);

            PlayerPersonnality.CanvasIndestrucitble = CanvasInstatiate;
            PlayerPersonnality.DialogueUIIndestructible = DialogueUIInstatiate;
            PlayerPersonnality.InventoryUIIndestructible = InventoryUIInstatiate;
            PlayerPersonnality.PannelENTUIIndestructible = PannelENTUIInstatiate;
            PlayerPersonnality.PannelAnnonceUIIndestructible = PannelAnnonceUIInstatiate;
            PlayerPersonnality.QCMPanelUIIndestructible = QCMPanelInstantiate;

            CanvasInstatiate.SetActive(false);
            DialogueUIInstatiate.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 15f, 0);
            DialogueUIInstatiate.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 2f);

            InventoryUIInstatiate.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // Left & Bottom
            InventoryUIInstatiate.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // Right & Top
            InventoryUIInstatiate.GetComponent<RectTransform>().localScale = Vector3.one;

            PannelENTUIInstatiate.GetComponent<RectTransform>().offsetMin = new Vector2(250f, 25f);
            PannelENTUIInstatiate.GetComponent<RectTransform>().offsetMax = new Vector2(-250f, -25f);
            PannelENTUIInstatiate.GetComponent<RectTransform>().localScale = Vector3.one;

            PannelAnnonceUIInstatiate.GetComponent<RectTransform>().offsetMin = new Vector2(350f, 200f);
            PannelAnnonceUIInstatiate.GetComponent<RectTransform>().offsetMax = new Vector2(-350f, -200f);
            PannelAnnonceUIInstatiate.GetComponent<RectTransform>().localScale = Vector3.one;

            QCMPanelInstantiate.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -600f);
            QCMPanelInstantiate.GetComponent<RectTransform>().sizeDelta = new Vector2(992f, 415f);
            QCMPanelInstantiate.GetComponent<RectTransform>().localScale = Vector3.one;


            InventoryUIInstatiate.GetComponent<InventoryScript>().PlayerScript = PlayerPersonnality;
            InventoryUIInstatiate.GetComponent<InventoryScript>().DialogueCanvas = DialogueUIInstatiate;
            InventoryUIInstatiate.GetComponent<InventoryScript>().PannelENTCanvas = PannelENTUIInstatiate;

            PlayerPersonnality.FadeAnimation = CanvasInstatiate.transform.GetChild(CanvasInstatiate.transform.childCount - 1).GetComponent<Image>();
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
        PlayerApparance.GetComponent<PlayerScript>().PlayerSexualGenre = GenderValue;
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
            ChoiceDisplay(HairChoice);
        if (CurrentCategorie == 2)
            ChoiceDisplay(TopChoice);
        if (CurrentCategorie == 3)
            ChoiceDisplay(PantsChoice);
        if (CurrentCategorie == 4)
            ChoiceDisplay(ShoeChoice);
    }

    public void ChoiceDisplay(List<ItemCustomer> CategorieChoice)
    {
        // Affiche dans la liste le : dernier PREMIER second
        if (ChoiceInCategorie[CurrentCategorie - 1] == 0)
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[CategorieChoice.Count - 1].DisplayCustomisation;
            ChoiceDisplayer[2].sprite = CategorieChoice[ChoiceInCategorie[CurrentCategorie - 1] + 1].DisplayCustomisation;
        }

        // Set sur le Player la sélection
        ChoiceDisplayer[1].sprite = CategorieChoice[ChoiceInCategorie[CurrentCategorie - 1]].DisplayCustomisation;

        // Affiche dans la liste le : avant-dernier DERNIER premier
        if (ChoiceInCategorie[CurrentCategorie - 1] == CategorieChoice.Count - 1)
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[ChoiceInCategorie[CurrentCategorie - 1] - 1].DisplayCustomisation;
            ChoiceDisplayer[2].sprite = CategorieChoice[0].DisplayCustomisation;
        }

        // Affiche dans la liste le : n-1 N n+1
        if ((ChoiceInCategorie[CurrentCategorie - 1] != 0) && (ChoiceInCategorie[CurrentCategorie - 1] != CategorieChoice.Count - 1))
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[ChoiceInCategorie[CurrentCategorie - 1] - 1].DisplayCustomisation;
            ChoiceDisplayer[2].sprite = CategorieChoice[ChoiceInCategorie[CurrentCategorie - 1] + 1].DisplayCustomisation;
        }

        //if(CurrentCategorie == 1)PlayerApparance.Animators[CurrentCategorie - 1].SetFloat("HairChoice", 1);
        PlayerApparance.SpriteDisplay[CurrentCategorie - 1] = CategorieChoice[ChoiceInCategorie[CurrentCategorie - 1]].Animator;
        SetAvatar();
    }

    // Fonction pour l'interaction -/+ sur les changement de custom
    public void ChangeDisplay(int ValueAdd)
    {
        ResetBtnSprite(PreviousChoiceButton);
        ResetBtnSprite(NextChoiceButton);

        if (CurrentCategorie == 1)
            ChangeChoice(ValueAdd, HairChoice, 1);
        if (CurrentCategorie == 2)
            ChangeChoice(ValueAdd, TopChoice, 2);
        if (CurrentCategorie == 3)
            ChangeChoice(ValueAdd, PantsChoice, 3);
        if (CurrentCategorie == 4)
            ChangeChoice(ValueAdd, ShoeChoice, 4);
    }

    void ChangeChoice(int ChoiceValueAdd, List<ItemCustomer> CategorieChoice, int CategorieNum)
    {
        // Choix Précédent --
        if (ChoiceValueAdd < 0)
        {
            if (ChoiceInCategorie[CurrentCategorie - 1] == 0)
                ChoiceInCategorie[CurrentCategorie - 1] = CategorieChoice.Count - 1;
            else
                ChoiceInCategorie[CurrentCategorie - 1]--;
        }

        // Choix Suivant ++
        if (ChoiceValueAdd > 0)
        {
            if (ChoiceInCategorie[CurrentCategorie - 1] == CategorieChoice.Count - 1)
                ChoiceInCategorie[CurrentCategorie - 1] = 0;
            else
                ChoiceInCategorie[CurrentCategorie - 1]++;
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
        for (int ChoiceInCategorieHair = 0; ChoiceInCategorieHair < HairChoice.Count; ChoiceInCategorieHair++)
        {
            if (HairChoice[ChoiceInCategorieHair].Animator == PlayerApparance.SpriteDisplay[0])
                ChoiceInCategorie[0] = ChoiceInCategorieHair;
        }

        // Récupère les choix de Top
        for (int ChoiceInCategorieTop = 0; ChoiceInCategorieTop < TopChoice.Count; ChoiceInCategorieTop++)
        {
            if (TopChoice[ChoiceInCategorieTop].Animator == PlayerApparance.SpriteDisplay[1])
                ChoiceInCategorie[1] = ChoiceInCategorieTop;
        }

        // Récupère les choix de Pants
        for (int ChoiceInCategoriePants = 0; ChoiceInCategoriePants < PantsChoice.Count; ChoiceInCategoriePants++)
        {
            if (PantsChoice[ChoiceInCategoriePants].Animator == PlayerApparance.SpriteDisplay[2])
                ChoiceInCategorie[2] = ChoiceInCategoriePants;
        }

        // Récupère les choix de Shoe
        for (int ChoiceInCategorieShoe = 0; ChoiceInCategorieShoe < ShoeChoice.Count; ChoiceInCategorieShoe++)
        {
            if (ShoeChoice[ChoiceInCategorieShoe].Animator == PlayerApparance.SpriteDisplay[3])
                ChoiceInCategorie[3] = ChoiceInCategorieShoe;
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
        PlayerApparance.ColorsDisplay[CurrentCategorie] = ColorCustomList[ColorNumList];
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
        // Set Color
        for (int i = 0; i < PlayerApparance.PlayerRenderers.Count; i++)
        {
            PlayerApparance.PlayerRenderers[i].color = PlayerApparance.ColorsDisplay[i];
        }
        DisableButtonColorChoice();


        // Afficher le choix
        for (int i = 0; i < PlayerApparance.SpriteDisplay.Count; i++)
        {
            PlayerApparance.Animators[i + 1].runtimeAnimatorController = PlayerApparance.SpriteDisplay[i];
        }


        // Reset Animator pour les coordonnées
        for (int a = 0; a < PlayerApparance.Animators.Count; a++)
        { PlayerApparance.Animators[a].Rebind(); }

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
        if(PlayerPersonnality.PlayerSexualGenre == 0)
        {
            List<string> maleNames = new List<string>();
            maleNames = playerNamesSelection.GetSumLists(maleNames, playerNamesSelection.maleRandomNames);
            maleNames = playerNamesSelection.GetSumLists(maleNames, playerNamesSelection.mixedRandomNames);

            /*for (int i = 0; i < maleNames.Count; i++)
            {
                Debug.Log(maleNames[i]);
            }*/

            NamingField.text = maleNames[Random.Range(0, maleNames.Count)];
        }
        else if (PlayerPersonnality.PlayerSexualGenre == 1)
        {
            List<string> femaleNames = new List<string>();
            femaleNames = playerNamesSelection.GetSumLists(femaleNames, playerNamesSelection.femaleRandomNames);
            femaleNames = playerNamesSelection.GetSumLists(femaleNames, playerNamesSelection.mixedRandomNames);

            /*for (int i = 0; i < femaleNames.Count; i++)
            {
                Debug.Log(femaleNames[i]);
            }*/

            NamingField.text = femaleNames[Random.Range(0, femaleNames.Count)];
        }
        else if (PlayerPersonnality.PlayerSexualGenre == 2)
        {
            List<string> nonBinaryNames = new List<string>();
            nonBinaryNames = playerNamesSelection.GetSumLists(nonBinaryNames, playerNamesSelection.maleRandomNames);
            nonBinaryNames = playerNamesSelection.GetSumLists(nonBinaryNames, playerNamesSelection.femaleRandomNames);
            nonBinaryNames = playerNamesSelection.GetSumLists(nonBinaryNames, playerNamesSelection.mixedRandomNames);

            /*for (int i = 0; i < nonBinaryNames.Count; i++)
            {
                Debug.Log(nonBinaryNames[i]);
            }*/

            NamingField.text = nonBinaryNames[Random.Range(0, nonBinaryNames.Count)];
        }


        // Random Skin
        PlayerApparance.ValueColorDisplay[0] = Random.Range(0f, 1f);
        SkinSlider.value = PlayerApparance.ValueColorDisplay[0];

        // Random Hair
        ChoiceInCategorie[0] = Random.Range(0, HairChoice.Count);
        PlayerApparance.SpriteDisplay[0] = HairChoice[ChoiceInCategorie[0]].Animator;
        PlayerApparance.ValueColorDisplay[1] = Random.Range(0, AllChoiceColorButton.Count);
        PlayerApparance.ColorsDisplay[1] = ColorCustomList[(int)PlayerApparance.ValueColorDisplay[1]];

        // Random Top
        ChoiceInCategorie[1] = Random.Range(0, TopChoice.Count);
        PlayerApparance.SpriteDisplay[1] = TopChoice[ChoiceInCategorie[1]].Animator;
        PlayerApparance.ValueColorDisplay[2] = Random.Range(0, AllChoiceColorButton.Count);
        PlayerApparance.ColorsDisplay[2] = ColorCustomList[(int)PlayerApparance.ValueColorDisplay[2]];

        // Random Bottom
        ChoiceInCategorie[2] = Random.Range(0, PantsChoice.Count /*- 1*/);
        PlayerApparance.SpriteDisplay[2] = PantsChoice[ChoiceInCategorie[2]].Animator;
        PlayerApparance.ValueColorDisplay[3] = Random.Range(0, AllChoiceColorButton.Count);
        PlayerApparance.ColorsDisplay[3] = ColorCustomList[(int)PlayerApparance.ValueColorDisplay[3]];

        // Random Shoe
        ChoiceInCategorie[3] = Random.Range(0, ShoeChoice.Count);
        PlayerApparance.SpriteDisplay[3] = ShoeChoice[ChoiceInCategorie[3]].Animator;
        PlayerApparance.ValueColorDisplay[4] = Random.Range(0, AllChoiceColorButton.Count);
        PlayerApparance.ColorsDisplay[4] = ColorCustomList[(int)PlayerApparance.ValueColorDisplay[4]];

        ChangeDisplay(0);
        SetAvatar();
    }




    public void SubmitCustom()
    {
        GetComponent<AnimationCustomizer>().CustomizationFinish();
        FadeImage.gameObject.SetActive(true);
        GetComponent<AnimationCustomizer>().ChangeTitleCategories(6);

        PlayerPersonnality.playerBackpack.GetComponent<CSVReader>().SetUpDialogueAdhérent();


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

        PlayerPersonnality.PreviousSceneName = SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(0.5f);
        AudioController.Instance.StopAllAmbients();
        AudioController.Instance.StopAllMusics();
        SceneManager.LoadScene("Game In");
    }


    public void PlaySound(AudioData audioData)
    {
        AllosiusDev.Audio.AudioController.Instance.PlayAudio(audioData);
    }

    #endregion




}
