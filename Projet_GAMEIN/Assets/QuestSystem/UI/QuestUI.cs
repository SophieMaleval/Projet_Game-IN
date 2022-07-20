using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    #region Fields

    private QuestList questList;

    private List<QuestItemUI> questsUi = new List<QuestItemUI>();

    #endregion

    #region UnityInspector

    [SerializeField] QuestItemUI questPrefab;
    [SerializeField] QuestStepItemUI questStepPrefab;

    [SerializeField] private Transform contentQuests;

    #endregion

    #region Behaviour

    void Start()
    {
        questList = GameManager.Instance.questManager;
        questList.OnUpdate += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        foreach (Transform item in contentQuests)
        {
            Destroy(item.gameObject);
        }

        questsUi.Clear();

        foreach (QuestStatus status in questList.GetStatuses())
        {
            if (status.GetQuestCompleted() == false)
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, contentQuests);

                foreach (QuestStepStatus step in status.GetQuestStepStatuses())
                {
                    if (step.isUnlocked)
                    {
                        QuestStepItemUI stepUiInstance = Instantiate<QuestStepItemUI>(questStepPrefab, contentQuests);
                        stepUiInstance.Setup(step);
                        uiInstance.AddStepsUi(stepUiInstance);
                    }
                }

                uiInstance.Setup(status);
                questsUi.Add(uiInstance);
            }
        }
    }

    public List<QuestItemUI> GetQuestsUI()
    {
        return questsUi;
    }

    #endregion
}
