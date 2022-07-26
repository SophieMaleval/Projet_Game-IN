using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestTrackingUI : MonoBehaviour
{
    #region Properties

    public QuestStatus currentQuestStatusActive { get; set; }

    public TextMeshProUGUI QuestTrackingTitleText => questTrackingTitleText;

    public TextMeshProUGUI QuestTrackingDescriptionText => questTrackingDescriptionText;

    #endregion

    #region UnityInspector

    [SerializeField] private TextMeshProUGUI questTrackingTitleText;

    [SerializeField] private TextMeshProUGUI questTrackingDescriptionText;

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

        if (questStatus != null)
        {
            SetQuestTrackingTitle(questStatus.GetQuest().name);

            List<QuestStepStatus> steps = new List<QuestStepStatus>();
            foreach (QuestStepStatus step in questStatus.GetQuestStepStatuses())
            {
                if (step.isUnlocked)
                {
                    steps.Add(step);
                }
            }
            SetQuestTrackingDescription(steps[steps.Count - 1].GetQuestStep().description);

        }

        currentQuestStatusActive = questStatus;
        gameObject.SetActive(value);
    }

    public void SetQuestTrackingTitle(string text)
    {
        questTrackingTitleText.text = text;
    }

    public void SetQuestTrackingDescription(string text)
    {
        questTrackingDescriptionText.text = text;
    }

    #endregion
}
