﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllosiusDev;
using UnityEngine.UI;
using Core.Session;
using UnityEngine.EventSystems;
using AllosiusDev.QuestSystem;
using AllosiusDev.DialogSystem;

public class GameManager : Singleton<GameManager>
{
    #region Fields

    private PlayerMovement PlayerApparance;

    private Customizer customizer;

    #endregion

    #region Properties

    public PlayerScript player { get; set; }

    public GameCanvasManager gameCanvasManager { get; set; }

    public QuestList questManager { get; protected set; }

    public Vector2 InitMenuExitPlayerSpawnPos => initMenuExitPlayerSpawnPos;
    public Vector2 InitExteriorMapPlayerSpawnPos => initExteriorMapPlayerSpawnPos;

    #endregion

    #region UnityInspector

    [SerializeField] private SamplableLibrary dialoguesLibrary;

    [SerializeField] private QuestList questManagerPrefab;

    [Space]

    [Header("Player Positions Init Offsets")]

    [Tooltip("Position where the player will appear when leaving the Customer Menu or at the initialization directly on Game In for the first time")]
    [SerializeField] private Vector2 initMenuExitPlayerSpawnPos;

    [Tooltip("Position where the player will appear when leaving the Game In building for the first time to arrive on the outdoor map")]
    [SerializeField] private Vector2 initExteriorMapPlayerSpawnPos;

    [Space]

    public List<int> ChoiceInCategorie;

    [SerializeField] private int AllChoiceColors;
    [SerializeField] private int AllGendersPossibles;


    [Header("Item Disponible")]
    public List<ItemCustomer> HairChoice;
    public List<ItemCustomer> TopChoice;
    public List<ItemCustomer> PantsChoice;
    public List<ItemCustomer> ShoeChoice;

    [Header("Custom Color Possible")]
    public List<Color> ColorCustomList;

    [Header("Custom Avatar")]
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private PlayerNamesSelection playerNamesSelection;

    [Header("Canvas")]
    [SerializeField] private GameObject CanvasPrefab;
    [SerializeField] private GameObject DialogueUIPrefab;
    [SerializeField] private GameObject InventoryUIPrefab;
    [SerializeField] private GameObject PannelENTUIPrefab;
    [SerializeField] private GameObject PannelAnnonceUIPrefab;
    [SerializeField] private GameObject EventSystemPrefab;

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        player = FindObjectOfType<PlayerScript>();

        InitGame();
    }

    private void Start()
    {
        ResetDialogues();
    }

    public void ResetDialogues()
    {
        for (int i = 0; i < dialoguesLibrary.library.Count; i++)
        {
            DialogueGraph dialogue = (DialogueGraph)dialoguesLibrary.library[i];

            foreach (var item in dialogue.nodes)
            {
                DialogueTextNode node = (DialogueTextNode)item;
                node.SetAlreadyReadValue(false);
                node.timerBeforeNextNode = 0.0f;
            }
        }
    }

    public void InitGame()
    {
        Debug.Log("Init");

        QuestList questList = Instantiate(questManagerPrefab, transform);
        questManager = questList;

        ResetPlayerPrefsValues();

        if (GameCore.Instance != null && GameCore.Instance.CurrentScene.isGameScene)
        {
            EssentialInitPlayer();
        }
        else
        {
            GameCore gameCore = FindObjectOfType<GameCore>();
            if(gameCore != null && gameCore.CurrentScene.isGameScene)
            {
                EssentialInitPlayer();
            }
        }
    }

    public void ResetPlayerPrefsValues()
    {
        PlayerPrefs.SetFloat("VolumeGlobal", 1.0f);
        PlayerPrefs.SetFloat("Musique", 1.0f);
        PlayerPrefs.SetFloat("SFX", 1.0f);
    }

    public void EssentialInitPlayer()
    {
        Debug.Log("Init Player");

        CreatePlayer(Vector3.zero);

        SetAvatarColor();

        UpdateAvatarAnimators();


        SetPlayerGender(Random.Range(0, AllGendersPossibles));

        player.PlayerName = RandomPlayerName();

        RandomSkinPlayer();

        RandomAppearancePlayer();


        UpdateAvatarAnimators();
        SetAvatarColor();


        //SetupDialogues();

        PlayerApparance.enabled = true;

        player.CanvasIndestrucitble.SetActive(true);
        player.PannelENTUIIndestructible.SetActive(false);
        player.PannelAnnonceUIIndestructible.SetActive(false);

        
    }

    public void CreatePlayer(Vector3 _playerPositionCustom)
    {
        if(player == null)
        {
            GetCustomizer();

            GameObject PlayerInstantiate = Instantiate(PlayerPrefab, _playerPositionCustom, Quaternion.identity);
            player = PlayerInstantiate.GetComponent<PlayerScript>();
            PlayerApparance = PlayerInstantiate.GetComponent<PlayerMovement>();
            PlayerApparance.enabled = false;
            PlayerApparance.gameObject.name = "Player";



            GameObject CanvasInstatiate = Instantiate(CanvasPrefab);
            GameObject DialogueUIInstantiate = Instantiate(DialogueUIPrefab);
            GameObject InventoryUIInstatiate = Instantiate(InventoryUIPrefab);
            GameObject PannelENTUIInstatiate = Instantiate(PannelENTUIPrefab);
            GameObject PannelAnnonceUIInstatiate = Instantiate(PannelAnnonceUIPrefab);
            GameObject EventSystemInstantiate = Instantiate(EventSystemPrefab);

            DialogueUIInstantiate.transform.SetParent(CanvasInstatiate.transform);
            DialogueUIInstantiate.transform.SetSiblingIndex(0);
            DialogueUIInstantiate.name = "Dialogue Canvas";

            PannelENTUIInstatiate.transform.SetParent(CanvasInstatiate.transform);
            PannelENTUIInstatiate.transform.SetSiblingIndex(1);
            PannelENTUIInstatiate.name = "Pannel ENT";

            PannelAnnonceUIInstatiate.transform.SetParent(CanvasInstatiate.transform);
            PannelAnnonceUIInstatiate.transform.SetSiblingIndex(1);
            PannelAnnonceUIInstatiate.name = "Pannel Annonce";

            InventoryUIInstatiate.transform.SetParent(CanvasInstatiate.transform);
            InventoryUIInstatiate.transform.SetSiblingIndex(4);
            InventoryUIInstatiate.name = "Inventory";

            GameCanvasManager _gameCanvasManager = CanvasInstatiate.GetComponent<GameCanvasManager>();

            gameCanvasManager = _gameCanvasManager;

            gameCanvasManager.inventory = InventoryUIInstatiate.GetComponent<InventoryScript>();
            gameCanvasManager.questTrackingUi = gameCanvasManager.inventory.QuestTrackingUi;
            gameCanvasManager.questUi = gameCanvasManager.inventory.QuestUi;

            gameCanvasManager.dialogCanvas = DialogueUIInstantiate.GetComponent<DialogueDisplayUI>();

            gameCanvasManager.eventSystem = EventSystemInstantiate.GetComponent<EventSystem>();

            DontDestroyOnLoad(PlayerApparance.gameObject);
            DontDestroyOnLoad(CanvasInstatiate.gameObject);
            DontDestroyOnLoad(EventSystemInstantiate);

            player.CanvasIndestrucitble = CanvasInstatiate;
            player.InventoryUIIndestructible = InventoryUIInstatiate;
            player.PannelENTUIIndestructible = PannelENTUIInstatiate;
            player.PannelAnnonceUIIndestructible = PannelAnnonceUIInstatiate;

            CanvasInstatiate.SetActive(false);


            DialogueUIInstantiate.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 15f, 0);
            DialogueUIInstantiate.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 2f);

            InventoryUIInstatiate.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // Left & Bottom
            InventoryUIInstatiate.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // Right & Top
            InventoryUIInstatiate.GetComponent<RectTransform>().localScale = Vector3.one;

            PannelENTUIInstatiate.GetComponent<RectTransform>().offsetMin = new Vector2(250f, 25f);
            PannelENTUIInstatiate.GetComponent<RectTransform>().offsetMax = new Vector2(-250f, -25f);
            PannelENTUIInstatiate.GetComponent<RectTransform>().localScale = Vector3.one;

            PannelAnnonceUIInstatiate.GetComponent<RectTransform>().offsetMin = new Vector2(350f, 200f);
            PannelAnnonceUIInstatiate.GetComponent<RectTransform>().offsetMax = new Vector2(-350f, -200f);
            PannelAnnonceUIInstatiate.GetComponent<RectTransform>().localScale = Vector3.one;


            InventoryUIInstatiate.GetComponent<InventoryScript>().PlayerScript = player;
            InventoryUIInstatiate.GetComponent<InventoryScript>().PannelENTCanvas = PannelENTUIInstatiate;

            player.FadeAnimation = CanvasInstatiate.transform.GetChild(CanvasInstatiate.transform.childCount - 1).GetComponent<Image>();
        }
        else
        {
            Debug.LogWarning("Player already exists");
        }
    }

    public void GetCustomizer()
    {
        customizer = FindObjectOfType<Customizer>();

        if(customizer != null)
        {
            AllChoiceColors = customizer.AllChoiceColorButton.Count;

            AllGendersPossibles = customizer.GenderButton.Count;
        }
    }

    public void SetAvatarColor()
    {
        // Set Color
        for (int i = 0; i < PlayerApparance.PlayerRenderers.Count; i++)
        {
            PlayerApparance.PlayerRenderers[i].color = PlayerApparance.ColorsDisplay[i];
        }
    }

    public void UpdateAvatarAnimators()
    {
        // Afficher le choix
        for (int i = 0; i < PlayerApparance.SpriteDisplay.Count; i++)
        {
            PlayerApparance.Animators[i + 1].runtimeAnimatorController = PlayerApparance.SpriteDisplay[i];
        }


        // Reset Animator pour les coordonnées
        for (int a = 0; a < PlayerApparance.Animators.Count; a++)
        { PlayerApparance.Animators[a].Rebind(); }
    }

    public void SetPlayerGender(int GenderValue)
    {
        PlayerApparance.GetComponent<PlayerScript>().PlayerSexualGenre = GenderValue;
    }

    public string RandomPlayerName()
    {
        if (player.PlayerSexualGenre == 0)
        {
            List<string> femaleNames = new List<string>();
            femaleNames = playerNamesSelection.GetSumLists(femaleNames, playerNamesSelection.femaleRandomNames);
            femaleNames = playerNamesSelection.GetSumLists(femaleNames, playerNamesSelection.mixedRandomNames);

            /*for (int i = 0; i < femaleNames.Count; i++)
            {
                Debug.Log(femaleNames[i]);
            }*/

            return femaleNames[Random.Range(0, femaleNames.Count)];
        }
        else if (player.PlayerSexualGenre == 1)
        {
            List<string> maleNames = new List<string>();
            maleNames = playerNamesSelection.GetSumLists(maleNames, playerNamesSelection.maleRandomNames);
            maleNames = playerNamesSelection.GetSumLists(maleNames, playerNamesSelection.mixedRandomNames);

            /*for (int i = 0; i < maleNames.Count; i++)
            {
                Debug.Log(maleNames[i]);
            }*/

            return maleNames[Random.Range(0, maleNames.Count)];
        }
        else if (player.PlayerSexualGenre == 2)
        {
            List<string> nonBinaryNames = new List<string>();
            nonBinaryNames = playerNamesSelection.GetSumLists(nonBinaryNames, playerNamesSelection.maleRandomNames);
            nonBinaryNames = playerNamesSelection.GetSumLists(nonBinaryNames, playerNamesSelection.femaleRandomNames);
            nonBinaryNames = playerNamesSelection.GetSumLists(nonBinaryNames, playerNamesSelection.mixedRandomNames);

            /*for (int i = 0; i < nonBinaryNames.Count; i++)
            {
                Debug.Log(nonBinaryNames[i]);
            }*/

            return nonBinaryNames[Random.Range(0, nonBinaryNames.Count)];
        }

        return "";
    }

    public void RandomSkinPlayer()
    {
        PlayerApparance.ValueColorDisplay[0] = Random.Range(0f, 1f);
    }

    public void RandomAppearancePlayer()
    {
        // Random Hair
        ChoiceInCategorie[0] = Random.Range(0, HairChoice.Count);
        PlayerApparance.SpriteDisplay[0] = HairChoice[ChoiceInCategorie[0]].Animator;
        PlayerApparance.ValueColorDisplay[1] = Random.Range(0, AllChoiceColors);
        PlayerApparance.ColorsDisplay[1] = ColorCustomList[(int)PlayerApparance.ValueColorDisplay[1]];

        // Random Top
        ChoiceInCategorie[1] = Random.Range(0, TopChoice.Count);
        PlayerApparance.SpriteDisplay[1] = TopChoice[ChoiceInCategorie[1]].Animator;
        PlayerApparance.ValueColorDisplay[2] = Random.Range(0, AllChoiceColors);
        PlayerApparance.ColorsDisplay[2] = ColorCustomList[(int)PlayerApparance.ValueColorDisplay[2]];

        // Random Bottom
        ChoiceInCategorie[2] = Random.Range(0, PantsChoice.Count /*- 1*/);
        PlayerApparance.SpriteDisplay[2] = PantsChoice[ChoiceInCategorie[2]].Animator;
        PlayerApparance.ValueColorDisplay[3] = Random.Range(0, AllChoiceColors);
        PlayerApparance.ColorsDisplay[3] = ColorCustomList[(int)PlayerApparance.ValueColorDisplay[3]];

        // Random Shoe
        ChoiceInCategorie[3] = Random.Range(0, ShoeChoice.Count);
        PlayerApparance.SpriteDisplay[3] = ShoeChoice[ChoiceInCategorie[3]].Animator;
        PlayerApparance.ValueColorDisplay[4] = Random.Range(0, AllChoiceColors);
        PlayerApparance.ColorsDisplay[4] = ColorCustomList[(int)PlayerApparance.ValueColorDisplay[4]];
    }

    /*public void SetupDialogues()
    {
        player.playerBackpack.GetComponent<CSVReader>().SetUpDialogueAdhérent();
    }*/

    private void OnDestroy()
    {
        ResetDialogues();
    }

    #endregion
}
