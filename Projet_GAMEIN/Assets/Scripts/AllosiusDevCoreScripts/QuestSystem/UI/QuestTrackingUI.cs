using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using AllosiusDev.TranslationSystem;

namespace AllosiusDev.QuestSystem
{
    public class QuestTrackingUI : MonoBehaviour
    {
        #region Properties

        public QuestStatus currentQuestStatusActive { get; set; }
        public QuestStepStatus currentQuestStepStatusActive { get; protected set; }

        public TextMeshProUGUI QuestTrackingTitleText => questTrackingTitleText;

        //public TextMeshProUGUI QuestTrackingDescriptionText => questTrackingDescriptionText;

        #endregion

        #region UnityInspector

        [SerializeField] private CanvasGroup animTitle;
        [SerializeField] private CanvasGroup animContent;
        [SerializeField] private float transitionAnimDuration;

        [Space]

        [SerializeField] private TextMeshProUGUI questTrackingTitleText;

        //[SerializeField] private TextMeshProUGUI questTrackingDescriptionText;
        [SerializeField] private ToTranslateObject questTrackingDescription;

        #endregion

        #region Behaviour

        private void Awake()
        {
            SetQuestTrackingState(false, null);
        }

        private void Start()
        {
            GameManager.Instance.questManager.OnUpdate += GameManager.Instance.gameCanvasManager.questUi.Redraw;
        }

        public void SetQuestTrackingState(bool value, QuestStatus questStatus)
        {
            Debug.Log("Set Quest Tracking State");

            gameObject.SetActive(value);

            if (questStatus != null)
            {
                StartCoroutine(SetQuestTrackingTitle(questStatus.GetQuest().name));

                List<QuestStepStatus> steps = new List<QuestStepStatus>();
                foreach (QuestStepStatus step in questStatus.GetQuestStepStatuses())
                {
                    if (step.isUnlocked)
                    {
                        steps.Add(step);
                    }
                }
                currentQuestStepStatusActive = steps[steps.Count - 1];
                StartCoroutine(SetQuestTrackingDescription(currentQuestStepStatusActive.GetQuestStep().descriptionTranslateKey));

            }

            currentQuestStatusActive = questStatus;

            if(GameManager.Instance.gameCanvasManager.inventory != null)
                GameManager.Instance.gameCanvasManager.inventory.encyclopaediaMenu.InitEncyclopaediaMenu();
        }

        public IEnumerator SetQuestTrackingTitle(string text)
        {
            if(questTrackingTitleText.text != text && currentQuestStatusActive != null)
            {
                FadeOutUI(animTitle);

                yield return new WaitForSeconds(transitionAnimDuration);

                FadeInUI(animTitle);
            }

            questTrackingTitleText.text = text;
        }

        public IEnumerator SetQuestTrackingDescription(string text)
        {
            if (questTrackingDescription.TranslationKey != text && currentQuestStatusActive != null)
            {
                FadeOutUI(animContent);

                yield return new WaitForSeconds(transitionAnimDuration);

                FadeInUI(animContent);
            }

            questTrackingDescription.SetTranslationKey(text, TypeDictionary.Quests);
        }

        private void FadeOutUI(CanvasGroup canvasGroup)
        {
            canvasGroup.DOFade(0, transitionAnimDuration);
        }

        private void FadeInUI(CanvasGroup canvasGroup)
        {
            canvasGroup.DOFade(1, transitionAnimDuration);
        }

        #endregion
    }
}