using AllosiusDev.Audio;
using AllosiusDev.TranslationSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    #region Fields

    private RectTransform ThisRect;

    private Coroutine writeTextCoroutine;
    private Coroutine animationPassTouchCoroutine;

    private bool canTurnNext = true;

    #endregion

    #region Properties

    public string messageKey { get; set; }

    public Button NextButton => nextButton;

    public Button YesButton => yesButton;
    public Button NoButton => noButton;

    #endregion

    #region UnityInspector

    [SerializeField] private TypeDictionary typeDictionaryMessage = TypeDictionary.GeneralsUI;

    [SerializeField] private TextMeshProUGUI messageDisplayerText;

    [SerializeField] private float boxSize = 96f;

    [SerializeField] private float delayAnimationText = 0.025f;

    [Space]

    [SerializeField] private GameObject informations;
    [SerializeField] private Button nextButton;

    [Space]

    [SerializeField] private GameObject choices;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [Space]

    [Header("Sounds")]
    [SerializeField] private AudioData sfxTextWrite;
    [SerializeField] private AudioData sfxValidate;

    #endregion

    #region Behaviour

    private void Awake()
    {
        ThisRect = gameObject.GetComponent<RectTransform>();

        nextButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }

    public void SetBoxSize(float value)
    {
        boxSize = value;
    }

    public void UpdateMessage(bool isChoiceBox = false)
    {
        informations.SetActive(!isChoiceBox);
        choices.SetActive(isChoiceBox);

        GameManager.Instance.player.GetComponent<PlayerMovement>().StartActivity();
        GameManager.Instance.player.CanSwitchState = false;
        GameManager.Instance.gameCanvasManager.dialogCanvas.canInteract = false;
        GameManager.Instance.gameCanvasManager.inventory.canInteract = false;

        GameManager.Instance.gameCanvasManager.DarkScreen.gameObject.SetActive(true);

        ThisRect.sizeDelta = new Vector2(ThisRect.sizeDelta.x, boxSize);

        writeTextCoroutine = StartCoroutine(WriteText(GetKeyText()));

        GameManager.Instance.gameCanvasManager.eventSystem.SetSelectedGameObject(gameObject);
    }

    private IEnumerator WriteText(string text)
    {
        Debug.Log("Write Text Coroutine");

        canTurnNext = false;

        messageDisplayerText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            messageDisplayerText.text += text[i];

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
        messageDisplayerText.text = text;

        canTurnNext = true;

        if (animationPassTouchCoroutine != null)
        {
            StopCoroutine(animationPassTouchCoroutine);
        }

        if (informations.activeSelf)
        {
            nextButton.gameObject.SetActive(true);
            GameManager.Instance.gameCanvasManager.eventSystem.SetSelectedGameObject(nextButton.gameObject);
        }
        else if(choices.activeSelf)
        {
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
            GameManager.Instance.gameCanvasManager.eventSystem.SetSelectedGameObject(yesButton.gameObject);
        }

        GetComponent<Button>().enabled = false;
    }

    public void ClickUIBox()
    {
        if(!canTurnNext)
        {
            canTurnNext = true;

            if (writeTextCoroutine != null)
            {
                StopCoroutine(writeTextCoroutine);

                EndTextWrite(GetKeyText());
            }
        }
    }

    public void ButtonNext()
    {
        Debug.Log("Button Next");

        if (canTurnNext)
        {
            AudioController.Instance.PlayAudio(sfxValidate);

            GameManager.Instance.player.GetComponent<PlayerMovement>().EndActivity();
            GameManager.Instance.player.CanSwitchState = true;
            GameManager.Instance.gameCanvasManager.dialogCanvas.canInteract = true;
            GameManager.Instance.gameCanvasManager.inventory.canInteract = true;

            GameManager.Instance.gameCanvasManager.DarkScreen.gameObject.SetActive(false);

            GameManager.Instance.CheckEventSystemState();

            Destroy(gameObject);
            return;
        }
        else
        {
            canTurnNext = true;

            if (writeTextCoroutine != null)
            {
                StopCoroutine(writeTextCoroutine);

                EndTextWrite(GetKeyText());
            }
        }


    }

    public string GetKeyText()
    {
        if (messageKey == null)
        {
            return "";
        }

        string message = LangueManager.Instance.Translate(messageKey, typeDictionaryMessage);

        return message;
    }

    #endregion
}
