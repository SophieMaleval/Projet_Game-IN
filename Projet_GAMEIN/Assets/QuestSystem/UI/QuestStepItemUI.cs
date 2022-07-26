using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestStepItemUI : MonoBehaviour
{
    #region Fields

    QuestStepStatus statuts;

    #endregion

    #region Properties

    public TextMeshProUGUI Description => description;

    #endregion

    #region UnityInspector

    [SerializeField] TextMeshProUGUI description;

    #endregion

    #region Behaviour

    public void Setup(QuestStepStatus statuts)
    {
        this.statuts = statuts;
        description.text = statuts.GetQuestStep().description;

        if(statuts.IsComplete())
        {
            description.fontStyle = FontStyles.Strikethrough;
        }
        else
        {
            description.fontStyle = FontStyles.Normal;
        }
    }

    public QuestStepStatus GetQuestStatus()
    {
        return statuts;
    }

    #endregion
}
