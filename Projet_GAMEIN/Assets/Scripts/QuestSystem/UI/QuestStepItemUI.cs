using AllosiusDev.TranslationSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AllosiusDev.QuestSystem
{
    public class QuestStepItemUI : MonoBehaviour
    {
        #region Fields

        QuestStepStatus statuts;

        #endregion

        #region Properties

        public ToTranslateObject Description => description;

        #endregion

        #region UnityInspector

        [SerializeField] ToTranslateObject description;

        #endregion

        #region Behaviour
        public QuestStepStatus GetQuestStatus()
        {
            return statuts;
        }

        public void Setup(QuestStepStatus statuts)
        {
            this.statuts = statuts;

            //description.text = statuts.GetQuestStep().descriptionTranslateKey;
            description.SetTranslationKey(statuts.GetQuestStep().descriptionTranslateKey);

            if (statuts.IsComplete())
            {
                description.TextToTranslate.fontStyle = FontStyles.Strikethrough;
            }
            else
            {
                description.TextToTranslate.fontStyle = FontStyles.Normal;
            }
        }


        #endregion
    }
}
