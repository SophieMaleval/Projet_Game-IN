using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    #region Fields

    public List<QuestStatus> statuses = new List<QuestStatus>();

    #endregion

    #region Events

    public event Action OnUpdate;

    #endregion

    #region Behaviour

    public void AddQuest(QuestData quest)
    {
        if (HasQuest(quest)) return;
        QuestStatus newStatus = new QuestStatus(quest);
        newStatus.SetQuestStepUnlocked(newStatus.GetQuestStepStatuses()[0], true);
        statuses.Add(newStatus);
        if (OnUpdate != null)
        {
            OnUpdate();
        }
    }

    public void CompleteQuestStep(QuestData quest, QuestStepData step)
    {
        QuestStatus status = GetQuestStatus(quest);
        if (status != null)
        {
            Debug.Log(status);
            status.CompleteQuestStep(step);

            if (OnUpdate != null)
            {
                OnUpdate();
            }
            else
            {
                Debug.LogWarning("OnUpdate is null");
            }
        }
    }

    public void CompleteQuestTask(QuestData quest, QuestStepData step, QuestTaskData task)
    {
        QuestStatus status = GetQuestStatus(quest);
        if (status != null)
        {
            Debug.Log(status);
            status.CompleteQuestTask(step, task);

            if (OnUpdate != null)
            {
                OnUpdate();
            }
            else
            {
                Debug.LogWarning("OnUpdate is null");
            }
        }
    }

    public bool HasQuest(QuestData quest, bool questMustBeComplete = false)
    {
        return GetQuestStatus(quest) != null;
    }

    public IEnumerable<QuestStatus> GetStatuses()
    {
        return statuses;
    }

    private QuestStatus GetQuestStatus(QuestData quest, bool questMustBeComplete = false)
    {
        foreach (QuestStatus status in statuses)
        {
            if (status.GetQuest() == quest && status.GetQuestCompleted() == questMustBeComplete)
            {
                return status;
            }
        }
        return null;
    }

    #endregion
}
