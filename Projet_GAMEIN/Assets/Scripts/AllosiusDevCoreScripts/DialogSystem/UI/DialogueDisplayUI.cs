using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AllosiusDev.Audio;
using AllosiusDev.Core;
using AllosiusDev.TranslationSystem;

namespace AllosiusDev.DialogSystem
{
    public class DialogueDisplayUI : MonoBehaviour
    {
        #region Fields

        private RectTransform ThisRect;

        private PlayerConversant playerConversant;

        private List<GameObject> currentChoices = new List<GameObject>();

        private Coroutine writeTextCoroutine;
        private Coroutine animationPassTouchCoroutine;

        private bool canTurnNext = true;

        private bool updateButtons;
        private List<Button> currentChoicesButtons = new List<Button>();
        #endregion

        #region Properties

        public TextMeshProUGUI DialogueDisplayerText => dialogueDisplayerText;

        public List<GameObject> CurrentChoices => currentChoices;

        public Button NextButton => nextButton;

        #endregion

        #region UnityInspector

        [SerializeField] private TextMeshProUGUI dialogueDisplayerText;
        [SerializeField] private RectTransform passTouch;

        [SerializeField] private TextMeshProUGUI dialogueNpcNameText;

        [SerializeField] private Transform choicesRoot;
        [SerializeField] private GameObject choicePrefab;

        [SerializeField] private Button nextButton;

        [SerializeField] private Color32 QuestionClassicColor;
        [SerializeField] private Color32 QuestionQuestColor;

        [SerializeField] private float delayAnimationText = 0.025f;

        [Header("Sounds")]
        [SerializeField] private AudioData sfxTextWrite;
        [SerializeField] private AudioData sfxValidate;

        #endregion

        #region Behaviour

        private void Awake()
        {
            ThisRect = gameObject.GetComponent<RectTransform>();
        }

        private void Start()
        {
            //Debug.LogError("Init Dialogue UI");

            playerConversant = GameManager.Instance.player.GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;

            UpdateUI();
        }

        private void LateUpdate()
        {
            UpdateDialogueChoicesButtons();
        }

        private void UpdateDialogueChoicesButtons()
        {
            //Debug.Log("Update DIalogue Choices Buttons");

            if (currentChoicesButtons.Count > 0 && updateButtons)
            {
                float HeightFinalBox = 30f;

                foreach (Button button in currentChoicesButtons)
                {
                    RectTransform hoverElementRect = button.GetComponentInChildren<TextMeshProUGUI>().GetComponent<RectTransform>();
                    //Debug.Log(hoverElementRect.gameObject.name);
                    //Debug.Log(hoverElementRect.sizeDelta);

                    button.GetComponent<RectTransform>().sizeDelta += hoverElementRect.sizeDelta;

                    //Debug.Log(button.GetComponent<RectTransform>().sizeDelta.y);

                    HeightFinalBox += button.GetComponent<RectTransform>().sizeDelta.y;
                    ThisRect.sizeDelta = new Vector2(ThisRect.sizeDelta.x, HeightFinalBox);
                }

                //Debug.Log(HeightFinalBox);
            }
        }

        private void UpdateButtonsListeners()
        {
            //Debug.Log("Clear Next and Exit Dialogue Buttons");

            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() => ButtonNext());
        }

        private void UpdateUI()
        {
            //Debug.LogError("Update UI");


            UpdateButtonsListeners();
            updateButtons = false;

            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive())
            {
                return;
            }

            GameManager.Instance.player.textDebug2.text = "Update UI";

            ThisRect.sizeDelta = new Vector2(ThisRect.sizeDelta.x, 96f);

            dialogueNpcNameText.text = playerConversant.GetCurrentConversantName();
            SetNPCNameWidth(playerConversant.GetCurrentConversantName());

            dialogueDisplayerText.gameObject.SetActive(!playerConversant.IsChoosing());
            choicesRoot.gameObject.SetActive(playerConversant.IsChoosing());

            if (writeTextCoroutine != null)
            {
                StopCoroutine(writeTextCoroutine);
            }

            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                writeTextCoroutine = StartCoroutine(WriteText(playerConversant.GetKeyText()));

                GameManager.Instance.gameCanvasManager.eventSystem.SetSelectedGameObject(nextButton.gameObject);
            }

        }

        private void SetNPCNameWidth(string Name)
        {
            int NumOfChar = Name.Length;
            dialogueNpcNameText.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2((NumOfChar * 14f) + 10f, dialogueNpcNameText.transform.parent.GetComponent<RectTransform>().sizeDelta.y);
        }



        private void BuildChoiceList()
        {
            //Debug.LogError("Build Choice List");

            foreach (Transform item in choicesRoot)
            {
                Destroy(item.gameObject);
            }

            currentChoices.Clear();
            currentChoicesButtons.Clear();



            foreach (DialogueTextNode choice in playerConversant.GetChoices())
            {
                //Debug.Log("Instantiate");
                GameObject _choiceInstance = Instantiate(choicePrefab, choicesRoot);
                currentChoices.Add(_choiceInstance);

                var _textComp = _choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                //_textComp.text = choice.message;
                var _textTranslate = _choiceInstance.GetComponentInChildren<ToTranslateObject>();
                _textTranslate.SetTranslationKey(choice.keyText, TypeDictionary.Dialogues);

                if (choice.hasRequirements)
                {
                    for (int i = 0; i < choice.gameRequirements.requirementsList.Count; i++)
                    {
                        if (choice.gameRequirements.requirementsList[i].requirementType == RequirementType.HasQuest
                            || choice.gameRequirements.requirementsList[i].requirementType == RequirementType.QuestState)
                        {
                            _textComp.color = QuestionQuestColor;
                        }
                    }
                }
                else
                {
                    _textComp.color = QuestionClassicColor;
                }

                Button button = _choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    ButtonSelectChoice(choice);
                });

                currentChoicesButtons.Add(button);
            }

            updateButtons = true;

            for (int i = 0; i < currentChoices.Count; i++)
            {
                Button button = currentChoices[i].GetComponent<Button>();

                Navigation newNav = new Navigation();
                newNav.mode = Navigation.Mode.Explicit;
                newNav.selectOnUp = button.navigation.selectOnUp;
                newNav.selectOnDown = button.navigation.selectOnDown;
                newNav.selectOnLeft = button.navigation.selectOnLeft;
                newNav.selectOnRight = button.navigation.selectOnRight;

                if (i > 0)
                {
                    newNav.selectOnUp = currentChoices[i - 1].GetComponent<Selectable>();
                }

                if (i < currentChoices.Count - 1)
                {
                    newNav.selectOnDown = currentChoices[i + 1].GetComponent<Selectable>();
                }

                button.navigation = newNav;

            }

            //Debug.Log(GameManager.Instance.gameCanvasManager.eventSystem.currentSelectedGameObject.name);
            GameManager.Instance.gameCanvasManager.eventSystem.SetSelectedGameObject(currentChoices[0]);
            //Debug.Log(GameManager.Instance.gameCanvasManager.eventSystem.currentSelectedGameObject.name);
        }

        private void ButtonNext()
        {
            //Debug.LogError("Button Next");

            if (canTurnNext)
            {
                AudioController.Instance.PlayAudio(sfxValidate);
                playerConversant.Next();

                if (animationPassTouchCoroutine != null)
                {
                    StopCoroutine(animationPassTouchCoroutine);
                }
                passTouch.gameObject.SetActive(false);
            }
            else
            {
                canTurnNext = true;

                if (writeTextCoroutine != null)
                {
                    StopCoroutine(writeTextCoroutine);

                    EndTextWrite(playerConversant.GetKeyText());
                }
            }


        }

        private void ButtonSelectChoice(DialogueTextNode choice)
        {
            AudioController.Instance.PlayAudio(sfxValidate);
            playerConversant.SelectChoice(choice);
        }

        private IEnumerator WriteText(string text)
        {
            Debug.Log("Write Text Coroutine");

            canTurnNext = false;

            dialogueDisplayerText.text = "";
            //Debug.Log(dialogueDisplayerText.text);

            if (playerConversant.CurrentConversant != null)
            {
                playerConversant.CurrentConversant.PNJTalkAnimation(true);
            }

            for (int i = 0; i < text.Length; i++)
            {
                dialogueDisplayerText.text += text[i];
                //Debug.Log(dialogueDisplayerText.text);

                if (text[i] != ' ')
                {
                    AudioController.Instance.PlayAudio(sfxTextWrite);
                }

                yield return new WaitForSeconds(delayAnimationText);
            }

            EndTextWrite(text);
        }

        private void EndTextWrite(string text)
        {
            //Debug.Log("End Text Write");

            dialogueDisplayerText.text = text;
            //Debug.Log(dialogueDisplayerText.text);

            if (playerConversant.CurrentConversant != null)
            {
                playerConversant.CurrentConversant.PNJTalkAnimation(false);
            }

            canTurnNext = true;

            if (animationPassTouchCoroutine != null)
            {
                StopCoroutine(animationPassTouchCoroutine);
            }
            animationPassTouchCoroutine = StartCoroutine(AnimationPassText());
        }

        IEnumerator AnimationPassText()
        {
            if (canTurnNext)
            {
                passTouch.gameObject.SetActive(true);

                //passTouch.anchoredPosition = new Vector2(passTouch.anchoredPosition.x, passTouch.anchoredPosition.y + 2.5f);
                yield return new WaitForSeconds(0.5f);
                //passTouch.anchoredPosition = new Vector2(passTouch.anchoredPosition.x, passTouch.anchoredPosition.y - 2.5f);
                yield return new WaitForSeconds(0.5f);

                StartCoroutine(AnimationPassText());
            }
            else
            {
                passTouch.gameObject.SetActive(false);
            }


        }

        #endregion

    }
}
