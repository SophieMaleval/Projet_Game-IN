using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Village.EncyclopaediaMenu
{
    public class EncyclopaediaLocationsDropdownItem : MonoBehaviour
    {
        #region Properties

        public TextMeshProUGUI ItemLabel => itemLabel;

        public GameObject[] QuestIcons => questIcons;

        #endregion

        #region UnityInspector

        [SerializeField] private TextMeshProUGUI itemLabel;

        [SerializeField] private GameObject[] questIcons;

        #endregion
    }
}
